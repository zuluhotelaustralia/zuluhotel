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
    public class OrcWarrior : BaseCreature
    {
        static OrcWarrior()
        {
            CreatureProperties.Register<OrcWarrior>(new CreatureProperties
            {
                // DataElementId = orc4,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orc4,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                LootTable = "43",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 30 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x29,
                CorpseNameOverride = "corpse of <random> the orc warrior",
                CreatureType = CreatureType.Orc,
                DamageMax = 29,
                DamageMin = 8,
                Dex = 190,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 160,
                Hue = 0,
                Int = 35,
                ManaMaxSeed = 25,
                Name = "<random> the orc warrior",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 95},
                    {SkillName.Tactics, 90},
                    {SkillName.MagicResist, 60}
                },
                StamMaxSeed = 70,
                Str = 160,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public OrcWarrior() : base(CreatureProperties.Get<OrcWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc4 Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcWarrior(Serial serial) : base(serial)
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