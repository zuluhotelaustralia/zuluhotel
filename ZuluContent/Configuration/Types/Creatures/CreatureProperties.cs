#nullable enable
using System;
using System.Collections.Generic;
using Scripts.Cue;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server;
using Server.Engines.Magic;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ZuluContent.Configuration.Types.Creatures
{
    public record CreatureProperties
    {
        
        private static readonly Action<CreatureProperties, BaseCreature> MapAction
            = ZuluUtil.BuildMapAction<CreatureProperties, BaseCreature>();
        
        #region Properties
        public string Name { get; set; } = "<CreatureProperties unset>";
        public string Kind { get; set; } = "Npc";
        public string? CorpseNameOverride { get; set; }

        public Type BaseType { get; set; } = typeof(BaseCreatureTemplate);

        public PropValue Str { get; set; } = 1;
        public PropValue Int { get; set; } = 1;
        public PropValue Dex { get; set; } = 1;
        public PropValue ActiveSpeed { get; set; } = 0.2;
        public PropValue PassiveSpeed { get; set; } = 0.2;
        public AIType AiType { get; set; }
        public bool? AlwaysAttackable { get; set; }
        public bool? AlwaysMurderer { get; set; }
        public bool? AutoDispel { get; set; }
        public bool? BardImmune { get; set; }
        public int? BaseSoundID { get; set; }
        public Body Body { get; set; }
        public bool? CanFly { get; set; }
        public bool? CanRummageCorpses { get; set; } = true;
        public bool? CanSwim { get; set; }
        public int? ClassLevel { get; set; }
        public ZuluClassType? ClassType { get; set; }
        public bool? ClickTitle { get; set; }
        public int? ControlSlots { get; set; }
        public CreatureType? CreatureType { get; set; }
        public PropValue VirtualArmor { get; set; } = 0;
        public bool? DeleteCorpseOnDeath { get; set; }
        public PropValue Fame { get; set; } = 0;
        public bool? Female { get; set; }
        public FightMode FightMode { get; set; }
        public PropValue FightRange { get; set; } = 1;
        public HideType? HideType { get; set; }
        public int? Hides { get; set; }
        public PropValue HitsMaxSeed { get; set; } = 1;
        public PropValue Hue { get; set; } = 0;
        public bool? InitialInnocent { get; set; }
        public PropValue Karma { get; set; } = 0;
        public int? LootItemChance { get; set; }
        public int? LootItemLevel { get; set; }
        public string? LootTable { get; set; }
        public PropValue ManaMaxSeed { get; set; } = 0;
        public PropValue Meat { get; set; } = 0;
        public PropValue MinTameSkill { get; set; } = 0;
        public OppositionGroup? OppositionGroup { get; set; }
        public PropValue PerceptionRange { get; set; } = 1;
        public int? ProvokeSkillOverride { get; set; }
        public Race? Race { get; set; }
        public TimeSpan? ReacquireDelay { get; set; }
        public TimeSpan? RiseCreatureDelay { get; set; }
        public string? RiseCreatureTemplate { get; set; }
        public bool? SaySpellMantra { get; set; }
        public InhumanSpeech? SpeechType { get; set; }
        public PropValue StamMaxSeed { get; set; } = 1;
        public bool? Tamable { get; set; }
        public bool? TargetAcquireExhaustion { get; set; }
        public int? Team { get; set; }
        public string? Title { get; set; }
        public PropValue? TreasureMapLevel { get; set; }

        [CueExpression(@"[...string]")]
        public List<SpellEntry> PreferredSpells { get; set; } = new();

        [CueExpression(@"{ ... }")]
        public Dictionary<ElementalType, PropValue> Resistances { get; set; } = new();

        [CueExpression(@"{ ... }")]
        public Dictionary<SkillName, PropValue> Skills { get; set; } = new();
        public CreatureAttack? Attack { get; set; }
        public List<CreatureEquip>? Equipment { get; set; } = new();

        #endregion
    }
}