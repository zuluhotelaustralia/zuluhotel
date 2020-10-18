using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class HeavenlyDrake : BaseCreature
    {
        static HeavenlyDrake()
        {
            CreatureProperties.Register<HeavenlyDrake>(new CreatureProperties
            {
                // CProp_HolyProtection = i8,
                // CProp_PermMagicImmunity = i5,
                // DataElementId = heavenlydrake,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = heavenlydrake,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // lootgroup = 37,
                // MagicItemChance = 25,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // script = firebreather,
                // Speed = 45 /* Weapon */,
                // TrueColor = 1181,
                // virtue = 7,
                AiType = AIType.AI_Melee /* firebreather */,
                BaseSoundID = 362,
                Body = 0x3c,
                CorpseNameOverride = "corpse of a Heavenly Drake",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Dragon,
                HitsMax = 400,
                Hue = 1181,
                InitialInnocent = true,
                Int = 90,
                ManaMaxSeed = 80,
                MinTameSkill = 125,
                Name = "a Heavenly Drake",
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 100,
                Str = 400,
                Tamable = true,
                VirtualArmor = 40,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public HeavenlyDrake() : base(CreatureProperties.Get<HeavenlyDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Heavenly Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public HeavenlyDrake(Serial serial) : base(serial)
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