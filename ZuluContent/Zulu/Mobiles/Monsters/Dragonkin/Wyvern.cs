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
    public class Wyvern : BaseCreature
    {
        static Wyvern()
        {
            CreatureProperties.Register<Wyvern>(new CreatureProperties
            {
                // CProp_PermMagicImmunity = i3,
                // DataElementId = wyvern,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = wyvern,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "36",
                LootItemChance = 25,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // script = killpcs,
                // Speed = 45 /* Weapon */,
                // TrueColor = 1304,
                // virtue = 7,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0x3c,
                CorpseNameOverride = "corpse of a Wyvern",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Wyrm,
                HitPoison = Poison.Greater,
                HitsMax = 450,
                Hue = 1304,
                Int = 90,
                ManaMaxSeed = 80,
                MinTameSkill = 120,
                Name = "a Wyvern",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 120,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 130},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 100,
                Str = 450,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public Wyvern() : base(CreatureProperties.Get<Wyvern>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Wyvern Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Wyvern(Serial serial) : base(serial)
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