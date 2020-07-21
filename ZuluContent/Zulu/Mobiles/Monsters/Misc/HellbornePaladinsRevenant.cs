

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
    public class HellbornePaladinsRevenant : BaseCreature
    {
        static HellbornePaladinsRevenant() => CreatureProperties.Register<HellbornePaladinsRevenant>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_AttackTypeImmunities = i256,
            // CProp_BaseHpRegen = i500,
            // CProp_BaseManaRegen = i1000,
            // CProp_EarthProtection = i3,
            // CProp_NecroProtection = i8,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i7,
            // CProp_Permmr = i5,
            // DataElementId = paladinghost,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = paladinghost,
            // hostile = 1,
            // lootgroup = 150,
            // MagicItemChance = 100,
            // MagicItemLevel = 10,
            // num_casts = 4,
            // Parry_0 = 200,
            // script = spellkillpcsTeleporter,
            // speech = 35,
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
            // TrueColor = 1157,
            // virtue = 2,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            Body = 0x3ca,
            CanFly = true,
            CorpseNameOverride = "corpse of The Hellborne Paladin's Revenant",
            Dex = 200,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 5000,
            Hue = 1157,
            Int = 2000,
            ManaMaxSeed = 6000,
            Name = "The Hellborne Paladin's Revenant",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.PlagueSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Eighth.EarthquakeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.SpectresTouchSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.WraithBreathSpell),
                typeof(Scripts.Zulu.Spells.Necromancy.DarknessSpell),
            },
            ProvokeSkillOverride = 160,
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
                { SkillName.MagicResist, 250 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 300 },
                { SkillName.Magery, 300 },
                { SkillName.EvalInt, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Hiding, 200 },
            },
            StamMaxSeed = 300,
            Str = 1500,
            Tamable = false,
            TargetAcquireExhaustion = true,

        });


        [Constructible]
public HellbornePaladinsRevenant() : base(CreatureProperties.Get<HellbornePaladinsRevenant>())
        {
            // Add customization here


        }

        [Constructible]
public HellbornePaladinsRevenant(Serial serial) : base(serial) {}



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
