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
    public class BrigandMarksmen : BaseCreature
    {
        static BrigandMarksmen()
        {
            CreatureProperties.Register<BrigandMarksmen>(new CreatureProperties
            {
                // ammoamount = 30,
                // ammotype = 0xf3f,
                // DataElementId = brigandmarksmen,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = brigandmarksmen,
                // HitSound = 0x235 /* Weapon */,
                LootTable = "52",
                // Macefighting = 75,
                LootItemChance = 1,
                // missileweapon = archer,
                // MissSound = 0x239 /* Weapon */,
                // script = archerkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* archerkillpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a brigand Marksmen",
                CreatureType = CreatureType.Human,
                DamageMax = 22,
                DamageMin = 10,
                Dex = 105,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 95,
                Hue = 0,
                Int = 45,
                ManaMaxSeed = 0,
                Name = "a brigand Marksmen",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 75},
                    {SkillName.Archery, 100}
                },
                StamMaxSeed = 50,
                Str = 95,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public BrigandMarksmen() : base(CreatureProperties.Get<BrigandMarksmen>())
        {
            // Add customization here
        }

        [Constructible]
        public BrigandMarksmen(Serial serial) : base(serial)
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