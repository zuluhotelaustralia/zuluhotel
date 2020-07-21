

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
    public class ArchVampire : BaseCreature
    {
        static ArchVampire() => CreatureProperties.Register<ArchVampire>(new CreatureProperties
        {
            // cast_pct = 80,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i3,
            // CProp_PermMagicImmunity = i3,
            // CProp_Untameable = i1,
            // DataElementId = archvampire,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = Vampire2,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:lifedrainscript /* Weapon */,
            // HitSound = 0x16C /* Weapon */,
            // hostile = 1,
            // lootgroup = 71,
            // MagicItemChance = 75,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 100,
            // script = spellkillpcsTeleporter,
            // speech = 54,
            // Speed = 30 /* Weapon */,
            // spell = MassCast paralyse,
            // spell_0 = MassCast kill,
            // spell_1 = MassCast ebolt,
            // spell_10 = MassCast icestrike,
            // spell_11 = MassCast shiftingearth,
            // spell_12 = teletoplayer,
            // spell_2 = MassCast plague,
            // spell_3 = MassCast sorcerersbane,
            // spell_4 = MassCast wyvernstrike,
            // spell_5 = MassCast dispel,
            // spell_6 = MassCast spectretouch,
            // spell_7 = MassCast darkness,
            // spell_8 = MassCast gustofair,
            // spell_9 = MassCast mindblast,
            // TrueColor = 1176,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            Body = 0x190,
            ClassLevel = 4,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of Arch Vampire",
            CreatureType = CreatureType.Undead,
            DamageMax = 40,
            DamageMin = 12,
            Dex = 550,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 350,
            Hue = 0,
            Int = 700,
            ManaMaxSeed = 700,
            Name = "Arch Vampire",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.PlagueSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SpectresTouchSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.DarknessSpell),
                typeof(Scripts.Zulu.Spells.Earth.GustOfAirSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Scripts.Zulu.Spells.Earth.IceStrikeSpell),
                typeof(Scripts.Zulu.Spells.Earth.ShiftingEarthSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 100 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 200 },
                { SkillName.Magery, 200 },
            },
            StamMaxSeed = 550,
            Str = 550,
            TargetAcquireExhaustion = true,
            VirtualArmor = 25,

        });


        [Constructible]
public ArchVampire() : base(CreatureProperties.Get<ArchVampire>())
        {
            // Add customization here

            AddItem(new ShortHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1,
            });

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Vampire2 Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16C,
                MissSound = 0x239,
            });


        }

        [Constructible]
public ArchVampire(Serial serial) : base(serial) {}



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
