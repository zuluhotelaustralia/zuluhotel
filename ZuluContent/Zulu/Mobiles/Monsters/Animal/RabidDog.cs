

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
    public class RabidDog : BaseCreature
    {
        static RabidDog() => CreatureProperties.Register<RabidDog>(new CreatureProperties
        {
            // DataElementId = rabiddog,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = rabiddog,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x88 /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 133,
            Body = 0xd9,
            CorpseNameOverride = "corpse of a rabid dog",
            CreatureType = CreatureType.Animal,
            DamageMax = 9,
            DamageMin = 3,
            Dex = 85,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 40,
            Hue = 33784,
            Int = 25,
            ManaMaxSeed = 15,
            MinTameSkill = 35,
            Name = "a rabid dog",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 20,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 60 },
                { SkillName.Macing, 60 },
            },
            StamMaxSeed = 50,
            Str = 40,
            Tamable = true,
            VirtualArmor = 5,

        });


        [Constructible]
public RabidDog() : base(CreatureProperties.Get<RabidDog>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rabiddog Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x88,
                MissSound = 0x239,
            });


        }

        [Constructible]
public RabidDog(Serial serial) : base(serial) {}



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
