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
    public class LizardmanKing : BaseCreature
    {
        static LizardmanKing()
        {
            CreatureProperties.Register<LizardmanKing>(new CreatureProperties
            {
                // DataElementId = lizardmanking,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = lizardmanking,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1A6 /* Weapon */,
                // hostile = 1,
                LootTable = "56",
                LootItemChance = 50,
                LootItemLevel = 3,
                // MissSound = 0x1A3 /* Weapon */,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // TrueColor = 0x0465,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 417,
                Body = 0x21,
                CorpseNameOverride = "corpse of <random> the Lizardman King",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 200,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Lizard,
                HitsMax = 300,
                Hue = 0x0465,
                Int = 75,
                ManaMaxSeed = 65,
                Name = "<random> the Lizardman King",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 110,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.MagicImmunity, 1}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 100},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 100,
                Str = 300,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public LizardmanKing() : base(CreatureProperties.Get<LizardmanKing>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lizardman King Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A6,
                MissSound = 0x1A3
            });
        }

        [Constructible]
        public LizardmanKing(Serial serial) : base(serial)
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