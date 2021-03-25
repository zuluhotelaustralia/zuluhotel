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
    public class BrigandKing : BaseCreature
    {
        static BrigandKing()
        {
            CreatureProperties.Register<BrigandKing>(new CreatureProperties
            {
                // DataElementId = brigandking,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = brigandking,
                // Graphic = 0x1440 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "111",
                LootItemChance = 100,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // Parrying = 80,
                // script = killpcs,
                // Speed = 40 /* Weapon */,
                // Swordsmanship = 130,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of Brigand King",
                CreatureType = CreatureType.Human,
                DamageMax = 31,
                DamageMin = 19,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 600,
                Hue = 0,
                Int = 400,
                ManaMaxSeed = 400,
                Name = "Brigand King",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.PermMagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.MagicResist, 130}
                },
                StamMaxSeed = 400,
                Str = 600
            });
        }


        [Constructible]
        public BrigandKing() : base(CreatureProperties.Get<BrigandKing>())
        {
            // Add customization here

            AddItem(new Cutlass
            {
                Movable = false,
                Name = "Brigand King Weapon",
                Speed = 40,
                Hue = 1172,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public BrigandKing(Serial serial) : base(serial)
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