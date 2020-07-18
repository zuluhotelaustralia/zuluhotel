

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
    public class Snoworc : BaseCreature
    {
        static Snoworc() => CreatureProperties.Register<Snoworc>(new CreatureProperties
        {
            // DataElementId = snoworc1,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = snoworc1,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 29,
            // MissSound = 0x234 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x11,
            CorpseNameOverride = "corpse of <random> the snoworc",
            CreatureType = CreatureType.Orc,
            DamageMax = 25,
            DamageMin = 5,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 110,
            Hue = 1154,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "<random> the snoworc",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 60,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 100 },
                { SkillName.Hiding, 50 },
                { SkillName.Macing, 80 },
            },
            StamMaxSeed = 50,
            Str = 110,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public Snoworc() : base(CreatureProperties.Get<Snoworc>())
        {
            // Add customization here

  
        }

        public Snoworc(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}