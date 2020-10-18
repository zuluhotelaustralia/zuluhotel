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
    public class GiantRockScorpion : BaseCreature
    {
        static GiantRockScorpion()
        {
            CreatureProperties.Register<GiantRockScorpion>(new CreatureProperties
            {
                // DataElementId = rockscorpion,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rockscorpion,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x190 /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1118,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 397,
                Body = 0x30,
                CorpseNameOverride = "corpse of a giant rock scorpion",
                CreatureType = CreatureType.Animal,
                DamageMax = 35,
                DamageMin = 11,
                Dex = 80,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitPoison = Poison.Greater,
                HitsMax = 230,
                Hue = 1118,
                Int = 40,
                ManaMaxSeed = 0,
                MinTameSkill = 80,
                Name = "a giant rock scorpion",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 130},
                    {SkillName.MagicResist, 50}
                },
                StamMaxSeed = 70,
                Str = 230,
                Tamable = true,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public GiantRockScorpion() : base(CreatureProperties.Get<GiantRockScorpion>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rock Scorpion Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x190,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantRockScorpion(Serial serial) : base(serial)
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