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
    public class TrollMarksman : BaseCreature
    {
        static TrollMarksman()
        {
            CreatureProperties.Register<TrollMarksman>(new CreatureProperties
            {
                // ammoamount = 35,
                // ammotype = 0x0f3f,
                // DataElementId = trollmarksman2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = trollmarksman2,
                // HitSound = 0x235 /* Weapon */,
                // hostile = 1,
                LootTable = "41",
                // missileweapon = archer,
                // MissSound = 0x239 /* Weapon */,
                // script = explosionkillpcs,
                // Speed = 30 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* explosionkillpcs */,
                AlwaysMurderer = true,
                Body = 0x36,
                CorpseNameOverride = "corpse of a troll marksman",
                CreatureType = CreatureType.Troll,
                DamageMax = 27,
                DamageMin = 12,
                Dex = 120,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 185,
                Hue = 33784,
                Int = 55,
                ManaMaxSeed = 0,
                Name = "a troll marksman",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 80,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 105},
                    {SkillName.Macing, 115},
                    {SkillName.Archery, 130}
                },
                StamMaxSeed = 60,
                Str = 185,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public TrollMarksman() : base(CreatureProperties.Get<TrollMarksman>())
        {
            // Add customization here
        }

        [Constructible]
        public TrollMarksman(Serial serial) : base(serial)
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