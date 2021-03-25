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
    public class EvilBookOfEarth : BaseCreature
    {
        static EvilBookOfEarth()
        {
            CreatureProperties.Register<EvilBookOfEarth>(new CreatureProperties
            {
                // cast_pct = 60,
                // CProp_BaseHpRegen = i350,
                // CProp_BaseManaRegen = i500,

                // CProp_Permmr = i5,
                // DataElementId = EvilBookOfTheEarth,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = EvilBookOfTheEarth,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x263 /* Weapon */,
                // hostile = 1,
                LootTable = "140",
                LootItemChance = 60,
                LootItemLevel = 5,
                // MissSound = 0x264 /* Weapon */,
                // num_casts = 20,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 50 /* Weapon */,
                // spell = shiftingearth,
                // spell_0 = calllightning,
                // spell_1 = gustofair,
                // spell_2 = risingfire,
                // spell_3 = icestrike,
                // spell_4 = masscurse,
                // spell_5 = earthquake,
                // spell_6 = teletoplayer,
                // Swordsmanship = 150,
                // TrueColor = 1162,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x3d9,
                CorpseNameOverride = "corpse of an Evil Book Of The Earth",
                CreatureType = CreatureType.Animated,
                DamageMax = 70,
                DamageMin = 25,
                Dex = 600,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 850,
                Hue = 1162,
                Int = 910,
                ManaMaxSeed = 1600,
                Name = "an Evil Book Of The Earth",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(ShiftingEarthSpell),
                    typeof(CallLightningSpell),
                    typeof(GustOfAirSpell),
                    typeof(RisingFireSpell),
                    typeof(IceStrikeSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Eighth.EarthquakeSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Fire, 75},
                    {ElementalType.Physical, 75},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 75},
                    {ElementalType.PermMagicImmunity, 8}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.MagicResist, 120},
                    {SkillName.Tactics, 100},
                    {SkillName.Magery, 180},
                    {SkillName.Healing, 100}
                },
                StamMaxSeed = 200,
                Str = 600,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public EvilBookOfEarth() : base(CreatureProperties.Get<EvilBookOfEarth>())
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
                MissSound = 0x264
            });
        }

        [Constructible]
        public EvilBookOfEarth(Serial serial) : base(serial)
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