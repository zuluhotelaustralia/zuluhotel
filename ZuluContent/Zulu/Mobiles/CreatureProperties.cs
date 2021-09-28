#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Cue;
using Server.Engines.Magic;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Spells;
using Server.Utilities;
using ZuluContent.Zulu.Engines.Magic;

// ReSharper disable InconsistentNaming

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Server.Mobiles
{
    public record CreatureProperties
    {
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
        public string Title { get; set; }
        public PropValue? TreasureMapLevel { get; set; }

        [CueExpression(@"[...string]")]
        public List<SpellEntry> PreferredSpells { get; set; } = new();

        [CueExpression(@"{ ... }")]
        public Dictionary<ElementalType, PropValue> Resistances { get; set; } = new();

        [CueExpression(@"{ ... }")]
        public Dictionary<SkillName, PropValue> Skills { get; set; } = new();
        public CreatureAttack Attack { get; set; } = new();
        public List<CreatureEquip> Equipment { get; set; } = new();

        private static readonly Action<CreatureProperties, BaseCreature> MapAction
            = ZuluUtil.BuildMapAction<CreatureProperties, BaseCreature>();

        #endregion

        public void ApplyTo<T>(T dest) where T : BaseCreature
        {
            // If dest null throw an exception
            if (dest == null)
                throw new ArgumentNullException($"Failed to apply properties to {nameof(dest)}");

            MapAction(this, dest);

            // Non-mappable props
            if (Skills.Any())
            {
                foreach (var (skill, prop) in Skills)
                    dest.SetSkill(skill, prop);
            }

            if (Resistances.Any())
            {
                foreach (var (resistance, prop) in Resistances)
                    dest.TrySetResist(resistance, prop);
            }

            Dress(dest);
        }

        private void Dress(BaseCreature dest)
        {
            if (!dest.Items.Any() && Equipment.Any())
            {
                foreach (var equip in Equipment)
                {
                    var item = equip.ItemType?.CreateInstance<Item>();

                    if (item == null)
                        continue;

                    if (equip.Name != null)
                        item.Name = equip.Name;

                    if (equip.Hue != null)
                        item.Hue = equip.Hue;

                    if (item is BaseArmor armor && equip.ArmorRating != null)
                        armor.BaseArmorRating = (int)equip.ArmorRating;

                    dest.AddItem(item);
                    item.Movable = false;
                }
            }


            if (Attack != null)
            {
                dest.DamageMin = (int)Attack.Damage.Min;
                dest.DamageMax = (int)(Attack.Damage.Max ?? Attack.Damage.Min);

                if (dest.Weapon == null && (
                        Attack.Animation != null || Attack.HitSound != null ||
                        Attack.MissSound != null || Attack.MaxRange != null
                    )
                )
                {
                    dest.AddItem(new Fists());
                }


                if (dest.Weapon is BaseWeapon weapon)
                {
                    if (Attack.Animation != null)
                        weapon.Animation = Attack.Animation.Value;

                    if (Attack.HitSound != null)
                        weapon.HitSound = Attack.HitSound.Value;

                    if (Attack.MissSound != null)
                        weapon.MissSound = Attack.MissSound.Value;

                    if (Attack.MaxRange != null)
                        weapon.MaxRange = Attack.MaxRange.Value;

                    if (Attack.Speed != null)
                        weapon.Speed = Attack.Speed;

                    if (Attack.HitPoison != null)
                        weapon.Poison = Attack.HitPoison;

                    if (Attack.ProjectileEffectId != null && weapon is BaseRanged br)
                        br.EffectId = Attack.ProjectileEffectId.Value;
                }
            }
        }
    }
}