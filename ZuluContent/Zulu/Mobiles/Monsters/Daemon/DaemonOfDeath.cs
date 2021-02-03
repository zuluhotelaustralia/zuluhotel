using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class DaemonOfDeath : BaseCreature
    {
        static DaemonOfDeath()
        {
            CreatureProperties.Register<DaemonOfDeath>(new CreatureProperties
            {
                // cast_pct = 20,
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i1000,
                // CProp_EarthProtection = i3,
                // CProp_NecroProtection = i8,
                // CProp_noanimate = i1,
                // CProp_NoReactiveArmour = i1,
                // CProp_PermMagicImmunity = i8,
                // CProp_Permmr = i5,
                // DataElementId = daemonofdeath,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = daemonofdeath,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:voidscript /* Weapon */,
                // HitSound = 0x25F /* Weapon */,
                // hostile = 1,
                LootTable = "150",
                LootItemChance = 90,
                LootItemLevel = 9,
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 4,
                // Parry_0 = 200,
                // script = spellkillpcsTeleporter,
                // speech = 35,
                // Speed = 65 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_2 = summonbalronspawn,
                // spell_3 = plague,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = earthquake,
                // spell_7 = dispel,
                // spell_8 = spectretouch,
                // spell_9 = wraithbreath,
                // TrueColor = 1160,
                // virtue = 2,
                AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 38,
                CanFly = true,
                CorpseNameOverride = "corpse of a Daemon Of Death",
                CreatureType = CreatureType.Daemon,
                DamageMax = 99,
                DamageMin = 57,
                Dex = 200,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 5,
                HitsMax = 6000,
                Hue = 1160,
                Int = 2000,
                ManaMaxSeed = 2000,
                Name = "a Daemon Of Death",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(PlagueSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(SpectresTouchSpell),
                    typeof(WraithBreathSpell)
                },
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Water, 75},
                    {ElementalType.Air, 75},
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 200},
                    {SkillName.MagicResist, 300},
                    {SkillName.Tactics, 200},
                    {SkillName.Fencing, 200},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 300},
                    {SkillName.EvalInt, 200},
                    {SkillName.DetectHidden, 200},
                    {SkillName.Hiding, 200}
                },
                StamMaxSeed = 300,
                Str = 2000,
                Tamable = false,
                TargetAcquireExhaustion = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public DaemonOfDeath() : base(CreatureProperties.Get<DaemonOfDeath>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Soul Slayer Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169,
                MaxRange = 5
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR40",
                BaseArmorRating = 40,
                MaxHitPoints = 500,
                HitPoints = 500
            });
        }

        [Constructible]
        public DaemonOfDeath(Serial serial) : base(serial)
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