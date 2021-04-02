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
    public class GoldenDragonLord : BaseCreature
    {
        static GoldenDragonLord()
        {
            CreatureProperties.Register<GoldenDragonLord>(new CreatureProperties
            {
                // CProp_looter = s1,
                // CProp_noanimate = i1,
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i500,

                // CProp_Permmr = i6,
                // DataElementId = goldendragonlord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = goldendragonlord,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                LootTable = "150",
                LootItemChance = 100,
                LootItemLevel = 9,
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
                // TrueColor = 1300,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                AutoDispel = true,
                BardImmune = true,
                BaseSoundID = 362,
                Body = 0xc,
                CorpseNameOverride = "corpse of a Golden Dragon Lord",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 150,
                DamageMin = 15,
                Dex = 450,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 5250,
                Hue = 1300,
                Int = 500,
                ManaMaxSeed = 800,
                Name = "a Golden Dragon Lord",
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
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 50},
                    {ElementalType.Earth, 100},
                    {ElementalType.Necro, 50},
                    {ElementalType.MagicImmunity, 7}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 250},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 150},
                    {SkillName.MagicResist, 300},
                    {SkillName.Parry, 300},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 500,
                Str = 2250,
                Tamable = false,
                VirtualArmor = 100
            });
        }


        [Constructible]
        public GoldenDragonLord() : base(CreatureProperties.Get<GoldenDragonLord>())
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
        public GoldenDragonLord(Serial serial) : base(serial)
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