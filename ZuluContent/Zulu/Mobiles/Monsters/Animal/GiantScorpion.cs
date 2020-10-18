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
    public class GiantScorpion : BaseCreature
    {
        static GiantScorpion()
        {
            CreatureProperties.Register<GiantScorpion>(new CreatureProperties
            {
                // DataElementId = scorp,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = scorp,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x190 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // Swordsmanship = 85,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 397,
                Body = 0x30,
                CorpseNameOverride = "corpse of a giant scorpion",
                CreatureType = CreatureType.Animal,
                DamageMax = 24,
                DamageMin = 3,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitPoison = Poison.Regular,
                HitsMax = 100,
                Hue = 0,
                Int = 35,
                ManaMaxSeed = 25,
                MinTameSkill = 70,
                Name = "a giant scorpion",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.Poisoning, 90},
                    {SkillName.MagicResist, 40},
                    {SkillName.Tactics, 70}
                },
                StamMaxSeed = 80,
                Str = 100,
                Tamable = true,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public GiantScorpion() : base(CreatureProperties.Get<GiantScorpion>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Scorp Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x190,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantScorpion(Serial serial) : base(serial)
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