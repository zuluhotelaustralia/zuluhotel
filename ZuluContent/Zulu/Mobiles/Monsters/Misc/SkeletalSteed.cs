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
    public class SkeletalSteed : BaseCreature
    {
        static SkeletalSteed()
        {
            CreatureProperties.Register<SkeletalSteed>(new CreatureProperties
            {
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0xAB,
                // AttackMissSound = 0x239,
                // AttackSpeed = 35,
                // CProp_noloot = i1,
                // damagedsound = 0xac,
                // DataElementId = skeletalsteed,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // food = meat,
                // guardignore = 1,
                // herdskill = 130,
                // idlesound1 = 0xa9,
                // idlesound2 = 0xaa,
                // orneriness = 4,
                // script = animal,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Animal /* animal */,
                AlwaysAttackable = true,
                BaseSoundID = 168,
                Body = 0x85,
                CorpseNameOverride = "corpse of a skeletal steed",
                DamageMax = 16,
                DamageMin = 2,
                Dex = 55,
                Fame = 3,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 300,
                Hue = 0,
                Int = 60,
                Karma = 3,
                ManaMaxSeed = 60,
                MinTameSkill = 120,
                Name = "a skeletal steed",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 50},
                    {SkillName.Wrestling, 80}
                },
                StamMaxSeed = 55,
                Str = 500,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public SkeletalSteed() : base(CreatureProperties.Get<SkeletalSteed>())
        {
            // Add customization here
        }

        [Constructible]
        public SkeletalSteed(Serial serial) : base(serial)
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