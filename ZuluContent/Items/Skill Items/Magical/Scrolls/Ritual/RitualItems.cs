using System;
using System.Collections.Generic;
using ModernUO.Serialization;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Utilities;
using ZuluContent.Configuration.Types.Rituals;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items;

public enum RuneDirection
{
    North,
    East,
    South,
    West
}

public enum RitualPhase
{
    DrawCircle,
    PlaceItem,
    PerformRitual,
    CompleteRitual,
    UndrawCircle
}

[SerializationGenerator(0, false)]
public abstract partial class RitualRune : Item
{
    [SerializableField(0)] private RuneDirection _direction;

    [SerializableField(1)] private PlayerMobile _owner;

    public override string DefaultName => "Magic Rune";

    public RitualRune(int itemID, RuneDirection direction) : base(itemID)
    {
        _direction = direction;

        Movable = false;

        Light = LightType.Circle225;
    }

    [AfterDeserialization]
    private void OnAfterDeserialization()
    {
        if (_owner == null)
            Delete();
    }
}

[SerializationGenerator(0, false)]
public partial class RitualRuneNorth : RitualRune
{
    [Constructible]
    public RitualRuneNorth() : base(3676, RuneDirection.North)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RitualRuneEast : RitualRune
{
    [Constructible]
    public RitualRuneEast() : base(3679, RuneDirection.East)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RitualRuneSouth : RitualRune
{
    [Constructible]
    public RitualRuneSouth() : base(3682, RuneDirection.South)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RitualRuneWest : RitualRune
{
    [Constructible]
    public RitualRuneWest() : base(3688, RuneDirection.West)
    {
    }
}

[SerializationGenerator(0, false)]
public partial class RitualSpeechCaptor : Item
{
    public override string DefaultName => "Ritual Speech Captor";

    public override bool HandlesOnSpeech => true;

    [CommandProperty(AccessLevel.GameMaster)]
    private RitualPhase Phase { get; set; }

    [CommandProperty(AccessLevel.GameMaster)]
    private int Counter { get; set; }

    private Point3D StartLocation => new(Location.X, Location.Y - 5, Location.Z);

    private int RitualMasterStartingHits { get; }

    private List<Item> RitualItems { get; }

    public List<PlayerMobile> RitualMembers { get; }

    public List<PlayerMobile> PendingRitualMembers { get; }

    private Dictionary<uint, bool> RitualSpeechSaid { get; set; }

    private RitualCircle Circle { get; set; }

    private RitualCircleComponent ManaPile { get; set; }

    private int ManaPool { get; set; }

    private string RitualType { get; }

    private RitualScroll RitualScroll { get; }

    private bool HasPermanentCircle { get; }

    private TimerExecutionToken? RitualTimerToken { get; set; }

    [SerializableField(0)] private BaseEquippableItem _itemToEnchant;

    [SerializableField(1)] private PlayerMobile _ritualMaster;

    [Constructible]
    public RitualSpeechCaptor(bool hasPermanentCircle, PlayerMobile ritualMaster, string ritualType, RitualScroll scroll) : base(1)
    {
        _ritualMaster = ritualMaster;
        RitualMasterStartingHits = ritualMaster.Hits;
        RitualItems = new List<Item>();
        RitualType = ritualType;
        RitualScroll = scroll;
        HasPermanentCircle = hasPermanentCircle;
        Movable = false;
        Visible = false;

        RitualMembers = new List<PlayerMobile> { ritualMaster };
        PendingRitualMembers = new List<PlayerMobile>();

        RitualSpeechSaid = new Dictionary<uint, bool>();

        if (hasPermanentCircle)
        {
            Phase = RitualPhase.PlaceItem;

            OnDrawCircleComplete();
        }
        else
        {
            Phase = RitualPhase.DrawCircle;
            ritualMaster.SendSuccessMessage("You can start to draw the circle.");

            StartRitualTimer();
        }
    }

    private void StartRitualTimer()
    {
        Timer.StartTimer(TimeSpan.FromSeconds(15.0), DisruptRitual, out var timerToken);
        RitualTimerToken = timerToken;
    }

    public override void OnSpeech(SpeechEventArgs e)
    {
        var from = e.Mobile;

        var range = Phase == RitualPhase.PerformRitual ? 3 : 6;

        if (!e.Handled && from.InRange(this, range))
        {
            var speech = e.Speech;

            if (from == _ritualMaster && Phase == RitualPhase.DrawCircle)
                OnDrawCircle(speech);
            else if (Phase == RitualPhase.PerformRitual)
                OnPerformRitual(from, speech);
            else if (from == _ritualMaster && Phase == RitualPhase.UndrawCircle)
                OnUndrawCircle(speech);
        }
    }

    private void OnDrawCircle(string speech)
    {
        var drawCircleData = ZhConfig.Rituals.DrawCircle;
        var drawCircleDatum = drawCircleData[Counter];
        var nextDrawCircleDatum = Counter < drawCircleData.Length - 1 ? drawCircleData[Counter + 1] : null;
        var locationToBe = new Point3D(StartLocation.X + drawCircleDatum.XMod, StartLocation.Y + drawCircleDatum.YMod,
            StartLocation.Z);

        if (_ritualMaster.Location != locationToBe || _ritualMaster.Hits < RitualMasterStartingHits ||
            (_ritualMaster.Direction & Direction.Mask) != drawCircleDatum.Direction || speech != drawCircleDatum.Speech)
        {
            DisruptRitual();
            return;
        }

        RitualTimerToken?.Cancel();

        PlayDrawCircleEffects(drawCircleDatum, new Point3D(
            StartLocation.X + nextDrawCircleDatum?.XMod ?? drawCircleDatum.XMod,
            StartLocation.Y + nextDrawCircleDatum?.YMod ?? drawCircleDatum.YMod,
            StartLocation.Z));

        if (Counter == drawCircleData.Length - 1)
        {
            Phase = RitualPhase.PlaceItem;
            Counter = 0;

            OnDrawCircleComplete();
        }
        else
        {
            Counter++;
            StartRitualTimer();
        }
    }

    private void OnDrawCircleComplete()
    {
        CreateRitualCircle();

        if (RitualType == "Immutability")
        {
            AddManaPile();
        }
        else
        {
            _ritualMaster.SendSuccessMessage("Choose the item you wish to enchant.");
            _ritualMaster.Target = new RitualItemTarget(_ritualMaster, this);
        }
    }

    private async void OnPerformRitual(Mobile from, string speech)
    {
        var ritualData = ZhConfig.Rituals.RitualsSettings[RitualType];
        var ritualDatum = ritualData[Counter];

        if (from is PlayerMobile player && !RitualMembers.Contains(player))
        {
            DisruptRitual();
            return;
        }

        RitualSpeechSaid.TryGetValue(from.Serial.Value, out var saidSpeech);

        if (saidSpeech)
        {
            DisruptRitual();
            return;
        }

        if (speech != ritualDatum.Speech)
        {
            DisruptRitual();
            return;
        }

        RitualTimerToken?.Cancel();

        PlayRitualEffects(ritualDatum, from);

        RitualSpeechSaid[from.Serial.Value] = true;

        if (RitualSpeechSaid.Count == RitualMembers.Count)
        {
            if (Counter == ritualData.Length - 1)
            {
                Phase = RitualPhase.CompleteRitual;
                Counter = 0;

                foreach (var ritualMember in RitualMembers)
                {
                    var manaAmount = ritualMember.Mana / 4;
                    _ritualMaster.Mana -= manaAmount;
                    ManaPool += manaAmount;
                    Effects.SendMovingEffect(ritualMember.Map, 0x3778, ritualMember.Location,
                        ManaPile.Location, 7, 0);
                    ritualMember.Animate(16, 7, 1, true, false, 0);
                }

                _itemToEnchant?.PublicOverheadMessage(MessageType.Regular,
                    0x3B2,
                    true, "*As the power words sequence is finished, the mystic energy channels above the item*");

                var ritualAppear = ritualData[0].Appear;
                if (ritualAppear != null)
                    foreach (var appear in ritualAppear)
                        Circle.AddComponent(new RitualCircleComponent(appear.ItemId, 0, "Magical Substance"),
                            new Point3D(appear.XMod, appear.YMod, 0));

                _itemToEnchant?.MoveToWorld(new Point3D(_itemToEnchant.Location.X, _itemToEnchant.Location.Y,
                    _itemToEnchant.Location.Z + 2));

                await Timer.Pause(5000);

                foreach (var ritualMember in RitualMembers)
                {
                    ManaPool += ritualMember.Mana;
                    ritualMember.Mana = 0;
                    Effects.SendMovingEffect(ritualMember.Map, 0x3778, ritualMember.Location,
                        ManaPile.Location, 7, 0);
                    ritualMember.Animate(16, 7, 1, true, false, 0);
                }

                _ritualMaster.PublicOverheadMessage(MessageType.Regular,
                    0x3B2,
                    true, $"*All the mystic energy goes through {_ritualMaster.Name} as the final ritual process begins*");
                _ritualMaster.PlaySound(0x1FD);

                var manaPart = ManaPool / 10;
                for (var i = 0; i < 10; i++)
                {
                    Effects.SendMovingEffect(_ritualMaster.Map, 0x3778, ManaPile.Location,
                        _ritualMaster.Location, 7, 0);
                    _ritualMaster.Mana += manaPart;
                    ManaPool -= manaPart;
                    await Timer.Pause(500);
                }

                ManaPile.Visible = false;

                if (!RitualSkillCheck())
                {
                    return;
                }

                await Timer.Pause(5000);

                if (HasPermanentCircle)
                {
                    OnUnDrawCircleComplete();
                }
                else
                {
                    _ritualMaster.SendSuccessMessage("You must now undo the magic circle to successfully complete the ritual.");

                    Phase = RitualPhase.UndrawCircle;
                }
            }
            else
            {
                RitualSpeechSaid = new Dictionary<uint, bool>();
                Counter++;
                StartRitualTimer();
            }
        }
    }

    private bool RitualSkillCheck()
    {
        var circle = RitualScroll.Circle;
        var points = circle.PointValue;
        var mana = circle.Mana;
        var difficulty = circle.Difficulty;
        var minDifficulty = 100 + (circle.Id - 29) * 15;

        foreach (var ritualMember in RitualMembers)
        {
            var magery = ritualMember.Skills[SkillName.Magery].Value;

            if (ritualMember != _ritualMaster)
            {
                magery /= 2;

                if (ritualMember.ClassContainsSkill(SkillName.Magery))
                {
                    magery *= ritualMember.GetClassModifier(SkillName.Magery);
                }
            }
            else if (ritualMember is IZuluClassed { ZuluClass: { Type: ZuluClassType.Mage, Level: >= 5 } })
            {
                magery = magery * ritualMember.GetClassModifier(SkillName.Magery) - magery * 2;
            }

            difficulty -= (int)magery;
        }

        difficulty = Math.Max(difficulty, minDifficulty);

        if (_ritualMaster.Mana < mana)
        {
            _ritualMaster.FixedEffect(0x3735, 6, 30);
            _ritualMaster.PlaySound(0x5C);
            _ritualMaster.SendFailureMessage("Insufficient mana.");
            DisruptRitual();
            return false;
        }

        _ritualMaster.Mana -= mana;

        if (!_ritualMaster.ShilCheckSkill(SkillName.Magery, difficulty, points))
        {
            if (_ritualMaster is IZuluClassed { ZuluClass.Type: ZuluClassType.Mage })
            {
                if (!_ritualMaster.ShilCheckSkill(SkillName.Magery, difficulty, points))
                {
                    _ritualMaster.FixedEffect(0x3735, 6, 30);
                    _ritualMaster.PlaySound(0x5C);
                    _ritualMaster.SendFailureMessage("The ritual failed.");
                    DisruptRitual();
                    return false;
                }
            }
            else
            {
                _ritualMaster.FixedEffect(0x3735, 6, 30);
                _ritualMaster.PlaySound(0x5C);
                _ritualMaster.SendFailureMessage("The ritual failed.");
                DisruptRitual();
                return false;
            }
        }

        if (_itemToEnchant != null)
        {
            _itemToEnchant.PublicOverheadMessage(MessageType.Regular,
                0x3B2,
                true, "The item absorbed the enchantment successfully!");
            Effects.SendLocationEffect(_itemToEnchant, 14201, 16);
        }

        _ritualMaster.PlaySound(0x1FA);

        foreach (var ritualMember in RitualMembers)
        {
            Titles.AwardFame(ritualMember, (difficulty - 90) * 50, true);
        }

        return true;
    }

    private void OnUndrawCircle(string speech)
    {
        var drawCircleData = ZhConfig.Rituals.UndrawCircle;
        var drawCircleDatum = drawCircleData[Counter];
        var locationToBe = new Point3D(StartLocation.X + drawCircleDatum.XMod, StartLocation.Y + drawCircleDatum.YMod,
            StartLocation.Z);

        if (_ritualMaster.Location != locationToBe || _ritualMaster.Hits < RitualMasterStartingHits ||
            (_ritualMaster.Direction & Direction.Mask) != drawCircleDatum.Direction || speech != drawCircleDatum.Speech)
        {
            DisruptRitual();
            return;
        }

        RitualTimerToken?.Cancel();

        PlayDrawCircleEffects(drawCircleDatum);

        if (Counter == drawCircleData.Length - 1)
        {
            OnUnDrawCircleComplete();
        }
        else
        {
            Counter++;
            StartRitualTimer();
        }
    }

    private void OnUnDrawCircleComplete()
    {
        _ritualMaster.LightLevel = 30;

        if (_itemToEnchant != null)
        {
            _itemToEnchant.Movable = true;
            _ritualMaster.Backpack.AddItem(_itemToEnchant);
        }

        foreach (var ritualMember in RitualMembers)
        {
            ritualMember.Stam = 0;
            ritualMember.Mana = 0;
            ritualMember.Hits = 1;
            ritualMember.SendFailureMessage("You're exhausted from the ritual!");
        }

        RitualScroll.OnRitualComplete(_ritualMaster, _itemToEnchant);

        EndRitual();
    }

    private void AddManaPile()
    {
        ManaPile = new RitualCircleComponent(14217, 0, "Mana Cloud");
        Circle.AddComponent(ManaPile, new Point3D(0, 5, 4));

        const string gatherText = "*Every wizard gather in the circle as the magical ritual begins*";

        if (_itemToEnchant != null)
        {
            _itemToEnchant.PublicOverheadMessage(MessageType.Regular,
                0x3B2,
                true, gatherText);
        }
        else
        {
            ManaPile.PublicOverheadMessage(MessageType.Regular,
                0x3B2,
                true, gatherText);
        }

        foreach (var ritualMember in RitualMembers)
        {
            ritualMember.CloseGump<RitualWordsGump>();
            ritualMember.SendGump(new RitualWordsGump(ritualMember, RitualType));
        }

        Phase = RitualPhase.PerformRitual;
        StartRitualTimer();
    }

    public void OnItemSelected(BaseEquippableItem itemToEnchant)
    {
        itemToEnchant.MoveToWorld(new Point3D(StartLocation.X, StartLocation.Y + 5, StartLocation.Z), Circle.Map);
        itemToEnchant.Movable = false;
        _itemToEnchant = itemToEnchant;

        _ritualMaster.CloseGump<WarningGump>();
        _ritualMaster.SendGump(
            new WarningGump(1060637, 30720,
                "Do you wish to invite other wizards to your ritual?",
                0xFFC000, 360, 260, OnInviteOthersToRitual));
    }

    private void OnInviteOthersToRitual(bool okay)
    {
        if (okay)
        {
            _ritualMaster.SendSuccessMessage("Select the wizard you want to invite. Hit Esc to cancel.");
            _ritualMaster.Target = new RitualMemberTarget(_ritualMaster, this);
        }
        else
        {
            AddManaPile();
        }
    }

    private void OnJoinRitual(bool okay, PlayerMobile wizard, TimerExecutionToken timerToken)
    {
        timerToken.Cancel();
        PendingRitualMembers.Remove(wizard);

        if (okay)
        {
            RitualMembers.Add(wizard);
            _ritualMaster.SendSuccessMessage($"{wizard.Name} agreed to join the ritual.");
            wizard.SendSuccessMessage("You can go in the Circle.");

        }
        else
        {
            _ritualMaster.SendFailureMessage($"{wizard.Name} declined to join the ritual.");
        }

        if (PendingRitualMembers.Count == 0 && Phase == RitualPhase.PlaceItem)
            AddManaPile();
    }

    private void CreateRitualCircle()
    {
        _ritualMaster.PrivateOverheadMessage(MessageType.Regular,
            0x3B2,
            true, "*The sky darkens*", _ritualMaster.NetState);
        _ritualMaster.LightLevel = 10;
        _ritualMaster.PlaySound(0x1FD);
        _ritualMaster.Animate(17, 7, 1, true, false, 0);

        Circle = new RitualCircle();
        Circle.MoveToWorld(_ritualMaster.Location, _ritualMaster.Map);
    }

    private void PlayDrawCircleEffects(RitualDrawSettings settings, Point3D? locationToBe = null)
    {
        if (!string.IsNullOrEmpty(settings.Text))
            _ritualMaster.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, settings.Text);

        if (settings.SoundEffect > 0)
            _ritualMaster.PlaySound(settings.SoundEffect);

        if (settings.Appear != null && locationToBe != null)
        {
            var item = settings.Appear.CreateInstance<Item>();
            item.MoveToWorld((Point3D)locationToBe, Map);
            RitualItems.Add(item);
        }
    }

    private void PlayRitualEffects(RitualSettings settings, Mobile mobile)
    {
        Effects.SendMovingEffect(mobile.Map, 0x3778, mobile.Location,
            ManaPile.Location, 7, 0);

        if (!string.IsNullOrEmpty(settings.Text))
            mobile.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, settings.Text);

        if (settings.SoundEffect > 0)
            mobile.PlaySound(settings.SoundEffect);
    }

    private void DisruptRitual()
    {
        _itemToEnchant.Delete();

        foreach (var ritualMember in RitualMembers)
        {
            ManaPool += ritualMember.Mana;
            ritualMember.Mana = 0;
        }

        var damage = ManaPool + RitualItems.Count * 2 / RitualMembers.Count;

        foreach (var ritualMember in RitualMembers)
        {
            ritualMember.PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                $"*The mystic energy goes out of control and unleashes itself upon {ritualMember.Name}!*");
            ritualMember.BoltEffect(0);
            ritualMember.Damage(damage);
        }

        EndRitual();
    }

    private void EndRitual()
    {
        foreach (var ritualItem in RitualItems)
            if (ritualItem is not RitualRune { Owner: not null })
                ritualItem?.Delete();

        Circle?.Delete();

        Delete();
    }

    [AfterDeserialization]
    private void OnAfterDeserialization()
    {
        if (_itemToEnchant != null && _ritualMaster != null)
        {
            _itemToEnchant.Movable = true;
            _ritualMaster.Backpack.AddItem(_itemToEnchant);
        }

        Delete();
    }

    private class RitualItemTarget : Target
    {
        private PlayerMobile RitualMaster { get; }
        private RitualSpeechCaptor Captor { get; }

        public RitualItemTarget(PlayerMobile ritualMaster, RitualSpeechCaptor captor) : base(0,
            false, TargetFlags.None)
        {
            RitualMaster = ritualMaster;
            Captor = captor;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (targeted is BaseEquippableItem itemToEnchant)
            {
                if (!itemToEnchant.IsChildOf(from.Backpack))
                {
                    from.SendFailureMessage("That must be in your backpack!");
                    from.Target = new RitualItemTarget(RitualMaster, Captor);
                    return;
                }

                if (itemToEnchant.CheckNewbied())
                {
                    from.SendFailureMessage("You can't enchant newbied items.");
                    from.Target = new RitualItemTarget(RitualMaster, Captor);
                    return;
                }

                if (itemToEnchant.Cursed != CurseType.None)
                {
                    from.SendFailureMessage("You can't enchant cursed items.");
                    from.Target = new RitualItemTarget(RitualMaster, Captor);
                    return;
                }

                if (itemToEnchant is IGMItem)
                {
                    from.SendFailureMessage("You can't enchant GM items.");
                    from.Target = new RitualItemTarget(RitualMaster, Captor);
                    return;
                }

                Captor.OnItemSelected(itemToEnchant);
            }
            else
                Captor.DisruptRitual();
        }

        protected override void OnTargetOutOfRange(Mobile from, object targeted)
        {
            from.Target = new RitualItemTarget(RitualMaster, Captor);
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            Captor.DisruptRitual();
        }
    }

    private class RitualMemberTarget : Target
    {
        private PlayerMobile RitualMaster { get; }
        private RitualSpeechCaptor Captor { get; }

        public RitualMemberTarget(PlayerMobile ritualMaster, RitualSpeechCaptor captor) : base(12,
            false, TargetFlags.None)
        {
            RitualMaster = ritualMaster;
            Captor = captor;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (targeted is PlayerMobile wizard)
            {
                if (Captor.PendingRitualMembers.Contains(wizard))
                {
                    from.SendFailureMessage("That wizard has already been invited.");
                    from.Target = new RitualMemberTarget(RitualMaster, Captor);
                    return;
                }

                if (Captor.RitualMembers.Contains(wizard))
                {
                    from.SendFailureMessage("That wizard is already apart of the ritual.");
                    from.Target = new RitualMemberTarget(RitualMaster, Captor);
                    return;
                }

                if (!RitualScroll.CheckRitualEquip(wizard))
                {
                    from.SendFailureMessage("That wizard isn't wearing the required equipment to perform a ritual.");
                    wizard.SendFailureMessage("You aren't wearing the required equipment to perform a ritual.");
                    from.Target = new RitualMemberTarget(RitualMaster, Captor);
                    return;
                }

                Captor.PendingRitualMembers.Add(wizard);
                Timer.StartTimer(TimeSpan.FromSeconds(30.0), () => OnInvitedWizardTimeout(wizard), out var timerToken);
                wizard.CloseGump<WarningGump>();
                wizard.SendGump(
                    new WarningGump(1060637, 30720,
                        $"Do you wish to join {from.Name}'s ritual?",
                        0xFFC000, 360, 260, okay => Captor.OnJoinRitual(okay, wizard, timerToken)));

                from.Target = new RitualMemberTarget(RitualMaster, Captor);
            }
            else
            {
                from.SendFailureMessage("That isn't a wizard!");
                from.Target = new RitualMemberTarget(RitualMaster, Captor);
            }
        }

        private void OnInvitedWizardTimeout(PlayerMobile wizard)
        {
            wizard.CloseGump<WarningGump>();
            Captor.PendingRitualMembers.Remove(wizard);

            RitualMaster.SendFailureMessage($"{wizard.Name} failed to accept the invite in time.");

            if (Captor.PendingRitualMembers.Count == 0 && Captor.Phase == RitualPhase.PlaceItem)
                Captor.AddManaPile();
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            if (Captor.PendingRitualMembers.Count == 0 && Captor.Phase == RitualPhase.PlaceItem)
                Captor.AddManaPile();
        }
    }
}

[SerializationGenerator(0, false)]
public partial class RitualCircleComponent : Item
{
    [SerializableField(0)] private RitualCircle _circle;

    [SerializableField(1)] private Point3D _offset;

    [Constructible]
    public RitualCircleComponent(int itemID, int hue, string name, LightType light = LightType.Empty) : base(itemID)
    {
        Hue = hue;
        Name = name;
        Movable = false;
        Light = light;
    }

    public override void OnLocationChange(Point3D old)
    {
        if (_circle != null)
            _circle.Location = new Point3D(X - _offset.X, Y - _offset.Y, Z - _offset.Z);
    }

    public override void OnMapChange()
    {
        if (_circle != null)
            _circle.Map = Map;
    }

    public override void OnAfterDelete()
    {
        base.OnAfterDelete();

        if (_circle != null)
            _circle.Delete();
    }
}

[SerializationGenerator(0, false)]
public partial class RitualCircle : Item
{
    [SerializableField(0)][Tidy] private List<RitualCircleComponent> _components;

    [Constructible]
    public RitualCircle() : base(1)
    {
        _components = new List<RitualCircleComponent>();

        Movable = false;
        Visible = false;

        const string circleName = "Magic Circle";
        const int circleHue = 1170;

        AddComponent(new RitualCircleComponent(7409, circleHue, circleName), new Point3D(2, 7, 0));
        AddComponent(new RitualCircleComponent(7410, circleHue, circleName), new Point3D(1, 7, 0));
        AddComponent(new RitualCircleComponent(7411, circleHue, circleName), new Point3D(0, 7, 0));
        AddComponent(new RitualCircleComponent(7412, circleHue, circleName), new Point3D(-1, 7, 0));
        AddComponent(new RitualCircleComponent(7413, circleHue, circleName), new Point3D(-2, 7, 0));
        AddComponent(new RitualCircleComponent(7414, circleHue, circleName), new Point3D(-2, 6, 0));
        AddComponent(new RitualCircleComponent(7415, circleHue, circleName), new Point3D(-2, 5, 0));
        AddComponent(new RitualCircleComponent(7416, circleHue, circleName), new Point3D(-2, 4, 0));
        AddComponent(new RitualCircleComponent(7417, circleHue, circleName), new Point3D(-2, 3, 0));
        AddComponent(new RitualCircleComponent(7418, circleHue, circleName), new Point3D(-1, 3, 0));
        AddComponent(new RitualCircleComponent(7419, circleHue, circleName), new Point3D(-1, 2, 0));
        AddComponent(new RitualCircleComponent(7420, circleHue, circleName), new Point3D(0, 2, 0));
        AddComponent(new RitualCircleComponent(7421, circleHue, circleName), new Point3D(1, 2, 0));
        AddComponent(new RitualCircleComponent(7422, circleHue, circleName), new Point3D(2, 2, 0));
        AddComponent(new RitualCircleComponent(7423, circleHue, circleName), new Point3D(3, 2, 0));
        AddComponent(new RitualCircleComponent(7424, circleHue, circleName), new Point3D(3, 3, 0));
        AddComponent(new RitualCircleComponent(7425, circleHue, circleName), new Point3D(3, 4, 0));
        AddComponent(new RitualCircleComponent(7426, circleHue, circleName), new Point3D(3, 5, 0));
        AddComponent(new RitualCircleComponent(7427, circleHue, circleName), new Point3D(3, 6, 0));
        AddComponent(new RitualCircleComponent(7428, circleHue, circleName), new Point3D(2, 6, 0));
        AddComponent(new RitualCircleComponent(7429, circleHue, circleName), new Point3D(1, 6, 0));
        AddComponent(new RitualCircleComponent(7430, circleHue, circleName), new Point3D(0, 6, 0));
        AddComponent(new RitualCircleComponent(7431, circleHue, circleName), new Point3D(-1, 6, 0));
        AddComponent(new RitualCircleComponent(7432, circleHue, circleName), new Point3D(-1, 5, 0));
        AddComponent(new RitualCircleComponent(7433, circleHue, circleName), new Point3D(-1, 4, 0));
        AddComponent(new RitualCircleComponent(7434, circleHue, circleName), new Point3D(0, 3, 0));
        AddComponent(new RitualCircleComponent(7435, circleHue, circleName), new Point3D(1, 3, 0));
        AddComponent(new RitualCircleComponent(7436, circleHue, circleName), new Point3D(2, 3, 0));
        AddComponent(new RitualCircleComponent(7437, circleHue, circleName), new Point3D(2, 4, 0));
        AddComponent(new RitualCircleComponent(7438, circleHue, circleName), new Point3D(2, 5, 0));
        AddComponent(new RitualCircleComponent(7439, circleHue, circleName), new Point3D(1, 5, 0));
        AddComponent(new RitualCircleComponent(7440, circleHue, circleName), new Point3D(0, 5, 0));
        AddComponent(new RitualCircleComponent(7441, circleHue, circleName), new Point3D(0, 4, 0));
        AddComponent(new RitualCircleComponent(7442, circleHue, circleName), new Point3D(1, 4, 0));

        AddCandles();
    }

    public async void AddCandles()
    {
        const string candleName = "Magic Candle";

        AddComponent(new RitualCircleComponent(0x1854, 0, candleName, LightType.Circle225), new Point3D(0, 2, 0));
        await Timer.Pause(1000);
        AddComponent(new RitualCircleComponent(0x1854, 0, candleName, LightType.Circle225), new Point3D(3, 3, 0));
        await Timer.Pause(1000);
        AddComponent(new RitualCircleComponent(0x1854, 0, candleName, LightType.Circle225), new Point3D(2, 6, 0));
        await Timer.Pause(1000);
        AddComponent(new RitualCircleComponent(0x1854, 0, candleName, LightType.Circle225), new Point3D(-1, 7, 0));
        await Timer.Pause(1000);
        AddComponent(new RitualCircleComponent(0x1854, 0, candleName, LightType.Circle225), new Point3D(-2, 4, 0));
    }

    public void AddComponent(RitualCircleComponent c, Point3D location)
    {
        if (Deleted)
            return;

        _components.Add(c);

        c.Circle = this;
        c.Offset = location;
        c.MoveToWorld(new Point3D(X + location.X, Y + location.Y, Z + location.Z), Map);
    }

    public override void OnLocationChange(Point3D oldLoc)
    {
        if (Deleted)
            return;

        foreach (var c in _components)
            c.Location = new Point3D(X + c.Offset.X, Y + c.Offset.Y, Z + c.Offset.Z);
    }

    public override void OnMapChange()
    {
        if (Deleted)
            return;

        foreach (var c in _components)
            c.Map = Map;
    }

    public override void OnAfterDelete()
    {
        base.OnAfterDelete();

        foreach (var c in _components)
            c.Delete();
    }

    [AfterDeserialization]
    private async void OnAfterDeserialization()
    {
        await Timer.Pause(5000);
        Delete();
    }
}