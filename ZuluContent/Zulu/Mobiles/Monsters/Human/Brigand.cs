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
    public class Brigand : BaseCreature
    {
        static Brigand()
        {
            CreatureProperties.Register<Brigand>(new CreatureProperties
            {
                // DataElementId = brigandhorseman,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = brigandhorseman,
                // Graphic = 0x1440 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                LootTable = "47",
                LootItemChance = 1,
                // MissSound = 0x239 /* Weapon */,
                // Parrying = 80,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // Swordsmanship = 110,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a brigand",
                CreatureType = CreatureType.Human,
                DamageMax = 50,
                DamageMin = 5,
                Dex = 300,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 130,
                Hue = 0,
                Int = 105,
                ManaMaxSeed = 95,
                Name = "a brigand",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 94,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 85},
                    {SkillName.Fencing, 85},
                    {SkillName.MagicResist, 50}
                },
                StamMaxSeed = 50,
                Str = 130,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Brigand() : base(CreatureProperties.Get<Brigand>())
        {
            // Add customization here

            AddItem(new Cutlass
            {
                Movable = false,
                Name = "Brigand1 Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Brigand(Serial serial) : base(serial)
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