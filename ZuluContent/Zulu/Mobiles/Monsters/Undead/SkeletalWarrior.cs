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
    public class SkeletalWarrior : BaseCreature
    {
        static SkeletalWarrior()
        {
            CreatureProperties.Register<SkeletalWarrior>(new CreatureProperties
            {
                // DataElementId = skeletalwarrior,
                // DataElementType = NpcTemplate,
                // Equip = skeletalwarrior,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x23D /* Weapon */,
                // hostile = 1,
                LootTable = "59",
                LootItemChance = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 40 /* Weapon */,
                // Swordsmanship = 120,
                // targetText = "Ego apokteinou",
                // TrueColor = 1127,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x39,
                CorpseNameOverride = "corpse of a skeletal warrior",
                CreatureType = CreatureType.Undead,
                DamageMax = 45,
                DamageMin = 13,
                Dex = 350,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 1127,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "a skeletal warrior",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 175,
                Str = 300,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public SkeletalWarrior() : base(CreatureProperties.Get<SkeletalWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Skeletal Warrior Weapon",
                Speed = 40,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public SkeletalWarrior(Serial serial) : base(serial)
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