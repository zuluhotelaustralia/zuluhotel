using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class BalronLord : BaseCreature
    {
        static BalronLord() => CreatureProperties.Register<BalronLord>(new CreatureProperties
        {
            // cast_pct = 80,
            // CProp_BaseHpRegen = i500,
            // CProp_BaseManaRegen = i1000,
            // CProp_EarthProtection = i4,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // CProp_Permmr = i5,
            // DataElementId = hiddenbalronlord,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = balronlord,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x305 /* Weapon */,
            // hostile = 1,
            // lootgroup = 201,
            // MagicItemChance = 100,
            // MagicItemLevel = 9,
            // MissSound = 0x303 /* Weapon */,
            // num_casts = 800,
            // Parry_0 = 100,
            // script = spellkillpcsTeleporter,
            // speech = 35,
            // Speed = 55 /* Weapon */,
            // spell = MassCast fstrike,
            // spell_0 = MassCast kill,
            // spell_1 = MassCast ebolt,
            // spell_10 = MassCast firefield,
            // spell_11 = MassCast icestrike,
            // spell_12 = MassCast meteor_swarm,
            // spell_13 = MassCast shiftingearth,
            // spell_14 = MassCast calllightning,
            // spell_15 = summonbalronspawn,
            // spell_16 = summonbalronspawn,
            // spell_17 = summonbalronspawn,
            // spell_18 = summonbalronspawn,
            // spell_2 = MassCast plague,
            // spell_3 = MassCast sorcerersbane,
            // spell_4 = MassCast wyvernstrike,
            // spell_5 = MassCast dispel,
            // spell_6 = MassCast spectretouch,
            // spell_7 = MassCast darkness,
            // spell_8 = MassCast gustofair,
            // spell_9 = MassCast mindblast,
            // TrueColor = 0x0454,
            // virtue = 2,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            AutoDispel = true,
            BardImmune = true,
            Body = 784,
            CanFly = true,
            CorpseNameOverride = "corpse of a Balron Lord",
            CreatureType = CreatureType.Daemon,
            DamageMax = 105,
            DamageMin = 24,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 3,
            HitsMax = 2200,
            Hue = 0x0454,
            Int = 3000,
            ManaMaxSeed = 2000,
            Name = "a Balron Lord",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Seventh.FlameStrikeSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.PlagueSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SpectresTouchSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.DarknessSpell),
                typeof(GustOfAirSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.Fourth.FireFieldSpell),
                typeof(IceStrikeSpell),
                typeof(Spells.Seventh.MeteorSwarmSpell),
                typeof(ShiftingEarthSpell),
                typeof(CallLightningSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Cold, 75 },
                { ElementalType.Energy, 100 },
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 200 },
                { SkillName.MagicResist, 200 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 150 },
                { SkillName.Magery, 250 },
                { SkillName.Hiding, 200 },
                { SkillName.EvalInt, 200 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 70,
            Str = 1100,
            TargetAcquireExhaustion = true,
            VirtualArmor = 75,

        });


        [Constructible]
public BalronLord() : base(CreatureProperties.Get<BalronLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Balron Lord Weapon",
                Speed = 55,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x305,
                MissSound = 0x303,
                MaxRange = 3,
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500,
            });


        }

        [Constructible]
public BalronLord(Serial serial) : base(serial) {}



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
