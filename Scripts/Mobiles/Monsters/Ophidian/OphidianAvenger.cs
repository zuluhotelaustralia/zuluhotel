

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
    public class OphidianAvenger : BaseCreature
    {
        static OphidianAvenger() => CreatureProperties.Register<OphidianAvenger>(new CreatureProperties
        {
            // CProp_EarthProtection = i4,
            // DataElementId = ophidianavenger,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ophidianavenger,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 71,
            // MagicItemChance = 20,
            // MagicItemLevel = 2,
            // MissSound = 0x169 /* Weapon */,
            // script = killpcssprinters,
            // Speed = 37 /* Weapon */,
            // TrueColor = 0,
            // virtue = 3,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcssprinters */,
            AlwaysMurderer = true,
            Body = 0x56,
            CorpseNameOverride = "corpse of an Ophidian Avenger",
            CreatureType = CreatureType.Ophidian,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 210,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Serpent,
            HitPoison = Poison.Regular,
            HitsMax = 350,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "an Ophidian Avenger",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 110,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 120 },
                { SkillName.Magery, 80 },
                { SkillName.Macing, 130 },
                { SkillName.Tactics, 130 },
                { SkillName.MagicResist, 130 },
            },
            StamMaxSeed = 70,
            Str = 350,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public OphidianAvenger() : base(CreatureProperties.Get<OphidianAvenger>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidianavenger Weapon",
                Speed = 37,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169,
            });
  
  
        }

        public OphidianAvenger(Serial serial) : base(serial) {}

  

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