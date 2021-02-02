using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class Wraith : BaseCreature
    {
        static Wraith()
        {
            CreatureProperties.Register<Wraith>(new CreatureProperties
            {
                // cast_pct = 30,
                // CProp_AttackTypeImmunities = i256,
                // CProp_NecroProtection = i4,
                // DataElementId = wraith,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = wraith,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x283 /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 80,
                LootItemLevel = 5,
                // MissSound = 0x282 /* Weapon */,
                // num_casts = 4,
                // script = spellkillpcs,
                // Speed = 40 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_10 = wraithbreath,
                // spell_11 = icestrike,
                // spell_2 = ebolt,
                // spell_3 = plague,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = earthquake,
                // spell_7 = dispel,
                // spell_8 = massdispel,
                // spell_9 = spectretouch,
                // TrueColor = 0x4001,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x1a,
                CorpseNameOverride = "corpse of a wraith",
                CreatureType = CreatureType.Undead,
                DamageMax = 40,
                DamageMin = 15,
                Dex = 380,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 125,
                Hue = 0x4001,
                Int = 1025,
                ManaMaxSeed = 1025,
                Name = "a wraith",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(PlagueSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(Spells.Seventh.MassDispelSpell),
                    typeof(SpectresTouchSpell),
                    typeof(WraithBreathSpell),
                    typeof(IceStrikeSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.EvalInt, 100},
                    {SkillName.Magery, 150},
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 105},
                    {SkillName.Tactics, 120},
                    {SkillName.Fencing, 120}
                },
                StamMaxSeed = 80,
                Str = 125,
                Tamable = false,
                VirtualArmor = 40,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public Wraith() : base(CreatureProperties.Get<Wraith>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "wraith Weapon",
                Hue = 1,
                Speed = 40,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x283,
                MissSound = 0x282
            });
        }

        [Constructible]
        public Wraith(Serial serial) : base(serial)
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