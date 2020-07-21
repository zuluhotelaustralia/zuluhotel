

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
    public class GreaterEvilBookOfEarth : BaseCreature
    {
        static GreaterEvilBookOfEarth() => CreatureProperties.Register<GreaterEvilBookOfEarth>(new CreatureProperties
        {
            // cast_pct = 75,
            // count_casts = 0,
            // CProp_AttackTypeImmunities = i256,
            // CProp_BaseHpRegen = i450,
            // CProp_BaseManaRegen = i500,
            // CProp_EarthProtection = i4,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i1,
            // CProp_PermMagicImmunity = i8,
            // CProp_Permmr = i4,
            // DataElementId = GreaterEvilBookOfTheEarth,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = EvilBookOfTheEarth,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x263 /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 65,
            // MagicItemLevel = 7,
            // MissSound = 0x264 /* Weapon */,
            // num_casts = 40,
            // script = spellkillpcsTeleporter,
            // speech = 35,
            // Speed = 50 /* Weapon */,
            // spell = MassCast	shiftingearth,
            // spell_0 = MassCast	gustofair,
            // spell_1 = MassCast	calllightning,
            // spell_2 = MassCast	icestrike,
            // spell_3 = MassCast	paralyse,
            // spell_4 = massdispel,
            // spell_5 = MassCast,
            // spell_6 = teletoplayer,
            // Swordsmanship = 150,
            // TrueColor = 1645 ,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x3d9,
            CorpseNameOverride = "corpse of a Greater Evil Book of the Earth",
            CreatureType = CreatureType.Animated,
            DamageMax = 70,
            DamageMin = 25,
            Dex = 1600,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 1850,
            Hue = 1645 ,
            Int = 1910,
            ManaMaxSeed = 1600,
            Name = "a Greater Evil Book of the Earth",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(ShiftingEarthSpell),
                typeof(GustOfAirSpell),
                typeof(CallLightningSpell),
                typeof(IceStrikeSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(Spells.First.ClumsySpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 25 },
                { ElementalType.Fire, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 150 },
                { SkillName.MagicResist, 150 },
                { SkillName.Tactics, 150 },
                { SkillName.Magery, 185 },
                { SkillName.Healing, 175 },
            },
            StamMaxSeed = 800,
            Str = 1600,
            TargetAcquireExhaustion = true,
            VirtualArmor = 25,

        });


        [Constructible]
public GreaterEvilBookOfEarth() : base(CreatureProperties.Get<GreaterEvilBookOfEarth>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Evil Book Of The Earth Weapon",
                Speed = 50,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x263,
                MissSound = 0x264,
            });


        }

        [Constructible]
public GreaterEvilBookOfEarth(Serial serial) : base(serial) {}



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
