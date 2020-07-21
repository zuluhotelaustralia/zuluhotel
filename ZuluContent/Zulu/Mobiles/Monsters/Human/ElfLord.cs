

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
    public class ElfLord : BaseCreature
    {
        static ElfLord() => CreatureProperties.Register<ElfLord>(new CreatureProperties
        {
            // cast_pct = 50,
            // count_casts = 0,
            // CProp_Elf = i1,
            // CProp_leavecorpse = i1,
            // CProp_looter = s1,
            // CProp_NecroProtection = i4,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = elflord,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = elflord,
            // Graphic = 0x13F9 /* Weapon */,
            // Hitscript = :combat:customanim /* Weapon */,
            // HitSound = 0x27 /* Weapon */,
            // hostile = 1,
            // lootgroup = 136,
            // MagicItemChance = 60,
            // MagicItemLevel = 5,
            // MissSound = 0x26 /* Weapon */,
            // mount = 0x3e9f 1176,
            // num_casts = 5,
            // script = elfspellkillpcs,
            // Speed = 30 /* Weapon */,
            // spell = shiftingearth,
            // spell_0 = gustofair,
            // spell_1 = calllightning,
            // spell_2 = flamestrike,
            // spell_3 = summonelf,
            // spell_4 = icestrike,
            // Swordsmanship = 110,
            // TrueColor = 0x0302,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* elfspellkillpcs */,
            Body = 0x190,
            ClassLevel = 2,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of an Elf Lord",
            CreatureType = CreatureType.Human,
            DamageMax = 61,
            DamageMin = 31,
            Dex = 195,
            Female = true,
            FightMode = FightMode.Closest,
            FightRange = 12,
            HitsMax = 1500,
            Hue = 0x0302,
            InitialInnocent = true,
            Int = 1500,
            ManaMaxSeed = 1500,
            Name = "an Elf Lord",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Scripts.Zulu.Spells.Earth.ShiftingEarthSpell),
                typeof(Scripts.Zulu.Spells.Earth.GustOfAirSpell),
                typeof(Scripts.Zulu.Spells.Earth.CallLightningSpell),
                typeof(Scripts.Zulu.Spells.Earth.IceStrikeSpell),
            },
            ProvokeSkillOverride = 140,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Energy, 50 },
                { ElementalType.Cold, 50 },
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 150 },
                { SkillName.Tactics, 75 },
                { SkillName.MagicResist, 150 },
                { SkillName.Magery, 200 },
                { SkillName.DetectHidden, 100 },
            },
            StamMaxSeed = 195,
            Str = 700,

        });


        [Constructible]
public ElfLord() : base(CreatureProperties.Get<ElfLord>())
        {
            // Add customization here

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Staff of Water",
                Hue = 1099,
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x27,
                MissSound = 0x26,
                MaxRange = 12,
                Animation = (WeaponAnimation)0xC,
            });

            AddItem(new PlateChest
            {
                Movable = false,
                Hue = 0x494,
                Name = "Elven Breastplate",
                BaseArmorRating = 80,
                MaxHitPoints = 110,
                HitPoints = 110,
            });

            AddItem(new PlateLegs
            {
                Movable = false,
                Hue = 0x494,
                Name = "Long pants",
                BaseArmorRating = 70,
                MaxHitPoints = 110,
                HitPoints = 110,
            });


        }

        [Constructible]
public ElfLord(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
