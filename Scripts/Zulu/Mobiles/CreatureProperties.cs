using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Misc;
using RunZH.Scripts.Zulu.Engines.Classes;
using Server.Spells;
using Server.Spells.Fourth;

namespace Server.Mobiles
{
    public /* data */ struct CreatureProp
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

        public static implicit operator int(CreatureProp d) => Convert.ToInt32(d.Next());
        public static implicit operator CreatureProp(int d) => new CreatureProp(d);

        public static implicit operator double(CreatureProp d) => d.Next();
        public static implicit operator CreatureProp(double d) => new CreatureProp(d);

        private double Next()
        {
            if (!Max.HasValue)
                return Min;

            return Min + Utility.RandomDouble() * (Max.GetValueOrDefault(0.0) - Min);
        }
    }

    public class CreatureProperties
    {
        private static readonly Dictionary<Type, CreatureProperties> CreatureMap = new Dictionary<Type, CreatureProperties>();
        public static IReadOnlyDictionary<Type, CreatureProperties> Creatures => CreatureMap;
        public static bool Register<T>(CreatureProperties props) where T : BaseCreature => CreatureMap.TryAdd(typeof(T), props);
        public static CreatureProperties Get<T>() where T : BaseCreature => Get(typeof(T));
        public static CreatureProperties Get(Type T) => CreatureMap.GetValueOrDefault(T);
        public bool InitialInnocent { get; set; }
        public CreatureProp Str { get; set; } = 0;
        public CreatureProp Dex { get; set; } = 0;
        public CreatureProp Int { get; set; } = 0;
        public CreatureProp HitsMax { get; set; } = 0;
        public CreatureProp DamageMin { get; set; } = 0;
        public CreatureProp DamageMax { get; set; } = 0;
        public CreatureProp ManaMaxSeed { get; set; } = 0;
        public CreatureProp StamMaxSeed { get; set; } = 0;
        public bool HasBreath { get; set; } = false;
        public Poison HitPoison { get; set; } = null;
        public bool CanSwim { get; set; } = false;
        public bool CanFly { get; set; } = false;
        public string Title { get; set; }
        public string CorpseNameOverride { get; set; }
        public string Name { get; set; } = "<MobileInitProperties unset>";
        public Body Body { get; set; } = 0;
        public Race Race { get; set; }
        public CreatureProp Hue { get; set; }
        public int BaseSoundID { get; set; }
        public AIType AiType { get; set; } = AIType.AI_Mage;
        public FightMode FightMode { get; set; } = FightMode.Aggressor;
        public CreatureProp PerceptionRange { get; set; } = 10;
        public CreatureProp FightRange { get; set; } = 1;

        public CreatureProp ActiveSpeed { get; set; } = 0.2;
        public CreatureProp PassiveSpeed { get; set; } = 0.2;
        public InhumanSpeech SpeechType { get; set; } = null;
        public int Team { get; set; } = 0;
        public bool Female { get; set; } = false;
        public CreatureProp Fame { get; set; } = 0;
        public CreatureProp Karma { get; set; } = 0;
        public CreatureProp VirtualArmor { get; set; } = 0;
        public OppositionGroup OppositionGroup { get; set; }
        public TimeSpan ReacquireDelay { get; set; } = TimeSpan.FromSeconds(10.0);
        public bool ClickTitle { get; set; } = false;
        public bool CanRummageCorpses { get; set; } = true;
        public CreatureProp TreasureMapLevel { get; set; } = 0;
        public CreatureProp Meat { get; set; } = 0;
        public bool AlwaysMurderer { get; set; } = false;
        public bool Tamable { get; set; } = false;
        public int ControlSlots { get; set; }
        public CreatureProp MinTameSkill { get; set; }
        public Dictionary<SkillName, CreatureProp> Skills { get; set; } = new Dictionary<SkillName, CreatureProp>();

        public Dictionary<ElementalType, CreatureProp> Resistances { get; set; } =
            new Dictionary<ElementalType, CreatureProp>();

        public Dictionary<LootPack, CreatureProp> Loot { get; set; } = new Dictionary<LootPack, CreatureProp>();

        public Dictionary<ElementalType, CreatureProp> DamageTypes { get; set; } =
            new Dictionary<ElementalType, CreatureProp>();

        public WeaponAbility WeaponAbility { get; set; } = null;
        public double WeaponAbilityChance { get; set; } = 0.4;
        public bool AutoDispel { get; set; } = false;
        public bool AlwaysAttackable { get; set; } = false;
        public bool DeleteCorpseOnDeath { get; set; } = false;
        public Type RiseCreatureType { get; set; } = null;
        public TimeSpan RiseCreatureDelay { get; set; } = TimeSpan.MaxValue;
        public bool BardImmune { get; set; }
        public int ProvokeSkillOverride { get; set; }
        public bool SaySpellMantra { get; set; }
        public List<Type> PreferredSpells { get; set; }
        public SpecName ClassSpec { get; set; }
        public int ClassLevel { get; set; }
        public CreatureType CreatureType { get; set; }
        public HideType HideType { get; set; }
        public int Hides { get; set; }
        public bool TargetAcquireExhaustion { get; set; }

        private static readonly Action<CreatureProperties, BaseCreature> MapAction 
            = Utility.BuildMapAction<CreatureProperties, BaseCreature>();
        
        
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

            // foreach (var (resistance, prop) in Resistances)
            //     dest.SetResistance(resistance, prop);

            foreach (var (pack, amount) in Loot)
                dest.AddLoot(pack, amount);

            // foreach (var (damageType, prop) in ElementalType.
            //     dest.SetDamageType(damageType, prop);
        }
    }
}