

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
    public class SnowOrcWarrior : BaseCreature
    {
        static SnowOrcWarrior() => CreatureProperties.Register<SnowOrcWarrior>(new CreatureProperties
        {
            // DataElementId = snoworc3,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = snoworc3,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x23D /* Weapon */,
            // hostile = 1,
            // lootgroup = 44,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 55 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x29,
            CorpseNameOverride = "corpse of <random> the snow orc warrior",
            CreatureType = CreatureType.Orc,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 160,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 175,
            Hue = 1154,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "<random> the snow orc warrior",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 90,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 90 },
                { SkillName.Tactics, 85 },
                { SkillName.MagicResist, 60 },
            },
            StamMaxSeed = 60,
            Str = 175,
            VirtualArmor = 30,

        });


        [Constructible]
public SnowOrcWarrior() : base(CreatureProperties.Get<SnowOrcWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Captain Weapon",
                Speed = 55,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public SnowOrcWarrior(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
