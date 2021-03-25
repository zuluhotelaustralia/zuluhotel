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
    public class CelestialOstard : BaseCreature
    {
        static CelestialOstard()
        {
            CreatureProperties.Register<CelestialOstard>(new CreatureProperties
            {
                // CProp_SuperOsty = i1,
                // DataElementId = celestialostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = celestialostard,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = daves_healer,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1176,
                // virtue = 8,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Healer /* daves_healer */,
                AutoDispel = true,
                BaseSoundID = 624,
                Body = 0xda,
                CanFly = true,
                CorpseNameOverride = "corpse of Celestial Ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 300,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 650,
                Hue = 1176,
                InitialInnocent = true,
                Int = 400,
                ManaMaxSeed = 200,
                MinTameSkill = 115,
                Name = "Celestial Ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 150,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Air, 50},
                    {ElementalType.Water, 50},
                    {ElementalType.Fire, 50},
                    {ElementalType.Necro, 50},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 5}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 140},
                    {SkillName.DetectHidden, 130},
                    {SkillName.Magery, 200}
                },
                StamMaxSeed = 175,
                Str = 650,
                Tamable = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public CelestialOstard() : base(CreatureProperties.Get<CelestialOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Great Wyrm Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239
            });
        }

        [Constructible]
        public CelestialOstard(Serial serial) : base(serial)
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