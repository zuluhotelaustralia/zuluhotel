

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class OphidianDefender : BaseCreature
    {
        static OphidianDefender() => CreatureProperties.Register<OphidianDefender>(new CreatureProperties
        {
            // CProp_EarthProtection = i4,
            // DataElementId = ophidiandefender,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ophidiandefender,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 70,
            // MissSound = 0x169 /* Weapon */,
            // script = killpcssprinters,
            // Speed = 47 /* Weapon */,
            // TrueColor = 0,
            // virtue = 2,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcssprinters */,
            AlwaysMurderer = true,
            Body = 0x56,
            ClassLevel = 3,
            ClassSpec = SpecName.Warrior,
            CorpseNameOverride = "corpse of an Ophidian Defender",
            CreatureType = CreatureType.Ophidian,
            DamageMax = 44,
            DamageMin = 8,
            Dex = 210,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Serpent,
            HitsMax = 250,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "an Ophidian Defender",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 110,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 200 },
                { SkillName.Magery, 80 },
                { SkillName.Macing, 100 },
                { SkillName.Tactics, 90 },
                { SkillName.MagicResist, 130 },
            },
            StamMaxSeed = 70,
            Str = 2050,
            VirtualArmor = 25,

        });


        [Constructible]
public OphidianDefender() : base(CreatureProperties.Get<OphidianDefender>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Defender Weapon",
                Speed = 47,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169,
            });


        }

        [Constructible]
public OphidianDefender(Serial serial) : base(serial) {}



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
