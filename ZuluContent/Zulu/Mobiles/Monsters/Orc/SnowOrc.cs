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
    public class SnowOrc : BaseCreature
    {
        static SnowOrc()
        {
            CreatureProperties.Register<SnowOrc>(new CreatureProperties
            {
                // DataElementId = snoworc2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = snoworc2,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                LootTable = "43",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 45 /* Weapon */,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x29,
                CorpseNameOverride = "corpse of <random> the snow orc",
                CreatureType = CreatureType.Orc,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 135,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 150,
                Hue = 1154,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "<random> the snow orc",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 95,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Macing, 85},
                    {SkillName.Tactics, 80}
                },
                StamMaxSeed = 50,
                Str = 150,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public SnowOrc() : base(CreatureProperties.Get<SnowOrc>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Elite Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SnowOrc(Serial serial) : base(serial)
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