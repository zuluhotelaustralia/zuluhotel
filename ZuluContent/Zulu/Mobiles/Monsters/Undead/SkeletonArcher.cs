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
    public class SkeletonArcher : BaseCreature
    {
        static SkeletonArcher()
        {
            CreatureProperties.Register<SkeletonArcher>(new CreatureProperties
            {
                // ammoamount = 10,
                // ammotype = 0xf3f,
                // buddyText = "Emos hetairos",
                // DataElementId = skeletonarcher,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = skeletonarcher,
                // HitSound = 0x235 /* Weapon */,
                // hostile = 1,
                // leaderText = "Ego akoloutheou",
                LootTable = "49",
                // missileweapon = archer,
                // MissSound = 0x239 /* Weapon */,
                // script = archerkillpcs,
                // Speed = 35 /* Weapon */,
                // targetText = "Ego apokteinou",
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* archerkillpcs */,
                AlwaysMurderer = true,
                Body = 0x32,
                CorpseNameOverride = "corpse of a skeleton archer",
                CreatureType = CreatureType.Undead,
                DamageMax = 22,
                DamageMin = 10,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 100,
                Hue = 33784,
                Int = 25,
                ManaMaxSeed = 0,
                Name = "a skeleton archer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 30},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 95},
                    {SkillName.Archery, 60}
                },
                StamMaxSeed = 50,
                Str = 100,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public SkeletonArcher() : base(CreatureProperties.Get<SkeletonArcher>())
        {
            // Add customization here
        }

        [Constructible]
        public SkeletonArcher(Serial serial) : base(serial)
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