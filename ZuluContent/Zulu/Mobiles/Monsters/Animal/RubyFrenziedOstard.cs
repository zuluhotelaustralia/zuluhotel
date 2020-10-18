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
    public class RubyFrenziedOstard : BaseCreature
    {
        static RubyFrenziedOstard()
        {
            CreatureProperties.Register<RubyFrenziedOstard>(new CreatureProperties
            {
                // DataElementId = rubyfrenziedostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rubyfrenziedostard,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // TrueColor = 1645,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xda,
                CorpseNameOverride = "corpse of a ruby frenzied ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 45,
                DamageMin = 10,
                Dex = 320,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 210,
                Hue = 1645,
                Int = 170,
                ManaMaxSeed = 160,
                MinTameSkill = 95,
                Name = "a ruby frenzied ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 120,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 160}
                },
                StamMaxSeed = 160,
                Str = 210,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public RubyFrenziedOstard() : base(CreatureProperties.Get<RubyFrenziedOstard>())
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
        public RubyFrenziedOstard(Serial serial) : base(serial)
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