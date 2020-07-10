

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
    public class TerathanDrone : BaseCreature
    {
        static TerathanDrone() => CreatureProperties.Register<TerathanDrone>(new CreatureProperties
        {
            // DataElementId = terathandrone,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = terathandrone,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x252 /* Weapon */,
            // hostile = 1,
            // lootgroup = 64,
            // MissSound = 0x253 /* Weapon */,
            // script = killpcs,
            // speech = 6,
            // Speed = 43 /* Weapon */,
            // Swordsmanship = 50,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x47,
            CorpseNameOverride = "corpse of a Terathan Drone",
            CreatureType = CreatureType.Terathan,
            DamageMax = 44,
            DamageMin = 8,
            Dex = 135,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 70,
            Hue = 0,
            Int = 55,
            ManaMaxSeed = 0,
            Name = "a Terathan Drone",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 50 },
                { SkillName.MagicResist, 30 },
                { SkillName.Parry, 40 },
            },
            StamMaxSeed = 125,
            Str = 170,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public TerathanDrone() : base(CreatureProperties.Get<TerathanDrone>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Drone Weapon",
                Speed = 43,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x252,
                MissSound = 0x253,
            });
  
  
        }

        public TerathanDrone(Serial serial) : base(serial) {}

  

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