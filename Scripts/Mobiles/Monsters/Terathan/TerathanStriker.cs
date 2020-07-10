

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
    public class TerathanStriker : BaseCreature
    {
        static TerathanStriker() => CreatureProperties.Register<TerathanStriker>(new CreatureProperties
        {
            // DataElementId = terathanstriker,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = terathandrone,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x252 /* Weapon */,
            // hostile = 1,
            // lootgroup = 64,
            // MissSound = 0x253 /* Weapon */,
            // script = killpcssprinters,
            // speech = 6,
            // Speed = 43 /* Weapon */,
            // Swordsmanship = 100,
            // TrueColor = 11,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcssprinters */,
            AlwaysMurderer = true,
            Body = 0x47,
            CorpseNameOverride = "corpse of a Terathan Striker",
            CreatureType = CreatureType.Terathan,
            DamageMax = 44,
            DamageMin = 8,
            Dex = 635,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 370,
            Hue = 11,
            Int = 55,
            ManaMaxSeed = 0,
            Name = "a Terathan Striker",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 75 },
                { ElementalType.Energy, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.MagicResist, 100 },
                { SkillName.Parry, 100 },
            },
            StamMaxSeed = 635,
            Str = 370,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public TerathanStriker() : base(CreatureProperties.Get<TerathanStriker>())
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

        public TerathanStriker(Serial serial) : base(serial) {}

  

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