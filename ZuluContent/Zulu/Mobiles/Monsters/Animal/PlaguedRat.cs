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
    public class PlaguedRat : BaseCreature
    {
        static PlaguedRat()
        {
            CreatureProperties.Register<PlaguedRat>(new CreatureProperties
            {
                // DataElementId = plaguedrat,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = plaguedrat,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0xCF /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 15 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xee,
                CorpseNameOverride = "corpse of a plagued rat",
                CreatureType = CreatureType.Animal,
                DamageMax = 10,
                DamageMin = 1,
                Dex = 80,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitPoison = Poison.Lesser,
                HitsMax = 30,
                Hue = 33784,
                Int = 110,
                ManaMaxSeed = 200,
                MinTameSkill = 35,
                Name = "a plagued rat",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 50},
                    {SkillName.Magery, 40}
                },
                StamMaxSeed = 70,
                Str = 30,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public PlaguedRat() : base(CreatureProperties.Get<PlaguedRat>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Plagued Rat Weapon",
                Speed = 15,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xCF,
                MissSound = 0x239
            });
        }

        [Constructible]
        public PlaguedRat(Serial serial) : base(serial)
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