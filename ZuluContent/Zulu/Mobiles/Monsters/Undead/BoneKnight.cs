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
    public class BoneKnight : BaseCreature
    {
        static BoneKnight()
        {
            CreatureProperties.Register<BoneKnight>(new CreatureProperties
            {
                // DataElementId = boneknight,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = boneknight,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1C6 /* Weapon */,
                // hostile = 1,
                LootTable = "16",
                LootItemChance = 2,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 45 /* Weapon */,
                // Swordsmanship = 105,
                // targetText = "Ego apokteinou",
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x39,
                CorpseNameOverride = "corpse of a bone knight",
                CreatureType = CreatureType.Undead,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 160,
                Hue = 33784,
                Int = 35,
                ManaMaxSeed = 25,
                Name = "a bone knight",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 50}
                },
                StamMaxSeed = 80,
                Str = 160,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public BoneKnight() : base(CreatureProperties.Get<BoneKnight>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bone Knight Weapon",
                Speed = 45,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C6,
                MissSound = 0x239
            });
        }

        [Constructible]
        public BoneKnight(Serial serial) : base(serial)
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