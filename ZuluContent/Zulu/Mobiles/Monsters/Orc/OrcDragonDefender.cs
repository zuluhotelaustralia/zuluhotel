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
    public class OrcDragonDefender : BaseCreature
    {
        static OrcDragonDefender()
        {
            CreatureProperties.Register<OrcDragonDefender>(new CreatureProperties
            {
                // DataElementId = orcdefender,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orcdefender,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B3 /* Weapon */,
                // hostile = 1,
                // lootgroup = 13,
                // MagicItemChance = 1,
                // MissSound = 0x1B1 /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 45 /* Weapon */,
                // TrueColor = 0x0455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 7,
                CorpseNameOverride = "corpse of <random> the Orc Dragon Defender",
                CreatureType = CreatureType.Orc,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 230,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 305,
                Hue = 0x0455,
                Int = 85,
                ManaMaxSeed = 75,
                Name = "<random> the Orc Dragon Defender",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 114,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 130}
                },
                StamMaxSeed = 50,
                Str = 305,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public OrcDragonDefender() : base(CreatureProperties.Get<OrcDragonDefender>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Defender Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B3,
                MissSound = 0x1B1
            });
        }

        [Constructible]
        public OrcDragonDefender(Serial serial) : base(serial)
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