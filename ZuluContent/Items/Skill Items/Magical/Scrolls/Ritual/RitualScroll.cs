using System;
using ModernUO.Serialization;
using Scripts.Zulu.Utilities;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public abstract partial class RitualScroll : Item
    {
        protected abstract string RitualType { get; }

        public abstract SpellCircle Circle { get; }

        protected RitualScroll(int itemId) : this(itemId, 1)
        {
        }

        protected RitualScroll(int itemId, int amount) : base(itemId)
        {
            Amount = amount;
            Stackable = true;
            Weight = 1;
            Hue = 1160;
        }

        private static int GetPermanentCirclePieces(PlayerMobile from)
        {
            var location = new Point3D(from.Location.X, from.Location.Y + 5, from.Location.Z);
            var map = from.Map;
            var runeCount = 0;

            IPooledEnumerable eable = map.GetItemsInRange(location, 6);

            foreach (Item item in eable)
            {
                if (item is RitualRune rune && rune.Owner == from)
                {
                    runeCount++;
                }
            }

            eable.Free();

            return runeCount;
        }

        private static bool CanDrawCircle(Mobile from)
        {
            var location = new Point3D(from.Location.X, from.Location.Y + 5, from.Location.Z);
            var map = from.Map;

            var itemInWay = false;

            IPooledEnumerable eable = map.GetItemsInRange(location, 0);

            foreach (Item item in eable)
                if (map.LineOfSight(from, item))
                {
                    itemInWay = true;
                    break;
                }

            eable.Free();

            return !itemInWay;
        }

        public static bool CheckRitualEquip(Mobile from)
        {
            var robe = from.FindItemOnLayer(Layer.OuterTorso);
            var staff = from.FindItemOnLayer(Layer.TwoHanded);
            var mounted = from.Mounted;

            if (robe is not RitualRobe ritualRobe || staff is not YoungOakStaff youngOakStaff)
            {
                from.SendFailureMessage("You must wear a consecrated ritual robe and young oak staff to perform a ritual.");
                return false;
            }

            if (ritualRobe.Owner != from || youngOakStaff.Owner != from)
            {
                from.SendFailureMessage("Your ritual equipment isn't consecrated.");
                return false;
            }

            if (mounted)
            {
                from.SendFailureMessage("You can't perform a ritual while mounted.");
                return false;
            }

            return true;
        }

        public abstract void OnRitualComplete(Mobile ritualMaster, BaseEquippableItem itemToEnchant);

        public override void OnDoubleClick(Mobile from)
        {
            if (from is not PlayerMobile player)
                return;

            if (!IsChildOf(from.Backpack))
            {
                from.SendFailureMessage("That must be in your backpack!");
                return;
            }

            if (RitualType != "Consecration" && !CheckRitualEquip(from))
            {
                return;
            }

            var permanentCirclePieces = GetPermanentCirclePieces(player);

            if (permanentCirclePieces is >= 1 and < 4)
            {
                from.SendFailureMessage("Your circle was corrupted!");
                return;
            }

            var hasPermanentCircle = permanentCirclePieces == 4;

            if (!hasPermanentCircle && !CanDrawCircle(from))
            {
                from.SendFailureMessage("You don't have the necessary space to draw a circle there.");
                return;
            }

            Consume();

            var speechCaptor = new RitualSpeechCaptor(hasPermanentCircle, player, RitualType, this);
            speechCaptor.MoveToWorld(new Point3D(from.Location.X, from.Location.Y + 5, from.Location.Z), from.Map);
        }
    }

    [SerializationGenerator(0, false)]
    public partial class RitualOfFreeMovement : RitualScroll
    {
        protected override string RitualType => "FreeMovement";

        public override SpellCircle Circle => 29;

        public override string DefaultName => "Ritual of Free Movement";

        [Constructible]
        public RitualOfFreeMovement() : base(0x1f31)
        {
        }

        public override void OnRitualComplete(Mobile from, BaseEquippableItem itemToEnchant)
        {
            if (itemToEnchant.ParalysisProtection >= 1)
            {
                from.SendFailureMessage("This item already has a more powerful enchantment on it.");
            }
            else
            {
                itemToEnchant.ParalysisProtection = 1;
                from.SendSuccessMessage($"This {itemToEnchant.Name} will now protect its wearer from paralysis.");
            }
        }
    }

    [SerializationGenerator(0, false)]
    public partial class RitualOfImmutability : RitualScroll
    {
        protected override string RitualType => "Immutability";

        public override SpellCircle Circle => 32;

        public override string DefaultName => "Ritual of Immutability";

        [Constructible]
        public RitualOfImmutability() : base(0x1f31)
        {
        }

        public override void OnRitualComplete(Mobile ritualMaster, BaseEquippableItem itemToEnchant)
        {
            var location = new Point3D(ritualMaster.Location.X, ritualMaster.Location.Y + 5, ritualMaster.Location.Z);
            var map = ritualMaster.Map;

            IPooledEnumerable eable = map.GetItemsInRange(location, 6);

            foreach (Item item in eable)
            {
                if (item is RitualRune rune && ritualMaster is PlayerMobile player)
                {
                    rune.Owner = player;
                }
            }

            eable.Free();

            ritualMaster.SendSuccessMessage("You will be able to call upon this circle everything you need now.");
        }
    }
}