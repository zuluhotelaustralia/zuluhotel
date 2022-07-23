using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.ContextMenus;
using Server.Engines.Magic;
using Server.Regions;
using Server.Network;
using Server.Multis;
using Server.Spells;
using Server.Misc;
using Server.Items;
using Server.Engines.PartySystem;
using Server.Engines.Spawners;
using Server.Guilds;
using Server.Gumps;
using Server.SkillHandlers;
using Server.Scripts.Engines.Loot;
using Server.Utilities;
using ZuluContent.Configuration.Types.Creatures;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;
using ZuluContent.Zulu.Items;
using static Scripts.Zulu.Engines.Classes.SkillCheck;

namespace Server.Mobiles
{
    public partial class BaseCreature : Mobile, IShilCheckSkill, IBuffable
    {
        public const int MaxLoyalty = 100;

        #region Var declarations

        private AIType m_CurrentAI; // The current AI
        private AIType m_DefaultAI; // The default AI

        private int m_iTeam; // Monster Team

        private double m_dCurrentSpeed; // The current speed, lets say it could be changed by something;

        private Point3D m_pHome; // The home position of the creature, used by some AI

        List<Type> m_arSpellAttack; // List of attack spell/power
        List<Type> m_arSpellDefense; // List of defensive spell/power

        private bool m_bControlled; // Is controlled
        private Mobile m_ControlMaster; // My master
        private OrderType m_ControlOrder; // My order
        private int m_ControlSlots;

        private int m_Loyalty;

        private bool m_bSummoned = false;

        private Mobile m_SummonMaster;

        private int m_DamageMin = -1;
        private int m_DamageMax = -1;

        private bool m_IsStabled;

        private bool m_HasGeneratedLoot; // have we generated our loot yet?

        private int m_FailedReturnHome; /* return to home failure counter */

        #endregion

        /* Do not serialize this till the code is finalized */

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SeeksHome { get; set; }

        private string m_RandomName;

        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = NameList.HasPlaceholder(value)
                    ? NameList.SubstitutePlaceholderName(value, m_RandomName ??= NameList.RandomName(value))
                    : value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string CorpseNameOverride
        {
            get => m_CorpseNameOverride;
            set
            {
                m_CorpseNameOverride = NameList.HasPlaceholder(value)
                    ? NameList.SubstitutePlaceholderName(value, m_RandomName ??= NameList.RandomName(value))
                    : value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster, AccessLevel.Administrator)]
        public bool IsStabled
        {
            get { return m_IsStabled; }
            set
            {
                m_IsStabled = value;
                if (m_IsStabled)
                    StopDeleteTimer();
            }
        }

        [CommandProperty(AccessLevel.GameMaster, AccessLevel.Administrator)]
        public Mobile StabledBy { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsPrisoner { get; set; }

        protected DateTime SummonEnd { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile LastOwner
        {
            get
            {
                if (Owners == null || Owners.Count == 0)
                    return null;

                return Owners[Owners.Count - 1];
            }
        }

        #region Delete Previously Tamed Timer

        private DeleteTimer m_DeleteTimer;

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan DeleteTimeLeft
        {
            get
            {
                if (m_DeleteTimer != null && m_DeleteTimer.Running)
                    return m_DeleteTimer.Next - DateTime.Now;

                return TimeSpan.Zero;
            }
        }

        private class DeleteTimer : Timer
        {
            private Mobile m;

            public DeleteTimer(Mobile creature, TimeSpan delay) : base(delay)
            {
                m = creature;
            }

            protected override void OnTick()
            {
                m.Delete();
            }
        }

        public void BeginDeleteTimer()
        {
            if (!(this is BaseEscortable) && !Summoned && !Deleted && !IsStabled)
            {
                StopDeleteTimer();
                m_DeleteTimer = new DeleteTimer(this, TimeSpan.FromDays(3.0));
                m_DeleteTimer.Start();
            }
        }

        public void StopDeleteTimer()
        {
            if (m_DeleteTimer != null)
            {
                m_DeleteTimer.Stop();
                m_DeleteTimer = null;
            }
        }

        #endregion

        public virtual FoodType FavoriteFood
        {
            get { return FoodType.Meat; }
        }

        public List<Mobile> Owners { get; private set; }

        public virtual bool Commandable
        {
            get { return true; }
        }

        public virtual Poison PoisonImmune
        {
            get { return null; }
        }

        public virtual bool Unprovokable
        {
            get { return BardImmune; }
        }

        public virtual bool Uncalmable
        {
            get { return BardImmune; }
        }

        public virtual bool AreaPeaceImmune
        {
            get { return BardImmune; }
        }

        public virtual double DispelDifficulty
        {
            get { return 0.0; }
        } // at this skill level we dispel 50% chance

        public virtual double DispelFocus
        {
            get { return 20.0; }
        } // at difficulty - focus we have 0%, at difficulty + focus we have 100%

        public virtual bool DisplayWeight
        {
            get { return Backpack is StrongBackpack; }
        }

        #region Breath ability, like dragon fire breath

        private long m_NextBreathTime;

        // Base damage given is: CurrentHitPoints * BreathDamageScalar
        public virtual double BreathDamageScalar
        {
            get { return 0.05; }
        }

        // Min/max seconds until next breath
        public virtual double BreathMinDelay
        {
            get { return 30.0; }
        }

        public virtual double BreathMaxDelay
        {
            get { return 45.0; }
        }

        // Creature stops moving for 1.0 seconds while breathing
        public virtual double BreathStallTime
        {
            get { return 1.0; }
        }

        // Effect is sent 1.3 seconds after BreathAngerSound and BreathAngerAnimation is played
        public virtual double BreathEffectDelay
        {
            get { return 1.3; }
        }

        // Damage is given 1.0 seconds after effect is sent
        public virtual double BreathDamageDelay
        {
            get { return 1.0; }
        }

        public virtual int BreathRange
        {
            get { return RangePerception; }
        }

        // Effect details and sound
        public virtual int BreathEffectItemID
        {
            get { return 0x36D4; }
        }

        public virtual int BreathEffectSpeed
        {
            get { return 5; }
        }

        public virtual int BreathEffectDuration
        {
            get { return 0; }
        }

        public virtual bool BreathEffectExplodes
        {
            get { return false; }
        }

        public virtual bool BreathEffectFixedDir
        {
            get { return false; }
        }

        public virtual int BreathEffectHue
        {
            get { return 0; }
        }

        public virtual int BreathEffectRenderMode
        {
            get { return 0; }
        }

        public virtual int BreathEffectSound
        {
            get { return 0x227; }
        }

        // Anger sound/animations
        public virtual int BreathAngerSound
        {
            get { return GetAngerSound(); }
        }

        public virtual int BreathAngerAnimation
        {
            get { return 12; }
        }

        public virtual void BreathStart(Mobile target)
        {
            BreathStallMovement();
            BreathPlayAngerSound();
            BreathPlayAngerAnimation();

            Direction = GetDirectionTo(target);

            Timer.StartTimer(TimeSpan.FromSeconds(BreathEffectDelay), () => BreathEffect_Callback(target));
        }

        public virtual void BreathStallMovement()
        {
            if (AIObject != null)
                AIObject.NextMove = Core.TickCount + (int)TimeSpan.FromSeconds(BreathStallTime).TotalMilliseconds;
        }

        public virtual void BreathPlayAngerSound()
        {
            PlaySound(BreathAngerSound);
        }

        public virtual void BreathPlayAngerAnimation()
        {
            Animate(BreathAngerAnimation, 5, 1, true, false, 0);
        }

        public virtual void BreathEffect_Callback(Mobile target)
        {
            if (!target.Alive || !CanBeHarmful(target))
                return;

            BreathPlayEffectSound();
            BreathPlayEffect(target);

            Timer.StartTimer(TimeSpan.FromSeconds(BreathDamageDelay), () => BreathDamage_Callback(target));
        }

        public virtual void BreathPlayEffectSound()
        {
            PlaySound(BreathEffectSound);
        }

        public virtual void BreathPlayEffect(Mobile target)
        {
            Effects.SendMovingEffect(this, target, BreathEffectItemID,
                BreathEffectSpeed, BreathEffectDuration, BreathEffectFixedDir,
                BreathEffectExplodes, BreathEffectHue, BreathEffectRenderMode);
        }

        public virtual void BreathDamage_Callback(Mobile target)
        {
            if (CanBeHarmful(target))
            {
                DoHarmful(target, false);
                BreathDealDamage(target);
            }
        }

        public virtual void BreathDealDamage(Mobile target)
        {
            target.Damage(BreathComputeDamage(), this);
        }

        public virtual int BreathComputeDamage()
        {
            int damage = (int)(Hits * BreathDamageScalar);

            if (damage > 200)
                damage = 200;

            return damage;
        }

        #endregion

        #region Spitting web ability

        public virtual double SpitWebMinDelay
        {
            get { return 5.0; }
        }

        public virtual double SpitWebMaxDelay
        {
            get { return 15.0; }
        }

        #endregion

        #region Flee!!!

        public virtual bool CanFlee
        {
            get { return true; }
        }

        public DateTime EndFleeTime { get; set; }

        public virtual void StopFlee()
        {
            EndFleeTime = DateTime.MinValue;
        }

        public virtual bool CheckFlee()
        {
            if (EndFleeTime == DateTime.MinValue)
                return false;

            if (DateTime.Now >= EndFleeTime)
            {
                StopFlee();
                return false;
            }

            return true;
        }

        public virtual void BeginFlee(TimeSpan maxDuration)
        {
            EndFleeTime = DateTime.Now + maxDuration;
        }

        #endregion

        public virtual bool IsInvulnerable
        {
            get { return false; }
        }

        public BaseAI AIObject { get; private set; }

        public const int MaxOwners = 5;
        
        public void StallMovement(TimeSpan stallTime)
        {
            if (AIObject != null)
                AIObject.NextMove = Core.TickCount + (int)stallTime.TotalMilliseconds;
        }

        #region Friends

        public List<Mobile> Friends { get; private set; }

        public virtual bool AllowNewPetFriend
        {
            get { return Friends == null || Friends.Count < 5; }
        }

        public virtual bool IsPetFriend(Mobile m)
        {
            return Friends != null && Friends.Contains(m);
        }

        public virtual void AddPetFriend(Mobile m)
        {
            if (Friends == null)
                Friends = new List<Mobile>();

            Friends.Add(m);
        }

        public virtual void RemovePetFriend(Mobile m)
        {
            Friends?.Remove(m);
        }

        public virtual bool IsFriend(Mobile m)
        {
            var g = OppositionGroup;

            if (!(m is BaseCreature creature))
                return false;

            return g?.IsFriendly(this, creature) ?? true;
        }

        #endregion

        public virtual bool IsEnemy(Mobile m)
        {
            var g = OppositionGroup;

            if (m is BaseGuard)
                return false;

            if (!(m is BaseCreature creature))
                return true;

            if (g != null && g.IsEnemy(this, creature))
                return true;

            if (FightMode == FightMode.Evil && m.Karma < 0 || creature.FightMode == FightMode.Evil && Karma < 0)
                return true;

            return m_iTeam != creature.m_iTeam || (m_bSummoned || m_bControlled) != (creature.m_bSummoned || creature.m_bControlled);
        }

        public virtual bool CheckControlChance(Mobile m)
        {
            if (CanBeControlledBy(m))
                return true;

            AIObject.DoOrderRelease(true);

            return false;
        }

        public virtual bool CanBeControlledBy(Mobile m)
        {
            if (m is PlayerMobile owner)
            {
                const int minSlots = 2;
                int maxSlots;
                var petSlots = 0;

                IPooledEnumerable eable = owner.Map.GetMobilesInRange(owner.Location, 8);

                foreach (Mobile nearbyMobile in eable)
                {
                    if (nearbyMobile is BaseCreature creature &&
                        (creature.ControlMaster == owner || creature.SummonMaster == owner))
                    {
                        if (!(creature.Summoned &&
                              creature.Serial != Serial &&
                              creature is BaseElementalLord &&
                              creature.AIObject.DoOrderRelease(true)
                            ))
                        {
                            petSlots += creature.GetPetSlots(owner);
                        }
                    }
                }

                eable.Free();

                if (m_bSummoned || SpellBound)
                {
                    var magery = m.Skills[SkillName.Magery].Value;
                    m.FireHook(h => h.OnModifyWithMagicEfficiency(m, ref magery));
                    maxSlots = (int)(magery / 20);
                    return !(petSlots > maxSlots && petSlots > minSlots);
                }

                var animalLore = (int)m.Skills[SkillName.AnimalLore].Value;
                var animalTaming = (int)m.Skills[SkillName.AnimalTaming].Value;
                maxSlots = (animalLore + animalTaming) / 15;
                return !(petSlots > maxSlots && petSlots > minSlots);
            }

            return false;
        }

        public virtual bool DeleteCorpseOnDeath { get; set; }

        public override void SetLocation(Point3D newLocation, bool isTeleport)
        {
            base.SetLocation(newLocation, isTeleport);

            if (isTeleport)
                AIObject?.OnTeleported();
        }

        public override ApplyPoisonResult ApplyPoison(Mobile from, Poison poison)
        {
            if (!Alive)
                return ApplyPoisonResult.Immune;

            ApplyPoisonResult result = base.ApplyPoison(from, poison);

            if (from != null && result == ApplyPoisonResult.Poisoned && PoisonTimer is PoisonImpl.PoisonTimer)
                (PoisonTimer as PoisonImpl.PoisonTimer).From = from;

            return result;
        }

        public override bool CheckPoisonImmunity(Mobile from, Poison poison)
        {
            var immune = base.CheckPoisonImmunity(from, poison);
            this.FireHook(h => h.OnCheckPoisonImmunity(from, this, poison, ref immune));

            if (immune)
                return true;

            var p = PoisonImmune;

            return p != null && p.Level >= poison.Level;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Loyalty
        {
            get { return m_Loyalty; }
            set { m_Loyalty = Math.Min(Math.Max(value, 0), MaxLoyalty); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WayPoint CurrentWayPoint { get; set; } = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public IPoint2D TargetLocation { get; set; } = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual Mobile ConstantFocus { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool DisallowAllMoves { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public override double ArmorRating
        {
            get
            {
                var mod = this.GetAllEnchantmentsOfType<IArmorMod>().Sum(b => b.ArmorMod);
                var rating = Items.OfType<IArmorRating>().Sum(i => i.ArmorRatingScaled);
                var value = VirtualArmor + VirtualArmorMod + rating + mod;

                return value >= 0 ? value : 0;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int DamageMin
        {
            get { return m_DamageMin; }
            set { m_DamageMin = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int DamageMax
        {
            get { return m_DamageMax; }
            set { m_DamageMax = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int HitsMax
        {
            get
            {
                if (HitsMaxSeed > 0)
                {
                    int value = HitsMaxSeed + GetStatOffset(StatType.Str);

                    if (value < 1)
                        value = 1;
                    else if (value > 65000)
                        value = 65000;

                    return value;
                }

                return Str;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitsMaxSeed { get; set; } = -1;

        [CommandProperty(AccessLevel.GameMaster)]
        public override int StamMax
        {
            get
            {
                if (StamMaxSeed > 0)
                {
                    int value = StamMaxSeed + GetStatOffset(StatType.Dex);

                    if (value < 1)
                        value = 1;
                    else if (value > 65000)
                        value = 65000;

                    return value;
                }

                return Dex;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StamMaxSeed { get; set; } = -1;

        [CommandProperty(AccessLevel.GameMaster)]
        public override int ManaMax
        {
            get
            {
                if (ManaMaxSeed > 0)
                {
                    int value = ManaMaxSeed + GetStatOffset(StatType.Int);

                    if (value < 1)
                        value = 1;
                    else if (value > 65000)
                        value = 65000;

                    return value;
                }

                return Int;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ManaMaxSeed { get; set; } = -1;

        public virtual bool CanOpenDoors
        {
            get { return !Body.IsAnimal && !Body.IsSea; }
        }

        public virtual bool CanMoveOverObstacles
        {
            get { return Body.IsMonster; }
        }

        public virtual bool CanDestroyObstacles
        {
            get
            {
                // to enable breaking of furniture, 'return CanMoveOverObstacles;'
                return false;
            }
        }

        public void Unpacify()
        {
            BardEndTime = DateTime.Now;
            BardPacified = false;
        }

        /*
    
        Seems this actually was removed on OSI somewhere between the original bug report and now.
        We will call it ML, until we can get better information. I suspect it was on the OSI TC when
        originally it taken out of RunUO, and not implmented on OSIs production shards until more
        recently.  Either way, this is, or was, accurate OSI behavior, and just entirely
        removing it was incorrect.  OSI followers were distracted by being attacked well into
        AoS, at very least.
    
        */

        public virtual bool CanBeDistracted
        {
            get { return true; }
        }

        public virtual void CheckDistracted(Mobile from)
        {
            if (Utility.RandomDouble() < .10)
            {
                ControlTarget = from;
                ControlOrder = OrderType.Attack;
                Combatant = from;
                Warmode = true;
            }
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (BardPacified && (HitsMax - Hits) * 0.001 > Utility.RandomDouble())
                Unpacify();

            if (amount > 0)
            {
                var c = BandageContext.GetContext(this);
                c?.Slip();
            }

            WeightOverloading.FatigueOnDamage(this, amount);

            InhumanSpeech speechType = SpeechType;

            if (speechType != null && !willKill)
                speechType.OnDamage(this, amount);

            if (!willKill)
            {
                if (CanBeDistracted && ControlOrder == OrderType.Follow)
                {
                    CheckDistracted(from);
                }
            }

            base.OnDamage(amount, from, willKill);
        }

        public virtual void OnDamagedBySpell(Mobile from)
        {
            if (CanBeDistracted && ControlOrder == OrderType.Follow)
            {
                CheckDistracted(from);
            }
        }

        public virtual void OnCarve(Mobile from, Corpse corpse, Item with)
        {
            int feathers = Feathers;
            int wool = Wool;
            int meat = Meat;
            int hides = Hides;

            if (feathers == 0 && wool == 0 && meat == 0 && hides == 0 || Summoned || corpse.Animated)
            {
                if (corpse.Animated)
                    corpse.SendLocalizedMessageTo(from, 500464); // Use this on corpses to carve away meat and hide
                else
                    from.SendLocalizedMessage(500485); // You see nothing useful to carve from the corpse.
            }
            else
            {
                new Blood(0x122D).MoveToWorld(corpse.Location, corpse.Map);

                if (feathers != 0)
                {
                    corpse.AddCarvedItem(new Feather(feathers), from);
                    from.SendLocalizedMessage(500479); // You pluck the bird. The feathers are now on the corpse.
                }

                if (wool != 0)
                {
                    corpse.AddCarvedItem(new TaintedWool(wool), from);
                    from.SendLocalizedMessage(500483); // You shear it, and the wool is now on the corpse.
                }

                if (meat != 0)
                {
                    if (MeatType == MeatType.Ribs)
                        corpse.AddCarvedItem(new RawRibs(meat), from);
                    else if (MeatType == MeatType.Bird)
                        corpse.AddCarvedItem(new RawBird(meat), from);
                    else if (MeatType == MeatType.LambLeg)
                        corpse.AddCarvedItem(new RawLambLeg(meat), from);

                    from.SendLocalizedMessage(500467); // You carve some meat, which remains on the corpse.
                }

                if (hides != 0)
                {
                    var hideType = CraftResources.GetTypeFromResource((CraftResource)(101 + HideType));
                    if (hideType != null)
                    {
                        from.PlaceInBackpack(hideType.CreateInstance<BaseHide>(hides));
                        from.SendSuccessMessage("You place the items in your pack.");
                    }
                }

                corpse.Carved = true;

                if (corpse.IsCriminalAction(from))
                    from.CriminalAction(true);
            }
        }
        
        public const int DefaultRangePerception = 10;

        public BaseCreature(AIType ai,
            FightMode mode,
            int iRangePerception,
            int iRangeFight,
            double dActiveSpeed,
            double dPassiveSpeed)
        {
            m_Loyalty = MaxLoyalty; // Wonderfully Happy

            m_CurrentAI = ai;
            m_DefaultAI = ai;

            RangePerception = iRangePerception;
            RangeFight = iRangeFight;

            FightMode = mode;

            m_iTeam = 0;

            ActiveSpeed = dActiveSpeed;
            PassiveSpeed = dPassiveSpeed;
            m_dCurrentSpeed = dPassiveSpeed;

            Debug = false;

            m_arSpellAttack = new List<Type>();
            m_arSpellDefense = new List<Type>();

            m_bControlled = false;
            m_ControlMaster = null;
            ControlTarget = null;
            m_ControlOrder = OrderType.None;

            Tamable = false;

            Owners = new List<Mobile>();

            NextReacquireTime = Core.TickCount + (int)ReacquireDelay.TotalMilliseconds;

            ChangeAIType(AI);

            InhumanSpeech speechType = SpeechType;

            speechType?.OnConstruct(this);
        }

        protected BaseCreature(CreatureProperties p) : this(p.AiType, p.FightMode, p.PerceptionRange, p.FightRange,
            p.ActiveSpeed, p.PassiveSpeed)
        {
        }

        public BaseCreature(Serial serial) : base(serial)
        {
            m_arSpellAttack = new List<Type>();
            m_arSpellDefense = new List<Type>();

            Debug = false;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)18); // version

            writer.Write((int)m_CurrentAI);
            writer.Write((int)m_DefaultAI);

            writer.Write((int)RangePerception);
            writer.Write((int)RangeFight);

            writer.Write((int)m_iTeam);

            writer.Write((double)ActiveSpeed);
            writer.Write((double)PassiveSpeed);
            writer.Write((double)m_dCurrentSpeed);

            writer.Write((int)m_pHome.X);
            writer.Write((int)m_pHome.Y);
            writer.Write((int)m_pHome.Z);

            // Version 1
            writer.Write((int)RangeHome);

            int i = 0;

            writer.Write((int)m_arSpellAttack.Count);
            for (i = 0; i < m_arSpellAttack.Count; i++)
            {
                writer.Write(m_arSpellAttack[i].ToString());
            }

            writer.Write((int)m_arSpellDefense.Count);
            for (i = 0; i < m_arSpellDefense.Count; i++)
            {
                writer.Write(m_arSpellDefense[i].ToString());
            }

            // Version 2
            writer.Write((int)FightMode);

            writer.Write((bool)m_bControlled);
            writer.Write((Mobile)m_ControlMaster);
            writer.Write((Mobile)ControlTarget);
            writer.Write((Point3D)ControlDest);
            writer.Write((int)m_ControlOrder);
            writer.Write((double)MinTameSkill);
            // Removed in version 9
            //writer.Write( (double) m_dMaxTameSkill );
            writer.Write((bool)Tamable);
            writer.Write((bool)m_bSummoned);

            if (m_bSummoned)
                writer.WriteDeltaTime(SummonEnd);

            writer.Write((int)m_ControlSlots);

            // Version 3
            writer.Write((int)m_Loyalty);

            // Version 4
            writer.Write(CurrentWayPoint);

            // Verison 5
            writer.Write(m_SummonMaster);

            // Version 6
            writer.Write((int)HitsMaxSeed);
            writer.Write((int)StamMaxSeed);
            writer.Write((int)ManaMaxSeed);
            writer.Write((int)m_DamageMin);
            writer.Write((int)m_DamageMax);

            // Version 8
            writer.Write(Owners);

            // Version 11
            writer.Write((bool)m_HasGeneratedLoot);

            // Version 13
            writer.Write((bool)(Friends != null && Friends.Count > 0));

            if (Friends != null && Friends.Count > 0)
                writer.Write(Friends);

            // Version 14
            writer.Write((bool)RemoveIfUntamed);
            writer.Write((int)RemoveStep);

            // Version 17
            if (IsStabled || Controlled && ControlMaster != null)
                writer.Write(TimeSpan.Zero);
            else
                writer.Write(DeleteTimeLeft);

            // Version 18
            writer.Write(CorpseNameOverride);
        }

        private static double[] m_StandardActiveSpeeds = new double[]
        {
            0.175, 0.1, 0.15, 0.2, 0.25, 0.3, 0.4, 0.5, 0.6, 0.8
        };

        private static double[] m_StandardPassiveSpeeds = new double[]
        {
            0.350, 0.2, 0.4, 0.5, 0.6, 0.8, 1.0, 1.2, 1.6, 2.0
        };

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_CurrentAI = (AIType)reader.ReadInt();
            m_DefaultAI = (AIType)reader.ReadInt();

            RangePerception = reader.ReadInt();
            RangeFight = reader.ReadInt();

            m_iTeam = reader.ReadInt();

            ActiveSpeed = reader.ReadDouble();
            PassiveSpeed = reader.ReadDouble();
            m_dCurrentSpeed = reader.ReadDouble();

            m_pHome.X = reader.ReadInt();
            m_pHome.Y = reader.ReadInt();
            m_pHome.Z = reader.ReadInt();

            if (version >= 1)
            {
                RangeHome = reader.ReadInt();

                int i, iCount;

                iCount = reader.ReadInt();
                for (i = 0; i < iCount; i++)
                {
                    string str = reader.ReadString();
                    Type type = Type.GetType(str);

                    if (type != null)
                    {
                        m_arSpellAttack.Add(type);
                    }
                }

                iCount = reader.ReadInt();
                for (i = 0; i < iCount; i++)
                {
                    string str = reader.ReadString();
                    Type type = Type.GetType(str);

                    if (type != null)
                    {
                        m_arSpellDefense.Add(type);
                    }
                }
            }
            else
            {
                RangeHome = 0;
            }

            if (version >= 2)
            {
                FightMode = (FightMode)reader.ReadInt();

                m_bControlled = reader.ReadBool();
                m_ControlMaster = reader.ReadEntity<Mobile>();
                ControlTarget = reader.ReadEntity<Mobile>();
                ControlDest = reader.ReadPoint3D();
                m_ControlOrder = (OrderType)reader.ReadInt();

                MinTameSkill = reader.ReadDouble();

                if (version < 9)
                    reader.ReadDouble();

                Tamable = reader.ReadBool();
                m_bSummoned = reader.ReadBool();

                if (m_bSummoned)
                {
                    SummonEnd = reader.ReadDeltaTime();
                    new UnsummonTimer(m_ControlMaster, this, SummonEnd - DateTime.Now).Start();
                }

                m_ControlSlots = reader.ReadInt();
            }
            else
            {
                FightMode = FightMode.Closest;

                m_bControlled = false;
                m_ControlMaster = null;
                ControlTarget = null;
                m_ControlOrder = OrderType.None;
            }

            if (version >= 3)
                m_Loyalty = reader.ReadInt();
            else
                m_Loyalty = MaxLoyalty; // Wonderfully Happy

            if (version >= 4)
                CurrentWayPoint = reader.ReadEntity<WayPoint>();

            if (version >= 5)
                m_SummonMaster = reader.ReadEntity<Mobile>();

            if (version >= 6)
            {
                HitsMaxSeed = reader.ReadInt();
                StamMaxSeed = reader.ReadInt();
                ManaMaxSeed = reader.ReadInt();
                m_DamageMin = reader.ReadInt();
                m_DamageMax = reader.ReadInt();
            }

            if (version >= 8)
                Owners = reader.ReadEntityList<Mobile>();
            else
                Owners = new List<Mobile>();

            if (version >= 11)
                m_HasGeneratedLoot = reader.ReadBool();
            else
                m_HasGeneratedLoot = true;

            if (version >= 13 && reader.ReadBool())
                Friends = reader.ReadEntityList<Mobile>();
            else if (version < 13 && m_ControlOrder >= OrderType.Unfriend)
                ++m_ControlOrder;

            if (version < 16 && Loyalty != MaxLoyalty)
                Loyalty *= 10;

            double activeSpeed = ActiveSpeed;
            double passiveSpeed = PassiveSpeed;

            bool isStandardActive = false;
            for (int i = 0; !isStandardActive && i < m_StandardActiveSpeeds.Length; ++i)
                isStandardActive = ActiveSpeed == m_StandardActiveSpeeds[i];

            bool isStandardPassive = false;
            for (int i = 0; !isStandardPassive && i < m_StandardPassiveSpeeds.Length; ++i)
                isStandardPassive = PassiveSpeed == m_StandardPassiveSpeeds[i];

            if (isStandardActive && m_dCurrentSpeed == ActiveSpeed)
                m_dCurrentSpeed = activeSpeed;
            else if (isStandardPassive && m_dCurrentSpeed == PassiveSpeed)
                m_dCurrentSpeed = passiveSpeed;

            if (isStandardActive)
                ActiveSpeed = activeSpeed;

            if (isStandardPassive)
                PassiveSpeed = passiveSpeed;

            if (version >= 14)
            {
                RemoveIfUntamed = reader.ReadBool();
                RemoveStep = reader.ReadInt();
            }

            TimeSpan deleteTime = TimeSpan.Zero;

            if (version >= 17)
                deleteTime = reader.ReadTimeSpan();

            if (deleteTime > TimeSpan.Zero || LastOwner != null && !Controlled && !IsStabled)
            {
                if (deleteTime == TimeSpan.Zero)
                    deleteTime = TimeSpan.FromDays(3.0);

                m_DeleteTimer = new DeleteTimer(this, deleteTime);
                m_DeleteTimer.Start();
            }

            if (version >= 18)
                CorpseNameOverride = reader.ReadString();

            CheckStatTimers();

            ChangeAIType(m_CurrentAI);

            AddFollowers();
        }

        public virtual bool IsHumanInTown()
        {
            return Body.IsHuman && Region.IsPartOf<GuardedRegion>();
        }

        public virtual bool CheckGold(Mobile from, Item dropped)
        {
            if (dropped is Gold gold)
                return OnGoldGiven(from, gold);

            return false;
        }

        public virtual bool OnGoldGiven(Mobile from, Gold dropped)
        {
            if (CheckTeachingMatch(from))
            {
                if (Teach(m_Teaching, from, dropped.Amount, true))
                {
                    dropped.Delete();
                    return true;
                }
            }
            else if (IsHumanInTown())
            {
                Direction = GetDirectionTo(from);

                int oldSpeechHue = SpeechHue;

                SpeechHue = 0x23F;
                SayTo(from, "Thou art giving me gold?");

                if (dropped.Amount >= 400)
                    SayTo(from, "'Tis a noble gift.");
                else
                    SayTo(from, "Money is always welcome.");

                SpeechHue = 0x3B2;
                SayTo(from, 501548); // I thank thee.

                SpeechHue = oldSpeechHue;

                dropped.Delete();
                return true;
            }

            return false;
        }

        public virtual bool CheckTrainingDeed(Mobile from, Item dropped)
        {
            if (dropped is SkillTrainingDeed deed)
                return OnTrainingDeedGiven(from, deed);

            return false;
        }

        public virtual bool OnTrainingDeedGiven(Mobile from, SkillTrainingDeed dropped)
        {
            if (from != dropped.Player)
            {
                Say("That is not yours to use! I have confiscated it.");
                dropped.Delete();
                return false;
            }

            if (CheckTeachingMatch(from))
            {
                var pointsToLearn = 0;
                CheckTeachSkills(m_Teaching, from, 0, ref pointsToLearn, false);
                from.SendGump(new TrainSkillsGump(from, this, dropped, m_Teaching, pointsToLearn));
                return false;
            }

            Say("Unfortunately I can only accept that for training skills!");

            return false;
        }

        public override bool ShouldCheckStatTimers
        {
            get { return false; }
        }

        #region Food

        private static Type[] m_Eggs = new Type[]
        {
            typeof(FriedEggs), typeof(Eggs)
        };

        private static Type[] m_Fish = new Type[]
        {
            typeof(FishSteak), typeof(RawFishSteak)
        };

        private static Type[] m_GrainsAndHay = new Type[]
        {
            typeof(BreadLoaf), typeof(FrenchBread), typeof(SheafOfHay)
        };

        private static Type[] m_Meat = new Type[]
        {
            /* Cooked */
            typeof(Bacon), typeof(CookedBird), typeof(Sausage),
            typeof(Ham), typeof(Ribs), typeof(LambLeg),
            typeof(ChickenLeg),

            /* Uncooked */
            typeof(RawBird), typeof(RawRibs), typeof(RawLambLeg),
            typeof(RawChickenLeg),

            /* Body Parts */
            typeof(Head), typeof(LeftArm), typeof(LeftLeg),
            typeof(Torso), typeof(RightArm), typeof(RightLeg)
        };

        private static Type[] m_FruitsAndVegies = new Type[]
        {
            typeof(HoneydewMelon), typeof(YellowGourd), typeof(GreenGourd),
            typeof(Banana), typeof(Bananas), typeof(Lemon), typeof(Lime),
            typeof(Dates), typeof(Grapes), typeof(Peach), typeof(Pear),
            typeof(Apple), typeof(Watermelon), typeof(Squash),
            typeof(Cantaloupe), typeof(Carrot), typeof(Cabbage),
            typeof(Onion), typeof(Lettuce), typeof(Pumpkin)
        };

        private static Type[] m_Gold = new Type[]
        {
            // white wyrms eat gold..
            typeof(Gold)
        };

        public virtual bool CheckFoodPreference(Item f)
        {
            if (CheckFoodPreference(f, FoodType.Eggs, m_Eggs))
                return true;

            if (CheckFoodPreference(f, FoodType.Fish, m_Fish))
                return true;

            if (CheckFoodPreference(f, FoodType.GrainsAndHay, m_GrainsAndHay))
                return true;

            if (CheckFoodPreference(f, FoodType.Meat, m_Meat))
                return true;

            if (CheckFoodPreference(f, FoodType.FruitsAndVegies, m_FruitsAndVegies))
                return true;

            if (CheckFoodPreference(f, FoodType.Gold, m_Gold))
                return true;

            return false;
        }

        public virtual bool CheckFoodPreference(Item fed, FoodType type, Type[] types)
        {
            if ((FavoriteFood & type) == 0)
                return false;

            Type fedType = fed.GetType();
            bool contains = false;

            for (int i = 0; !contains && i < types.Length; ++i)
                contains = fedType == types[i];

            return contains;
        }

        public virtual bool CheckFeed(Mobile from, Item dropped)
        {
            if (Controlled && (ControlMaster == from || IsPetFriend(from)))
            {
                Item f = dropped;

                if (CheckFoodPreference(f))
                {
                    int amount = f.Amount;

                    if (amount > 0)
                    {
                        int stamGain;

                        if (f is Gold)
                            stamGain = amount - 50;
                        else
                            stamGain = amount * 15 - 50;

                        if (stamGain > 0)
                            Stam += stamGain;

                        for (int i = 0; i < amount; ++i)
                        {
                            if (m_Loyalty < MaxLoyalty && 0.5 >= Utility.RandomDouble())
                            {
                                m_Loyalty += 10;
                            }
                        }

                        SayTo(from, 502060); // Your pet looks happier.

                        if (Body.IsAnimal)
                            Animate(3, 5, 1, true, false, 0);
                        else if (Body.IsMonster)
                            Animate(17, 5, 1, true, false, 0);

                        dropped.Delete();
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        public virtual bool OverrideBondingReqs()
        {
            return false;
        }

        public virtual bool CanAngerOnTame
        {
            get { return false; }
        }

        #region OnAction[...]

        public virtual void OnActionWander()
        {
        }

        public virtual void OnActionCombat()
        {
        }

        public virtual void OnActionGuard()
        {
        }

        public virtual void OnActionFlee()
        {
        }

        public virtual void OnActionInteract()
        {
        }

        public virtual void OnActionBackoff()
        {
        }

        #endregion

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (CheckFeed(from, dropped))
                return true;

            if (CheckGold(from, dropped))
                return true;

            if (CheckTrainingDeed(from, dropped))
                return true;

            return base.OnDragDrop(from, dropped);
        }

        protected virtual BaseAI ForcedAI
        {
            get { return null; }
        }

        public void ChangeAIType(AIType newAi)
        {
            AIObject?.m_Timer.Stop();

            if (ForcedAI != null)
            {
                AIObject = ForcedAI;
                return;
            }

            AIObject = newAi switch
            {
                AIType.AI_Melee => new MeleeAI(this),
                AIType.AI_Animal => new AnimalAI(this),
                AIType.AI_Berserk => new BerserkAI(this),
                AIType.AI_Archer => new ArcherAI(this),
                AIType.AI_Healer => new HealerAi(this),
                AIType.AI_Vendor => new VendorAI(this),
                AIType.AI_Mage => new MageAI(this),
                AIType.AI_Predator =>
                    //m_AI = new PredatorAI(this);
                    new MeleeAI(this),
                AIType.AI_Thief => new ThiefAI(this),
                AIType.AI_Familiar => new FamiliarAI(this),
                _ => null
            };
        }

        public void ChangeAIToDefault()
        {
            ChangeAIType(m_DefaultAI);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public AIType AI
        {
            get { return m_CurrentAI; }
            set
            {
                m_CurrentAI = value;

                if (m_CurrentAI == AIType.AI_Use_Default)
                {
                    m_CurrentAI = m_DefaultAI;
                }

                ChangeAIType(m_CurrentAI);
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Debug { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Team
        {
            get { return m_iTeam; }
            set
            {
                m_iTeam = value;

                OnTeamChange();
            }
        }

        public virtual void OnTeamChange()
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile FocusMob { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public FightMode FightMode { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RangePerception { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RangeFight { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RangeHome { get; set; } = 10;

        [CommandProperty(AccessLevel.GameMaster)]
        public double ActiveSpeed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public double PassiveSpeed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public double CurrentSpeed
        {
            get
            {
                if (TargetLocation != null)
                    return 0.3;

                return m_dCurrentSpeed;
            }
            set
            {
                if (m_dCurrentSpeed != value)
                {
                    m_dCurrentSpeed = value;

                    AIObject?.OnCurrentSpeedChanged();
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Home
        {
            get { return m_pHome; }
            set { m_pHome = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Controlled
        {
            get { return m_bControlled; }
            set
            {
                if (m_bControlled == value)
                    return;

                m_bControlled = value;
                Delta(MobileDelta.Noto);
            }
        }

        public void RemoveFollowers()
        {
            if (m_ControlMaster != null)
            {
                m_ControlMaster.Followers -= ControlSlots;
                (m_ControlMaster as PlayerMobile)?.AllFollowers.Remove(this);
            }
            else if (m_SummonMaster != null)
            {
                m_SummonMaster.Followers -= ControlSlots;
                (m_SummonMaster as PlayerMobile)?.AllFollowers.Remove(this);
            }

            if (m_ControlMaster != null && m_ControlMaster.Followers < 0)
                m_ControlMaster.Followers = 0;

            if (m_SummonMaster != null && m_SummonMaster.Followers < 0)
                m_SummonMaster.Followers = 0;
        }

        public void AddFollowers()
        {
            if (m_ControlMaster != null)
            {
                m_ControlMaster.Followers += ControlSlots;
                (m_ControlMaster as PlayerMobile)?.AllFollowers.Add(this);
            }
            else if (m_SummonMaster != null)
            {
                m_SummonMaster.Followers += ControlSlots;
                (m_SummonMaster as PlayerMobile)?.AllFollowers.Add(this);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile ControlMaster
        {
            get { return m_ControlMaster; }
            set
            {
                if (m_ControlMaster == value || this == value)
                    return;

                RemoveFollowers();
                m_ControlMaster = value;
                AddFollowers();
                if (m_ControlMaster != null)
                    StopDeleteTimer();

                Delta(MobileDelta.Noto);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile SummonMaster
        {
            get { return m_SummonMaster; }
            set
            {
                if (m_SummonMaster == value || this == value)
                    return;

                RemoveFollowers();
                m_SummonMaster = value;
                AddFollowers();

                Delta(MobileDelta.Noto);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile ControlTarget { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D ControlDest { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public OrderType ControlOrder
        {
            get { return m_ControlOrder; }
            set
            {
                m_ControlOrder = value;

                AIObject?.OnCurrentOrderChanged();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BardProvoked { get; set; } = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool BardPacified { get; set; } = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile BardMaster { get; set; } = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile BardTarget { get; set; } = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime BardEndTime { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public double MinTameSkill { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Tamable { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public long UnresponsiveToTamingEndTime { get; set; }

        [CommandProperty(AccessLevel.Administrator)]
        public bool Summoned
        {
            get { return m_bSummoned; }
            set
            {
                if (m_bSummoned == value)
                    return;

                NextReacquireTime = Core.TickCount;

                DeleteCorpseOnDeath = value;
                m_bSummoned = value;
                Delta(MobileDelta.Noto);
            }
        }

        [CommandProperty(AccessLevel.Administrator)]
        public int ControlSlots
        {
            get
            {
                var creatureScoreSlots = GetCreatureScore() / 30;
                return Math.Max(m_ControlSlots, creatureScoreSlots);
            }
            set => m_ControlSlots = value;
        }

        public virtual bool NoHouseRestrictions
        {
            get { return false; }
        }

        public virtual bool IsHouseSummonable
        {
            get { return false; }
        }

        #region Corpse Resources

        public virtual int Feathers
        {
            get { return 0; }
        }

        public virtual int Wool
        {
            get { return 0; }
        }

        public virtual MeatType MeatType
        {
            get { return MeatType.Ribs; }
        }

        #endregion


        public virtual double AutoDispelChance
        {
            get { return 1.0; }
        }

        public virtual bool IsScaryToPets
        {
            get { return false; }
        }

        public virtual bool IsScaredOfScaryThings
        {
            get { return true; }
        }

        public virtual int GetCreatureScore()
        {
            var highestStat = new List<int> { Str, Int, Dex }.Max() / 10;
            highestStat = (int)(highestStat * ZuluClass.Bonus);
            highestStat = Math.Min(highestStat, 150);

            return highestStat;
        }

        public virtual int GetPetSlots(PlayerMobile owner)
        {
            int slots;

            if (m_bSummoned)
            {
                var ownerInt = (double)owner.Int;
                owner.FireHook(h => h.OnModifyWithMagicEfficiency(owner, ref ownerInt));
                if (this is BaseElementalLord)
                    slots = 3;
                else if (Int > ownerInt || HitsMax > ownerInt)
                    slots = 2;
                else
                    slots = 1;
            }
            else
            {
                if (AI == AIType.AI_Mage)
                {
                    slots = HitsMax switch
                    {
                        > 500 => 9,
                        > 250 => 6,
                        _ => 3
                    };
                }
                else if (HasBreath)
                {
                    slots = HitsMax switch
                    {
                        > 500 => 12,
                        > 250 => 8,
                        _ => 4
                    };
                }
                else
                {
                    slots = HitsMax switch
                    {
                        > 300 => 3,
                        > 150 => 2,
                        _ => 1
                    };
                }
            }

            return slots;
        }

        public virtual void OnGotMeleeAttack(Mobile attacker)
        {
            if (AutoDispel && attacker is BaseCreature { IsDispellable: true } creature &&
                AutoDispelChance > Utility.RandomDouble())
                Dispel(creature);
        }

        public virtual void Dispel(Mobile m)
        {
            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8,
                20, 5042);
            Effects.PlaySound(m, 0x201);

            m.Delete();
        }

        public virtual bool DeleteOnRelease
        {
            get { return m_bSummoned; }
        }

        public virtual void OnGaveMeleeAttack(Mobile defender)
        {
            Poison p = HitPoison;

            if (p != null && HitPoisonChance >= Utility.RandomDouble())
            {
                defender.ApplyPoison(this, p);

                if (Controlled)
                    CheckSkill(SkillName.Poisoning, 0, Skills[SkillName.Poisoning].Cap);
            }

            if (AutoDispel && defender is BaseCreature && ((BaseCreature)defender).IsDispellable &&
                AutoDispelChance > Utility.RandomDouble())
                Dispel(defender);
        }

        public override void OnAfterDelete()
        {
            if (AIObject != null)
            {
                AIObject.m_Timer?.Stop();

                AIObject = null;
            }

            if (m_DeleteTimer != null)
            {
                m_DeleteTimer.Stop();
                m_DeleteTimer = null;
            }

            FocusMob = null;

            base.OnAfterDelete();
        }

        public void DebugSay(string text)
        {
            if (Debug)
                PublicOverheadMessage(MessageType.Regular, 41, false, text);
        }

        public void DebugSay(string format, params object[] args)
        {
            if (Debug)
                PublicOverheadMessage(MessageType.Regular, 41, false, String.Format(format, args));
        }

        /*
         * This function can be overriden.. so a "Strongest" mobile, can have a different definition depending
         * on who check for value
         * -Could add a FightMode.Prefered
         *
         */

        public virtual double GetFightModeRanking(Mobile m, FightMode acqType, bool bPlayerOnly)
        {
            if (bPlayerOnly && m.Player || !bPlayerOnly)
            {
                switch (acqType)
                {
                    case FightMode.Strongest:
                        return m.Skills[SkillName.Tactics].Value + m.Str; //returns strongest mobile

                    case FightMode.Weakest:
                        return -m.Hits; // returns weakest mobile

                    default:
                        return -GetDistanceToSqrt(m); // returns closest mobile
                }
            }
            else
            {
                return double.MinValue;
            }
        }

        // Turn, - for left, + for right
        // Basic for now, needs work
        public virtual void Turn(int iTurnSteps)
        {
            int v = (int)Direction;

            Direction = (Direction)((((v & 0x7) + iTurnSteps) & 0x7) | (v & 0x80));
        }

        public virtual void TurnInternal(int iTurnSteps)
        {
            int v = (int)Direction;

            SetDirection((Direction)((((v & 0x7) + iTurnSteps) & 0x7) | (v & 0x80)));
        }

        public bool IsHurt()
        {
            return Hits != HitsMax;
        }

        public double GetHomeDistance()
        {
            return GetDistanceToSqrt(m_pHome);
        }

        public virtual int GetTeamSize(int iRange)
        {
            int iCount = 0;

            foreach (Mobile m in GetMobilesInRange(iRange))
            {
                if (m is BaseCreature)
                {
                    if (((BaseCreature)m).Team == Team)
                    {
                        if (!m.Deleted)
                        {
                            if (m != this)
                            {
                                if (CanSee(m))
                                {
                                    iCount++;
                                }
                            }
                        }
                    }
                }
            }

            return iCount;
        }

        #region Teaching

        public virtual bool CanTeach
        {
            get { return false; }
        }

        public virtual bool CheckTeach(SkillName skill, Mobile from)
        {
            if (!CanTeach)
                return false;

            if (skill == SkillName.Stealth && from.Skills[SkillName.Hiding].Base < Stealth.HidingRequirement)
                return false;

            if (skill == SkillName.RemoveTrap && (from.Skills[SkillName.Lockpicking].Base < 50.0 ||
                                                  from.Skills[SkillName.DetectHidden].Base < 50.0))
                return false;

            if (skill == SkillName.Focus || skill == SkillName.Chivalry || skill == SkillName.Necromancy)
                return false;

            return true;
        }

        public enum TeachResult
        {
            Success,
            Failure,
            KnowsMoreThanMe,
            KnowsWhatIKnow,
            SkillNotRaisable,
            NotEnoughFreePoints
        }

        public virtual TeachResult CheckTeachSkills(SkillName skill, Mobile m, int maxPointsToLearn,
            ref int pointsToLearn, bool doTeach)
        {
            if (!CheckTeach(skill, m) || !m.CheckAlive())
                return TeachResult.Failure;

            var ourSkill = Skills[skill];
            var theirSkill = m.Skills[skill];

            if (ourSkill == null || theirSkill == null)
                return TeachResult.Failure;

            int baseToSet = ourSkill.BaseFixedPoint / 3;

            if (baseToSet > 420)
                baseToSet = 420;
            else if (baseToSet < 200)
                return TeachResult.Failure;

            if (baseToSet > theirSkill.CapFixedPoint)
                baseToSet = theirSkill.CapFixedPoint;

            pointsToLearn = baseToSet - theirSkill.BaseFixedPoint;

            if (maxPointsToLearn > 0 && pointsToLearn > maxPointsToLearn)
            {
                pointsToLearn = maxPointsToLearn;
                baseToSet = theirSkill.BaseFixedPoint + pointsToLearn;
            }

            if (pointsToLearn < 0)
                return TeachResult.KnowsMoreThanMe;

            if (pointsToLearn == 0)
                return TeachResult.KnowsWhatIKnow;

            if (theirSkill.Lock != SkillLock.Up)
                return TeachResult.SkillNotRaisable;

            if (doTeach)
                theirSkill.BaseFixedPoint = baseToSet;

            return TeachResult.Success;
        }

        public virtual bool CheckTeachingMatch(Mobile m)
        {
            if (m_Teaching == (SkillName)(-1))
                return false;

            if (m is PlayerMobile)
                return ((PlayerMobile)m).Learning == m_Teaching;

            return true;
        }

        private SkillName m_Teaching = (SkillName)(-1);

        public virtual bool Teach(SkillName skill, Mobile m, int maxPointsToLearn, bool doTeach)
        {
            int pointsToLearn = 0;
            TeachResult res = CheckTeachSkills(skill, m, maxPointsToLearn, ref pointsToLearn, doTeach);

            switch (res)
            {
                case TeachResult.KnowsMoreThanMe:
                    {
                        Say(501508); // I cannot teach thee, for thou knowest more than I!
                        break;
                    }
                case TeachResult.KnowsWhatIKnow:
                    {
                        Say(501509); // I cannot teach thee, for thou knowest all I can teach!
                        break;
                    }
                case TeachResult.NotEnoughFreePoints:
                case TeachResult.SkillNotRaisable:
                    {
                        // Make sure this skill is marked to raise. If you are near the skill cap (700 points) you may need to lose some points in another skill first.
                        m.SendLocalizedMessage(501510, "", 0x22);
                        break;
                    }
                case TeachResult.Success:
                    {
                        if (doTeach)
                        {
                            Say(501539); // Let me show thee something of how this is done.
                            m.SendSuccessMessage(501540); // Your skill level increases.

                            m_Teaching = (SkillName)(-1);

                            if (m is PlayerMobile)
                                ((PlayerMobile)m).Learning = (SkillName)(-1);
                        }
                        else
                        {
                            // I will teach thee all I know, if paid the amount in full.  The price is:
                            Say(1019077, AffixType.Append, String.Format(" {0}", pointsToLearn), "");
                            Say(1043108); // For less I shall teach thee less.

                            m_Teaching = skill;

                            if (m is PlayerMobile)
                                ((PlayerMobile)m).Learning = skill;
                        }

                        return true;
                    }
            }

            return false;
        }

        #endregion

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            if (ControlMaster != null)
                if (NotorietyHandlers.CheckAggressor(ControlMaster.Aggressors, aggressor))
                    aggressor.Aggressors.Add(AggressorInfo.Create(this, aggressor, true));

            OrderType ct = m_ControlOrder;

            AIObject?.OnAggressiveAction(aggressor);

            StopFlee();

            ForceReacquire();

            if (aggressor.ChangingCombatant && (m_bControlled || m_bSummoned) &&
                (ct == OrderType.Come || ct == OrderType.Stay || ct == OrderType.Stop || ct == OrderType.None ||
                 ct == OrderType.Follow))
            {
                ControlTarget = aggressor;
                ControlOrder = OrderType.Attack;
            }
            else if (Combatant == null && !BardPacified)
            {
                Warmode = true;
                Combatant = aggressor;
            }
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m is BaseCreature && !((BaseCreature)m).Controlled)
                return !Alive || !m.Alive || Hidden && AccessLevel > AccessLevel.Player;

            return base.OnMoveOver(m);
        }

        public virtual bool CanDrop
        {
            get { return false; }
        }

        public virtual void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            if (Commandable)
            {
                AIObject?.GetContextMenuEntries(from, list);
            }

            if (Tamable && !Controlled && from.Alive)
            {
                list.Add(new TameEntry(from, this));
                list.Add(new LoreEntry(from, this));
            }

            AddCustomContextEntries(from, list);

            if (CanTeach && from.Alive)
            {
                var ourSkills = Skills;
                var theirSkills = from.Skills;

                for (var i = 0; i < ourSkills.Length && i < theirSkills.Length; ++i)
                {
                    var skill = ourSkills[i];
                    var theirSkill = theirSkills[i];

                    if (skill?.Base >= 60.0 && CheckTeach(skill.SkillName, from))
                    {
                        var toTeach = skill.BaseFixedPoint / 3;

                        if (toTeach > 420)
                        {
                            toTeach = 420;
                        }

                        list.Add(new TeachEntry((SkillName)i, this, from, toTeach > theirSkill.BaseFixedPoint));
                    }
                }
            }
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            InhumanSpeech speechType = SpeechType;

            if (speechType != null && (speechType.Flags & IHSFlags.OnSpeech) != 0 && from.InRange(this, 3))
                return true;

            return AIObject != null && AIObject.HandlesOnSpeech(@from) && @from.InRange(this, RangePerception);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            InhumanSpeech speechType = SpeechType;

            if (speechType != null && speechType.OnSpeech(this, e.Mobile, e.Speech))
                e.Handled = true;
            else if (!e.Handled && AIObject != null && e.Mobile.InRange(this, RangePerception))
                AIObject.OnSpeech(e);
        }

        public override bool IsHarmfulCriminal(Mobile target)
        {
            if (Controlled && target == m_ControlMaster || Summoned && target == m_SummonMaster)
                return false;

            if (target is BaseCreature && ((BaseCreature)target).InitialInnocent &&
                !((BaseCreature)target).Controlled)
                return false;

            if (target is PlayerMobile && ((PlayerMobile)target).PermaFlags.Count > 0)
                return false;

            return base.IsHarmfulCriminal(target);
        }

        public override void CriminalAction(bool message)
        {
            base.CriminalAction(message);

            if (Controlled || Summoned)
            {
                if (m_ControlMaster != null && m_ControlMaster.Player)
                    m_ControlMaster.CriminalAction(false);
                else if (m_SummonMaster != null && m_SummonMaster.Player)
                    m_SummonMaster.CriminalAction(false);
            }
        }

        public override void DoHarmful(Mobile target, bool indirect)
        {
            base.DoHarmful(target, indirect);

            if (target == this || target == m_ControlMaster || target == m_SummonMaster || !Controlled && !Summoned)
                return;

            List<AggressorInfo> list = Aggressors;

            for (int i = 0; i < list.Count; ++i)
            {
                AggressorInfo ai = list[i];

                if (ai.Attacker == target)
                    return;
            }

            list = Aggressed;

            for (int i = 0; i < list.Count; ++i)
            {
                AggressorInfo ai = list[i];

                if (ai.Defender == target)
                {
                    if (m_ControlMaster != null && m_ControlMaster.Player &&
                        m_ControlMaster.CanBeHarmful(target, false))
                        m_ControlMaster.DoHarmful(target, true);
                    else if (m_SummonMaster != null && m_SummonMaster.Player &&
                             m_SummonMaster.CanBeHarmful(target, false))
                        m_SummonMaster.DoHarmful(target, true);

                    return;
                }
            }
        }

        private static Mobile m_NoDupeGuards;

        public void ReleaseGuardDupeLock()
        {
            m_NoDupeGuards = null;
        }

        public void ReleaseGuardLock()
        {
            EndAction(typeof(GuardedRegion));
        }

        private DateTime m_IdleReleaseTime;

        public virtual bool CheckIdle()
        {
            if (Combatant != null)
                return false; // in combat.. not idling

            if (m_IdleReleaseTime > DateTime.MinValue)
            {
                // idling...

                if (DateTime.Now >= m_IdleReleaseTime)
                {
                    m_IdleReleaseTime = DateTime.MinValue;
                    return false; // idle is over
                }

                return true; // still idling
            }

            if (95 > Utility.Random(100))
                return false; // not idling, but don't want to enter idle state

            m_IdleReleaseTime = DateTime.Now + TimeSpan.FromSeconds(Utility.RandomMinMax(15, 25));

            if (Body.IsHuman)
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        CheckedAnimate(5, 5, 1, true, true, 1);
                        break;
                    case 1:
                        CheckedAnimate(6, 5, 1, true, false, 1);
                        break;
                }
            }
            else if (Body.IsAnimal)
            {
                switch (Utility.Random(3))
                {
                    case 0:
                        CheckedAnimate(3, 3, 1, true, false, 1);
                        break;
                    case 1:
                        CheckedAnimate(9, 5, 1, true, false, 1);
                        break;
                    case 2:
                        CheckedAnimate(10, 5, 1, true, false, 1);
                        break;
                }
            }
            else if (Body.IsMonster)
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        CheckedAnimate(17, 5, 1, true, false, 1);
                        break;
                    case 1:
                        CheckedAnimate(18, 5, 1, true, false, 1);
                        break;
                }
            }

            PlaySound(GetIdleSound());
            return true; // entered idle state
        }

        /*
            this way, due to the huge number of locations this will have to be changed
            Perhaps we can change this in the future when fixing game play is not the
            major issue.
        */

        public virtual void CheckedAnimate(int action, int frameCount, int repeatCount, bool forward, bool repeat,
            int delay)
        {
            if (!Mounted)
            {
                base.Animate(action, frameCount, repeatCount, forward, repeat, delay);
            }
        }

        public override void Animate(int action, int frameCount, int repeatCount, bool forward, bool repeat, int delay)
        {
            base.Animate(action, frameCount, repeatCount, forward, repeat, delay);
        }

        private void CheckAIActive()
        {
            Map map = Map;

            if (PlayerRangeSensitive && AIObject != null && map != null && map.GetSector(Location).Active)
                AIObject.Activate();
        }

        public override void OnCombatantChange()
        {
            base.OnCombatantChange();

            Warmode = Combatant is { Deleted: false, Alive: true };

            if (CanFly && Warmode)
            {
                Flying = false;
            }
        }

        protected override void OnMapChange(Map oldMap)
        {
            CheckAIActive();

            base.OnMapChange(oldMap);
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            CheckAIActive();

            base.OnLocationChange(oldLocation);
        }

        public virtual void ForceReacquire()
        {
            NextReacquireTime = Core.TickCount;
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (ReacquireOnMovement)
            {
                ForceReacquire();
            }

            InhumanSpeech speechType = SpeechType;

            speechType?.OnMovement(this, m, oldLocation);

            /* Begin notice sound */
            if ((!m.Hidden || m.AccessLevel == AccessLevel.Player) && m.Player && FightMode != FightMode.Aggressor &&
                FightMode != FightMode.None && Combatant == null && !Controlled && !Summoned)
            {
                // If this creature defends itself but doesn't actively attack (animal) or
                // doesn't fight at all (vendor) then no notice sounds are played..
                // So, players are only notified of aggressive monsters

                // Monsters that are currently fighting are ignored

                // Controlled or summoned creatures are ignored

                if (InRange(m.Location, 18) && !InRange(oldLocation, 18))
                {
                    if (Body.IsMonster)
                        Animate(11, 5, 1, true, false, 1);

                    PlaySound(GetAngerSound());
                }
            }
            /* End notice sound */

            if (m_NoDupeGuards == m)
                return;

            if (!Body.IsHuman || Kills >= 5 || AlwaysMurderer || AlwaysAttackable || m.Kills < 5 ||
                !m.InRange(Location, 12) || !m.Alive)
                return;

            GuardedRegion guardedRegion = (GuardedRegion)Region.GetRegion(typeof(GuardedRegion));

            if (guardedRegion != null)
            {
                if (!guardedRegion.IsDisabled() && guardedRegion.IsGuardCandidate(m) &&
                    BeginAction(typeof(GuardedRegion)))
                {
                    Say(1013037 + Utility.Random(16));
                    guardedRegion.CallGuards(Location);

                    Timer.DelayCall(TimeSpan.FromSeconds(5.0), ReleaseGuardLock);

                    m_NoDupeGuards = m;
                    Timer.DelayCall(TimeSpan.Zero, ReleaseGuardDupeLock);
                }
            }
        }

        public override void OnHiddenChanged()
        {
            base.OnHiddenChanged();
            this.FireHook(h => h.OnHiddenChanged(this));
        }

        public void AddSpellAttack(Type type)
        {
            m_arSpellAttack.Add(type);
        }

        public void AddSpellDefense(Type type)
        {
            m_arSpellDefense.Add(type);
        }

        public Spell GetAttackSpellRandom()
        {
            if (m_arSpellAttack.Count > 0)
            {
                Type type = m_arSpellAttack[Utility.Random(m_arSpellAttack.Count)];

                object[] args = { this, null };
                return Activator.CreateInstance(type, args) as Spell;
            }
            else
            {
                return null;
            }
        }

        public Spell GetDefenseSpellRandom()
        {
            if (m_arSpellDefense.Count > 0)
            {
                Type type = m_arSpellDefense[Utility.Random(m_arSpellDefense.Count)];

                object[] args = { this, null };
                return Activator.CreateInstance(type, args) as Spell;
            }
            else
            {
                return null;
            }
        }

        public Spell GetSpellSpecific(Type type)
        {
            int i;

            for (i = 0; i < m_arSpellAttack.Count; i++)
            {
                if (m_arSpellAttack[i] == type)
                {
                    object[] args = { this, null };
                    return Activator.CreateInstance(type, args) as Spell;
                }
            }

            for (i = 0; i < m_arSpellDefense.Count; i++)
            {
                if (m_arSpellDefense[i] == type)
                {
                    object[] args = { this, null };
                    return Activator.CreateInstance(type, args) as Spell;
                }
            }

            return null;
        }

        #region Set[...]

        public void SetDamage(int val)
        {
            m_DamageMin = val;
            m_DamageMax = val;
        }

        public void SetDamage(int min, int max)
        {
            m_DamageMin = min;
            m_DamageMax = max;
        }

        public void SetHits(int val)
        {
            HitsMaxSeed = val;
            Hits = HitsMax;
        }

        public void SetHits(int min, int max)
        {
            HitsMaxSeed = Utility.RandomMinMax(min, max);
            Hits = HitsMax;
        }

        public void SetStam(int val)
        {
            StamMaxSeed = val;
            Stam = StamMax;
        }

        public void SetStam(int min, int max)
        {
            StamMaxSeed = Utility.RandomMinMax(min, max);
            Stam = StamMax;
        }

        public void SetMana(int val)
        {
            ManaMaxSeed = val;
            Mana = ManaMax;
        }

        public void SetMana(int min, int max)
        {
            ManaMaxSeed = Utility.RandomMinMax(min, max);
            Mana = ManaMax;
        }

        public void SetStr(int val)
        {
            RawStr = val;
            Hits = HitsMax;
        }

        public void SetStr(int min, int max)
        {
            RawStr = Utility.RandomMinMax(min, max);
            Hits = HitsMax;
        }

        public void SetDex(int val)
        {
            RawDex = val;
            Stam = StamMax;
        }

        public void SetDex(int min, int max)
        {
            RawDex = Utility.RandomMinMax(min, max);
            Stam = StamMax;
        }

        public void SetInt(int val)
        {
            RawInt = val;
            Mana = ManaMax;
        }

        public void SetInt(int min, int max)
        {
            RawInt = Utility.RandomMinMax(min, max);
            Mana = ManaMax;
        }

        public void SetSkill(SkillName name, double val)
        {
            Skills[name].BaseFixedPoint = (int)(val * 10);

            if (Skills[name].Base > Skills[name].Cap)
            {
                Skills[name].Cap = Skills[name].Base;
            }
        }

        public void SetSkill(SkillName name, double min, double max)
        {
            int minFixed = (int)(min * 10);
            int maxFixed = (int)(max * 10);

            Skills[name].BaseFixedPoint = Utility.RandomMinMax(minFixed, maxFixed);

            if (Skills[name].Base > Skills[name].Cap)
            {
                Skills[name].Cap = Skills[name].Base;
            }
        }

        public void SetFameLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Fame = Utility.RandomMinMax(0, 1249);
                    break;
                case 2:
                    Fame = Utility.RandomMinMax(1250, 2499);
                    break;
                case 3:
                    Fame = Utility.RandomMinMax(2500, 4999);
                    break;
                case 4:
                    Fame = Utility.RandomMinMax(5000, 9999);
                    break;
                case 5:
                    Fame = Utility.RandomMinMax(10000, 10000);
                    break;
            }
        }

        public void SetKarmaLevel(int level)
        {
            switch (level)
            {
                case 0:
                    Karma = -Utility.RandomMinMax(0, 624);
                    break;
                case 1:
                    Karma = -Utility.RandomMinMax(625, 1249);
                    break;
                case 2:
                    Karma = -Utility.RandomMinMax(1250, 2499);
                    break;
                case 3:
                    Karma = -Utility.RandomMinMax(2500, 4999);
                    break;
                case 4:
                    Karma = -Utility.RandomMinMax(5000, 9999);
                    break;
                case 5:
                    Karma = -Utility.RandomMinMax(10000, 10000);
                    break;
            }
        }

        #endregion

        public static void Cap(ref int val, int min, int max)
        {
            if (val < min)
                val = min;
            else if (val > max)
                val = max;
        }

        #region Pack & Loot

        public void PackPotion()
        {
            PackItem(Loot.RandomPotion());
        }

        public void PackScroll(int minCircle, int maxCircle)
        {
            PackScroll(Utility.RandomMinMax(minCircle, maxCircle));
        }

        public void PackScroll(int circle)
        {
            int min = (circle - 1) * 8;

            PackItem(Loot.RandomScroll(min, min + 7, SpellbookType.Regular));
        }

        public void PackMagicItems(int minLevel, int maxLevel)
        {
            PackMagicItems(minLevel, maxLevel, 0.30, 0.15);
        }

        public void PackMagicItems(int minLevel, int maxLevel, double armorChance, double weaponChance)
        {
            if (!PackArmor(minLevel, maxLevel, armorChance))
                PackWeapon(minLevel, maxLevel, weaponChance);
        }

        public virtual void DropBackpack()
        {
            if (Backpack != null)
            {
                if (Backpack.Items.Count > 0)
                {
                    Backpack b = new CreatureBackpack(Name);

                    List<Item> list = new List<Item>(Backpack.Items);
                    foreach (Item item in list)
                    {
                        b.DropItem(item);
                    }

                    BaseHouse house = BaseHouse.FindHouseAt(this);
                    if (house != null)
                        b.MoveToWorld(house.BanLocation, house.Map);
                    else
                        b.MoveToWorld(Location, Map);
                }
            }
        }

        public virtual void GenerateLoot()
        {
            if (Summoned)
                return;

            if (LootTable == null || !ZhConfig.Loot.Tables.TryGetValue(LootTable, out var table))
                return;

            var backpack = Backpack;
            if (backpack == null)
            {
                backpack = new Backpack { Movable = false };
                AddItem(backpack);
            }

            LootGenerator.MakeLoot(LastKiller, backpack, table, LootItemLevel, LootItemChance);
        }

        public bool PackArmor(int minLevel, int maxLevel)
        {
            return PackArmor(minLevel, maxLevel, 1.0);
        }

        public bool PackArmor(int minLevel, int maxLevel, double chance)
        {
            if (chance <= Utility.RandomDouble())
                return false;

            Cap(ref minLevel, 0, 5);
            Cap(ref maxLevel, 0, 5);

            BaseArmor armor = Loot.RandomArmorOrShield();

            if (armor == null)
                return false;

            armor.ProtectionLevel = (ArmorProtectionLevel)RandomMinMaxScaled(minLevel, maxLevel);
            armor.Durability = (ArmorDurabilityLevel)RandomMinMaxScaled(minLevel, maxLevel);

            PackItem(armor);

            return true;
        }

        public static int RandomMinMaxScaled(int min, int max)
        {
            if (min == max)
                return min;

            if (min > max)
            {
                int hold = min;
                min = max;
                max = hold;
            }

            int count = max - min + 1;
            int total = 0, toAdd = count;

            for (int i = 0; i < count; ++i, --toAdd)
                total += toAdd * toAdd;

            int rand = Utility.Random(total);
            toAdd = count;

            int val = min;

            for (int i = 0; i < count; ++i, --toAdd, ++val)
            {
                rand -= toAdd * toAdd;

                if (rand < 0)
                    break;
            }

            return val;
        }

        public bool PackWeapon(int minLevel, int maxLevel)
        {
            return PackWeapon(minLevel, maxLevel, 1.0);
        }

        public bool PackWeapon(int minLevel, int maxLevel, double chance)
        {
            if (chance <= Utility.RandomDouble())
                return false;

            Cap(ref minLevel, 0, 5);
            Cap(ref maxLevel, 0, 5);

            BaseWeapon weapon = Loot.RandomWeapon();

            if (weapon == null)
                return false;

            if (0.05 > Utility.RandomDouble())
                weapon.OldSlayer = SlayerName.Silver;

            weapon.DamageLevel = (WeaponDamageLevel)RandomMinMaxScaled(minLevel, maxLevel);
            weapon.AccuracyLevel = (WeaponAccuracyLevel)RandomMinMaxScaled(minLevel, maxLevel);
            weapon.DurabilityLevel = (WeaponDurabilityLevel)RandomMinMaxScaled(minLevel, maxLevel);

            PackItem(weapon);

            return true;
        }

        public void PackGold(int amount)
        {
            if (amount > 0)
                PackItem(new Gold(amount));
        }

        public void PackGold(int min, int max)
        {
            PackGold(Utility.RandomMinMax(min, max));
        }

        public void PackStatue(int min, int max)
        {
            PackStatue(Utility.RandomMinMax(min, max));
        }

        public void PackStatue(int amount)
        {
            for (int i = 0; i < amount; ++i)
                PackStatue();
        }

        public void PackStatue()
        {
            PackItem(Loot.RandomStatue());
        }

        public void PackGem()
        {
            PackGem(1);
        }

        public void PackGem(int min, int max)
        {
            PackGem(Utility.RandomMinMax(min, max));
        }

        public void PackGem(int amount)
        {
            if (amount <= 0)
                return;

            Item gem = Loot.RandomGem();

            gem.Amount = amount;

            PackItem(gem);
        }

        public void PackReg(int min, int max)
        {
            PackReg(Utility.RandomMinMax(min, max));
        }

        public void PackReg(int amount)
        {
            if (amount <= 0)
                return;

            Item reg = Loot.RandomReagent();

            reg.Amount = amount;

            PackItem(reg);
        }

        public void PackItem(Item item)
        {
            if (Summoned || item == null)
            {
                item?.Delete();

                return;
            }

            Container pack = Backpack;

            if (pack == null)
            {
                pack = new Backpack();

                pack.Movable = false;

                AddItem(pack);
            }

            if (!item.Stackable || !pack.TryDropItem(this, item, false)) // try stack
                pack.DropItem(item); // failed, drop it anyway
        }

        #endregion

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster && !Body.IsHuman)
            {
                Container pack = Backpack;

                pack?.DisplayTo(@from);
            }

            base.OnDoubleClick(from);
        }

        private static string[] m_GuildTypes = new string[]
        {
            "",
            " (Chaos)",
            " (Order)"
        };

        public override void OnSingleClick(Mobile from)
        {
            if (Deleted)
                return;
            else if (AccessLevel == AccessLevel.Player && DisableHiddenSelfClick && Hidden && from == this)
                return;

            BaseGuild guild = Guild;

            if (guild != null && (DisplayGuildTitle || Player && guild.Type != GuildType.Regular))
            {
                string title = GuildTitle;
                string type;

                if (title == null)
                    title = "";
                else
                    title = title.Trim();

                if (guild.Type >= 0 && (int)guild.Type < m_GuildTypes.Length)
                    type = m_GuildTypes[(int)guild.Type];
                else
                    type = "";

                string text = String.Format(title.Length <= 0 ? "[{1}]{2}" : "[{0}, {1}]{2}", title, guild.Abbreviation,
                    type);

                PrivateOverheadMessage(MessageType.Regular, SpeechHue, true, text, from.NetState);
            }

            int hue;

            if (NameHue != -1)
                hue = NameHue;
            else
                hue = Notoriety.GetHue(Notoriety.Compute(from, this));

            string name = Name;

            if (name == null)
                name = String.Empty;

            string prefix = "";

            if (ShowFameTitle && Body.IsHuman && Fame >= 10000)
                prefix = Female ? "Lady" : "Lord";

            string suffix = "";

            if (ClickTitle && Title?.Length > 0)
                suffix = Title;

            suffix = ApplyNameSuffix(suffix);

            string val;

            if (prefix.Length > 0 && suffix.Length > 0)
                val = String.Concat(prefix, " ", name, " ", suffix);
            else if (prefix.Length > 0)
                val = String.Concat(prefix, " ", name);
            else if (suffix.Length > 0)
                val = String.Concat(name, " ", suffix);
            else
                val = name;

            if (Controlled && Commandable)
            {
                if (AnimatedDead)
                    val = $"a Reanimated {ZuluUtil.TrimIndefiniteArticle(val)}";
                else if (SpellBound)
                    val += " [spellbound]";
                else if (Summoned)
                    val += " [summoned]"; // (summoned)
                else
                    val += " [tame]"; // (tame)
            }

            if (IsInvulnerable)
                val += " [invulnerable]";

            if (BardEndTime > Core.Now)
            {
                var timeDiff = BardEndTime - Core.Now;
                var timeVal = "";

                if (timeDiff.Minutes > 0)
                    timeVal += $"{timeDiff.Minutes}m ";

                if (timeDiff.Seconds > 0)
                    timeVal += $"{timeDiff.Seconds}s";

                if (BardPacified)
                    PrivateOverheadMessage(MessageType.Label, 0x9B, true, $"*pacified {timeVal}*", from.NetState);
                else if (BardProvoked)
                    PrivateOverheadMessage(MessageType.Label, 0x9B, true, $"*provoked {timeVal}*", from.NetState);

            }

            PrivateOverheadMessage(MessageType.Label, hue, true, val, from.NetState);
        }

        public virtual bool IgnoreYoungProtection
        {
            get { return false; }
        }

        public override bool OnBeforeDeath()
        {
            if (!Summoned && !NoKillAwards && !m_HasGeneratedLoot)
            {
                m_HasGeneratedLoot = true;
                GenerateLoot();
            }

            InhumanSpeech speechType = SpeechType;

            speechType?.OnDeath(this);

            return base.OnBeforeDeath();
        }

        public bool NoKillAwards { get; set; }

        public int ComputeBonusDamage(List<DamageEntry> list, Mobile m)
        {
            int bonus = 0;

            for (int i = list.Count - 1; i >= 0; --i)
            {
                DamageEntry de = list[i];

                if (de.Damager == m || !(de.Damager is BaseCreature))
                    continue;

                BaseCreature bc = (BaseCreature)de.Damager;
                Mobile master = null;

                master = bc.GetMaster();

                if (master == m)
                    bonus += de.DamageGiven;
            }

            return bonus;
        }

        public Mobile GetMaster()
        {
            if (Controlled && ControlMaster != null)
                return ControlMaster;
            else if (Summoned && SummonMaster != null)
                return SummonMaster;

            return null;
        }

        public static List<DamageStore> GetLootingRights(List<DamageEntry> damageEntries, int hitsMax)
        {
            List<DamageStore> rights = new List<DamageStore>();

            for (int i = damageEntries.Count - 1; i >= 0; --i)
            {
                if (i >= damageEntries.Count)
                    continue;

                DamageEntry de = damageEntries[i];

                if (de.HasExpired)
                {
                    damageEntries.RemoveAt(i);
                    continue;
                }

                int damage = de.DamageGiven;

                List<DamageEntry> respList = de.Responsible;

                if (respList != null)
                {
                    for (int j = 0; j < respList.Count; ++j)
                    {
                        DamageEntry subEntry = respList[j];
                        Mobile master = subEntry.Damager;

                        if (master == null || master.Deleted || !master.Player)
                            continue;

                        bool needNewSubEntry = true;

                        for (int k = 0; needNewSubEntry && k < rights.Count; ++k)
                        {
                            DamageStore ds = rights[k];

                            if (ds.m_Mobile == master)
                            {
                                ds.m_Damage += subEntry.DamageGiven;
                                needNewSubEntry = false;
                            }
                        }

                        if (needNewSubEntry)
                            rights.Add(new DamageStore(master, subEntry.DamageGiven));

                        damage -= subEntry.DamageGiven;
                    }
                }

                Mobile m = de.Damager;

                if (m == null || m.Deleted || !m.Player)
                    continue;

                if (damage <= 0)
                    continue;

                bool needNewEntry = true;

                for (int j = 0; needNewEntry && j < rights.Count; ++j)
                {
                    DamageStore ds = rights[j];

                    if (ds.m_Mobile == m)
                    {
                        ds.m_Damage += damage;
                        needNewEntry = false;
                    }
                }

                if (needNewEntry)
                    rights.Add(new DamageStore(m, damage));
            }

            if (rights.Count > 0)
            {
                rights[0].m_Damage =
                    (int)(rights[0].m_Damage *
                           1.25
                    ); //This would be the first valid person attacking it.  Gets a 25% bonus.  Per 1/19/07 Five on Friday

                if (rights.Count > 1)
                    rights.Sort(); //Sort by damage

                int topDamage = rights[0].m_Damage;
                int minDamage;

                if (hitsMax >= 3000)
                    minDamage = topDamage / 16;
                else if (hitsMax >= 1000)
                    minDamage = topDamage / 8;
                else if (hitsMax >= 200)
                    minDamage = topDamage / 4;
                else
                    minDamage = topDamage / 2;

                for (int i = 0; i < rights.Count; ++i)
                {
                    DamageStore ds = rights[i];

                    ds.m_HasRight = ds.m_Damage >= minDamage;
                }
            }

            return rights;
        }

        public virtual void OnKilledBy(Mobile mob)
        {
        }

        public override void OnDeath(Container c)
        {
            if (RiseCreatureTemplate != null)
                OnRiseSpawn(RiseCreatureTemplate, c);

            if (!Summoned && !NoKillAwards)
            {
                int totalFame = Fame / 100;
                int totalKarma = -Karma / 100;

                if (Map == Map.Felucca)
                {
                    totalFame += totalFame / 10 * 3;
                    totalKarma += totalKarma / 10 * 3;
                }

                List<DamageStore> list = GetLootingRights(DamageEntries, HitsMax);
                List<Mobile> titles = new List<Mobile>();
                List<int> fame = new List<int>();
                List<int> karma = new List<int>();

                for (int i = 0; i < list.Count; ++i)
                {
                    DamageStore ds = list[i];

                    if (!ds.m_HasRight)
                        continue;

                    Party party = Server.Engines.PartySystem.Party.Get(ds.m_Mobile);

                    if (party != null)
                    {
                        int divedFame = totalFame / party.Members.Count;
                        int divedKarma = totalKarma / party.Members.Count;

                        for (int j = 0; j < party.Members.Count; ++j)
                        {
                            PartyMemberInfo info = party.Members[j] as PartyMemberInfo;

                            if (info != null && info.Mobile != null)
                            {
                                int index = titles.IndexOf(info.Mobile);

                                if (index == -1)
                                {
                                    titles.Add(info.Mobile);
                                    fame.Add(divedFame);
                                    karma.Add(divedKarma);
                                }
                                else
                                {
                                    fame[index] += divedFame;
                                    karma[index] += divedKarma;
                                }
                            }
                        }
                    }
                    else
                    {
                        titles.Add(ds.m_Mobile);
                        fame.Add(totalFame);
                        karma.Add(totalKarma);
                    }

                    OnKilledBy(ds.m_Mobile);
                }

                for (int i = 0; i < titles.Count; ++i)
                {
                    Titles.AwardFame(titles[i], fame[i], true);
                    Titles.AwardKarma(titles[i], karma[i], true);
                }
            }

            base.OnDeath(c);

            if (DeleteCorpseOnDeath)
                c.Delete();
        }

        /* To save on cpu usage, RunUO creatures only reacquire creatures under the following circumstances:
         *  - 10 seconds have elapsed since the last time it tried
         *  - The creature was attacked
         *  - Some creatures, like dragons, will reacquire when they see someone move
         *
         * This functionality appears to be implemented on OSI as well
         */

        public long NextReacquireTime { get; set; }

        public virtual bool ReacquireOnMovement
        {
            get { return false; }
        }

        public override void OnDelete()
        {
            Mobile m = m_ControlMaster;

            SetControlMaster(null);
            SummonMaster = null;

            base.OnDelete();
        }

        public override bool CanBeHarmful(Mobile target, bool message, bool ignoreOurBlessedness)
        {
            if (target is BaseCreature && ((BaseCreature)target).IsInvulnerable || target is PlayerVendor ||
                target is TownCrier)
            {
                if (message)
                {
                    if (target.Title == null)
                        SendMessage("{0} cannot be harmed.", target.Name);
                    else
                        SendMessage("{0} {1} cannot be harmed.", target.Name, target.Title);
                }

                return false;
            }

            return base.CanBeHarmful(target, message, ignoreOurBlessedness);
        }

        public override bool CanBeRenamedBy(Mobile from)
        {
            bool ret = base.CanBeRenamedBy(from);

            if (Controlled && from == ControlMaster && !from.Region.IsPartOf<JailRegion>())
                ret = true;

            return ret;
        }

        public bool SetControlMaster(Mobile m)
        {
            if (m == null)
            {
                ControlMaster = null;
                Controlled = false;
                ControlTarget = null;
                ControlOrder = OrderType.None;
                Guild = null;

                Delta(MobileDelta.Noto);
            }
            else
            {
                var se = Spawner;
                if (se is { UnlinkOnTaming: true })
                {
                    Spawner.Remove(this);
                    Spawner = null;
                }

                if (m.Followers + ControlSlots > m.FollowersMax)
                {
                    m.SendFailureMessage(1049607); // You have too many followers to control that creature.
                    return false;
                }

                CurrentWayPoint = null; //so tamed animals don't try to go back

                Home = Point3D.Zero;

                ControlMaster = m;
                Controlled = true;
                ControlTarget = null;
                if (CheckControlChance(m))
                    ControlOrder = OrderType.Come;
                else
                    ControlOrder = OrderType.None;
                Guild = null;

                if (m_DeleteTimer != null)
                {
                    m_DeleteTimer.Stop();
                    m_DeleteTimer = null;
                }

                Delta(MobileDelta.Noto);
            }

            return true;
        }

        public override void OnRegionChange(Region old, Region @new)
        {
            base.OnRegionChange(old, @new);

            if (Controlled)
            {
                var se = Spawner;

                if (se != null && !se.UnlinkOnTaming && (@new == null || !@new.AcceptsSpawnsFrom(se.Region)))
                {
                    Spawner.Remove(this);
                    Spawner = null;
                }
            }
        }

        public static bool Summoning { get; set; }

        public static bool Summon(BaseCreature creature, Mobile caster, Point3D p, int sound, TimeSpan duration)
        {
            return Summon(creature, true, caster, p, sound, duration);
        }

        public static bool Summon(BaseCreature creature, bool controlled, Mobile caster, Point3D p, int sound,
            TimeSpan duration)
        {
            if (caster.Followers + creature.ControlSlots > caster.FollowersMax)
            {
                caster.SendFailureMessage(1049645); // You have too many followers to summon that creature.
                creature.Delete();
                return false;
            }

            Summoning = true;

            if (controlled)
                creature.SetControlMaster(caster);

            creature.RangeHome = 10;
            creature.Summoned = true;

            creature.SummonMaster = caster;

            Container pack = creature.Backpack;

            if (pack != null)
            {
                for (int i = pack.Items.Count - 1; i >= 0; --i)
                {
                    if (i >= pack.Items.Count)
                        continue;

                    pack.Items[i].Delete();
                }
            }

            new UnsummonTimer(caster, creature, duration).Start();
            creature.SummonEnd = DateTime.Now + duration;

            creature.MoveToWorld(p, caster.Map);

            Effects.PlaySound(p, creature.Map, sound);

            Summoning = false;

            return true;
        }

        public virtual async Task SpitWeb(Mobile target)
        {
            Say("The spider spits a web!");

            var toLocation = target.Location;

            await Timer.Pause(300);

            Effects.SendMovingEffect(target.Map, 0xEE4, Location, toLocation, 7, 0);

            await Timer.Pause(200);

            new SpiderWeb(toLocation, target.Map, TimeSpan.FromMinutes(2));

            if (target.Location == toLocation)
            {
                SpiderWeb.Stick(target);
            }
        }

        private static bool EnableRummaging = true;

        private const double ChanceToRummage = 0.5; // 50%

        private const double MinutesToNextRummageMin = 1.0;
        private const double MinutesToNextRummageMax = 4.0;

        private const double MinutesToNextChanceMin = 0.25;
        private const double MinutesToNextChanceMax = 0.75;

        private long m_NextRummageTime;

        private long m_NextSpitWebTime;

        public virtual bool CanSpitWebs
        {
            get { return HasWebs && !Summoned; }
        }

        public virtual bool CanBreath
        {
            get { return HasBreath && !Summoned; }
        }

        public virtual bool IsDispellable
        {
            get { return Summoned; }
        }

        #region Healing

        public virtual bool CanHeal
        {
            get { return false; }
        }

        public virtual bool CanHealOwner
        {
            get { return false; }
        }

        public virtual double HealScalar
        {
            get { return 1.0; }
        }

        public virtual int HealSound
        {
            get { return 0x57; }
        }

        public virtual int HealStartRange
        {
            get { return 2; }
        }

        public virtual int HealEndRange
        {
            get { return RangePerception; }
        }

        public virtual double HealTrigger
        {
            get { return 0.78; }
        }

        public virtual double HealDelay
        {
            get { return 6.5; }
        }

        public virtual double HealInterval
        {
            get { return 0.0; }
        }

        public virtual bool HealFully
        {
            get { return true; }
        }

        public virtual double HealOwnerTrigger
        {
            get { return 0.78; }
        }

        public virtual double HealOwnerDelay
        {
            get { return 6.5; }
        }

        public virtual double HealOwnerInterval
        {
            get { return 30.0; }
        }

        public virtual bool HealOwnerFully
        {
            get { return false; }
        }

        private long m_NextHealTime = Core.TickCount;
        private long m_NextHealOwnerTime = Core.TickCount;
        private Timer m_HealTimer = null;

        public bool IsHealing
        {
            get { return m_HealTimer != null; }
        }

        public virtual void HealStart(Mobile patient)
        {
            bool onSelf = patient == this;

            //DoBeneficial( patient );

            RevealingAction();

            if (!onSelf)
            {
                patient.RevealingAction();
                patient.SendLocalizedMessage(1008078, false, Name); //  : Attempting to heal you.
            }

            double seconds = (onSelf ? HealDelay : HealOwnerDelay) + (patient.Alive ? 0.0 : 5.0);

            m_HealTimer = Timer.DelayCall(TimeSpan.FromSeconds(seconds), () => Heal_Callback(patient));
        }

        private void Heal_Callback(Mobile mobile)
        {
            Heal(mobile);
        }

        public virtual void Heal(Mobile patient)
        {
            if (!Alive || Map == Map.Internal || !CanBeBeneficial(patient, true, true) || patient.Map != Map ||
                !InRange(patient, HealEndRange))
            {
                StopHeal();
                return;
            }

            bool onSelf = patient == this;

            if (!patient.Alive)
            {
            }
            else if (patient.Poisoned)
            {
                int poisonLevel = patient.Poison.Level;

                double healing = Skills.Healing.Value;
                double anatomy = Skills.Anatomy.Value;
                double chance = (healing - 30.0) / 50.0 - poisonLevel * 0.1;

                if (healing >= 60.0 && anatomy >= 60.0 && chance > Utility.RandomDouble())
                {
                    if (patient.CurePoison(this))
                    {
                        patient.SendLocalizedMessage(1010059); // You have been cured of all poisons.

                        CheckSkill(SkillName.Healing, 0.0, 60.0 + poisonLevel * 10.0); // TODO: Verify formula
                        CheckSkill(SkillName.Anatomy, 0.0, 100.0);
                    }
                }
            }
            else
            {
                double healing = Skills.Healing.Value;
                double anatomy = Skills.Anatomy.Value;
                double chance = (healing + 10.0) / 100.0;

                if (chance > Utility.RandomDouble())
                {
                    double min, max;

                    min = anatomy / 10.0 + healing / 6.0 + 4.0;
                    max = anatomy / 8.0 + healing / 3.0 + 4.0;

                    if (onSelf)
                        max += 10;

                    double toHeal = min + Utility.RandomDouble() * (max - min);

                    toHeal *= HealScalar;

                    patient.Heal((int)toHeal);

                    CheckSkill(SkillName.Healing, 0.0, 90.0);
                    CheckSkill(SkillName.Anatomy, 0.0, 100.0);
                }
            }

            HealEffect(patient);

            StopHeal();

            if (onSelf && HealFully && Hits >= HealTrigger * HitsMax && Hits < HitsMax || !onSelf && HealOwnerFully &&
                patient.Hits >= HealOwnerTrigger * patient.HitsMax && patient.Hits < patient.HitsMax)
                HealStart(patient);
        }

        public virtual void StopHeal()
        {
            m_HealTimer?.Stop();

            m_HealTimer = null;
        }

        public virtual void HealEffect(Mobile patient)
        {
            patient.PlaySound(HealSound);
        }

        #endregion

        #region Damaging Aura

        private long m_NextAura;

        public virtual bool HasAura
        {
            get { return false; }
        }

        public virtual TimeSpan AuraInterval
        {
            get { return TimeSpan.FromSeconds(5); }
        }

        public virtual int AuraRange
        {
            get { return 4; }
        }

        public virtual int AuraBaseDamage
        {
            get { return 5; }
        }

        public virtual void AuraDamage()
        {
            if (!Alive)
                return;

            List<Mobile> list = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(AuraRange))
            {
                if (m == this || !CanBeHarmful(m, false))
                    continue;

                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;

                    if (bc.Controlled || bc.Summoned || bc.Team != Team)
                        list.Add(m);
                }
                else if (m.Player)
                {
                    list.Add(m);
                }
            }

            foreach (Mobile m in list)
            {
                m.Damage(AuraBaseDamage, this);
                AuraEffect(m);
            }
        }

        public virtual void AuraEffect(Mobile m)
        {
        }

        #endregion

        private class TameEntry : ContextMenuEntry
        {
            private readonly BaseCreature m_Mobile;

            public TameEntry(Mobile from, BaseCreature creature) : base(6130, 12)
            {
                m_Mobile = creature;
            }

            public override void OnClick()
            {
                if (!Owner.From.CheckAlive())
                {
                    return;
                }

                Owner.From.TargetLocked = true;

                if (Owner.From.UseSkill(SkillName.AnimalTaming))
                {
                    Owner.From.Target.Invoke(Owner.From, m_Mobile);
                }

                Owner.From.TargetLocked = false;
            }
        }
        
        private class LoreEntry : ContextMenuEntry
        {
            private readonly BaseCreature m_Mobile;

            public LoreEntry(Mobile from, BaseCreature creature) : base(-1997993, 12)
            {
                m_Mobile = creature;
            }

            public override void OnClick()
            {
                if (!Owner.From.CheckAlive())
                {
                    return;
                }

                Owner.From.TargetLocked = true;

                if (Owner.From.UseSkill(SkillName.AnimalLore))
                {
                    Owner.From.Target.Invoke(Owner.From, m_Mobile);
                }

                Owner.From.TargetLocked = false;
            }
        }

        public virtual void OnThink()
        {
            if (EnableRummaging && CanRummageCorpses && !Summoned && !Controlled && Core.TickCount >= m_NextRummageTime)
            {
                double min, max;

                if (ChanceToRummage > Utility.RandomDouble() && Rummage())
                {
                    min = MinutesToNextRummageMin;
                    max = MinutesToNextRummageMax;
                }
                else
                {
                    min = MinutesToNextChanceMin;
                    max = MinutesToNextChanceMax;
                }

                double delay = min + Utility.RandomDouble() * (max - min);
                m_NextRummageTime = Core.TickCount + (int)TimeSpan.FromMinutes(delay).TotalMilliseconds;
            }

            if (CanBreath && Core.TickCount >= m_NextBreathTime) // tested: controlled dragons do breath fire, what about summoned skeletal dragons?
            {
                Mobile target = Combatant;

                if (target != null && target.Alive && CanBeHarmful(target) && target.Map == Map &&
                    target.InRange(this, BreathRange) && InLOS(target) && !BardPacified)
                {
                    if (Core.TickCount - m_NextBreathTime < (int)TimeSpan.FromSeconds(30).TotalMilliseconds && Utility.RandomBool())
                    {
                        BreathStart(target);
                    }

                    m_NextBreathTime = Core.TickCount +
                                       (int)TimeSpan.FromSeconds(
                                           BreathMinDelay + Utility.RandomDouble() * (BreathMaxDelay - BreathMinDelay)).TotalMilliseconds;
                }
            }

            if (CanSpitWebs && Core.TickCount >= m_NextSpitWebTime)
            {
                Mobile target = Combatant;

                if (target != null && target.Alive && CanBeHarmful(target) && target.Map == Map &&
                    InLOS(target) && !BardPacified && !target.Paralyzed)
                {
                    if (Core.TickCount - m_NextSpitWebTime < (int)TimeSpan.FromSeconds(30).TotalMilliseconds && Utility.RandomBool())
                    {
                        SpitWeb(target);
                    }

                    m_NextSpitWebTime = Core.TickCount +
                                         (int)TimeSpan.FromSeconds(
                                             SpitWebMinDelay + Utility.RandomDouble() * (SpitWebMaxDelay - SpitWebMinDelay)).TotalMilliseconds;
                }
            }

            if ((CanHeal || CanHealOwner) && Alive && !IsHealing && !BardPacified)
            {
                Mobile owner = ControlMaster;

                if (owner != null && CanHealOwner && Core.TickCount >= m_NextHealOwnerTime &&
                    CanBeBeneficial(owner, true, true) && owner.Map == Map && InRange(owner, HealStartRange) &&
                    InLOS(owner) && owner.Hits < HealOwnerTrigger * owner.HitsMax)
                {
                    HealStart(owner);

                    m_NextHealOwnerTime =
                        Core.TickCount + (int)TimeSpan.FromSeconds(HealOwnerInterval).TotalMilliseconds;
                }
                else if (CanHeal && Core.TickCount >= m_NextHealTime && CanBeBeneficial(this) &&
                         (Hits < HealTrigger * HitsMax || Poisoned))
                {
                    HealStart(this);

                    m_NextHealTime = Core.TickCount + (int)TimeSpan.FromSeconds(HealInterval).TotalMilliseconds;
                }
            }

            if (ReturnsToHome && IsSpawnerBound() && !InRange(Home, RangeHome))
            {
                if (Combatant == null && Warmode == false && Utility.RandomDouble() < .10) /* some throttling */
                {
                    m_FailedReturnHome = !Move(GetDirectionTo(Home.X, Home.Y)) ? m_FailedReturnHome + 1 : 0;

                    if (m_FailedReturnHome > 5)
                    {
                        SetLocation(Home, true);

                        m_FailedReturnHome = 0;
                    }
                }
            }
            else
            {
                m_FailedReturnHome = 0;
            }

            if (HasAura && Core.TickCount >= m_NextAura)
            {
                AuraDamage();
                m_NextAura = Core.TickCount + (int)AuraInterval.TotalMilliseconds;
            }
        }

        public virtual bool Rummage()
        {
            Corpse toRummage = null;

            foreach (Item item in GetItemsInRange(2))
            {
                if (item is Corpse && item.Items.Count > 0)
                {
                    toRummage = (Corpse)item;
                    break;
                }
            }

            if (toRummage == null)
                return false;

            Container pack = Backpack;

            if (pack == null)
                return false;

            List<Item> items = toRummage.Items;

            bool rejected;
            LRReason reason;

            for (int i = 0; i < items.Count; ++i)
            {
                Item item = items[Utility.Random(items.Count)];

                Lift(item, item.Amount, out rejected, out reason);

                if (!rejected && Drop(this, new Point3D(-1, -1, 0)))
                {
                    // *rummages through a corpse and takes an item*
                    PublicOverheadMessage(MessageType.Emote, 0x3B2, 1008086);
                    //TODO: Instancing of Rummaged stuff.
                    return true;
                }
            }

            return false;
        }
        
        public override int GetAttackSound()
        {
            if (Weapon is BaseWeapon weapon && weapon.HitSound != null && weapon.HitSound > 0)
            {
                return weapon.HitSound;
            }

            return base.GetAttackSound();
        }

        public void Pacify(Mobile master, DateTime endtime)
        {
            PublicOverheadMessage(MessageType.Emote, EmoteHue, false, "*looks calm*");
            BardPacified = true;
            BardEndTime = endtime;
        }

        public override Mobile GetDamageMaster(Mobile damagee)
        {
            if (BardProvoked && damagee == BardTarget)
                return BardMaster;
            else if (m_bControlled && m_ControlMaster != null)
                return m_ControlMaster;
            else if (m_bSummoned && m_SummonMaster != null)
                return m_SummonMaster;

            return base.GetDamageMaster(damagee);
        }

        public void Provoke(Mobile master, Mobile target, bool bSuccess)
        {
            BardProvoked = true;

            PublicOverheadMessage(MessageType.Emote, EmoteHue, false, "*looks furious*");

            if (bSuccess)
            {
                PlaySound(GetIdleSound());

                BardMaster = master;
                BardTarget = target;
                Combatant = target;
                BardEndTime = DateTime.Now + TimeSpan.FromSeconds(30.0);

                if (target is BaseCreature)
                {
                    BaseCreature t = (BaseCreature)target;

                    if (t.Unprovokable)
                        return;

                    t.BardProvoked = true;

                    t.BardMaster = master;
                    t.BardTarget = this;
                    t.Combatant = this;
                    t.BardEndTime = DateTime.Now + TimeSpan.FromSeconds(30.0);
                }
            }
            else
            {
                PlaySound(GetAngerSound());

                BardMaster = master;
                BardTarget = target;
            }
        }

        public bool FindMyName(string str, bool bWithAll)
        {
            int i, j;

            string name = Name;

            if (name == null || str.Length < name.Length)
                return false;

            string[] wordsString = str.Split(' ');
            string[] wordsName = name.Split(' ');

            for (j = 0; j < wordsName.Length; j++)
            {
                string wordName = wordsName[j];

                bool bFound = false;
                for (i = 0; i < wordsString.Length; i++)
                {
                    string word = wordsString[i];

                    if (InsensitiveStringHelpers.Equals(word, wordName))
                        bFound = true;

                    if (bWithAll && InsensitiveStringHelpers.Equals(word, "all"))
                        return true;
                }

                if (!bFound)
                    return false;
            }

            return true;
        }

        public static void TeleportPets(Mobile master, Point3D loc, Map map)
        {
            TeleportPets(master, loc, map, false);
        }

        public static void TeleportPets(Mobile master, Point3D loc, Map map, bool onlyBonded)
        {
            List<Mobile> move = new List<Mobile>();

            foreach (Mobile m in master.GetMobilesInRange(3))
            {
                if (m is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)m;

                    if (pet.Controlled && pet.ControlMaster == master)
                    {
                        if (!onlyBonded)
                        {
                            if (pet.ControlOrder == OrderType.Guard || pet.ControlOrder == OrderType.Follow ||
                                pet.ControlOrder == OrderType.Come)
                                move.Add(pet);
                        }
                    }
                }
            }

            foreach (Mobile m in move)
                m.MoveToWorld(loc, map);
        }

        public override bool CanBeDamaged()
        {
            if (IsInvulnerable)
                return false;

            return base.CanBeDamaged();
        }

        public virtual bool PlayerRangeSensitive
        {
            get { return CurrentWayPoint == null; }
        } //If they are following a waypoint, they'll continue to follow it even if players aren't around

        /* until we are sure about who should be getting deleted, move them instead */
        /* On OSI, they despawn */

        private bool m_ReturnQueued;

        private bool IsSpawnerBound()
        {
            if (Map != null && Map != Map.Internal)
            {
                if (FightMode != FightMode.None && RangeHome >= 0)
                {
                    if (!Controlled && !Summoned && Spawner is Spawner s)
                    {
                        if (s.Map == Map)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public virtual bool ReturnsToHome
        {
            get { return SeeksHome && Home != Point3D.Zero && !m_ReturnQueued && !Controlled && !Summoned; }
        }

        public override void OnSectorDeactivate()
        {
            if (!Deleted && ReturnsToHome && IsSpawnerBound() && !InRange(Home, RangeHome + 5))
            {
                Timer.DelayCall(TimeSpan.FromSeconds(Utility.Random(45) + 15), GoHome_Callback);

                m_ReturnQueued = true;
            }
            else if (PlayerRangeSensitive)
            {
                AIObject?.Deactivate();
            }

            base.OnSectorDeactivate();
        }

        public void GoHome_Callback()
        {
            if (m_ReturnQueued && IsSpawnerBound())
            {
                if (!Map.GetSector(X, Y).Active)
                {
                    SetLocation(Home, true);

                    if (!Map.GetSector(X, Y).Active)
                    {
                        AIObject?.Deactivate();
                    }
                }
            }

            m_ReturnQueued = false;
        }

        public override void OnSectorActivate()
        {
            if (PlayerRangeSensitive)
            {
                AIObject?.Activate();
            }

            base.OnSectorActivate();
        }

        // used for deleting untamed creatures [in houses]

        [CommandProperty(AccessLevel.GameMaster)]
        public bool RemoveIfUntamed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RemoveStep { get; set; }

        public bool AnimatedDead { get; set; }

        public bool SpellBound { get; set; }

        #region ShilCheckSkill

        public bool CheckSkill(SkillName skillName, int difficulty, int points)
        {
            if (skillName != SkillName.DetectHidden)
                return true;

            return PercentSkillCheck(this, skillName, points);
        }

        #endregion

        public virtual void CheckReflect(Mobile caster, ref bool reflect)
        {
        }

        #region BuffManager

        private BuffManager m_BuffManager;
        private string m_CorpseNameOverride;
        public BuffManager BuffManager => m_BuffManager ??= new BuffManager(this);

        #endregion
    }
}