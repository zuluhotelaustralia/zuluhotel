

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class DarkMage : BaseCreature
    {
        static DarkMage() => CreatureProperties.Register<DarkMage>(new CreatureProperties
        {
            // cast_pct = 100,
            // count_casts = 0,
            // CProp_looter = s1,
            // CProp_massCastRange = i15,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = DarkMage,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = darkelfnecromancer,
            // Graphic = 0x13f9 /* Weapon */,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 138,
            // MagicItemChance = 60,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 500,
            // script = spellkillpcsTeleporter,
            // Speed = 30 /* Weapon */,
            // spell = MassCast fstrike,
            // spell_0 = MassCast kill,
            // spell_1 = MassCast abyssalflame,
            // spell_10 = MassCast risingfire,
            // spell_11 = MassCast mindblast,
            // spell_12 = MassCast firefield,
            // spell_13 = MassCast icestrike,
            // spell_14 = MassCast meteor_swarm,
            // spell_15 = MassCast shiftingearth,
            // spell_16 = MassCast calllightning,
            // spell_17 = summonbalronspawn,
            // spell_2 = MassCast ebolt,
            // spell_3 = MassCast plague,
            // spell_4 = MassCast sorcerersbane,
            // spell_5 = MassCast wyvernstrike,
            // spell_6 = MassCast dispel,
            // spell_7 = MassCast spectretouch,
            // spell_8 = MassCast darkness,
            // spell_9 = MassCast gustofair,
            // Swordsmanship = 110,
            // TrueColor = 1109,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x190,
            ClassLevel = 10,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of <random> the Dark Mage",
            CreatureType = CreatureType.Human,
            DamageMax = 40,
            DamageMin = 4,
            Dex = 195,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 500,
            Hue = 1109,
            Int = 1000,
            ManaMaxSeed = 1000,
            Name = "<random> the Dark Mage",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Seventh.FlameStrikeSpell),
                typeof(WyvernStrikeSpell),
                typeof(AbyssalFlameSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(PlagueSpell),
                typeof(SorcerorsBaneSpell),
                typeof(WyvernStrikeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(SpectresTouchSpell),
                typeof(DarknessSpell),
                typeof(GustOfAirSpell),
                typeof(RisingFireSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.Fourth.FireFieldSpell),
                typeof(IceStrikeSpell),
                typeof(Spells.Seventh.MeteorSwarmSpell),
                typeof(ShiftingEarthSpell),
                typeof(CallLightningSpell),
            },
            ProvokeSkillOverride = 120,
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 95 },
                { SkillName.Tactics, 75 },
                { SkillName.MagicResist, 150 },
                { SkillName.Magery, 150 },
            },
            StamMaxSeed = 195,
            Str = 200,
            TargetAcquireExhaustion = true,
            VirtualArmor = 25,

        });


        [Constructible]
public DarkMage() : base(CreatureProperties.Get<DarkMage>())
        {
            // Add customization here

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1,
            });

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Evil Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239,
            });


        }

        [Constructible]
public DarkMage(Serial serial) : base(serial) {}



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
