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
    public class OrcishLord : BaseCreature
    {
        static OrcishLord()
        {
            CreatureProperties.Register<OrcishLord>(new CreatureProperties
            {
                // DataElementId = orc_lord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orc_lord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x23D /* Weapon */,
                // hostile = 1,
                LootTable = "42",
                LootItemChance = 6,
                LootItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 55 /* Weapon */,
                // Swordsmanship = 120,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0X07,
                CorpseNameOverride = "corpse of <random> the Orcish Lord",
                CreatureType = CreatureType.Orc,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 205,
                Hue = 33784,
                Int = 30,
                ManaMaxSeed = 0,
                Name = "<random> the Orcish Lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 75},
                    {ElementalType.Earth, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 80,
                Str = 205,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public OrcishLord() : base(CreatureProperties.Get<OrcishLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Lord Weapon",
                Speed = 55,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcishLord(Serial serial) : base(serial)
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