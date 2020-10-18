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
    public class RabidCat : BaseCreature
    {
        static RabidCat()
        {
            CreatureProperties.Register<RabidCat>(new CreatureProperties
            {
                // DataElementId = rabidcat,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rabidcat,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x6C /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0xc9,
                CorpseNameOverride = "corpse of a rabid cat",
                CreatureType = CreatureType.Animal,
                DamageMax = 6,
                DamageMin = 2,
                Dex = 80,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 22,
                Hue = 33784,
                Int = 25,
                ManaMaxSeed = 15,
                MinTameSkill = 30,
                Name = "a rabid cat",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 20,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 30}
                },
                StamMaxSeed = 50,
                Str = 22,
                Tamable = true,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public RabidCat() : base(CreatureProperties.Get<RabidCat>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rabidcat Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x6C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public RabidCat(Serial serial) : base(serial)
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