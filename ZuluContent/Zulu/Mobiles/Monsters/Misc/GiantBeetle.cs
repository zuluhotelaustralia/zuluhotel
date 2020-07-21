

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
    public class GiantBeetle : BaseCreature
    {
        static GiantBeetle() => CreatureProperties.Register<GiantBeetle>(new CreatureProperties
        {
            // AttackAttribute = Wrestling,
            // AttackHitScript = :combat:wrestlinghitscript,
            // AttackHitSound = 0xAB,
            // AttackMissSound = 0x239,
            // AttackSpeed = 35,
            // CProp_noloot = i1,
            // damagedsound = 0xac,
            // DataElementId = giantbeetle,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // food = veggie,
            // guardignore = 1,
            // herdskill = 85,
            // idlesound1 = 0xa9,
            // idlesound2 = 0xaa,
            // orneriness = 3,
            // script = animal,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            BaseSoundID = 168,
            Body = 0x85,
            CorpseNameOverride = "corpse of a giant beetle",
            DamageMax = 16,
            DamageMin = 2,
            Dex = 100,
            Fame = 3,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 200,
            Hue = 0,
            Int = 500,
            Karma = 3,
            ManaMaxSeed = 500,
            MinTameSkill = 115,
            Name = "a giant beetle",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 100 },
                { SkillName.Wrestling, 100 },
            },
            StamMaxSeed = 100,
            Str = 300,
            Tamable = true,
            VirtualArmor = 20,

        });


        [Constructible]
public GiantBeetle() : base(CreatureProperties.Get<GiantBeetle>())
        {
            // Add customization here


        }

        [Constructible]
public GiantBeetle(Serial serial) : base(serial) {}



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
