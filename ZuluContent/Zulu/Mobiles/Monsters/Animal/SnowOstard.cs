

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
    public class SnowOstard : BaseCreature
    {
        static SnowOstard() => CreatureProperties.Register<SnowOstard>(new CreatureProperties
        {
            // DataElementId = snowostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = snowostard,
            // food = veggie,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x254 /* Weapon */,
            // MissSound = 0x256 /* Weapon */,
            // script = animal,
            // Speed = 35 /* Weapon */,
            // TrueColor = 1156,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            Body = 0xdb,
            CorpseNameOverride = "corpse of a snow ostard",
            CreatureType = CreatureType.Animal,
            DamageMax = 64,
            DamageMin = 15,
            Dex = 400,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Ostard,
            HitsMax = 210,
            Hue = 1156,
            Int = 110,
            ManaMaxSeed = 100,
            MinTameSkill = 95,
            Name = "a snow ostard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 110,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 100 },
                { SkillName.MagicResist, 70 },
                { SkillName.Tactics, 90 },
                { SkillName.Macing, 100 },
            },
            StamMaxSeed = 150,
            Str = 210,
            Tamable = true,
            VirtualArmor = 35,

        });


        [Constructible]
public SnowOstard() : base(CreatureProperties.Get<SnowOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tropical Ostard Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256,
            });


        }

        [Constructible]
public SnowOstard(Serial serial) : base(serial) {}



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
