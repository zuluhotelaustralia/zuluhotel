using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Second;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace Scripts.Zulu.Engines.Classes
{
    [PropertyObject]
    public class ZuluClass : IEnchantmentHook
    {
        //reference original ZH Canada (ZH3) release
        private const double ClassPointsPerLevel = 120;
        private const double SkillBase = 480;
        private const double PercentPerLevel = 0.08;
        private const double PercentBase = 0.52;
        private const double PerLevel = 0.25; //25% per level
        private const double ClasseBonus = 1.5;
        private const int MaxLevel = 6;

        private static readonly double[] MinSkills =
            Enumerable
                .Range(0, MaxLevel + 1) // Technically lvl 0 (none) is a level
                .Select(i => SkillBase + ClassPointsPerLevel * i)
                .ToArray();

        private readonly IZuluClassed m_Parent;

        public static readonly IReadOnlyDictionary<ZuluClassType, SkillName[]> ClassSkills =
            new Dictionary<ZuluClassType, SkillName[]>
            {
                [ZuluClassType.Warrior] = new[]
                {
                    SkillName.Wrestling,
                    SkillName.Tactics,
                    SkillName.Healing,
                    SkillName.Anatomy,
                    SkillName.Swords,
                    SkillName.Macing,
                    SkillName.Fencing,
                    SkillName.Parry,
                },
                [ZuluClassType.Ranger] = new[]
                {
                    SkillName.Tracking,
                    SkillName.Archery,
                    SkillName.AnimalLore,
                    SkillName.Veterinary,
                    SkillName.AnimalTaming,
                    SkillName.Fishing,
                    SkillName.Camping,
                    SkillName.Cooking,
                },
                [ZuluClassType.Mage] = new[]
                {
                    SkillName.Alchemy,
                    SkillName.ItemID,
                    SkillName.EvalInt,
                    SkillName.Inscribe,
                    SkillName.MagicResist,
                    SkillName.Meditation,
                    SkillName.Magery,
                    SkillName.SpiritSpeak,
                },
                [ZuluClassType.Crafter] = new[]
                {
                    SkillName.Tinkering,
                    SkillName.ArmsLore,
                    SkillName.Fletching,
                    SkillName.Tailoring,
                    SkillName.Mining,
                    SkillName.Lumberjacking,
                    SkillName.Carpentry,
                    SkillName.Blacksmith,
                },
                [ZuluClassType.Thief] = new[]
                {
                    SkillName.Hiding,
                    SkillName.Stealth,
                    SkillName.Stealing,
                    SkillName.DetectHidden,
                    SkillName.RemoveTrap,
                    SkillName.Poisoning,
                    SkillName.Lockpicking,
                    SkillName.Snooping,
                },
                [ZuluClassType.Bard] = new[]
                {
                    SkillName.Provocation,
                    SkillName.Musicianship,
                    SkillName.Herding,
                    SkillName.Discordance,
                    SkillName.TasteID,
                    SkillName.Peacemaking,
                    SkillName.Cartography,
                    SkillName.Begging,
                }
            };

        [CommandProperty(AccessLevel.Counselor)]
        public int Level { get; set; } = 0;

        [CommandProperty(AccessLevel.Counselor)]
        public ZuluClassType Type { get; set; } = ZuluClassType.None;

        public ZuluClass(IZuluClassed parent)
        {
            m_Parent = parent;

            ComputeClass();
        }

        [CommandProperty(AccessLevel.Counselor)]
        public double Bonus => Type == ZuluClassType.PowerPlayer || Type == ZuluClassType.None
            ? 1.0
            : 1.0 + Level * PerLevel;

        public static double GetBonusFor(Mobile m, ZuluClassType name) =>
            m is IZuluClassed classed ? classed.ZuluClass.GetBonusFor(name) : 1.0;

        public double GetBonusFor(ZuluClassType name) => Type == name ? Bonus : 1.0;

        public static void Initialize()
        {
            CommandSystem.Register("Classe", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("ShowClasse", AccessLevel.Player, ClassOnCommand);
            CommandSystem.Register("SetClasse", AccessLevel.GameMaster, SetClass);
        }

        public static void ClassOnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (pm.AccessLevel >= AccessLevel.Counselor)
            {
                pm.Target = new InternalTarget();
                return;
            }

            pm.ZuluClass.ComputeClass();

            var message = pm.ZuluClass.Type == ZuluClassType.None
                ? "You aren't a member of any particular class."
                : $"You are a qualified level {pm.ZuluClass.Level} {pm.ZuluClass.Type.FriendlyName()}.";

            pm.SendMessage(message);
        }

        [Usage("SetClasse <class> <level>")]
        [Description("Sets you to the desired class and level, sets all other skills to 0.")]
        public static void SetClass(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (e.Length == 2 && Enum.TryParse(e.GetString(0), out ZuluClassType classType))
            {
                var level = e.GetInt32(1);

                if (level > MaxLevel || level < 0)
                    level = 0;

                foreach (var skill in pm.Skills)
                {
                    skill.Base = ClassSkills[classType].Contains(skill.SkillName)
                        ? MinSkills[level] / ClassSkills[classType].Length
                        : 0.0;
                }
            }
        }

        public static ZuluClass GetClass(Mobile m) => m is IZuluClassed classed ? classed.ZuluClass : null;

        public void ComputeClass()
        {
            if (m_Parent is BaseCreature)
                return;

            var allSkillsTotal = 0.0;
            foreach (var skill in m_Parent.Skills)
            {
                allSkillsTotal += skill.Value;
            }

            Type = ZuluClassType.None;
            Level = 0;

            double total = m_Parent.Skills.Total;
            total *= 0.1;

            switch (total)
            {
                case < 600.0:
                    Level = 0;
                    Type = ZuluClassType.None;
                    return;
                case >= 3920.0:
                {
                    Type = ZuluClassType.PowerPlayer;
                    Level = 1;

                    if (total >= 5145)
                    {
                        Level = 2;

                        if (total >= 6370)
                        {
                            Level = 3;
                        }
                    }

                    //we're a pp so:
                    return;
                }
            }

            foreach (var (classType, classSkills) in ClassSkills)
            {
                var classTotal = classSkills.Select(s => m_Parent.Skills[s].Value).Sum();

                var level = GetClassLevel(classTotal, allSkillsTotal);

                if (level > 0)
                {
                    Type = classType;
                    Level = level;
                }
            }

            if (Level > MaxLevel)
                Level = MaxLevel;

            if (Level <= 0)
            {
                Level = 0;
                Type = ZuluClassType.None;
            }
        }

        //idx:    0    1     2     3     4     5      6
        //Min: [ 480, 600,  720,  840,  960,  1080, 1200 ]
        //Max: [ 923, 1000, 1058, 1105, 1142, 1173, 1200 ]
        private int GetClassLevel(double classTotal, double allSkillsTotal)
        {
            for (int level = MinSkills.Length - 1; level >= 0; level--)
            {
                var levelReq = PercentBase + PercentPerLevel * level;
                var classPct = classTotal / allSkillsTotal;

                if (classTotal >= MinSkills[level] && classPct >= levelReq)
                    return level;
            }

            return 0;
        }
        
        public bool IsSkillInClass(SkillName sn)
        {
            return ClassSkills.FirstOrDefault(kv => kv.Value.Contains(sn)).Key == Type;
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(12, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object target)
            {
                if (from is IZuluClassed classed)
                {
                    classed.ZuluClass?.ComputeClass();
                    from.SendMessage("{0}: {1}, level {2}", from.Name, classed.ZuluClass?.Type,
                        classed.ZuluClass?.Level);
                }
            }
        }

        private static void DebugLog(Mobile m, string message)
        {
            if (m.AccessLevel >= AccessLevel.GameMaster)
                m.SendMessage(1283, message);
        }

        public double GetMagicEfficiencyPenalty()
        {
            if (m_Parent is Mobile mobile)
            {
                var armour = new[]
                {
                    mobile.ShieldArmor as BaseArmor,
                    mobile.NeckArmor as BaseArmor,
                    mobile.HandArmor as BaseArmor,
                    mobile.HeadArmor as BaseArmor,
                    mobile.ArmsArmor as BaseArmor,
                    mobile.LegsArmor as BaseArmor,
                    mobile.ChestArmor as BaseArmor,
                };

                return armour.Sum(a => a?.Enchantments.Get((MagicEfficiencyPenalty e) => e.Value) ?? 0);
            }

            return 0;
        }

        #region Class bonus hooks

        public void OnCheckMagicReflection(Mobile target, Spell spell, ref bool reflected)
        {
        }

        public void OnSpellAreaCalculation(Mobile caster, Spell spell, ElementalType damageType, ref double area)
        {
        }

        public void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
            DebugLog(attacker, $"OnSpellDamage::before {damage}");

            if (attacker is IZuluClassed {ZuluClass: { } attackerClass})
            {
                var bonus = attackerClass.Type switch
                {
                    ZuluClassType.Warrior => attackerClass.Bonus - 1.0,
                    ZuluClassType.Mage => attackerClass.Bonus,
                    _ => 1.0
                };

                damage = (int) (damage * bonus);

                DebugLog(attacker,
                    $"Changed damage of {spell} by {bonus} (level {attackerClass.Level} {attackerClass.Type})");
            }

            if (defender is IZuluClassed {ZuluClass: { } defenderClass})
            {
                var bonus = defenderClass.Type switch
                {
                    ZuluClassType.Warrior => defenderClass.Bonus,
                    ZuluClassType.Mage => defenderClass.Bonus - 1.0,
                    _ => 1.0
                };

                damage = (int) (damage * bonus);

                DebugLog(defender,
                    $"Changed damage of {spell} by {bonus} (level {defenderClass.Level} {defenderClass.Type})");
            }

            DebugLog(attacker, $"OnSpellDamage::after {damage}");
        }

        public void OnGetCastDelay(Mobile mobile, Spell spell, ref double delay)
        {
        }

        public void OnParalysis(Mobile mobile, ref TimeSpan duration, ref bool paralyze)
        {
        }

        public void OnCheckPoisonImmunity(Mobile attacker, Mobile defender, Poison poison, ref bool immune)
        {
        }

        public void OnHeal(Mobile healer, Mobile patient, object source, ref double healAmount)
        {
            if (healer is IZuluClassed {ZuluClass: { } cls})
            {
                var bonus = cls.Type switch
                {
                    ZuluClassType.Warrior when source is Bandage => cls.Bonus,
                    ZuluClassType.Mage when source is Spell => cls.Bonus,
                    _ => 1.0
                };

                healAmount *= bonus;

                if(bonus > 1.0)
                    DebugLog(healer, $"Increased healing from {source} " +
                                     $"by {cls.Bonus} " +
                                     $"(level {cls.Level} {cls.Type})");
            }
        }

        public void OnAnimalTaming(Mobile tamer, BaseCreature creature, ref int chance)
        {
            if (tamer is IZuluClassed {ZuluClass: {Type: ZuluClassType.Ranger}})
            {
                chance = (int) (chance / ClasseBonus);
            }
        }

        public void OnTracking(Mobile tracker, ref int range)
        {
            if (tracker is IZuluClassed {ZuluClass: {Type: ZuluClassType.Ranger} cls})
            {
                range = (int) (range * cls.Bonus);
            }
        }

        public void OnQualityBonus(Mobile crafter, ref int multiplier)
        {
            if (crafter is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter} cls})
            {
                multiplier = (int) (multiplier * cls.Bonus);
            }
        }

        public void OnArmsLoreBonus(Mobile crafter, ref double armsLoreValue)
        {
            if (crafter is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter} cls})
            {
                armsLoreValue = (int) (armsLoreValue * cls.Bonus);
            }
        }

        public void OnExceptionalChance(Mobile crafter, ref double exceptionalChance, ref int exceptionalDifficulty)
        {
            if (crafter is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter} cls})
            {
                exceptionalChance *= cls.Bonus;
                exceptionalDifficulty += 20;
            }
            else
            {
                exceptionalDifficulty += 40;
            }
        }

        public void OnMeditation(Mobile mobile, ref int regen, ref double tickIntervalSeconds)
        {
            if (mobile is IZuluClassed {ZuluClass: {Type: ZuluClassType.Mage}})
            {
                regen = (int) (regen * ClasseBonus);
                tickIntervalSeconds /= ClasseBonus;
            }
        }

        public void OnModifyWithMagicEfficiency(Mobile mobile, ref double value)
        {
            if (mobile is IZuluClassed {ZuluClass: { } cls})
            {
                var bonus = cls.Type switch
                {
                    ZuluClassType.Warrior => (int) (value / ClasseBonus),
                    ZuluClassType.Mage => (int) (value * ClasseBonus),
                    _ => (int) value
                };

                var penalty = GetMagicEfficiencyPenalty();

                value = (int) (bonus * (100 - penalty) / 100);
            }
        }

        public void OnHarvestAmount(Mobile mobile, ref int amount)
        {
            if (mobile is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter}})
            {
                amount = (int) (amount * ClasseBonus);
            }
        }

        public void OnHarvestBonus(Mobile mobile, ref int amount)
        {
            if (mobile is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter}})
            {
                amount = (int) (amount * ClasseBonus) + 1;
            }
        }

        public void OnHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
            if (mobile is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter}})
            {
                bonus = (int) (bonus * ClasseBonus);
                toMod = (int) (toMod / ClasseBonus);
            }
        }

        public void OnHarvestColoredChance(Mobile mobile, ref int chance)
        {
            if (mobile is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter}})
            {
                chance = (int) (chance * ClasseBonus);

                if (chance > 90)
                    chance = 90;
            }
        }

        public void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }

        public void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result)
        {
        }

        public void OnGetSwingDelay(ref double delayInSeconds, Mobile m)
        {
            m.SendAsciiMessage("OnSwing from ZuluClass");
        }

        public void OnCheckHit(Mobile attacker, Mobile defender, ref bool result)
        {
        }

        public void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield, ref int damage)
        {
        }

        public void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref int damage)
        {
        }

        public void OnCraftItemCreated(Mobile @from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
            if (
                craftSystem is DefAlchemy
                && item is BasePotion potion
                && from is IZuluClassed {ZuluClass: {Type: ZuluClassType.Mage}}
            )
            {
                var bonus = (uint) (potion.PotionStrength * ClasseBonus - potion.PotionStrength);
                if (bonus == 0)
                    bonus = 1;

                potion.PotionStrength += bonus;
            }
        }

        public void OnCraftItemAddToBackpack(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
        }

        public void OnCraftSkillRequiredForFame(Mobile from, ref int craftSkillRequiredForFame)
        {
            if (from is IZuluClassed {ZuluClass: {Type: ZuluClassType.Crafter}})
            {
                craftSkillRequiredForFame = 110;
            }
        }

        public void OnSummonFamiliar(Mobile caster, BaseCreature familiar)
        {
            if (caster is IZuluClassed {ZuluClass: {Type: ZuluClassType.Mage}})
            {
                familiar.RawStr += caster.RawStr;
                familiar.RawInt += caster.RawInt;
                familiar.RawDex += caster.RawDex;
            }
        }

        public void OnCure(Mobile caster, Mobile target, Poison poison, object source, ref double difficulty)
        {
            if (source is CureSpell && caster is IZuluClassed {ZuluClass: {Type: ZuluClassType.Mage}}) 
                difficulty /= Bonus;
        }

        public void OnTrap(Mobile caster, Container target, ref double strength)
        {
            if (caster is IZuluClassed {ZuluClass: {Type: ZuluClassType.Mage}})
                strength *= Bonus;
        }

        public virtual void OnMove(Mobile mobile, Direction direction, ref bool canMove)
        {
            
        }

        public void OnHiddenChanged(Mobile mobile) { }

        #endregion

        #region Unused hooks

        public void OnToolHarvestBonus(Mobile harvester, ref int amount)
        {
        }

        public void OnToolHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
        }

        public void OnIdentified(IEntity entity)
        {
        }

        public void OnAdded(IEntity entity)
        {
        }

        public void OnRemoved(IEntity entity, IEntity parent)
        {
        }

        public void OnBeforeRemoved(IEntity entity, Mobile @from, ref bool canRemove)
        {
        }

        #endregion
    }
}