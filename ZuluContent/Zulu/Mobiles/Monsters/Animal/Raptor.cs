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
    public class Raptor : BaseCreature
    {
        static Raptor()
        {
            CreatureProperties.Register<Raptor>(new CreatureProperties
            {
                // DataElementId = Raptor,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = Raptor,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1DE /* Weapon */,
                // lootgroup = 48,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // TrueColor = 0x0455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xD2,
                CorpseNameOverride = "corpse of a Raptor",
                CreatureType = CreatureType.Animal,
                DamageMax = 45,
                DamageMin = 3,
                Dex = 160,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 110,
                Hue = 0x0455,
                Int = 90,
                ManaMaxSeed = 80,
                MinTameSkill = 95,
                Name = "a Raptor",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 30,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150}
                },
                StamMaxSeed = 90,
                Str = 110,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Raptor() : base(CreatureProperties.Get<Raptor>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Raptor Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1DE,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Raptor(Serial serial) : base(serial)
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