

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
    public class HuntingDog : BaseCreature
    {
        static HuntingDog() => CreatureProperties.Register<HuntingDog>(new CreatureProperties
        {
            // DataElementId = HuntingDog,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = HuntingDog,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x88 /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = animal,
            // Speed = 30 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            BaseSoundID = 133,
            Body = 0xd9,
            CorpseNameOverride = "corpse of a hunting dog",
            CreatureType = CreatureType.Animal,
            DamageMax = 8,
            DamageMin = 2,
            Dex = 80,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 45,
            Hue = 33784,
            Int = 25,
            ManaMaxSeed = 0,
            MinTameSkill = 10,
            Name = "a hunting dog",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 30,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 60 },
                { SkillName.Macing, 55 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 50,
            Str = 45,
            Tamable = true,
            VirtualArmor = 5,

        });


        [Constructible]
public HuntingDog() : base(CreatureProperties.Get<HuntingDog>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Hunting Dog Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x88,
                MissSound = 0x239,
            });


        }

        [Constructible]
public HuntingDog(Serial serial) : base(serial) {}



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
