

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
    public class GuardDog : BaseCreature
    {
        static GuardDog() => CreatureProperties.Register<GuardDog>(new CreatureProperties
        {
            // DataElementId = Guarddog,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = Guarddog,
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
            Body = 0xe1,
            CorpseNameOverride = "corpse of a guard dog",
            CreatureType = CreatureType.Animal,
            DamageMax = 10,
            DamageMin = 2,
            Dex = 80,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 50,
            Hue = 33784,
            Int = 35,
            ManaMaxSeed = 0,
            MinTameSkill = 10,
            Name = "a guard dog",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 60 },
                { SkillName.Macing, 75 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 50,
            Str = 50,
            Tamable = true,
            VirtualArmor = 15,

        });


        [Constructible]
public GuardDog() : base(CreatureProperties.Get<GuardDog>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Guard Dog Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x88,
                MissSound = 0x239,
            });


        }

        [Constructible]
public GuardDog(Serial serial) : base(serial) {}



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
