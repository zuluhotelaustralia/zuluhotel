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
    public class Skeleton : BaseCreature
    {
        static Skeleton()
        {
            CreatureProperties.Register<Skeleton>(new CreatureProperties
            {
                // buddyText = "Emos hetairos",
                // DataElementId = skeleton3,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = skeleton3,
                // HitSound = 0x23C /* Weapon */,
                // hostile = 1,
                // leaderText = "Ego akoloutheou",
                LootTable = "15",
                // MissSound = 0x23A /* Weapon */,
                // script = killpcs,
                // Speed = 47 /* Weapon */,
                // Swordsmanship = 55,
                // targetText = "Ego apokteinou",
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x39,
                CorpseNameOverride = "corpse of a skeleton",
                CreatureType = CreatureType.Undead,
                DamageMax = 16,
                DamageMin = 4,
                Dex = 70,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 60,
                Hue = 33784,
                Int = 30,
                ManaMaxSeed = 0,
                Name = "a skeleton",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 50},
                    {SkillName.Tactics, 60},
                    {SkillName.Macing, 50},
                    {SkillName.MagicResist, 20}
                },
                StamMaxSeed = 60,
                Str = 60,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public Skeleton() : base(CreatureProperties.Get<Skeleton>())
        {
            // Add customization here
        }

        [Constructible]
        public Skeleton(Serial serial) : base(serial)
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