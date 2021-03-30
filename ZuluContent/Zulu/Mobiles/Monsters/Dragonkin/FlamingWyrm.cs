using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class FlamingWyrm : BaseCreature
    {
        static FlamingWyrm()
        {
            CreatureProperties.Register<FlamingWyrm>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i250,

                // DataElementId = flamingwyrm,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = flamingwyrm,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitScript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 65,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 60 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = harm,
                // spell_10 = flamestrike,
                // spell_11 = fireball,
                // spell_2 = mindblast,
                // spell_3 = magicarrow,
                // spell_4 = chainlightning,
                // spell_5 = masscurse,
                // spell_6 = gheal,
                // spell_7 = earthquake,
                // spell_8 = manavamp,
                // spell_9 = paralyze,
                // TrueColor = 1305,
                // virtue = 7,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 46,
                CorpseNameOverride = "corpse of a Flaming Wyrm",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 110,
                DamageMin = 20,
                Dex = 475,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 2000,
                Hue = 1305,
                Int = 3500,
                ManaMaxSeed = 1500,
                MinTameSkill = 160,
                Name = "a Flaming Wyrm",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Fourth.GreaterHealSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Seventh.ManaVampireSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Third.FireballSpell)
                },
                ProvokeSkillOverride = 150,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Air, 50},
                    {ElementalType.Fire, 100},
                    {ElementalType.Earth, 50},
                    {ElementalType.PermMagicImmunity, 5}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 180},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 200},
                    {SkillName.Magery, 150},
                    {SkillName.DetectHidden, 180}
                },
                StamMaxSeed = 175,
                Str = 2800,
                Tamable = true,
                VirtualArmor = 100,
                WeaponAbility = new SpellStrike<Spells.Seventh.FlameStrikeSpell>(),
                WeaponAbilityChance = 0.65
            });
        }


        [Constructible]
        public FlamingWyrm() : base(CreatureProperties.Get<FlamingWyrm>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Flaming Wyrm Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FlamingWyrm(Serial serial) : base(serial)
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