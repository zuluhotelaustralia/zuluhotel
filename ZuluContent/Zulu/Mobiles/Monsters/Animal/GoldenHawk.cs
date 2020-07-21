

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
    public class GoldenHawk : BaseCreature
    {
        static GoldenHawk() => CreatureProperties.Register<GoldenHawk>(new CreatureProperties
        {
            // DataElementId = goldenhawk,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = goldenhawk,
            // food = veggie,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x90 /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = animal,
            // Speed = 50 /* Weapon */,
            // TrueColor = 1127,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            Body = 0x05,
            CorpseNameOverride = "corpse of a golden hawk",
            CreatureType = CreatureType.Animal,
            DamageMax = 18,
            DamageMin = 3,
            Dex = 90,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 90,
            Hue = 1127,
            Int = 50,
            ManaMaxSeed = 40,
            MinTameSkill = 35,
            Name = "a golden hawk",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 30,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 90 },
            },
            StamMaxSeed = 80,
            Str = 90,
            Tamable = true,
            VirtualArmor = 20,

        });


        [Constructible]
public GoldenHawk() : base(CreatureProperties.Get<GoldenHawk>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Golden Hawk Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x90,
                MissSound = 0x239,
            });


        }

        [Constructible]
public GoldenHawk(Serial serial) : base(serial) {}



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
