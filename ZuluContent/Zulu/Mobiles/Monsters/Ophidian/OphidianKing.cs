using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class OphidianKing : BaseCreature
    {
        static OphidianKing()
        {
            CreatureProperties.Register<OphidianKing>(new CreatureProperties
            {
                // CProp_PhysicalResistance = i6,
                // DataElementId = ophidianking,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = greatwyrm,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 75,
                LootItemLevel = 7,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1209,
                // virtue = 2,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 0x56,
                ClassLevel = 6,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of The Ophidian King",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 800,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 2050,
                Hue = 1209,
                Int = 1,
                ManaMaxSeed = 1,
                Name = "The Ophidian King",
                PerceptionRange = 10,
                ProvokeSkillOverride = 150,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermMagicImmunity, 10}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 160},
                    {SkillName.Magery, 160},
                    {SkillName.Macing, 160},
                    {SkillName.Tactics, 160},
                    {SkillName.MagicResist, 160}
                },
                StamMaxSeed = 800,
                Str = 2050,
                TargetAcquireExhaustion = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public OphidianKing() : base(CreatureProperties.Get<OphidianKing>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Great Wyrm Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OphidianKing(Serial serial) : base(serial)
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