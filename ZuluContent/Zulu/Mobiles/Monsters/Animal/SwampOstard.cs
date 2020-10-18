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
    public class SwampOstard : BaseCreature
    {
        static SwampOstard()
        {
            CreatureProperties.Register<SwampOstard>(new CreatureProperties
            {
                // DataElementId = swampostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = swampostard,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = animal,
                // Speed = 25 /* Weapon */,
                // TrueColor = 2001,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Animal /* animal */,
                AlwaysAttackable = true,
                Body = 0xdb,
                CorpseNameOverride = "corpse of a swamp ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 45,
                DamageMin = 10,
                Dex = 240,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 120,
                Hue = 2001,
                Int = 35,
                ManaMaxSeed = 0,
                MinTameSkill = 55,
                Name = "a swamp ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 40},
                    {SkillName.MagicResist, 40},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 60}
                },
                StamMaxSeed = 50,
                Str = 120,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public SwampOstard() : base(CreatureProperties.Get<SwampOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Golden Frenzied Ostard Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256
            });
        }

        [Constructible]
        public SwampOstard(Serial serial) : base(serial)
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