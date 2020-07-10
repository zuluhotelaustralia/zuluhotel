

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
    public class StoneOstard : BaseCreature
    {
        static StoneOstard() => CreatureProperties.Register<StoneOstard>(new CreatureProperties
        {
            // DataElementId = stoneostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = stoneostard,
            // food = veggie,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x254 /* Weapon */,
            // MissSound = 0x256 /* Weapon */,
            // script = animal,
            // Speed = 35 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            Body = 0xdb,
            CorpseNameOverride = "corpse of a stone ostard",
            CreatureType = CreatureType.Animal,
            DamageMax = 48,
            DamageMin = 13,
            Dex = 210,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Ostard,
            HitsMax = 250,
            Hue = 1154,
            Int = 35,
            ManaMaxSeed = 125,
            MinTameSkill = 75,
            Name = "a stone ostard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 90,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 70 },
                { SkillName.Tactics, 80 },
                { SkillName.Macing, 80 },
            },
            StamMaxSeed = 125,
            Str = 250,
            Tamable = true,
            VirtualArmor = 20,
  
        });

        [Constructable]
        public StoneOstard() : base(CreatureProperties.Get<StoneOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tropical Frenzied Ostard Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256,
            });
  
  
        }

        public StoneOstard(Serial serial) : base(serial) {}

  

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