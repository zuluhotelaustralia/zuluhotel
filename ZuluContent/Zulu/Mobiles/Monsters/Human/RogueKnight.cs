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
    public class RogueKnight : BaseCreature
    {
        static RogueKnight()
        {
            CreatureProperties.Register<RogueKnight>(new CreatureProperties
            {
                // DataElementId = rogueknight,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rogueknight,
                // HitSound = 0x238 /* Weapon */,
                // hostile = 1,
                LootTable = "59",
                LootItemChance = 1,
                // MissSound = 0x233 /* Weapon */,
                // Parrying = 150,
                // script = killpcs,
                // Speed = 15 /* Weapon */,
                // Swordsmanship = 150,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a rogue knight",
                CreatureType = CreatureType.Human,
                DamageMax = 35,
                DamageMin = 10,
                Dex = 140,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 350,
                Hue = 33784,
                Int = 80,
                ManaMaxSeed = 0,
                Name = "a rogue knight",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.Fencing, 130},
                    {SkillName.MagicResist, 65}
                },
                StamMaxSeed = 125,
                Str = 350,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public RogueKnight() : base(CreatureProperties.Get<RogueKnight>())
        {
            // Add customization here
        }

        [Constructible]
        public RogueKnight(Serial serial) : base(serial)
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