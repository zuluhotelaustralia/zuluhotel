

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
    public class UmbralFlicker : BaseCreature
    {
        static UmbralFlicker() => CreatureProperties.Register<UmbralFlicker>(new CreatureProperties
        {
            // CProp_EarthProtection = i4,
            // CProp_NecroProtection = i8,
            // CProp_Permmr = i8,
            // DataElementId = umbralflicker,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = umbralflicker,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1D5 /* Weapon */,
            // lootgroup = 130,
            // MagicItemChance = 75,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // script = spellkillpcs,
            // speech = 7,
            // Speed = 35 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = explosion,
            // spell_1 = gheal,
            // spell_10 = sorcerersbane,
            // spell_11 = wyvernstrike,
            // spell_12 = earthquake,
            // spell_13 = dispel,
            // spell_14 = massdispel,
            // spell_15 = wraithbreath,
            // spell_16 = darkness,
            // spell_2 = calllightning,
            // spell_3 = gustofair,
            // spell_4 = icestrike,
            // spell_5 = shiftingearth,
            // spell_6 = risingfire,
            // spell_7 = kill,
            // spell_8 = abyssalflame,
            // spell_9 = plague,
            // Swordsmanship = 140,
            // TrueColor = 25125,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x3a,
            CanFly = true,
            CanSwim = true,
            ClassLevel = 4,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of an Umbral Flicker",
            CreatureType = CreatureType.Elemental,
            DamageMax = 2,
            DamageMin = 1,
            Dex = 250,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 450,
            Hue = 25125,
            Int = 2200,
            ManaMaxSeed = 2200,
            Name = "an Umbral Flicker",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(Spells.Fourth.GreaterHealSpell),
                typeof(Spells.Earth.CallLightningSpell),
                typeof(Spells.Earth.GustOfAirSpell),
                typeof(Spells.Earth.IceStrikeSpell),
                typeof(Spells.Earth.ShiftingEarthSpell),
                typeof(Spells.Earth.RisingFireSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Necromancy.PlagueSpell),
                typeof(Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Eighth.EarthquakeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(Spells.Necromancy.WraithBreathSpell),
                typeof(Spells.Necromancy.DarknessSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Fire, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 120 },
                { SkillName.MagicResist, 130 },
                { SkillName.Magery, 150 },
                { SkillName.EvalInt, 150 },
            },
            StamMaxSeed = 50,
            Str = 450,
            VirtualArmor = 40,
  
        });

        [Constructable]
        public UmbralFlicker() : base(CreatureProperties.Get<UmbralFlicker>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Umbral Flicker Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239,
            });
  
  
        }

        public UmbralFlicker(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}