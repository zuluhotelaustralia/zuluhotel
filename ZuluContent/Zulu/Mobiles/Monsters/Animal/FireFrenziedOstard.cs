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
    public class FireFrenziedOstard : BaseCreature
    {
        static FireFrenziedOstard()
        {
            CreatureProperties.Register<FireFrenziedOstard>(new CreatureProperties
            {
                // DataElementId = firefrenziedostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = firefrenziedostard,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x254 /* Weapon */,
                // MissSound = 0x256 /* Weapon */,
                // script = firebreather,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1158,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysAttackable = true,
                Body = 0xda,
                CorpseNameOverride = "corpse of a fire frenzied ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 48,
                DamageMin = 13,
                Dex = 400,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HasBreath = true,
                Hides = 4,
                HideType = HideType.Ostard,
                HitsMax = 225,
                Hue = 1158,
                Int = 135,
                ManaMaxSeed = 125,
                MinTameSkill = 115,
                Name = "a fire frenzied ostard",
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 110},
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 110},
                    {SkillName.Macing, 160}
                },
                StamMaxSeed = 175,
                Str = 225,
                Tamable = true,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public FireFrenziedOstard() : base(CreatureProperties.Get<FireFrenziedOstard>())
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
        public FireFrenziedOstard(Serial serial) : base(serial)
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