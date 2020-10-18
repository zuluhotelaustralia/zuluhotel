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
    public class HeavenlyOstard : BaseCreature
    {
        static HeavenlyOstard()
        {
            CreatureProperties.Register<HeavenlyOstard>(new CreatureProperties
            {
                // DataElementId = heavenlyostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = heavenlyostard,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = daves_healer,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1181,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Healer /* daves_healer */,
                AlwaysAttackable = true,
                Body = 0xdb,
                CorpseNameOverride = "corpse of a heavenly ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 64,
                DamageMin = 15,
                Dex = 400,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 250,
                Hue = 1181,
                Int = 160,
                ManaMaxSeed = 150,
                MinTameSkill = 105,
                Name = "a heavenly ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 90},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 200}
                },
                StamMaxSeed = 100,
                Str = 250,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public HeavenlyOstard() : base(CreatureProperties.Get<HeavenlyOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tropical Ostard Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256
            });
        }

        [Constructible]
        public HeavenlyOstard(Serial serial) : base(serial)
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