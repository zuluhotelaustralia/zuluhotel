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
    public class Hildisvini : BaseCreature
    {
        static Hildisvini()
        {
            CreatureProperties.Register<Hildisvini>(new CreatureProperties
            {
                // DataElementId = hildisvini,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = hildisvini,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0xC7 /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // TrueColor = 448,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xcb,
                CorpseNameOverride = "corpse of Hildisvini",
                CreatureType = CreatureType.Animal,
                DamageMax = 12,
                DamageMin = 6,
                Dex = 90,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 70,
                Hue = 448,
                Int = 55,
                ManaMaxSeed = 0,
                MinTameSkill = 35,
                Name = "Hildisvini",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 60,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 60},
                    {SkillName.Macing, 65},
                    {SkillName.MagicResist, 75}
                },
                StamMaxSeed = 60,
                Str = 70,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Hildisvini() : base(CreatureProperties.Get<Hildisvini>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Hildisvini Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xC7,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Hildisvini(Serial serial) : base(serial)
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