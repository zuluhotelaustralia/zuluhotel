using System;
using System.Collections.Generic;
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
    public class Dracoliche : BaseCreature
    {
        static Dracoliche()
        {
            CreatureProperties.Register<Dracoliche>(new CreatureProperties
            {
                // cast_pct = 20,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = dracoliche,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = dracoliche,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // lootgroup = 35,
                // MagicItemChance = 75,
                // Magicitemlevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 4,
                // script = spellkillpcs,
                // Speed = 50 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_10 = darkness,
                // spell_2 = ebolt,
                // spell_3 = plague,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = earthquake,
                // spell_7 = decayingray,
                // spell_8 = spectretouch,
                // spell_9 = wraithbreath,
                // TrueColor = 1282,
                // virtue = 8,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 104,
                CanFly = true,
                CorpseNameOverride = "corpse of a Dracoliche",
                CreatureType = CreatureType.Undead,
                DamageMax = 73,
                DamageMin = 33,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Liche,
                HitsMax = 650,
                Hue = 1282,
                Int = 700,
                ManaMaxSeed = 200,
                MinTameSkill = 135,
                Name = "a Dracoliche",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(PlagueSpell),
                    typeof(SorcerorsBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(DecayingRaySpell),
                    typeof(SpectresTouchSpell),
                    typeof(WraithBreathSpell),
                    typeof(DarknessSpell)
                },
                ProvokeSkillOverride = 120,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 110},
                    {SkillName.Macing, 130},
                    {SkillName.Magery, 140},
                    {SkillName.EvalInt, 140},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 140,
                Str = 650,
                Tamable = true,
                VirtualArmor = 40,
                WeaponAbility = new SpellStrike<WraithBreathSpell>(),
                WeaponAbilityChance = 0.4
            });
        }


        [Constructible]
        public Dracoliche() : base(CreatureProperties.Get<Dracoliche>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Dracoliche Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Dracoliche(Serial serial) : base(serial)
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