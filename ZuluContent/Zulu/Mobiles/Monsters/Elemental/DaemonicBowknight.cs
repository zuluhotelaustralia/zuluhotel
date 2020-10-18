using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class DaemonicBowknight : BaseCreature
    {
        static DaemonicBowknight()
        {
            CreatureProperties.Register<DaemonicBowknight>(new CreatureProperties
            {
                // ammoamount = 300,
                // ammotype = 0xEED,
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i8,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i8,
                // DataElementId = daemonicbowknight,
                // DataElementType = NpcTemplate,
                // Equip = daemonicbowknight,
                // graphic = 0x13B2 /* Weapon */,
                // HitSound = 0xFD /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 50,
                // MagicItemLevel = 5,
                // missileweapon = xbowman,
                // MissSound = 0x239 /* Weapon */,
                // script = explosionkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0x4001,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* explosionkillpcs */,
                AlwaysMurderer = true,
                Body = 318,
                CorpseNameOverride = "corpse of a Daemonic Bowknight",
                CreatureType = CreatureType.Elemental,
                DamageMax = 57,
                DamageMin = 17,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 7,
                HitsMax = 2250,
                Hue = 0x4001,
                Int = 55,
                ManaMaxSeed = 0,
                Name = "a Daemonic Bowknight",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Archery, 130},
                    {SkillName.Macing, 175},
                    {SkillName.MagicResist, 60},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 500,
                Str = 2250,
                Tamable = false,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public DaemonicBowknight() : base(CreatureProperties.Get<DaemonicBowknight>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Name = "Tainted Ranger Weapon",
                Hue = 0x0493,
                Speed = 35,
                Skill = SkillName.Archery,
                EffectID = 0x37C3,
                Animation = (WeaponAnimation) 0x12,
                MissSound = 0x239,
                HitSound = 0xFD,
                MaxHitPoints = 300,
                HitPoints = 300,
                MaxRange = 7
            });
        }

        [Constructible]
        public DaemonicBowknight(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}