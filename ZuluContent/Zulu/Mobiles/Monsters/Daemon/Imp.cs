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
    public class Imp : BaseCreature
    {
        static Imp()
        {
            CreatureProperties.Register<Imp>(new CreatureProperties
            {
                // DataElementId = imp,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = imp,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1A9 /* Weapon */,
                // hostile = 1,
                LootTable = "58",
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 30 /* Weapon */,
                // TrueColor = 0x23,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BaseSoundID = 422,
                Body = 0x27,
                CorpseNameOverride = "corpse of an imp",
                CreatureType = CreatureType.Daemon,
                DamageMax = 30,
                DamageMin = 2,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 175,
                Hue = 0x23,
                Int = 85,
                ManaMaxSeed = 0,
                MinTameSkill = 90,
                Name = "an imp",
                PerceptionRange = 10,
                ProvokeSkillOverride = 75,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 50},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 50,
                Str = 175,
                Tamable = true,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public Imp() : base(CreatureProperties.Get<Imp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Imp Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A9,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Imp(Serial serial) : base(serial)
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