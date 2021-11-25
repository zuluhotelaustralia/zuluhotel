using System;
using System.Collections.Generic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Server.Engines.Magic;
using Server.Network;
using Server.Spells;
using ZuluContent.Configuration.Types.Creatures;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace Server.Mobiles
{
    public partial class BaseCreature : Mobile, IEnchanted, IZuluClassed, IElementalResistible
    {
        public EnchantmentDictionary Enchantments { get; } = new();

        private ZuluClass m_ZuluClass;

        public virtual CreatureProperties InitProperties => this is BaseCreatureTemplate bt ? bt.Properties : null;

        #region IElementalResistible

        [CommandProperty(AccessLevel.GameMaster)]
        public int WaterResist => this.GetResist(ElementalType.Water);

        [CommandProperty(AccessLevel.GameMaster)]
        public int AirResist => this.GetResist(ElementalType.Air);

        [CommandProperty(AccessLevel.GameMaster)]
        public int PhysicalResist => this.GetResist(ElementalType.Physical);

        [CommandProperty(AccessLevel.GameMaster)]
        public int FireResist => this.GetResist(ElementalType.Fire);

        [CommandProperty(AccessLevel.GameMaster)]
        public int EarthResist => this.GetResist(ElementalType.Earth);

        [CommandProperty(AccessLevel.GameMaster)]
        public int NecroResist => this.GetResist(ElementalType.Necro);

        [CommandProperty(AccessLevel.GameMaster)]
        public int ParalysisProtection => this.GetResist(ElementalType.Paralysis);

        [CommandProperty(AccessLevel.GameMaster)]
        public int HealingBonus => this.GetResist(ElementalType.HealingBonus);

        [CommandProperty(AccessLevel.GameMaster)]
        public PoisonLevel PoisonImmunity => (PoisonLevel)this.GetResist(ElementalType.Poison);

        [CommandProperty(AccessLevel.GameMaster)]
        public SpellCircle MagicImmunity => this.GetResist(ElementalType.MagicImmunity);

        [CommandProperty(AccessLevel.GameMaster)]
        public SpellCircle MagicReflection => this.GetResist(ElementalType.MagicReflection);

        #endregion

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool InitialInnocent { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AlwaysMurderer { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AlwaysAttackable { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool TargetAcquireExhaustion { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual string RiseCreatureTemplate { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual TimeSpan RiseCreatureDelay { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual List<SpellEntry> PreferredSpells { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual CreatureType CreatureType { get; set; } = CreatureType.None;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual double WeaponAbilityChance { get; set; } = 0.4;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool HasBreath { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool HasWebs { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual Poison HitPoison { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual double HitPoisonChance { get; } = 0.5;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool CanFly { get; set; } = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual InhumanSpeech SpeechType { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual OppositionGroup OppositionGroup { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual TimeSpan ReacquireDelay { get; set; } = TimeSpan.FromSeconds(4);

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int TreasureMapLevel { get; set; } = -1;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool CanRummageCorpses { get; set; } = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int Meat { get; set; } = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int Hides { get; set; } = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual HideType HideType { get; set; } = HideType.Regular;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool AutoDispel { get; set; } = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool BardImmune { get; set; } = false;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int ProvokeSkillOverride { get; set; } = -1;

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool SaySpellMantra { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual string LootTable { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int LootItemLevel { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual int LootItemChance { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual ZuluClass ZuluClass
        {
            get
            {
                if (m_ZuluClass == null)
                {
                    m_ZuluClass = new ZuluClass(this);
                    if (InitProperties != null)
                    {
                        m_ZuluClass.Type = InitProperties.ClassType ?? ZuluClassType.None;
                        m_ZuluClass.Level = InitProperties.ClassLevel ?? 0;
                    }
                }

                return m_ZuluClass;
            }
        }

        public virtual WeaponAbility GetWeaponAbility() => InitProperties?.Attack?.Ability;
        public virtual void OnRiseSpawn(string creatureType, Container corpse)
        {
            if (!Creatures.Exists(creatureType))
                return;

            void Announce()
            {
                corpse.PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "* The corpse begins to stir! *");
            }

            var location = Location;
            var map = Map;
            var killer = LastKiller;

            void Rise()
            {
                try
                {
                    BaseCreature creature = creatureType;
                    if (creature == null)
                        return;

                    creature.MoveToWorld(location, map);
                    creature.Combatant = killer;
                    creature.FixedParticles(0, 0, 0, 0x13A7, EffectLayer.Waist);
                    Effects.PlaySound(creature.Location, creature.Map, 0x29);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                }
                finally
                {
                    corpse?.Delete();
                }
            }

            if (RiseCreatureDelay != TimeSpan.Zero)
                Timer.DelayCall(RiseCreatureDelay / 2, Announce);

            Timer.DelayCall(RiseCreatureDelay, Rise);
        }

        public static implicit operator BaseCreature(string template)
        {
            return Creatures.Create(template);
        }
    }
}