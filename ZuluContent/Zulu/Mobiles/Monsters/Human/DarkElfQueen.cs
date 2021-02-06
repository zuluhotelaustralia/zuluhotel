using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class DarkElfQueen : BaseCreature
    {
        static DarkElfQueen()
        {
            CreatureProperties.Register<DarkElfQueen>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_casts = 0,
                // CProp_Dark-Elf = i1,
                // CProp_leavecorpse = i1,
                // CProp_looter = s1,
                // CProp_NecroProtection = i4,
                // CProp_noanimate = i1,
                // CProp_NoReactiveArmour = i1,
                // CProp_PermMagicImmunity = i6,
                // DataElementId = darkelfqueen,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = darkelfqueen,
                // Graphic = 0x13F9 /* Weapon */,
                // Hitscript = :combat:customanim /* Weapon */,
                // HitSound = 0x11C /* Weapon */,
                // hostile = 1,
                LootTable = "137",
                LootItemChance = 60,
                LootItemLevel = 5,
                // MissSound = 0x11D /* Weapon */,
                // mount = 0x3e9f 1109,
                // num_casts = 5,
                // script = elfspellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = decayingray,
                // spell_0 = sorcerersbane,
                // spell_1 = wyvernstrike,
                // spell_2 = flamestrike,
                // spell_3 = kill,
                // spell_4 = summondarkelf,
                // Swordsmanship = 110,
                // TrueColor = 1109,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* elfspellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x191,
                ClassLevel = 2,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of a Dark-Elf Queen",
                CreatureType = CreatureType.Human,
                DamageMax = 61,
                DamageMin = 31,
                Dex = 195,
                Female = true,
                FightMode = FightMode.Closest,
                FightRange = 12,
                HitsMax = 1500,
                Hue = 1109,
                Int = 1500,
                ManaMaxSeed = 1500,
                Name = "a Dark-Elf Queen",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(DecayingRaySpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(WyvernStrikeSpell)
                },
                ProvokeSkillOverride = 140,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 50},
                    {ElementalType.Water, 25},
                    {ElementalType.Fire, 50},
                    {ElementalType.PermPoisonImmunity, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 150},
                    {SkillName.Tactics, 75},
                    {SkillName.MagicResist, 150},
                    {SkillName.Magery, 200},
                    {SkillName.DetectHidden, 100}
                },
                StamMaxSeed = 195,
                Str = 700
            });
        }


        [Constructible]
        public DarkElfQueen() : base(CreatureProperties.Get<DarkElfQueen>())
        {
            // Add customization here

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Staff of Fire",
                Hue = 1100,
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x11C,
                MissSound = 0x11D,
                MaxRange = 12,
                Animation = (WeaponAnimation) 0xC
            });

            AddItem(new FemalePlateChest
            {
                Movable = false,
                Name = "Elven Platemail",
                BaseArmorRating = 80,
                MaxHitPoints = 90,
                HitPoints = 90
            });

            AddItem(new PlateLegs
            {
                Movable = false,
                Hue = 0x494,
                Name = "Long pants",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110
            });

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 1156
            });
        }

        [Constructible]
        public DarkElfQueen(Serial serial) : base(serial)
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