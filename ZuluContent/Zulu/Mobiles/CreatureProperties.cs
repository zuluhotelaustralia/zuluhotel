using System;
using System.Collections.Generic;
using System.Linq;
using Server.Engines.Magic;
using Server.Engines.Magic.HitScripts;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles.Monsters;
using Server.Utilities;
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

        public string Name { get; set; } = "<CreatureProperties unset>";
        public string CorpseNameOverride { get; set; }
        public Type BaseType { get; set; } = typeof(BaseCreatureTemplate);
        public PropValue Str { get; set; }
        public PropValue Int { get; set; }
        public PropValue Dex { get; set; }
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
        public PropValue DamageMax { get; set; }
        public PropValue DamageMin { get; set; }
        public PropValue VirtualArmor { get; set; }
        public bool? DeleteCorpseOnDeath { get; set; }
        public PropValue Fame { get; set; }
        public bool? Female { get; set; }
        public FightMode FightMode { get; set; }
        public PropValue FightRange { get; set; }
        public bool? HasBreath { get; set; }
        public bool? HasWebs { get; set; }
        public HideType? HideType { get; set; }
        public int? Hides { get; set; }
        public Poison HitPoison { get; set; }
        public PropValue HitsMax { get; set; }
        public PropValue Hue { get; set; }
        public bool? InitialInnocent { get; set; }
        public PropValue Karma { get; set; }
        public int? LootItemChance { get; set; }
        public int? LootItemLevel { get; set; }
        public string LootTable { get; set; }
        public PropValue ManaMaxSeed { get; set; }
        public PropValue Meat { get; set; }
        public PropValue MinTameSkill { get; set; }
        public OppositionGroup OppositionGroup { get; set; }
        public PropValue PerceptionRange { get; set; }
        public int? ProvokeSkillOverride { get; set; }
        public Race Race { get; set; }
        public TimeSpan? ReacquireDelay { get; set; }
        public TimeSpan? RiseCreatureDelay { get; set; }
        public Type RiseCreatureType { get; set; }
        public bool? SaySpellMantra { get; set; }
        public InhumanSpeech SpeechType { get; set; }
        public PropValue StamMaxSeed { get; set; }
        public bool? Tamable { get; set; }
        public bool? TargetAcquireExhaustion { get; set; }
        public int? Team { get; set; }
        public string Title { get; set; }
        public PropValue TreasureMapLevel { get; set; }
        public List<Type> PreferredSpells { get; set; }
        public Dictionary<ElementalType, PropValue> Resistances { get; set; }
        public Dictionary<SkillName, PropValue> Skills { get; set; }
        public WeaponAbility WeaponAbility { get; set; }
        public double? WeaponAbilityChance { get; set; }
        public CreatureAttack Attack { get; set; }
        public List<CreatureEquip> Equipment { get; set; }

        private static readonly Action<CreatureProperties, BaseCreature> MapAction
            = ZuluUtil.BuildMapAction<CreatureProperties, BaseCreature>();

        public void ApplyTo<T>(T dest) where T : BaseCreature
        {
            // If dest null throw an exception
            if (dest == null)
                throw new ArgumentNullException($"Failed to apply properties to {nameof(dest)}");

            MapAction(this, dest);

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

            Dress(dest);
        }

        private void Dress(BaseCreature dest)
        {
            if (!dest.Items.Any() && Equipment != null && Equipment.Any())
            {
                foreach (var equip in Equipment)
                {
                    if (equip.ItemType == null)
                        continue;

                    var item = equip.ItemType.CreateInstance<Item>();

                    if (item == null)
                        continue;

                    if (equip.Name != null)
                        item.Name = equip.Name;

                    if (equip.Hue != null)
                        item.Hue = equip.Hue;

                    if (item is BaseArmor armor && equip.ArmorRating != null)
                        armor.BaseArmorRating = (int) equip.ArmorRating;

                    dest.AddItem(item);
                    item.Movable = false;
                }
            }


            if (Attack != null)
            {
                dest.DamageMin = (int) Attack.Damage.Min;
                dest.DamageMax = (int) (Attack.Damage.Max ?? Attack.Damage.Min);

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
                }
            }
        }
    }
}