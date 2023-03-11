using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public interface ISlayer
    {
        SlayerName OldSlayer { get; set; }
        SlayerName OldSlayer2 { get; set; }
    }

    public abstract class BaseWeapon : BaseEquippableItem, IWeapon, ICraftable, ISlayer, IDurability, IRepairable,
        IResource
    {
        /* Weapon internals work differently now (Mar 13 2003)
         *
         * The attributes defined below default to -1.
         * If the value is -1, the corresponding virtual 'Aos/Old' property is used.
         * If not, the attribute value itself is used. Here's the list:
         *  - MinDamage
         *  - MaxDamage
         *  - Speed
         *  - HitSound
         *  - MissSound
         *  - StrRequirement, DexRequirement, IntRequirement
         *  - WeaponType
         *  - WeaponAnimation
         *  - MaxRange
         */

        #region Var declarations

        // Instance values. These values are unique to each weapon.
        private int m_Hits;

        // private SkillMod m_SkillMod, m_MageMod;
        private MarkQuality m_Mark;
        private CraftResource m_Resource;

        // Overridable values. These values are provided to override the defaults which get defined in the individual weapon scripts.
        private int m_StrReq, m_DexReq, m_IntReq;
        private int m_MinDamage, m_MaxDamage;
        private int m_HitSound, m_MissSound;
        private float m_Speed;
        private int m_MaxRange;
        private SkillName m_Skill;
        private WeaponType m_Type;
        private WeaponAnimation m_Animation;

        #endregion

        #region Virtual Properties

        public virtual int DefaultHitSound => 0;
        public virtual int DefaultMissSound => 0;
        public virtual SkillName DefaultSkill => SkillName.Swords;
        public virtual WeaponType DefaultWeaponType => WeaponType.Slashing;
        public virtual WeaponAnimation DefaultAnimation => WeaponAnimation.Slash1H;
        public virtual int DefaultStrengthReq => 0;
        public virtual int DefaultDexterityReq => 0;
        public virtual int DefaultIntelligenceReq => 0;
        public virtual int DefaultMinDamage => 0;
        public virtual int DefaultMaxDamage => 0;
        public virtual int DefaultSpeed => 0;
        public virtual int DefaultMaxRange => 1;
        public bool CanFortify { get; } = true;

        public virtual int InitMinHits => 0;
        public virtual int InitMaxHits => 0;
        public virtual SkillName AccuracySkill => SkillName.Tactics;

        public virtual int VirtualDamageBonus => 0;

        #endregion

        #region Getters & Setters

        [CommandProperty(AccessLevel.GameMaster)]
        public MagicalWeaponType MagicalWeaponType
        {
            get => Enchantments.Get((MagicalWeapon e) => e.Value);
            set => Enchantments.Set((MagicalWeapon e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HarvestBonus
        {
            get => Enchantments.Get((HarvestBonus e) => e.Value);
            set => Enchantments.Set((HarvestBonus e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CreatureType Slayer
        {
            get => Enchantments.Get((SlayerHit e) => e.Type);
            set => Enchantments.Set((SlayerHit e) => e.Type = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SpellEntry SpellHitEntry
        {
            get => Enchantments.Get((SpellHit e) => e.SpellEntry);
            set => Enchantments.Set((SpellHit e) => e.SpellEntry = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double SpellHitChance
        {
            get => Enchantments.Get((SpellHit e) => e.Chance);
            set => Enchantments.Set((SpellHit e) => e.Chance = value);
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public EffectHitType EffectHitType
        {
            get => Enchantments.Get((EffectHit e) => e.EffectHitType);
            set => Enchantments.Set((EffectHit e) => e.EffectHitType = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double EffectHitTypeChance
        {
            get => Enchantments.Get((EffectHit e) => e.Chance);
            set => Enchantments.Set((EffectHit e) => e.Chance = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitPoints
        {
            get => m_Hits;
            set
            {
                if (m_Hits == value)
                    return;

                if (value > MaxHitPoints)
                    value = MaxHitPoints;

                m_Hits = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxHitPoints { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PoisonCharges
        {
            get => Enchantments.Get((PoisonHit e) => e.Charges);
            set => Enchantments.Set((PoisonHit e) => e.Charges = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double PoisonChance
        {
            get => Enchantments.Get((PoisonHit e) => e.Chance);
            set => Enchantments.Set((PoisonHit e) => e.Chance = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get => Enchantments.Get((PoisonHit e) => e.Poison);
            set => Enchantments.Set((PoisonHit e) => e.Level = (PoisonLevel) value.Level);
        }
        public bool PermaPoison { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => m_Mark;
            set
            {
                UnscaleDurability();
                m_Mark = value;
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double Quality
        {
            get => Enchantments.Get((ItemQuality e) => e.Value);
            set => Enchantments.Set((ItemQuality e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        // [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer { get; set; }

        // [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer2 { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get => m_Resource;
            set
            {
                UnscaleDurability();
                m_Resource = value;
                Hue = CraftResources.GetHue(m_Resource);
                ScaleDurability();
            }
        }

        private static readonly Dictionary<WeaponDamageLevel, int> DamageLevelToBonus = new()
        {
            [WeaponDamageLevel.Regular] = 0,
            [WeaponDamageLevel.Ruin] = 5,
            [WeaponDamageLevel.Might] = 10,
            [WeaponDamageLevel.Force] = 15,
            [WeaponDamageLevel.Power] = 20,
            [WeaponDamageLevel.Vanquishing] = 25,
            [WeaponDamageLevel.Devastation] = 30
        };

        public static int GetBonusForDamageLevel(WeaponDamageLevel level)
        {
            return DamageLevelToBonus[level];
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponDamageLevel DamageLevel
        {
            get => Enchantments.Get((WeaponDamageBonus e) => e.Value);
            set => Enchantments.Set((WeaponDamageBonus e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponDurabilityLevel DurabilityLevel
        {
            get => Enchantments.Get((DurabilityBonus e) => (WeaponDurabilityLevel) e.Value);
            set
            {
                UnscaleDurability();
                Enchantments.Set((DurabilityBonus e) => e.Value = (int) value);
                ScaleDurability();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxRange
        {
            get => m_MaxRange == -1 ? DefaultMaxRange : m_MaxRange;
            set => m_MaxRange = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponAnimation Animation
        {
            get => (int) m_Animation == -1 ? DefaultAnimation : m_Animation;
            set => m_Animation = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponType Type
        {
            get => m_Type == (WeaponType) (-1) ? DefaultWeaponType : m_Type;
            set => m_Type = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName Skill
        {
            get => m_Skill == (SkillName) (-1) ? DefaultSkill : m_Skill;
            set => m_Skill = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HitSound
        {
            get => m_HitSound == -1 ? DefaultHitSound : m_HitSound;
            set => m_HitSound = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MissSound
        {
            get => m_MissSound == -1 ? DefaultMissSound : m_MissSound;
            set => m_MissSound = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MinDamage
        {
            get => m_MinDamage == -1 ? DefaultMinDamage : m_MinDamage;
            set => m_MinDamage = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxDamage
        {
            get => m_MaxDamage == -1 ? DefaultMaxDamage : m_MaxDamage;
            set => m_MaxDamage = value;
        }

        public virtual bool UseSkillMod => true;

        [CommandProperty(AccessLevel.GameMaster)]
        public float Speed
        {
            get
            {
                if (m_Speed > 0)
                    return m_Speed;

                return DefaultSpeed;
            }
            set => m_Speed = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StrRequirement
        {
            get => m_StrReq == -1 ? DefaultStrengthReq : m_StrReq;
            set => m_StrReq = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DexRequirement
        {
            get => m_DexReq == -1 ? DefaultDexterityReq : m_DexReq;
            set => m_DexReq = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int IntRequirement
        {
            get => m_IntReq == -1 ? DefaultIntelligenceReq : m_IntReq;
            set => m_IntReq = value;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public WeaponAccuracyLevel AccuracyLevel
        {
            get => Enchantments.Get((WeaponAccuracyBonus e) => e.Value);
            set
            {
                if (AccuracyLevel != value)
                {
                    Enchantments.Set((WeaponAccuracyBonus e) => e.Value = value);

                    if (UseSkillMod)
                        Enchantments.Set((SecondSkillBonus e) =>
                        {
                            e.Skill = Skill;
                            return e.Value = (int) value * 5;
                        });
                }
            }
        }

        #endregion

        public virtual void UnscaleDurability()
        {
            var scale = 100 + GetDurabilityBonus();

            m_Hits = (m_Hits * 100 + (scale - 1)) / scale;
            MaxHitPoints = (MaxHitPoints * 100 + (scale - 1)) / scale;
        }

        public virtual void ScaleDurability()
        {
            var scale = 100 + GetDurabilityBonus();

            m_Hits = (m_Hits * scale + 99) / 100;
            MaxHitPoints = (MaxHitPoints * scale + 99) / 100;
        }

        public int GetDurabilityBonus()
        {
            var bonus = DurabilityLevel switch
            {
                WeaponDurabilityLevel.Durable => 20,
                WeaponDurabilityLevel.Substantial => 50,
                WeaponDurabilityLevel.Massive => 70,
                WeaponDurabilityLevel.Fortified => 100,
                WeaponDurabilityLevel.Indestructible => 120,
                _ => 0
            };

            var exceptionalBonus = Mark == MarkQuality.Exceptional ? 20 : 0;
            return bonus + exceptionalBonus;
        }

        private class ResetEquipTimer : Timer
        {
            private readonly Mobile m_Mobile;

            public ResetEquipTimer(Mobile m, TimeSpan duration) : base(duration)
            {
                m_Mobile = m;
            }

            protected override void OnTick()
            {
                m_Mobile.EndAction(typeof(BaseWeapon));
            }
        }

        public override bool CheckConflictingLayer(Mobile m, Item item, Layer layer)
        {
            if (base.CheckConflictingLayer(m, item, layer))
                return true;

            switch (Layer)
            {
                case Layer.TwoHanded when layer == Layer.OneHanded:
                    m.SendLocalizedMessage(500214); // You already have something in both hands.
                    return true;
                case Layer.OneHanded
                    when layer == Layer.TwoHanded && !(item is BaseShield) && !(item is BaseEquipableLight):
                    m.SendLocalizedMessage(500215); // You can only wield one weapon at a time.
                    return true;
                default:
                    return false;
            }
        }

        public virtual Race RequiredRace =>
            null; //On OSI, there are no weapons with race requirements, this is for custom stuff

        public override bool CanEquip(Mobile from)
        {
            if (RequiredRace != null && from.Race != RequiredRace)
            {
                if (RequiredRace == Race.Elf)
                    from.SendLocalizedMessage(1072203); // Only Elves may use this.
                else
                    from.SendMessage("Only {0} may use this.", RequiredRace.PluralName);

                return false;
            }

            if (from.Dex < DexRequirement)
            {
                from.SendMessage("You are not nimble enough to equip that.");
                return false;
            }

            if (from.Str < StrRequirement)
            {
                from.SendLocalizedMessage(500213); // You are not strong enough to equip that.
                return false;
            }

            if (from.Int < IntRequirement)
            {
                from.SendMessage("You are not smart enough to equip that.");
                return false;
            }

            return from.CanBeginAction<BaseWeapon>() && base.CanEquip(from);
        }

        public override bool OnEquip(Mobile from)
        {
            from.NextCombatTime = Core.TickCount + (int) GetDelay(from).TotalMilliseconds;

            return true;
        }

        public override void OnAdded(IEntity parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile from)
            {
                from.CheckStatTimers();
                from.Delta(MobileDelta.WeaponDamage);
            }
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is Mobile m)
            {
                if (m.Weapon is BaseWeapon weapon)
                    m.NextCombatTime = Core.TickCount + (int) weapon.GetDelay(m).TotalMilliseconds;

                m.CheckStatTimers();
                m.Delta(MobileDelta.WeaponDamage);
            }

            base.OnRemoved(parent);
        }

        public SkillName GetUsedSkill(Mobile m)
        {
            var sk = Skill;
            
            if (sk != SkillName.Wrestling && !m.Player && !m.Body.IsHuman &&
                m.Skills[SkillName.Wrestling].Value > m.Skills[sk].Value)
                sk = SkillName.Wrestling;

            return sk;
        }

        public double GetAttackSkillValue(Mobile attacker, Mobile defender)
        {
            return attacker.Skills[GetUsedSkill(attacker)].Value;
        }

        public virtual double GetDefendSkillValue(Mobile attacker, Mobile defender)
        {
            return defender.Skills[GetUsedSkill(defender)].Value;
        }

        public bool CheckHit(Mobile attacker, Mobile defender)
        {
            var atkWeapon = attacker.Weapon as BaseWeapon;
            var defWeapon = defender.Weapon as BaseWeapon;

            var atkValue = atkWeapon.GetAttackSkillValue(attacker, defender) + 50.0;
            var defValue = defWeapon.GetDefendSkillValue(attacker, defender) + 50.0;

            var hitChance = atkValue / (defValue * 2.0);

            return Utility.RandomDouble() < hitChance;
        }

        public TimeSpan GetDelay(Mobile m)
        {
            var speed = Speed;

            if (speed <= 0)
                return TimeSpan.FromSeconds(10);

            const double minDelay = 0.5;

            var delayInSeconds = 15000.0 / ((m.Dex + 100) * speed);

            m.FireHook(h => h.OnGetSwingDelay(ref delayInSeconds, m));

            return TimeSpan.FromSeconds(Math.Clamp(delayInSeconds, minDelay, 7.0));
        }

        public virtual void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }

        public virtual TimeSpan OnSwing(Mobile attacker, Mobile defender)
        {
            return OnSwing(attacker, defender, 0);
        }

        public virtual TimeSpan OnSwing(Mobile attacker, Mobile defender, double bonus)
        {
            if (attacker.HarmfulCheck(defender))
            {
                attacker.DisruptiveAction();

                attacker.NetState?.SendSwing(attacker.Serial, defender.Serial);

                if (CheckHit(attacker, defender))
                    OnHit(attacker, defender);
                else
                    OnMiss(attacker, defender);
            }

            return GetDelay(attacker);
        }

        #region Sounds

        public virtual int GetHitAttackSound(Mobile attacker, Mobile defender)
        {
            var sound = attacker.GetAttackSound();

            if (sound == -1)
                sound = HitSound;

            return sound;
        }

        public virtual int GetHitDefendSound(Mobile attacker, Mobile defender)
        {
            return defender.GetHurtSound();
        }

        public virtual int GetMissAttackSound(Mobile attacker, Mobile defender)
        {
            return MissSound;
        }

        public virtual int GetMissDefendSound(Mobile attacker, Mobile defender)
        {
            return -1;
        }

        #endregion

        public BaseArmor? GetDefenderArmor(Mobile defender)
        {
            if (defender == null)
                return null;

            var chance = Utility.RandomDouble();

            var armorItem = chance switch
            {
                < 0.07 => defender.NeckArmor,
                < 0.14 => defender.HandArmor,
                < 0.28 => defender.ArmsArmor,
                < 0.43 => defender.HeadArmor,
                < 0.65 => defender.LegsArmor,
                _ => defender.ChestArmor
            };

            return armorItem as BaseArmor;
        }

        public void CheckApplyPoison(Mobile attacker, Mobile defender)
        {
            if (Poison != null && (PoisonCharges > 0 || PermaPoison))
            {
                if (!PermaPoison)
                    PoisonCharges -= 1;

                defender.ApplyPoison(attacker, Poison);

                if (PoisonCharges == 0 && !PermaPoison)
                    Poison = null;
            }
        }

        public virtual void OnHit(Mobile attacker, Mobile defender)
        {
            PlaySwingAnimation(attacker);
            PlayHurtAnimation(defender);

            attacker.PlaySound(GetHitAttackSound(attacker, defender));
            defender.PlaySound(GetHitDefendSound(attacker, defender));

            var attackerWeapon = attacker.Weapon as BaseWeapon;
            var defenderArmor = GetDefenderArmor(defender);

            var damage = ComputeDamage(attacker, defender, attackerWeapon, defenderArmor);

            if (attacker is BaseCreature bc && bc.GetWeaponAbility() is { } ab &&
                bc.WeaponAbilityChance >= Utility.RandomDouble())
                ab.OnHit(attacker, defender, ref damage);

            if (attackerWeapon?.EffectHitType != EffectHitType.Piercing && damage < 2)
                damage = 2;

            CheckApplyPoison(attacker, defender);

            AddBlood(attacker, defender, damage);

            defender.Damage(damage, attacker);

            // Award skill points
            attacker.AwardSkillPoints(attackerWeapon!.GetUsedSkill(attacker), 20);
            
            if(attackerWeapon.AccuracySkill == SkillName.Tactics)
                attacker.AwardSkillPoints(SkillName.Tactics, 20);

            defenderArmor?.OnHit(attackerWeapon, damage);

            if (Utility.Random(100) < 2)
                if (MaxHitPoints > 0)
                    HitPoints -= 1;

            if (Quality > 0 && HitPoints < 1)
                Delete();

            if (attacker is BaseCreature attackingCreature)
                attackingCreature.OnGaveMeleeAttack(defender);

            if (defender is BaseCreature defendingCreature)
                defendingCreature.OnGotMeleeAttack(attacker);
        }

        public void AddBlood(Mobile attacker, Mobile defender, int damage)
        {
            if (damage > 0)
            {
                new Blood().MoveToWorld(defender.Location, defender.Map);

                var extraBlood = Utility.RandomMinMax(0, 1);

                for (var i = 0; i < extraBlood; i++)
                    new Blood().MoveToWorld(new Point3D(
                        defender.X + Utility.RandomMinMax(-1, 1),
                        defender.Y + Utility.RandomMinMax(-1, 1),
                        defender.Z), defender.Map);
            }
        }

        public virtual void OnMiss(Mobile attacker, Mobile defender)
        {
            PlaySwingAnimation(attacker);
            attacker.PlaySound(GetMissAttackSound(attacker, defender));
            defender.PlaySound(GetMissDefendSound(attacker, defender));
        }

        public void GetBaseDamageRange(Mobile attacker, out int min, out int max)
        {
            if (attacker is BaseCreature c)
            {
                if (c.DamageMin >= 0)
                {
                    min = c.DamageMin;
                    max = c.DamageMax;
                    return;
                }

                if (this is Fists && !c.Body.IsHuman)
                {
                    min = c.Str / 28;
                    max = c.Str / 28;
                    return;
                }
            }

            min = MinDamage;
            max = MaxDamage;
        }

        public int GetBaseDamage(Mobile attacker)
        {
            int min, max;

            GetBaseDamageRange(attacker, out min, out max);

            var bonusDamage = GetBonusForDamageLevel(DamageLevel);
            var damage = Utility.RandomMinMax(min, max) + bonusDamage;

            return damage;
        }

        public virtual void GetStatusDamage(Mobile from, out int min, out int max)
        {
            int baseMin, baseMax;

            GetBaseDamageRange(from, out baseMin, out baseMax);

            var attackerWeapon = from.Weapon as BaseWeapon;
            var bonusDamage = GetBonusForDamageLevel(DamageLevel);

            min = Math.Max((int) ScaleDamage(from, null, baseMin, attackerWeapon), 1) +
                  bonusDamage;
            max = Math.Max((int) ScaleDamage(from, null, baseMax, attackerWeapon), 1) +
                  bonusDamage;
        }

        public double ShieldAbsorbDamage(Mobile attacker, Mobile defender, double damage)
        {
            if (defender.FindItemOnLayer(Layer.TwoHanded) is BaseShield shield)
            {
                damage = shield.OnHit(this, damage);

                // ReSharper disable once AccessToModifiedClosure
                defender.FireHook(h => h.OnShieldHit(attacker, defender, this, shield, ref damage));
            }

            return damage;
        }

        public double ModByDist(Mobile attacker, Mobile? defender, double damage, BaseWeapon weapon)
        {
            if (weapon.GetUsedSkill(attacker) == SkillName.Archery)
            {
                damage *= (attacker.Dex + 60) * 0.01 /
                          ((attacker.Skills[SkillName.Tactics].Value + 50.0 + attacker.Str / 5.0) * 0.01);

                var dist = defender != null ? attacker.GetDistanceToSqrt(defender) : 2;

                if (dist <= 1 || dist > 10)
                    damage *= 0.25;

                if (attacker.ClassContainsSkill(SkillName.Archery))
                    damage *= attacker.GetClassModifier(SkillName.Archery);
            }
            else if (attacker.ClassContainsSkill(SkillName.Magery))
            {
                damage /= attacker.GetClassModifier(SkillName.Magery);
            }
            else if (attacker.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
            {
                if (defender is BaseCreature)
                {
                    damage *= attacker.GetClassModifier(SkillName.Swords);
                }
                else
                {
                    var level = attacker is BaseCreature ? 0 : attacker.GetClassLevel(SkillName.Swords);

                    if (level > 0)
                    {
                        level -= 2;

                        if (level >= 1)
                            damage *= ZuluClass.GetBonusByLevel(level);
                    }
                }
            }

            if (defender.ClassContainsSkill(SkillName.Magery))
                damage *= defender.GetClassModifier(SkillName.Magery);

            return damage;
        }

        public double ModByProt(Mobile attacker, Mobile defender, double damage)
        {
            defender.FireHook(h => h.OnAbsorbMeleeDamage(attacker, defender, this, ref damage));

            return damage;
        }

        public double ModByClass(Mobile attacker, Mobile defender, double damage)
        {
            if (attacker is BaseCreature &&
                defender.ClassContainsSkill(SkillName.Swords, SkillName.Macing, SkillName.Anatomy))
                damage /= defender.GetClassModifier(SkillName.Swords);

            return damage;
        }

        public double ScaleDamage(Mobile attacker, Mobile? defender, double baseDamage, BaseWeapon weapon,
            BaseArmor armor = null, bool piercing = false)
        {
            if (weapon.GetUsedSkill(attacker) != SkillName.Archery)
            {
                var damageMultiplier = attacker.Skills[SkillName.Tactics].Value + 50.0;
                damageMultiplier += attacker.Str * 0.2;
                damageMultiplier *= 0.01;

                baseDamage *= damageMultiplier;
            }
            
            var anatomyVal = attacker.Skills[SkillName.Anatomy].Value;
            double rawDamage;
            
            if (attacker is PlayerMobile && defender is BaseCreature)
                baseDamage *= 1.5;

            baseDamage = ModByDist(attacker, defender, baseDamage, weapon);
            baseDamage *= 1 + anatomyVal * 0.002;

            if (!piercing && defender != null)
            {
                var armorRating = armor?.ArmorRating ?? 0 + defender.VirtualArmor + defender.VirtualArmorMod;

                rawDamage = ShieldAbsorbDamage(attacker, defender, baseDamage);
                rawDamage -= armorRating * (Utility.Random(51) + 50) * 0.01;
            }
            else
            {
                rawDamage = baseDamage;
            }
            
            rawDamage *= 0.5;

            if (defender != null)
            {
                rawDamage = ModByProt(attacker, defender, rawDamage);
                rawDamage = ModByClass(attacker, defender, rawDamage);
            }

            rawDamage = Math.Max(rawDamage, 0);

            return rawDamage;
        }

        public int ComputeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor = null)
        {
            var baseDamage = GetBaseDamage(attacker);
            
            attacker.FireHook(h => h.OnMeleeHit(attacker, defender, this, ref baseDamage));

            var damage = (int) ScaleDamage(attacker, defender, baseDamage, weapon, armor);

            return damage;
        }

        public void PlayHurtAnimation(Mobile from)
        {
            int action;
            int frames;

            switch (from.Body.Type)
            {
                case BodyType.Sea:
                case BodyType.Animal:
                {
                    action = 7;
                    frames = 5;
                    break;
                }
                case BodyType.Monster:
                {
                    action = 10;
                    frames = 4;
                    break;
                }
                case BodyType.Human:
                {
                    action = 20;
                    frames = 5;
                    break;
                }
                default: return;
            }

            if (from.Mounted)
                return;

            from.Animate(action, frames, 1, true, false, 0);
        }

        public virtual void PlaySwingAnimation(Mobile from)
        {
            int action;

            switch (from.Body.Type)
            {
                case BodyType.Sea:
                case BodyType.Animal:
                {
                    action = Utility.Random(5, 2);
                    break;
                }
                case BodyType.Monster:
                {
                    switch (Animation)
                    {
                        default:
                            action = Utility.Random(4, 3);
                            break;
                        case WeaponAnimation.ShootBow: return; // 7
                        case WeaponAnimation.ShootXBow: return; // 8
                    }

                    break;
                }
                case BodyType.Human:
                {
                    if (!from.Mounted)
                        action = (int) Animation;
                    else
                        action = Animation switch
                        {
                            WeaponAnimation.Wrestle => 26,
                            WeaponAnimation.Bash1H => 26,
                            WeaponAnimation.Pierce1H => 26,
                            WeaponAnimation.Slash1H => 26,
                            WeaponAnimation.Bash2H => 29,
                            WeaponAnimation.Pierce2H => 29,
                            WeaponAnimation.Slash2H => 29,
                            WeaponAnimation.ShootBow => 27,
                            WeaponAnimation.ShootXBow => 28,
                            _ => 26
                        };

                    break;
                }
                default: return;
            }

            from.Animate(action, 7, 1, true, false, 0);
        }

        #region Serialization/Deserialization

        private static bool SetSaveFlag(ref SaveFlag flags, SaveFlag toSet, bool setIf)
        {
            if (setIf)
                flags |= toSet;

            return setIf;
        }

        private static bool GetSaveFlag(SaveFlag flags, SaveFlag toGet)
        {
            return (flags & toGet) != 0;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(11); // version

            var flags = SaveFlag.None;

            SetSaveFlag(ref flags, SaveFlag.ICraftable, true);
            SetSaveFlag(ref flags, SaveFlag.NewMagicalProperties, true);

            if (!GetSaveFlag(flags, SaveFlag.ICraftable))
            {
                SetSaveFlag(ref flags, SaveFlag.PlayerConstructed, PlayerConstructed);
                SetSaveFlag(ref flags, SaveFlag.Crafter, Crafter != null);
            }

            // Legacy property saving
            if (!GetSaveFlag(flags, SaveFlag.NewMagicalProperties))
            {
                SetSaveFlag(ref flags, SaveFlag.DamageLevel, DamageLevel != WeaponDamageLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.AccuracyLevel, AccuracyLevel != WeaponAccuracyLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.DurabilityLevel, DurabilityLevel != WeaponDurabilityLevel.Regular);
                SetSaveFlag(ref flags, SaveFlag.Mark, Mark != MarkQuality.Regular);
                SetSaveFlag(ref flags, SaveFlag.Identified, Identified);
                SetSaveFlag(ref flags, SaveFlag.Poison, Poison != null);
                SetSaveFlag(ref flags, SaveFlag.PoisonCharges, PoisonCharges != 0);
            }

            SetSaveFlag(ref flags, SaveFlag.Hits, m_Hits != 0);
            SetSaveFlag(ref flags, SaveFlag.MaxHits, MaxHitPoints != 0);
            SetSaveFlag(ref flags, SaveFlag.Slayer, OldSlayer != SlayerName.None);
            SetSaveFlag(ref flags, SaveFlag.StrReq, m_StrReq != -1);
            SetSaveFlag(ref flags, SaveFlag.DexReq, m_DexReq != -1);
            SetSaveFlag(ref flags, SaveFlag.IntReq, m_IntReq != -1);
            SetSaveFlag(ref flags, SaveFlag.MinDamage, m_MinDamage != -1);
            SetSaveFlag(ref flags, SaveFlag.MaxDamage, m_MaxDamage != -1);
            SetSaveFlag(ref flags, SaveFlag.HitSound, m_HitSound != -1);
            SetSaveFlag(ref flags, SaveFlag.MissSound, m_MissSound != -1);
            SetSaveFlag(ref flags, SaveFlag.Speed, m_Speed > -1);
            SetSaveFlag(ref flags, SaveFlag.MaxRange, m_MaxRange != -1);
            SetSaveFlag(ref flags, SaveFlag.Skill, m_Skill != (SkillName) (-1));
            SetSaveFlag(ref flags, SaveFlag.Type, m_Type != (WeaponType) (-1));
            SetSaveFlag(ref flags, SaveFlag.Animation, m_Animation != (WeaponAnimation) (-1));
            SetSaveFlag(ref flags, SaveFlag.Resource, m_Resource != CraftResource.Iron);
            SetSaveFlag(ref flags, SaveFlag.Slayer2, OldSlayer2 != SlayerName.None);

            writer.Write((int) flags);

            if (GetSaveFlag(flags, SaveFlag.ICraftable))
                ICraftable.Serialize(writer, this);

            if (GetSaveFlag(flags, SaveFlag.DamageLevel))
                writer.Write((int) DamageLevel);

            if (GetSaveFlag(flags, SaveFlag.AccuracyLevel))
                writer.Write((int) AccuracyLevel);

            if (GetSaveFlag(flags, SaveFlag.DurabilityLevel))
                writer.Write((int) DurabilityLevel);

            if (GetSaveFlag(flags, SaveFlag.Mark))
                writer.Write((int) Mark);

            if (GetSaveFlag(flags, SaveFlag.Hits))
                writer.Write(m_Hits);

            if (GetSaveFlag(flags, SaveFlag.MaxHits))
                writer.Write(MaxHitPoints);

            if (GetSaveFlag(flags, SaveFlag.Slayer))
                writer.Write((int) OldSlayer);

            if (GetSaveFlag(flags, SaveFlag.Poison))
                writer.Write(Poison);

            if (GetSaveFlag(flags, SaveFlag.PoisonCharges))
                writer.Write(PoisonCharges);

            if (GetSaveFlag(flags, SaveFlag.Crafter))
                writer.Write(Crafter);

            if (GetSaveFlag(flags, SaveFlag.Identified))
                writer.Write(Identified);

            if (GetSaveFlag(flags, SaveFlag.StrReq))
                writer.Write(m_StrReq);

            if (GetSaveFlag(flags, SaveFlag.DexReq))
                writer.Write(m_DexReq);

            if (GetSaveFlag(flags, SaveFlag.IntReq))
                writer.Write(m_IntReq);

            if (GetSaveFlag(flags, SaveFlag.MinDamage))
                writer.Write(m_MinDamage);

            if (GetSaveFlag(flags, SaveFlag.MaxDamage))
                writer.Write(m_MaxDamage);

            if (GetSaveFlag(flags, SaveFlag.HitSound))
                writer.Write(m_HitSound);

            if (GetSaveFlag(flags, SaveFlag.MissSound))
                writer.Write(m_MissSound);

            if (GetSaveFlag(flags, SaveFlag.Speed))
                writer.Write(m_Speed);

            if (GetSaveFlag(flags, SaveFlag.MaxRange))
                writer.Write(m_MaxRange);

            if (GetSaveFlag(flags, SaveFlag.Skill))
                writer.Write((int) m_Skill);

            if (GetSaveFlag(flags, SaveFlag.Type))
                writer.Write((int) m_Type);

            if (GetSaveFlag(flags, SaveFlag.Animation))
                writer.Write((int) m_Animation);

            if (GetSaveFlag(flags, SaveFlag.Resource))
                writer.Write((int) m_Resource);

            if (GetSaveFlag(flags, SaveFlag.PlayerConstructed))
                writer.Write(PlayerConstructed);

            if (GetSaveFlag(flags, SaveFlag.Slayer2))
                writer.Write((int) OldSlayer2);
        }

        [Flags]
        private enum SaveFlag
        {
            None = 0x00000000,
            DamageLevel = 0x00000001,
            AccuracyLevel = 0x00000002,
            DurabilityLevel = 0x00000004,
            Mark = 0x00000008,
            Hits = 0x00000010,
            MaxHits = 0x00000020,
            Slayer = 0x00000040,
            Poison = 0x00000080,
            PoisonCharges = 0x00000100,
            Crafter = 0x00000200,
            Identified = 0x00000400,
            StrReq = 0x00000800,
            DexReq = 0x00001000,
            IntReq = 0x00002000,
            MinDamage = 0x00004000,
            MaxDamage = 0x00008000,
            HitSound = 0x00010000,
            MissSound = 0x00020000,
            Speed = 0x00040000,
            MaxRange = 0x00080000,
            Skill = 0x00100000,
            Type = 0x00200000,
            Animation = 0x00400000,
            Resource = 0x00800000,
            ICraftable = 0x01000000,
            xWeaponAttributes = 0x02000000,
            PlayerConstructed = 0x04000000,
            SkillBonuses = 0x08000000,
            Slayer2 = 0x10000000,
            ElementalDamages = 0x20000000,
            NewMagicalProperties = 0x40000000
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            var flags = (SaveFlag) reader.ReadInt();

            switch (version)
            {
                case 11:
                    if (GetSaveFlag(flags, SaveFlag.ICraftable))
                        ICraftable.Deserialize(reader, this);
                    goto case 10;
                case 10:
                case 9:
                case 8:
                case 7:
                case 6:
                case 5:
                {
                    if (GetSaveFlag(flags, SaveFlag.DamageLevel))
                    {
                        DamageLevel = (WeaponDamageLevel) reader.ReadInt();

                        if (DamageLevel > WeaponDamageLevel.Vanquishing)
                            DamageLevel = WeaponDamageLevel.Ruin;
                    }

                    if (GetSaveFlag(flags, SaveFlag.AccuracyLevel))
                    {
                        AccuracyLevel = (WeaponAccuracyLevel) reader.ReadInt();

                        if (AccuracyLevel > WeaponAccuracyLevel.Supremely)
                            AccuracyLevel = WeaponAccuracyLevel.Accurate;
                    }

                    if (GetSaveFlag(flags, SaveFlag.DurabilityLevel))
                    {
                        DurabilityLevel = (WeaponDurabilityLevel) reader.ReadInt();

                        if (DurabilityLevel > WeaponDurabilityLevel.Indestructible)
                            DurabilityLevel = WeaponDurabilityLevel.Durable;
                    }

                    if (GetSaveFlag(flags, SaveFlag.Mark))
                        Mark = (MarkQuality) reader.ReadInt();
                    else
                        Mark = MarkQuality.Regular;

                    if (GetSaveFlag(flags, SaveFlag.Hits))
                        m_Hits = reader.ReadInt();

                    if (GetSaveFlag(flags, SaveFlag.MaxHits))
                        MaxHitPoints = reader.ReadInt();

                    if (GetSaveFlag(flags, SaveFlag.Slayer))
                        OldSlayer = (SlayerName) reader.ReadInt();

                    if (GetSaveFlag(flags, SaveFlag.Poison))
                        Poison = reader.ReadPoison();

                    if (GetSaveFlag(flags, SaveFlag.PoisonCharges))
                        PoisonCharges = reader.ReadInt();

                    if (GetSaveFlag(flags, SaveFlag.Crafter))
                        Crafter = reader.ReadEntity<Mobile>();

                    if (GetSaveFlag(flags, SaveFlag.Identified))
                        Identified = version >= 6 || reader.ReadBool();

                    if (GetSaveFlag(flags, SaveFlag.StrReq))
                        m_StrReq = reader.ReadInt();
                    else
                        m_StrReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.DexReq))
                        m_DexReq = reader.ReadInt();
                    else
                        m_DexReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.IntReq))
                        m_IntReq = reader.ReadInt();
                    else
                        m_IntReq = -1;

                    if (GetSaveFlag(flags, SaveFlag.MinDamage))
                        m_MinDamage = reader.ReadInt();
                    else
                        m_MinDamage = -1;

                    if (GetSaveFlag(flags, SaveFlag.MaxDamage))
                        m_MaxDamage = reader.ReadInt();
                    else
                        m_MaxDamage = -1;

                    if (GetSaveFlag(flags, SaveFlag.HitSound))
                        m_HitSound = reader.ReadInt();
                    else
                        m_HitSound = -1;

                    if (GetSaveFlag(flags, SaveFlag.MissSound))
                        m_MissSound = reader.ReadInt();
                    else
                        m_MissSound = -1;

                    if (GetSaveFlag(flags, SaveFlag.Speed))
                    {
                        if (version < 9)
                            m_Speed = reader.ReadInt();
                        else
                            m_Speed = reader.ReadFloat();
                    }
                    else
                    {
                        m_Speed = -1;
                    }

                    if (GetSaveFlag(flags, SaveFlag.MaxRange))
                        m_MaxRange = reader.ReadInt();
                    else
                        m_MaxRange = -1;

                    if (GetSaveFlag(flags, SaveFlag.Skill))
                        m_Skill = (SkillName) reader.ReadInt();
                    else
                        m_Skill = (SkillName) (-1);

                    if (GetSaveFlag(flags, SaveFlag.Type))
                        m_Type = (WeaponType) reader.ReadInt();
                    else
                        m_Type = (WeaponType) (-1);

                    if (GetSaveFlag(flags, SaveFlag.Animation))
                        m_Animation = (WeaponAnimation) reader.ReadInt();
                    else
                        m_Animation = (WeaponAnimation) (-1);

                    if (GetSaveFlag(flags, SaveFlag.Resource))
                        m_Resource = (CraftResource) reader.ReadInt();
                    else
                        m_Resource = CraftResource.Iron;

                    if (GetSaveFlag(flags, SaveFlag.PlayerConstructed))
                        PlayerConstructed = true;

                    if (GetSaveFlag(flags, SaveFlag.Slayer2))
                        OldSlayer2 = (SlayerName) reader.ReadInt();

                    break;
                }
                case 4:
                {
                    OldSlayer = (SlayerName) reader.ReadInt();

                    goto case 3;
                }
                case 3:
                {
                    m_StrReq = reader.ReadInt();
                    m_DexReq = reader.ReadInt();
                    m_IntReq = reader.ReadInt();

                    goto case 2;
                }
                case 2:
                {
                    Identified = reader.ReadBool();

                    goto case 1;
                }
                case 1:
                {
                    m_MaxRange = reader.ReadInt();

                    goto case 0;
                }
                case 0:
                {
                    if (version == 0)
                        m_MaxRange = 1; // default

                    if (version < 5) m_Resource = CraftResource.Iron;

                    m_MinDamage = reader.ReadInt();
                    m_MaxDamage = reader.ReadInt();

                    m_Speed = reader.ReadInt();

                    m_HitSound = reader.ReadInt();
                    m_MissSound = reader.ReadInt();

                    m_Skill = (SkillName) reader.ReadInt();
                    m_Type = (WeaponType) reader.ReadInt();
                    m_Animation = (WeaponAnimation) reader.ReadInt();
                    DamageLevel = (WeaponDamageLevel) reader.ReadInt();
                    AccuracyLevel = (WeaponAccuracyLevel) reader.ReadInt();
                    DurabilityLevel = (WeaponDurabilityLevel) reader.ReadInt();
                    Mark = (MarkQuality) reader.ReadInt();

                    Crafter = reader.ReadEntity<Mobile>();

                    Poison = reader.ReadPoison();
                    PoisonCharges = reader.ReadInt();

                    if (m_StrReq == DefaultStrengthReq)
                        m_StrReq = -1;

                    if (m_DexReq == DefaultDexterityReq)
                        m_DexReq = -1;

                    if (m_IntReq == DefaultIntelligenceReq)
                        m_IntReq = -1;

                    if (m_MinDamage == DefaultMinDamage)
                        m_MinDamage = -1;

                    if (m_MaxDamage == DefaultMaxDamage)
                        m_MaxDamage = -1;

                    if (m_HitSound == DefaultHitSound)
                        m_HitSound = -1;

                    if (m_MissSound == DefaultMissSound)
                        m_MissSound = -1;

                    if (Math.Abs(m_Speed - DefaultSpeed) < 0.1)
                        m_Speed = -1;

                    if (m_MaxRange == DefaultMaxRange)
                        m_MaxRange = -1;

                    if (m_Skill == DefaultSkill)
                        m_Skill = (SkillName) (-1);

                    if (m_Type == DefaultWeaponType)
                        m_Type = (WeaponType) (-1);

                    if (m_Animation == DefaultAnimation)
                        m_Animation = (WeaponAnimation) (-1);

                    break;
                }
            }

            if (Parent is Mobile)
                ((Mobile) Parent).CheckStatTimers();

            if (m_Hits <= 0 && MaxHitPoints <= 0)
                m_Hits = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

            if (version < 6)
                PlayerConstructed = true; // we don't know, so, assume it's crafted
        }

        #endregion

        public BaseWeapon(int itemID) : base(itemID)
        {
            Layer = (Layer) ItemData.Quality;

            Mark = MarkQuality.Regular;
            m_StrReq = -1;
            m_DexReq = -1;
            m_IntReq = -1;
            m_MinDamage = -1;
            m_MaxDamage = -1;
            m_HitSound = -1;
            m_MissSound = -1;
            m_Speed = -1;
            m_MaxRange = -1;
            m_Skill = (SkillName) (-1);
            m_Type = (WeaponType) (-1);
            m_Animation = (WeaponAnimation) (-1);

            m_Hits = MaxHitPoints = Utility.RandomMinMax(InitMinHits, InitMaxHits);

            m_Resource = CraftResource.Iron;
        }

        public BaseWeapon(Serial serial) : base(serial)
        {
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            var type = Enchantments.Get((MagicalWeapon e) => e.Value);
            return type == MagicalWeaponType.Mystical || type == MagicalWeaponType.Stygian;
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public static BaseWeapon Fists { get; set; }

        #region ICraftable Members

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (MarkQuality) mark;

            if (makersMark)
                Crafter = from;

            var resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            PlayerConstructed = true;

            var resEnchantments = CraftResources.GetEnchantments(Resource);

            if (resEnchantments != null)
                foreach (var (key, value) in resEnchantments)
                    Enchantments.SetFromResourceType(key, value);

            Quality = quality;

            MaxHitPoints = (int) (MaxHitPoints * quality);
            HitPoints = MaxHitPoints;

            return mark;
        }

        #endregion
    }
}