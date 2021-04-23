using System;
using System.Collections.Generic;
using System.Linq;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using ZuluContent.Zulu.Engines.Magic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Server.Mobiles
{
    public record CreatureProperties
    {
        private static readonly Dictionary<Type, CreatureProperties> CreatureMap = new();
        public static IReadOnlyDictionary<Type, CreatureProperties> Creatures => CreatureMap;

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

        public string Name { get; init; } = "<CreatureProperties unset>";
        public string CorpseNameOverride { get; init; }
        public Type BaseType { get; set; } = typeof(BaseCreatureTemplate);
        public CreatureProp Str { get; init; }
        public CreatureProp Int { get; init; }
        public CreatureProp Dex { get; init; }
        public CreatureProp ActiveSpeed { get; init; }
        public AIType AiType { get; init; }
        public bool? AlwaysAttackable { get; init; }
        public bool? AlwaysMurderer { get; init; }
        public bool? AutoDispel { get; init; }
        public bool? BardImmune { get; init; }
        public int? BaseSoundID { get; init; }
        public Body Body { get; init; }
        public bool? CanFly { get; init; }
        public bool? CanRummageCorpses { get; init; } = true;
        public bool? CanSwim { get; init; }
        public int? ClassLevel { get; init; }
        public ZuluClassType? ClassType { get; init; }
        public bool? ClickTitle { get; init; }
        public int? ControlSlots { get; init; }
        public CreatureType? CreatureType { get; init; }
        public CreatureProp DamageMax { get; init; }
        public CreatureProp DamageMin { get; init; }
        public bool? DeleteCorpseOnDeath { get; init; }
        public CreatureProp Fame { get; init; }
        public bool? Female { get; init; }
        public FightMode FightMode { get; init; }
        public CreatureProp FightRange { get; init; }
        public bool? HasBreath { get; init; }
        public bool? HasWebs { get; init; }
        public HideType? HideType { get; init; }
        public int? Hides { get; init; }
        public Poison HitPoison { get; init; }
        public CreatureProp HitsMax { get; init; }
        public CreatureProp Hue { get; init; }
        public bool? InitialInnocent { get; init; }
        public CreatureProp Karma { get; init; }
        public int? LootItemChance { get; set; }
        public int? LootItemLevel { get; set; }
        public string LootTable { get; set; }
        public CreatureProp ManaMaxSeed { get; init; }
        public CreatureProp Meat { get; init; }
        public CreatureProp MinTameSkill { get; init; }
        public OppositionGroup OppositionGroup { get; init; }
        public CreatureProp PassiveSpeed { get; init; }
        public CreatureProp PerceptionRange { get; init; }
        public List<Type> PreferredSpells { get; init; }
        public int? ProvokeSkillOverride { get; init; }
        public Race Race { get; init; }
        public TimeSpan? ReacquireDelay { get; init; }
        public Dictionary<ElementalType, CreatureProp> Resistances { get; init; }
        public TimeSpan? RiseCreatureDelay { get; init; }
        public Type RiseCreatureType { get; init; }
        public bool? SaySpellMantra { get; init; }
        public Dictionary<SkillName, CreatureProp> Skills { get; init; }
        public InhumanSpeech SpeechType { get; init; }
        public CreatureProp StamMaxSeed { get; init; }
        public bool? Tamable { get; init; }
        public bool? TargetAcquireExhaustion { get; init; }
        public int? Team { get; init; }
        public string Title { get; init; }
        public CreatureProp TreasureMapLevel { get; init; }
        public CreatureProp VirtualArmor { get; init; }
        public WeaponAbility WeaponAbility { get; init; }
        public double? WeaponAbilityChance { get; init; }

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
            if (Skills != null)
            {
                foreach (var (skill, prop) in Skills)
                    dest.SetSkill(skill, prop);
            }
            
            if (Resistances != null)
            {
                foreach (var (resistance, prop) in Resistances)
                    dest.TrySetResist(resistance, prop);
            }
        }
    }
}