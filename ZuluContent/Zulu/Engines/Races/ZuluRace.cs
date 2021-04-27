using System;
using System.Collections.Generic;
using Server;
using Server.Engines.Craft;
using Server.Engines.Magic;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Hooks;

namespace Scripts.Zulu.Engines.Races
{
    [PropertyObject]
    public class ZuluRace : IEnchantmentHook
    {
        private readonly IZuluRace m_Parent;

        public static readonly IReadOnlyDictionary<ZuluRaceType, ZuluRaceAttributes> Races =
            new Dictionary<ZuluRaceType, ZuluRaceAttributes>
            {
                [ZuluRaceType.Human] = new()
                {
                    Hue = 0,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                },
                [ZuluRaceType.Elf] = new()
                {
                    Hue = 770,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                },
                [ZuluRaceType.DarkElf] = new()
                {
                    Hue = 33877,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                },
                [ZuluRaceType.Goblin] = new()
                {
                    Hue = 34186,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                },
                [ZuluRaceType.Barbarian] = new()
                {
                    Hue = 33804,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                },
                [ZuluRaceType.Dwarf] = new()
                {
                    Hue = 33888,
                    Location = new (1475, 1645, 20),
                    Map = Map.Felucca
                }
            };

        [CommandProperty(AccessLevel.Counselor)]
        public ZuluRaceType Type { get; } = ZuluRaceType.None;

        public ZuluRace(IZuluRace parent)
        {
            m_Parent = parent;

            Type = m_Parent.ZuluRaceType;
        }

        public static void Initialize()
        {
            CommandSystem.Register("SetRace", AccessLevel.GameMaster, SetRace);
        }

        public override string ToString() => Type.FriendlyName();

        [Usage("SetRace <race>")]
        [Description("Sets you to the desired race.")]
        public static void SetRace(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            if (e.Length == 1 && Enum.TryParse(e.GetString(0), out ZuluRaceType raceType))
            {
                pm.ZuluRaceType = raceType;
                pm.Hue = Races[raceType].Hue;
            }
        }

        #region Unused hooks
        
        public void OnDeath(Mobile victim, ref bool resurrect)
        {
        }

        public void OnCheckMagicReflection(Mobile target, Spell spell, ref bool reflected)
        {
        }

        public void OnSpellAreaCalculation(Mobile caster, Spell spell, ElementalType damageType, ref double area)
        {
        }

        public void OnSpellDamage(Mobile attacker, Mobile defender, Spell spell, ElementalType damageType,
            ref int damage)
        {
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
        }

        public void OnAnimalTaming(Mobile tamer, BaseCreature creature, ref int chance)
        {
        }

        public void OnTracking(Mobile tracker, ref int range)
        {
        }

        public void OnQualityBonus(Mobile crafter, ref int multiplier)
        {
        }

        public void OnArmsLoreBonus(Mobile crafter, ref double armsLoreValue)
        {
        }

        public void OnExceptionalChance(Mobile crafter, ref double exceptionalChance, ref int exceptionalDifficulty)
        {
        }

        public void OnMeditation(Mobile mobile, ref int regen, ref double tickIntervalSeconds)
        {
        }

        public void OnModifyWithMagicEfficiency(Mobile mobile, ref double value)
        {
        }

        public void OnHarvestAmount(Mobile mobile, ref int amount)
        {
        }

        public void OnHarvestBonus(Mobile mobile, ref int amount)
        {
        }

        public void OnHarvestColoredQualityChance(Mobile mobile, ref int bonus, ref int toMod)
        {
        }

        public void OnHarvestColoredChance(Mobile mobile, ref int chance)
        {
        }

        public void OnBeforeSwing(Mobile attacker, Mobile defender)
        {
        }

        public void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result)
        {
        }

        public void OnGetSwingDelay(ref double delayInSeconds, Mobile m)
        {
        }

        public void OnCheckHit(Mobile attacker, Mobile defender, ref bool result)
        {
        }

        public void OnMeleeHit(Mobile attacker, Mobile defender, BaseWeapon weapon, ref int damage)
        {
        }

        public void OnAbsorbMeleeDamage(Mobile attacker, Mobile defender, BaseWeapon weapon, ref double damage)
        {
        }

        public void OnShieldHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseShield shield, ref double damage)
        {
        }

        public void OnArmorHit(Mobile attacker, Mobile defender, BaseWeapon weapon, BaseArmor armor, ref double damage)
        {
        }

        public void OnCraftItemCreated(Mobile @from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
        }

        public void OnCraftItemAddToBackpack(Mobile from, CraftSystem craftSystem, CraftItem craftItem, BaseTool tool,
            Item item)
        {
        }

        public void OnCraftSkillRequiredForFame(Mobile from, ref int craftSkillRequiredForFame)
        {
        }

        public void OnSummonFamiliar(Mobile caster, BaseCreature familiar)
        {
        }

        public void OnCure(Mobile caster, Mobile target, Poison poison, object source, ref double difficulty)
        {
        }

        public void OnTrap(Mobile caster, Container target, ref double strength)
        {
        }

        public virtual void OnMove(Mobile mobile, Direction direction, ref bool canMove)
        {
        }

        public void OnHiddenChanged(Mobile mobile) { }

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