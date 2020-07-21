

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
    public class RedSolenWorker : BaseCreature
    {
        static RedSolenWorker() => CreatureProperties.Register<RedSolenWorker>(new CreatureProperties
        {
            // AttackAttribute = Wrestling,
            // AttackHitScript = :combat:wrestlinghitscript,
            // AttackHitSound = 0x26A,
            // AttackMissSound = 0x26B,
            // AttackSpeed = 20,
            // damagedsound = 0x8F,
            // DataElementId = redsolenworker,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // hostile = 1,
            // idlesound1 = 0x35,
            // idlesound2 = 0x2E4,
            // lootgroup = 23,
            // script = killpcs,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 442,
            Body = 781,
            CorpseNameOverride = "corpse of a red solen worker",
            DamageMax = 20,
            DamageMin = 10,
            Dex = 105,
            Fame = 1,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 72,
            Hue = 0,
            Int = 60,
            Karma = 1,
            ManaMaxSeed = 60,
            Name = "a red solen worker",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 65,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 65 },
                { SkillName.Wrestling, 60 },
            },
            StamMaxSeed = 105,
            Str = 400,
            VirtualArmor = 30,

        });


        [Constructible]
public RedSolenWorker() : base(CreatureProperties.Get<RedSolenWorker>())
        {
            // Add customization here


        }

        [Constructible]
public RedSolenWorker(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
