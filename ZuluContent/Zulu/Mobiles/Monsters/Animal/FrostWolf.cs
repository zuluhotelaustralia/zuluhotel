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
    public class FrostWolf : BaseCreature
    {
        static FrostWolf()
        {
            CreatureProperties.Register<FrostWolf>(new CreatureProperties
            {
                // DataElementId = frostwolf,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = frostwolf,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0xE8 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0x0482,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0xe1,
                CorpseNameOverride = "corpse of a frost wolf",
                CreatureType = CreatureType.Animal,
                DamageMax = 20,
                DamageMin = 8,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Wolf,
                HitsMax = 135,
                Hue = 0x0482,
                Int = 35,
                ManaMaxSeed = 25,
                MinTameSkill = 75,
                Name = "a frost wolf",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 80,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 120}
                },
                StamMaxSeed = 60,
                Str = 135,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public FrostWolf() : base(CreatureProperties.Get<FrostWolf>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frost Wolf Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xE8,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FrostWolf(Serial serial) : base(serial)
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