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
    public class Balron : BaseCreature
    {
        static Balron()
        {
            CreatureProperties.Register<Balron>(new CreatureProperties
            {
                // cast_pct = 20,
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i1000,
                // CProp_Permmr = i5,
                // DataElementId = balron,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = balron1,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 80,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 4,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 65 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_10 = wraithbreath,
                // spell_11 = darkness,
                // spell_2 = ebolt,
                // spell_3 = plague,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = earthquake,
                // spell_7 = dispel,
                // spell_8 = massdispel,
                // spell_9 = spectretouch,
                // TrueColor = 0x4001,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 40,
                CanFly = true,
                ClassLevel = 3,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of a Balron",
                CreatureType = CreatureType.Daemon,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 1,
                HideType = HideType.Balron,
                HitsMax = 1100,
                Hue = 0x4001,
                Int = 2000,
                ManaMaxSeed = 2000,
                Name = "a Balron",
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
                    typeof(DarknessSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Water, 75},
                    {ElementalType.Air, 100},
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 75}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 200},
                    {SkillName.EvalInt, 200},
                    {SkillName.Parry, 130},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 70,
                Str = 1100,
                VirtualArmor = 75
            });
        }


        [Constructible]
        public Balron() : base(CreatureProperties.Get<Balron>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Balron Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
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
        public Balron(Serial serial) : base(serial)
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