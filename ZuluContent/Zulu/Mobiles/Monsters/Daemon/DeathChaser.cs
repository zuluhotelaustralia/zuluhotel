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
    public class DeathChaser : BaseCreature
    {
        static DeathChaser()
        {
            CreatureProperties.Register<DeathChaser>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i250,
                // DataElementId = deathchaser,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = greatwyrm,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 80,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
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
                // TrueColor = 1489,
                // virtue = 7,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 43,
                CorpseNameOverride = "corpse of a Death Chaser",
                CreatureType = CreatureType.Daemon,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 475,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 1100,
                Hue = 1489,
                Int = 650,
                ManaMaxSeed = 150,
                Name = "a Death Chaser",
                PassiveSpeed = 0.4,
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
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.MagicImmunity, 5}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 200},
                    {SkillName.MagicResist, 110},
                    {SkillName.Magery, 150},
                    {SkillName.DetectHidden, 150}
                },
                StamMaxSeed = 175,
                Str = 950,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public DeathChaser() : base(CreatureProperties.Get<DeathChaser>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Great Wyrm Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DeathChaser(Serial serial) : base(serial)
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