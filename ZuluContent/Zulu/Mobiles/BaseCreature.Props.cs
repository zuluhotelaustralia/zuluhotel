using System;
using System.Collections.Generic;
using Server.Engines.Magic.HitScripts;
using Server.Items;
using Server.Misc;
using Scripts.Zulu.Engines.Classes;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace Server.Mobiles
{
    public partial class BaseCreature : Mobile, IEnchanted, IZuluClassed
    {
        public EnchantmentDictionary Enchantments { get; } = new();

        private ZuluClass m_ZuluClass;

        public virtual CreatureProperties InitProperties
        {
            get { return CreatureProperties.Get(GetType()); }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalWaterResist
        {
            get => Enchantments.Get((WaterProtection e) => e.Value);
            set => Enchantments.Set((WaterProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalAirResist
        {
            get => Enchantments.Get((AirProtection e) => e.Value);
            set => Enchantments.Set((AirProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPhysicalResist 
        {
            get => Enchantments.Get((PhysicalProtection e) => e.Value);
            set => Enchantments.Set((PhysicalProtection e) => e.Value = value);
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalFireResist
        {
            get => Enchantments.Get((FireProtection e) => e.Value);
            set => Enchantments.Set((FireProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalPoisonResist
        {
            get => Enchantments.Get((PermPoisonProtection e) => e.Value);
            set => Enchantments.Set((PermPoisonProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalEarthResist
        {
            get => Enchantments.Get((EarthProtection e) => e.Value);
            set => Enchantments.Set((EarthProtection e) => e.Value = value);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ElementalNecroResist
        {
            get => Enchantments.Get((NecroProtection e) => e.Value);
            set => Enchantments.Set((NecroProtection e) => e.Value = value);
        }

        public virtual bool InitialInnocent
        {
            get { return InitProperties?.InitialInnocent ?? false; }
        }

        public virtual bool AlwaysMurderer
        {
            get { return InitProperties?.AlwaysMurderer ?? false; }
        }

        public virtual bool AlwaysAttackable
        {
            get { return InitProperties?.AlwaysAttackable ?? false; }
        }

        public virtual bool TargetAcquireExhaustion
        {
            get { return InitProperties.TargetAcquireExhaustion; }
        }

        public virtual Type RiseCreatureType
        {
            get { return InitProperties?.RiseCreatureType; }
        }

        public virtual TimeSpan RiseCreatureDelay
        {
            get { return InitProperties?.RiseCreatureDelay ?? TimeSpan.FromSeconds(5.0); }
        }

        public virtual List<Type> PreferredSpells
        {
            get { return InitProperties?.PreferredSpells; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual CreatureType CreatureType => InitProperties?.CreatureType ?? CreatureType.None;

        public virtual double WeaponAbilityChance
        {
            get { return InitProperties?.WeaponAbilityChance ?? 0.4; }
        }

        public virtual WeaponAbility GetWeaponAbility()
        {
            return InitProperties?.WeaponAbility;
        }

        public virtual bool HasBreath { get; set; } = false;

        public virtual Poison HitPoison { get; set; } = null;
        public virtual double HitPoisonChance { get; } = 0.5;
        public virtual bool CanFly { get; set; } = false;
        public virtual InhumanSpeech SpeechType { get; set; } = null;

        public virtual OppositionGroup OppositionGroup { get; set; } = null;
        public virtual TimeSpan ReacquireDelay { get; set; } = TimeSpan.FromSeconds(10.0);
        public virtual int TreasureMapLevel { get; set; } = -1;

        public virtual bool CanRummageCorpses { get; set; } = true;

        public virtual int Meat { get; set; } = 0;

        public virtual int Hides { get; set; } = 0;

        public virtual HideType HideType { get; set; } = HideType.Regular;

        public virtual bool AutoDispel { get; set; } = false;
        public virtual bool BardImmune { get; set; } = false;
        public virtual int ProvokeSkillOverride { get; set; } = -1;

        public virtual bool SaySpellMantra
        {
            get { return InitProperties?.SaySpellMantra ?? false; }
        }


        public virtual ZuluClass ZuluClass
        {
            get
            {
                if (m_ZuluClass == null)
                {
                    m_ZuluClass = new ZuluClass(this);
                    if (InitProperties != null)
                    {
                        m_ZuluClass.Type = InitProperties.ClassType;
                        m_ZuluClass.Level = InitProperties.ClassLevel;
                    }
                }

                return m_ZuluClass;
            }
        }

        public virtual void OnRiseSpawn(Type creatureType, Container corpse)
        {
            if (!creatureType.IsSubclassOf(typeof(BaseCreature)))
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
                    var creature = (BaseCreature) Activator.CreateInstance(creatureType, null);
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

    }
}