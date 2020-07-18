

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
    public class Gremlin : BaseCreature
    {
        static Gremlin() => CreatureProperties.Register<Gremlin>(new CreatureProperties
        {
            // DataElementId = gremlin,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = gremlin,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1A9 /* Weapon */,
            // hostile = 1,
            // lootgroup = 58,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 0x07D1,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 422,
            Body = 0x27,
            CorpseNameOverride = "corpse of a gremlin",
            CreatureType = CreatureType.Daemon,
            DamageMax = 30,
            DamageMin = 2,
            Dex = 60,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 205,
            Hue = 0x07D1,
            Int = 85,
            ManaMaxSeed = 75,
            MinTameSkill = 60,
            Name = "a gremlin",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 55,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 70 },
                { SkillName.Tactics, 80 },
                { SkillName.Macing, 60 },
            },
            StamMaxSeed = 50,
            Str = 205,
            Tamable = true,
            VirtualArmor = 15,
  
        });

        [Constructable]
        public Gremlin() : base(CreatureProperties.Get<Gremlin>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Gremlin Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A9,
                MissSound = 0x239,
            });
  
  
        }

        public Gremlin(Serial serial) : base(serial) {}

  

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