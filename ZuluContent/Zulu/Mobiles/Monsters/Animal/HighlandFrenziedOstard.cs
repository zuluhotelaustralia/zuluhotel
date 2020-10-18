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
    public class HighlandFrenziedOstard : BaseCreature
    {
        static HighlandFrenziedOstard()
        {
            CreatureProperties.Register<HighlandFrenziedOstard>(new CreatureProperties
            {
                // DataElementId = highlandfrenziedostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = highlandfrenziedostard,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // TrueColor = 1325,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xda,
                CorpseNameOverride = "corpse of a highland frenzied ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 45,
                DamageMin = 10,
                Dex = 180,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 130,
                Hue = 1325,
                Int = 35,
                ManaMaxSeed = 0,
                MinTameSkill = 85,
                Name = "a highland frenzied ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 140}
                },
                StamMaxSeed = 50,
                Str = 130,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public HighlandFrenziedOstard() : base(CreatureProperties.Get<HighlandFrenziedOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Golden Frenzied Ostard Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256
            });
        }

        [Constructible]
        public HighlandFrenziedOstard(Serial serial) : base(serial)
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