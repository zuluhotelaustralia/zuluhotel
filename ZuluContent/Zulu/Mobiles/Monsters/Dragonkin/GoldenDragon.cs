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
    public class GoldenDragon : BaseCreature
    {
        static GoldenDragon()
        {
            CreatureProperties.Register<GoldenDragon>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i500,
                // CProp_looter = s1,


                // CProp_Permmr = i6,
                // DataElementId = goldendragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = goldendragon,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 65,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 60 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = chainlightning,
                // spell_3 = masscurse,
                // spell_4 = gheal,
                // spell_5 = earthquake,
                // spell_6 = manavamp,
                // spell_7 = paralyze,
                // TrueColor = 48,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                AutoDispel = true,
                BaseSoundID = 362,
                Body = 0xc,
                CorpseNameOverride = "corpse of a Golden Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 150,
                DamageMin = 15,
                Dex = 450,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 1250,
                Hue = 48,
                Int = 500,
                ManaMaxSeed = 500,
                MinTameSkill = 170,
                Name = "a Golden Dragon",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Fourth.GreaterHealSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Seventh.ManaVampireSpell),
                    typeof(Spells.Fifth.ParalyzeSpell)
                },
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 50},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 7}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 190},
                    {SkillName.Magery, 150},
                    {SkillName.MagicResist, 100},
                    {SkillName.Parry, 100},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 1000,
                Str = 1250,
                Tamable = true,
                VirtualArmor = 75,
                ActiveSpeed = 0.05,
                PassiveSpeed = 0.2,
                CanFly = true,
                HideType = HideType.GoldenDragon,
                Hides = 5
            });
        }


        [Constructible]
        public GoldenDragon() : base(CreatureProperties.Get<GoldenDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Golden Dragon Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GoldenDragon(Serial serial) : base(serial)
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