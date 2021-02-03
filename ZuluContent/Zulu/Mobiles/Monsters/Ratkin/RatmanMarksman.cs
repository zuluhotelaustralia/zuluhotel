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
    public class RatmanMarksman : BaseCreature
    {
        static RatmanMarksman()
        {
            CreatureProperties.Register<RatmanMarksman>(new CreatureProperties
            {
                // ammoamount = 30,
                // ammotype = 0x1bfb,
                // DataElementId = ratmanmarksman,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ratmanmarksman,
                // HitSound = 0x235 /* Weapon */,
                // hostile = 1,
                LootTable = "51",
                // missileweapon = xbowman,
                // MissSound = 0x239 /* Weapon */,
                // script = explosionkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* explosionkillpcs */,
                AlwaysMurderer = true,
                Body = 0x2a,
                CorpseNameOverride = "corpse of <random> the ratman marksman",
                CreatureType = CreatureType.Ratkin,
                DamageMax = 22,
                DamageMin = 10,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Rat,
                HitsMax = 160,
                Hue = 0,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "<random> the ratman marksman",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 55,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 40},
                    {SkillName.Tactics, 60},
                    {SkillName.Macing, 65},
                    {SkillName.Archery, 100}
                },
                StamMaxSeed = 50,
                Str = 160,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public RatmanMarksman() : base(CreatureProperties.Get<RatmanMarksman>())
        {
            // Add customization here
        }

        [Constructible]
        public RatmanMarksman(Serial serial) : base(serial)
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