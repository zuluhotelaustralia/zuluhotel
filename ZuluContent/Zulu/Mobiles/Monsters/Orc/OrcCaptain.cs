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
    public class OrcCaptain : BaseCreature
    {
        static OrcCaptain()
        {
            CreatureProperties.Register<OrcCaptain>(new CreatureProperties
            {
                // DataElementId = orccaptain,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orccaptain,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x23D /* Weapon */,
                LootTable = "42",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 55 /* Weapon */,
                // Swordsmanship = 100,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 7,
                CorpseNameOverride = "corpse of <random> the Orc Captain",
                CreatureType = CreatureType.Orc,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 190,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 285,
                Hue = 33784,
                Int = 40,
                ManaMaxSeed = 30,
                Name = "<random> the Orc Captain",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 60}
                },
                StamMaxSeed = 90,
                Str = 285,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public OrcCaptain() : base(CreatureProperties.Get<OrcCaptain>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Captain Weapon",
                Speed = 55,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcCaptain(Serial serial) : base(serial)
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