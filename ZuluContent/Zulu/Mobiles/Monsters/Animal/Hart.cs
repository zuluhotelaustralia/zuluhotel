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
    public class Hart : BaseCreature
    {
        static Hart()
        {
            CreatureProperties.Register<Hart>(new CreatureProperties
            {
                // DataElementId = hart,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = hart,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x84 /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = animal,
                // Speed = 30 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Animal /* animal */,
                AlwaysAttackable = true,
                BaseSoundID = 128,
                Body = 0xea,
                CorpseNameOverride = "corpse of a hart",
                CreatureType = CreatureType.Animal,
                DamageMax = 8,
                DamageMin = 2,
                Dex = 90,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 70,
                Hue = 33784,
                Int = 35,
                ManaMaxSeed = 0,
                MinTameSkill = 60,
                Name = "a hart",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 35},
                    {SkillName.MagicResist, 35},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 75}
                },
                StamMaxSeed = 50,
                Str = 70,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Hart() : base(CreatureProperties.Get<Hart>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Hart Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x84,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Hart(Serial serial) : base(serial)
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