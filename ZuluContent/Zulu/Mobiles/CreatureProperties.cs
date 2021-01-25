using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Spells;
using Server.Spells.Fourth;
using Server.Scripts.Engines.Loot;

namespace Server.Mobiles
{
    public record CreatureProp
    {
        public double Min { get; private set; }
        public double? Max { get; private set; }

        public CreatureProp(double min, double? max = null)
        {
            Min = min;
            Max = max;
        }

        public static CreatureProp Between(double min, double max)
        {
            return new CreatureProp(min, max);
        }

        public static CreatureProp Dice(string d)
        {
            var dice = new LootPackDice(d);
            var value = new CreatureProp(dice.Count + dice.Bonus, dice.Count * dice.Sides + dice.Bonus);

            return value;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double min, out double? max)
        {
            min = Min;
            max = Max;
        }

        public static implicit operator int(CreatureProp d)
        {
            return Convert.ToInt32(d.Next());
        }

        public static implicit operator double(CreatureProp d)
        {
            return d.Next();
        }

        public static implicit operator CreatureProp(double d)
        {
            return new CreatureProp(d);
        }

        private double Next()
        {
            if (!Max.HasValue)
                return Min;

            return Min + Utility.RandomDouble() * (Max.GetValueOrDefault(0.0) - Min);
        }
    }

    public record CreatureProperties
    {
        private static readonly Dictionary<Type, CreatureProperties> CreatureMap =
            new Dictionary<Type, CreatureProperties>();

        public static IReadOnlyDictionary<Type, CreatureProperties> Creatures
        {
            get { return CreatureMap; }
        }

        public static bool Register<T>(CreatureProperties props) where T : BaseCreature
        {
            return CreatureMap.TryAdd(typeof(T), props);
        }

        public static CreatureProperties Get<T>() where T : BaseCreature
        {
            return Get(typeof(T));
        }

        public static CreatureProperties Get(Type T)
        {
            return CreatureMap.GetValueOrDefault(T);
        }

        public bool InitialInnocent { get; init; }
        public CreatureProp Str { get; init; } = 0;
        public CreatureProp Dex { get; init; } = 0;
        public CreatureProp Int { get; init; } = 0;
        public CreatureProp HitsMax { get; init; } = 0;
        public CreatureProp DamageMin { get; init; } = 0;
        public CreatureProp DamageMax { get; init; } = 0;
        public CreatureProp ManaMaxSeed { get; init; } = 0;
        public CreatureProp StamMaxSeed { get; init; } = 0;
        public bool HasBreath { get; init; } = false;
        public Poison HitPoison { get; init; } = null;
        public bool CanSwim { get; init; } = false;
        public bool CanFly { get; init; } = false;
        public string Title { get; init; }
        public string CorpseNameOverride { get; init; }
        public string Name { get; init; } = "<MobileInitProperties unset>";
        public Body Body { get; init; } = 0;
        public Race Race { get; init; }
        public CreatureProp Hue { get; init; }
        public int BaseSoundID { get; init; }
        public AIType AiType { get; init; } = AIType.AI_Mage;
        public FightMode FightMode { get; init; } = FightMode.Aggressor;
        public CreatureProp PerceptionRange { get; init; } = 10;
        public CreatureProp FightRange { get; init; } = 1;

        public CreatureProp ActiveSpeed { get; init; } = 0.2;
        public CreatureProp PassiveSpeed { get; init; } = 0.2;
        public InhumanSpeech SpeechType { get; init; } = null;
        public int Team { get; init; } = 0;
        public bool Female { get; init; } = false;
        public CreatureProp Fame { get; init; } = 0;
        public CreatureProp Karma { get; init; } = 0;
        public CreatureProp VirtualArmor { get; init; } = 0;
        public OppositionGroup OppositionGroup { get; init; }
        public TimeSpan ReacquireDelay { get; init; } = TimeSpan.FromSeconds(10.0);
        public bool ClickTitle { get; init; } = false;
        public bool CanRummageCorpses { get; init; } = true;
        public CreatureProp TreasureMapLevel { get; init; } = 0;
        public CreatureProp Meat { get; init; } = 0;
        public bool AlwaysMurderer { get; init; } = false;
        public bool Tamable { get; init; } = false;
        public int ControlSlots { get; init; }
        public CreatureProp MinTameSkill { get; init; }
        public Dictionary<SkillName, CreatureProp> Skills { get; init; } = new Dictionary<SkillName, CreatureProp>();

        public Dictionary<ElementalType, CreatureProp> Resistances { get; init; } =
            new Dictionary<ElementalType, CreatureProp>();

        public Dictionary<LootTable, CreatureProp> Loot { get; init; } = new Dictionary<LootTable, CreatureProp>();

        public Dictionary<ElementalType, CreatureProp> DamageTypes { get; init; } =
            new Dictionary<ElementalType, CreatureProp>();

        public WeaponAbility WeaponAbility { get; init; } = null;
        public double WeaponAbilityChance { get; init; } = 0.4;
        public bool AutoDispel { get; init; } = false;
        public bool AlwaysAttackable { get; init; } = false;
        public bool DeleteCorpseOnDeath { get; init; } = false;
        public Type RiseCreatureType { get; init; } = null;
        public TimeSpan RiseCreatureDelay { get; init; } = TimeSpan.MaxValue;
        public bool BardImmune { get; init; }
        public int ProvokeSkillOverride { get; init; }
        public bool SaySpellMantra { get; init; }
        public List<Type> PreferredSpells { get; init; }
        public SpecName ClassSpec { get; init; }
        public int ClassLevel { get; init; }
        public CreatureType CreatureType { get; init; }
        public HideType HideType { get; init; }
        public int Hides { get; init; }
        public bool TargetAcquireExhaustion { get; init; }

        private static readonly Action<CreatureProperties, BaseCreature> MapAction
            = ZuluUtil.BuildMapAction<CreatureProperties, BaseCreature>();


        public void ApplyTo<T>(T dest) where T : BaseCreature
        {
            // If dest null throw an exception
            if (dest == null)
                throw new ArgumentNullException($"Failed to apply properties to {nameof(dest)}");

            MapAction(this, dest);

            // Bug? Need to set it automatically
            dest.SetStam(dest.StamMax);
            dest.SetMana(dest.ManaMax);
            dest.SetHits(dest.HitsMax);

            // Non-mappable props
            foreach (var (skill, prop) in Skills)
                dest.SetSkill(skill, prop);

            foreach (var (resistance, prop) in Resistances)
                dest.Enchantments.SetResist(resistance, prop);

            foreach (var (table, amount) in Loot)
            {
                if(amount == 1)
                {
                    dest.AddLoot(table);
                }
            }

            // foreach (var (damageType, prop) in ElementalType.
            //     dest.SetDamageType(damageType, prop);
        }
    }
}