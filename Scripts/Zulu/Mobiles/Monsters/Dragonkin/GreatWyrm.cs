

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
    public class GreatWyrm : BaseCreature
    {
        static GreatWyrm() => CreatureProperties.Register<GreatWyrm>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i250,
            // CProp_PermMagicImmunity = i5,
            // DataElementId = greatwyrm,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = greatwyrm,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16B /* Weapon */,
            // hostile = 1,
            // lootgroup = 35,
            // MagicItemChance = 80,
            // MagicItemLevel = 5,
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
            // TrueColor = 1159,
            // virtue = 7,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            AutoDispel = true,
            BaseSoundID = 362,
            Body = 46,
            CorpseNameOverride = "corpse of a Great Wyrm",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 475,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            Hides = 5,
            HideType = HideType.Wyrm,
            HitsMax = 900,
            Hue = 1159,
            Int = 650,
            ManaMaxSeed = 150,
            MinTameSkill = 150,
            Name = "a Great Wyrm",
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
                typeof(Spells.Third.FireballSpell),
            },
            ProvokeSkillOverride = 150,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Fire, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 200 },
                { SkillName.MagicResist, 110 },
                { SkillName.Magery, 150 },
                { SkillName.DetectHidden, 150 },
            },
            StamMaxSeed = 175,
            Str = 900,
            Tamable = true,
            VirtualArmor = 50,
  
        });

        [Constructable]
        public GreatWyrm() : base(CreatureProperties.Get<GreatWyrm>())
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
                MissSound = 0x239,
            });
  
  
        }

        public GreatWyrm(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}