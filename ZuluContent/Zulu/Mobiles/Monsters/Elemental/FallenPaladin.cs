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
    public class FallenPaladin : BaseCreature
    {
        static FallenPaladin()
        {
            CreatureProperties.Register<FallenPaladin>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i2000,
                // CProp_noanimate = i1,
                // CProp_NoReactiveArmour = i1,
                // CProp_risewithloot = i1,
                // DataElementId = hiddenfallenpaladin,
                // DataElementType = NpcTemplate,
                // Equip = fallenpaladin,
                // Graphic = 0x143e /* Weapon */,
                // HitSound = 0x238 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x233 /* Weapon */,
                // Parrying = 300,
                // RunSpeed = 250,
                // script = killpcs,
                // Speed = 50 /* Weapon */,
                // Swordsmanship = 150,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a Fallen Paladin",
                CreatureType = CreatureType.Elemental,
                DamageMax = 80,
                DamageMin = 55,
                Dex = 1000,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 6000,
                Hue = 33784,
                Int = 250,
                ManaMaxSeed = 0,
                MinTameSkill = 170,
                Name = "a Fallen Paladin",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 170,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                RiseCreatureDelay = TimeSpan.FromSeconds(0),
                RiseCreatureType = typeof(HellbornePaladinsRevenant),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 350},
                    {SkillName.Fencing, 150},
                    {SkillName.MagicResist, 250},
                    {SkillName.DetectHidden, 200},
                    {SkillName.Hiding, 200}
                },
                StamMaxSeed = 3000,
                Str = 2500,
                Tamable = true
            });
        }


        [Constructible]
        public FallenPaladin() : base(CreatureProperties.Get<FallenPaladin>())
        {
            // Add customization here

            AddItem(new PlateChest
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new PlateGloves
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail Gloves of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new PlateGorget
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail Gorget of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new PlateLegs
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail Legs of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new PlateArms
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail Arms of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new PlateHelm
            {
                Movable = false,
                Hue = 0x498,
                Name = "Platemail Helm of the Paladin",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new Halberd
            {
                Movable = false,
                Name = "Paladin's Halberd of Destruction",
                Hue = 1157,
                Speed = 50,
                Skill = SkillName.Swords,
                Animation = (WeaponAnimation) 0xC,
                HitSound = 0x238,
                MissSound = 0x233,
                MaxHitPoints = 200,
                HitPoints = 200
            });
        }

        [Constructible]
        public FallenPaladin(Serial serial) : base(serial)
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