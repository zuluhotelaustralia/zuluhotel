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
    public class SnowOrcLord : BaseCreature
    {
        static SnowOrcLord()
        {
            CreatureProperties.Register<SnowOrcLord>(new CreatureProperties
            {
                // DataElementId = snoworclord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = snoworclord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B1 /* Weapon */,
                // lootgroup = 42,
                // MagicItemChance = 20,
                // MagicItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 55 /* Weapon */,
                // Swordsmanship = 110,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 7,
                CorpseNameOverride = "corpse of <random> the snow orc lord",
                CreatureType = CreatureType.Orc,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 210,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 180,
                Hue = 1154,
                Int = 30,
                ManaMaxSeed = 0,
                Name = "<random> the snow orc lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 100,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 90},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 80,
                Str = 180,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public SnowOrcLord() : base(CreatureProperties.Get<SnowOrcLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc King Weapon",
                Speed = 55,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B1,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SnowOrcLord(Serial serial) : base(serial)
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