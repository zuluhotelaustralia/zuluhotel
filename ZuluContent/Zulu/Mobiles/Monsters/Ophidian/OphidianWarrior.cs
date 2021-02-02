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
    public class OphidianWarrior : BaseCreature
    {
        static OphidianWarrior()
        {
            CreatureProperties.Register<OphidianWarrior>(new CreatureProperties
            {
                // DataElementId = ophidianwarrior,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianwarrior,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "67",
                // MissSound = 0x169 /* Weapon */,
                // script = killpcssprinters,
                // Speed = 37 /* Weapon */,
                // TrueColor = 0,
                // virtue = 3,
                ActiveSpeed = 0.150,
                AiType = AIType.AI_Melee /* killpcssprinters */,
                AlwaysMurderer = true,
                Body = 0x56,
                CorpseNameOverride = "corpse of an Ophidian Warrior",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 41,
                DamageMin = 11,
                Dex = 160,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 600,
                Hue = 0,
                Int = 160,
                ManaMaxSeed = 0,
                Name = "an Ophidian Warrior",
                PassiveSpeed = 0.300,
                PerceptionRange = 10,
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.Magery, 100},
                    {SkillName.Macing, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 100}
                },
                StamMaxSeed = 320,
                Str = 300,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public OphidianWarrior() : base(CreatureProperties.Get<OphidianWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Warrior Weapon",
                Speed = 37,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianWarrior(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}