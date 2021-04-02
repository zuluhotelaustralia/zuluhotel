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
    public class SkeletalAssassin : BaseCreature
    {
        static SkeletalAssassin()
        {
            CreatureProperties.Register<SkeletalAssassin>(new CreatureProperties
            {
                // buddyText = "Emos hetairos",
                // DataElementId = skeletalassassin,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = skeletalassassin,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1C6 /* Weapon */,
                // hostile = 1,
                // leaderText = "Ego akoloutheou",
                LootTable = "10",
                LootItemChance = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 90,
                // targetText = "Ego apokteinou",
                // TrueColor = 0x4631,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x32,
                CorpseNameOverride = "corpse of a skeletal assassin",
                CreatureType = CreatureType.Undead,
                DamageMax = 44,
                DamageMin = 8,
                Dex = 125,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 130,
                Hue = 0x4631,
                Int = 60,
                ManaMaxSeed = 50,
                Name = "a skeletal assassin",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Necro, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 50},
                    {SkillName.Tactics, 60},
                    {SkillName.Hiding, 90},
                    {SkillName.Stealth, 85}
                },
                StamMaxSeed = 115,
                Str = 130,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public SkeletalAssassin() : base(CreatureProperties.Get<SkeletalAssassin>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Skeletal Assassin Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C6,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SkeletalAssassin(Serial serial) : base(serial)
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