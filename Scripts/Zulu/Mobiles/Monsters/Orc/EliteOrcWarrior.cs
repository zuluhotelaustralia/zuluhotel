

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
    public class EliteOrcWarrior : BaseCreature
    {
        static EliteOrcWarrior() => CreatureProperties.Register<EliteOrcWarrior>(new CreatureProperties
        {
            // DataElementId = orcelite,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = orcelite,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 43,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // speech = 6,
            // Speed = 45 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x29,
            CorpseNameOverride = "corpse of an elite orc warrior",
            CreatureType = CreatureType.Orc,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 195,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 215,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "an elite orc warrior",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 100 },
                { SkillName.Tactics, 75 },
                { SkillName.MagicResist, 60 },
            },
            StamMaxSeed = 70,
            Str = 215,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public EliteOrcWarrior() : base(CreatureProperties.Get<EliteOrcWarrior>())
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
                MissSound = 0x239,
            });
  
  
        }

        public EliteOrcWarrior(Serial serial) : base(serial) {}

  

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