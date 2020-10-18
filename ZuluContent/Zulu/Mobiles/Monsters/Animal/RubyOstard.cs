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
    public class RubyOstard : BaseCreature
    {
        static RubyOstard()
        {
            CreatureProperties.Register<RubyOstard>(new CreatureProperties
            {
                // DataElementId = rubyostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rubyostard,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = animal,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1645,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Animal /* animal */,
                AlwaysAttackable = true,
                Body = 0xdb,
                CorpseNameOverride = "corpse of a ruby ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 48,
                DamageMin = 13,
                Dex = 350,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 175,
                Hue = 1645,
                Int = 135,
                ManaMaxSeed = 125,
                MinTameSkill = 75,
                Name = "a ruby ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 80},
                    {SkillName.Macing, 80}
                },
                StamMaxSeed = 125,
                Str = 175,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public RubyOstard() : base(CreatureProperties.Get<RubyOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tropical Frenzied Ostard Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256
            });
        }

        [Constructible]
        public RubyOstard(Serial serial) : base(serial)
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