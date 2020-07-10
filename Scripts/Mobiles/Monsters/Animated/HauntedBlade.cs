

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
    public class HauntedBlade : BaseCreature
    {
        static HauntedBlade() => CreatureProperties.Register<HauntedBlade>(new CreatureProperties
        {
            // DataElementId = hauntedblade,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = hauntedblade,
            // HitSound = 0x23C /* Weapon */,
            // hostile = 1,
            // lootgroup = 48,
            // MissSound = 0x23A /* Weapon */,
            // script = killpcs,
            // speech = 6,
            // Speed = 45 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x266,
            CorpseNameOverride = "corpse of a haunted blade",
            CreatureType = CreatureType.Animated,
            DamageMax = 17,
            DamageMin = 5,
            Dex = 250,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 180,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "a haunted blade",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 60,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Macing, 85 },
                { SkillName.Tactics, 90 },
            },
            StamMaxSeed = 50,
            Str = 180,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public HauntedBlade() : base(CreatureProperties.Get<HauntedBlade>())
        {
            // Add customization here

  
        }

        public HauntedBlade(Serial serial) : base(serial) {}

  

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