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
    public class RedSolenWarrior : BaseCreature
    {
        static RedSolenWarrior()
        {
            CreatureProperties.Register<RedSolenWarrior>(new CreatureProperties
            {
                // AccuracyAdjustment = 35,
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0xB6,
                // AttackMissSound = 0x28A,
                // AttackSpeed = 25,
                // damagedsound = 0xBD,
                // DataElementId = redsolenwarrior,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // hostile = 1,
                // idlesound1 = 0x463,
                // idlesound2 = 0x464,
                // lootgroup = 51,
                // MagicAdjustment = 35,
                // MagicItemChance = 100,
                // script = killpcs,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 265,
                Body = 782,
                CorpseNameOverride = "corpse of a red solen warrior",
                DamageMax = 25,
                DamageMin = 10,
                Dex = 125,
                Fame = 3,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 400,
                Hue = 0,
                Int = 60,
                Karma = 3,
                ManaMaxSeed = 60,
                Name = "a red solen warrior",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 80,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 80},
                    {SkillName.Wrestling, 80}
                },
                StamMaxSeed = 125,
                Str = 500,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public RedSolenWarrior() : base(CreatureProperties.Get<RedSolenWarrior>())
        {
            // Add customization here
        }

        [Constructible]
        public RedSolenWarrior(Serial serial) : base(serial)
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