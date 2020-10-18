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
    public class DemonicOstard : BaseCreature
    {
        static DemonicOstard()
        {
            CreatureProperties.Register<DemonicOstard>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i250,
                // CProp_EarthProtection = i2,
                // CProp_HolyProtection = i2,
                // CProp_NecroProtection = i2,
                // CProp_PermMagicImmunity = i5,
                // CProp_SuperOsty = i1,
                // DataElementId = demonicostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = demonicostard,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1259,
                // virtue = 7,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                AutoDispel = true,
                BaseSoundID = 625,
                Body = 0xda,
                CorpseNameOverride = "corpse of a Demonic Ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 475,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 850,
                Hue = 1259,
                Int = 650,
                ManaMaxSeed = 150,
                MinTameSkill = 115,
                Name = "a Demonic Ostard",
                PerceptionRange = 10,
                ProvokeSkillOverride = 150,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 50},
                    {ElementalType.Water, 50}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 110},
                    {SkillName.Magery, 150},
                    {SkillName.DetectHidden, 120}
                },
                StamMaxSeed = 175,
                Str = 850,
                Tamable = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public DemonicOstard() : base(CreatureProperties.Get<DemonicOstard>())
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
        public DemonicOstard(Serial serial) : base(serial)
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