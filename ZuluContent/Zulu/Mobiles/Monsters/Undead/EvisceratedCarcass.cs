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
    public class EvisceratedCarcass : BaseCreature
    {
        static EvisceratedCarcass()
        {
            CreatureProperties.Register<EvisceratedCarcass>(new CreatureProperties
            {
                // DataElementId = evisceratedcarcass,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = evisceratedcarcass,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1DA /* Weapon */,
                // hostile = 1,
                // lootgroup = 131,
                // MagicItemChance = 50,
                // Magicitemlevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // Swordsmanship = 130,
                // TrueColor = 1290,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x03,
                ClassLevel = 3,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of an Eviscerated Carcass",
                CreatureType = CreatureType.Undead,
                DamageMax = 55,
                DamageMin = 25,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 1290,
                Int = 15,
                ManaMaxSeed = 5,
                Name = "an Eviscerated Carcass",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 120}
                },
                StamMaxSeed = 30,
                Str = 300,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public EvisceratedCarcass() : base(CreatureProperties.Get<EvisceratedCarcass>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Eviscerated Carcass Weapon",
                Speed = 25,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1DA,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EvisceratedCarcass(Serial serial) : base(serial)
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