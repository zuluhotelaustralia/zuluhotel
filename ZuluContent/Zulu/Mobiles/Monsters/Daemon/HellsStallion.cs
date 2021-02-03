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
    public class HellsStallion : BaseCreature
    {
        static HellsStallion()
        {
            CreatureProperties.Register<HellsStallion>(new CreatureProperties
            {
                // CProp_NecroProtection = i3,
                // CProp_noanimate = i1,
                // CProp_PermMagicImmunity = i6,
                // DataElementId = hellsstallion,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = hellsstallion,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0xAB /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 90,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1305,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 793,
                CorpseNameOverride = "corpse of a Hells Stallion",
                CreatureType = CreatureType.Daemon,
                DamageMax = 150,
                DamageMin = 15,
                Dex = 650,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 1050,
                Hue = 1305,
                Int = 450,
                ManaMaxSeed = 200,
                Name = "a Hells Stallion",
                PerceptionRange = 10,
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.EvalInt, 100},
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 150}
                },
                StamMaxSeed = 150,
                Str = 1950,
                Tamable = false,
                VirtualArmor = 60
            });
        }


        [Constructible]
        public HellsStallion() : base(CreatureProperties.Get<HellsStallion>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Hells Stallion Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xAB,
                MissSound = 0x239
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500
            });
        }

        [Constructible]
        public HellsStallion(Serial serial) : base(serial)
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