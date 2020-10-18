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
    public class TerathanChampion : BaseCreature
    {
        static TerathanChampion()
        {
            CreatureProperties.Register<TerathanChampion>(new CreatureProperties
            {
                // CProp_EarthProtection = i4,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = terathanchampion,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = terathanwarrior,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x24D /* Weapon */,
                // hostile = 1,
                // lootgroup = 63,
                // MissSound = 0x24E /* Weapon */,
                // script = killpcssprinters,
                // speech = 6,
                // Speed = 37 /* Weapon */,
                // Swordsmanship = 130,
                // TrueColor = 1127,
                ActiveSpeed = 0.150,
                AiType = AIType.AI_Melee /* killpcssprinters */,
                AlwaysMurderer = true,
                Body = 0x46,
                ClassLevel = 4,
                ClassSpec = SpecName.Warrior,
                CorpseNameOverride = "corpse of a Terathan Champion",
                CreatureType = CreatureType.Terathan,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 105,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 650,
                Hue = 1127,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "a Terathan Champion",
                PassiveSpeed = 0.300,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Physical, 100},
                    {ElementalType.Fire, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.MagicResist, 130},
                    {SkillName.Parry, 135}
                },
                StamMaxSeed = 70,
                Str = 650,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public TerathanChampion() : base(CreatureProperties.Get<TerathanChampion>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Warrior Weapon",
                Speed = 37,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x24D,
                MissSound = 0x24E
            });
        }

        [Constructible]
        public TerathanChampion(Serial serial) : base(serial)
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