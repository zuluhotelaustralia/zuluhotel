

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

namespace Server.Mobiles
{
    public class SapphireWisp : BaseCreature
    {
        static SapphireWisp() => CreatureProperties.Register<SapphireWisp>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_EarthProtection = i4,
            // CProp_NecroProtection = i2,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = sapphirewisp,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = blackwisp,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x1D5 /* Weapon */,
            // hostile = 1,
            // lootgroup = 205,
            // MagicItemChance = 75,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = spellkillpcs,
            // speech = 7,
            // Speed = 35 /* Weapon */,
            // spell = ebolt,
            // spell_0 = lightning,
            // spell_1 = masscurse,
            // spell_2 = wyvernstrike,
            // spell_3 = spectretouch,
            // spell_4 = sorcerersbane,
            // spell_5 = icestrike,
            // spell_6 = gustofair,
            // spell_7 = chainlightning,
            // TrueColor = 1171,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 0x3a,
            CorpseNameOverride = "corpse of a sapphire wisp",
            CreatureType = CreatureType.Elemental,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 575,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 700,
            Hue = 1171,
            Int = 1100,
            ManaMaxSeed = 1100,
            Name = "a sapphire wisp",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Sixth.MassCurseSpell),
                typeof(WyvernStrikeSpell),
                typeof(SpectresTouchSpell),
                typeof(SorcerorsBaneSpell),
                typeof(IceStrikeSpell),
                typeof(GustOfAirSpell),
            },
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 100 },
                { SkillName.MagicResist, 200 },
                { SkillName.Magery, 200 },
                { SkillName.EvalInt, 200 },
            },
            StamMaxSeed = 175,
            Str = 700,
            VirtualArmor = 30,

        });


        [Constructible]
public SapphireWisp() : base(CreatureProperties.Get<SapphireWisp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Blackwisp Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239,
            });


        }

        [Constructible]
public SapphireWisp(Serial serial) : base(serial) {}



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
