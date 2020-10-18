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
    public class HeavenlyFrenziedOstard : BaseCreature
    {
        static HeavenlyFrenziedOstard()
        {
            CreatureProperties.Register<HeavenlyFrenziedOstard>(new CreatureProperties
            {
                // DataElementId = heavenlyfrenziedostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = heavenlyfrenziedostard,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = daves_healer,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1181,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Healer /* daves_healer */,
                AlwaysAttackable = true,
                Body = 0xda,
                CorpseNameOverride = "corpse of a heavenly frenzied ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 48,
                DamageMin = 13,
                Dex = 400,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 225,
                Hue = 1181,
                Int = 185,
                ManaMaxSeed = 175,
                MinTameSkill = 110,
                Name = "a heavenly frenzied ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 110},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 110},
                    {SkillName.Macing, 160},
                    {SkillName.Magery, 225}
                },
                StamMaxSeed = 125,
                Str = 225,
                Tamable = true,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public HeavenlyFrenziedOstard() : base(CreatureProperties.Get<HeavenlyFrenziedOstard>())
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
        public HeavenlyFrenziedOstard(Serial serial) : base(serial)
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