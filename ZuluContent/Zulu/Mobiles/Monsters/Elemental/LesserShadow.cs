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
    public class LesserShadow : BaseCreature
    {
        static LesserShadow()
        {
            CreatureProperties.Register<LesserShadow>(new CreatureProperties
            {
                // cast_pct = 20,
                // CProp_AttackTypeImmunities = i256,
                // CProp_BaseHpRegen = i500,
                // CProp_Permmr = i2,
                // DataElementId = lessershadow,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = lessershadow,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:blindingscript /* Weapon */,
                // HitSound = 0x283 /* Weapon */,
                // hostile = 1,
                LootTable = "77",
                LootItemChance = 15,
                LootItemLevel = 3,
                // MissSound = 0x282 /* Weapon */,
                // num_casts = 4,
                // Parrying = 100,
                // script = spellkillpcs,
                // Speed = 37 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_2 = ebolt,
                // spell_3 = sorcerersbane,
                // spell_4 = wyvernstrike,
                // spell_5 = decayingray,
                // spell_6 = darkness,
                // spell_7 = dispel,
                // TrueColor = 1,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 573,
                CanSwim = true,
                CorpseNameOverride = "corpse of a Lesser Shadow",
                CreatureType = CreatureType.Elemental,
                DamageMax = 53,
                DamageMin = 13,
                Dex = 200,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 300,
                Hue = 1,
                Int = 500,
                ManaMaxSeed = 125,
                Name = "a Lesser Shadow",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(DecayingRaySpell),
                    typeof(DarknessSpell),
                    typeof(Spells.Fifth.DispelFieldSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Necro, 100},
                    {ElementalType.MagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 105},
                    {SkillName.Tactics, 125},
                    {SkillName.Fencing, 130},
                    {SkillName.EvalInt, 100},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 80,
                Str = 300,
                Tamable = false,
                WeaponAbility = new SpellStrike(typeof(DarknessSpell)),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public LesserShadow() : base(CreatureProperties.Get<LesserShadow>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "LessershadowWeapon",
                Hue = 1,
                Speed = 37,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x283,
                MissSound = 0x282
            });

            AddItem(new BoneGloves
            {
                Movable = false,
                Name = "Red Bone Gloves AR10",
                Hue = 0x0494,
                BaseArmorRating = 10,
                MaxHitPoints = 200,
                HitPoints = 200
            });

            AddItem(new BoneHelm
            {
                Movable = false,
                Name = "Red Bone Helm AR45",
                Hue = 0x0494,
                BaseArmorRating = 45,
                MaxHitPoints = 450,
                HitPoints = 450
            });
        }

        [Constructible]
        public LesserShadow(Serial serial) : base(serial)
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