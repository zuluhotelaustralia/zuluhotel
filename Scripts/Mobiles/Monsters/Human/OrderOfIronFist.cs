

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
    public class OrderOfIronFist : BaseCreature
    {
        static OrderOfIronFist() => CreatureProperties.Register<OrderOfIronFist>(new CreatureProperties
        {
            // DataElementId = servantofcain,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = servantofcain,
            // guardignore = 1,
            // HitSound = 0x238 /* Weapon */,
            // hostile = 1,
            // lootgroup = 59,
            // MissSound = 0x233 /* Weapon */,
            // script = killpcs,
            // Speed = 15 /* Weapon */,
            // Swordsmanship = 150,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of <random>, Order of the Iron Fist",
            CreatureType = CreatureType.Human,
            DamageMax = 35,
            DamageMin = 10,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 300,
            Hue = 0,
            Int = 210,
            ManaMaxSeed = 200,
            Name = "<random>, Order of the Iron Fist",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 120 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 200,
            Str = 300,
  
        });

        [Constructable]
        public OrderOfIronFist() : base(CreatureProperties.Get<OrderOfIronFist>())
        {
            // Add customization here

  
        }

        public OrderOfIronFist(Serial serial) : base(serial) {}

  

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