

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
    public class BirdDog : BaseCreature
    {
        static BirdDog() => CreatureProperties.Register<BirdDog>(new CreatureProperties
        {
            // DataElementId = birddog,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = birddog,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x88 /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = animal,
            // Speed = 35 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            BaseSoundID = 133,
            Body = 0xd9,
            CorpseNameOverride = "corpse of a bird dog",
            CreatureType = CreatureType.Animal,
            DamageMax = 6,
            DamageMin = 1,
            Dex = 70,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 30,
            Hue = 33784,
            Int = 25,
            ManaMaxSeed = 0,
            MinTameSkill = 10,
            Name = "a bird dog",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 20,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 50 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 50,
            Str = 30,
            Tamable = true,
            VirtualArmor = 5,

        });


        [Constructible]
public BirdDog() : base(CreatureProperties.Get<BirdDog>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bird Dog Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x88,
                MissSound = 0x239,
            });


        }

        [Constructible]
public BirdDog(Serial serial) : base(serial) {}



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
