

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
    public class PoisonWyrm : BaseCreature
    {
        static PoisonWyrm() => CreatureProperties.Register<PoisonWyrm>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_PermMagicImmunity = i5,
            // DataElementId = poisonwyrm,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = poisonwyrm,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x16B /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 90,
            // Magicitemlevel = 6,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = firebreather,
            // Speed = 35 /* Weapon */,
            // spell = fireball,
            // spell_0 = flamestrike,
            // spell_1 = ebolt,
            // spell_2 = lightning,
            // spell_3 = harm,
            // spell_4 = mindblast,
            // spell_5 = magicarrow,
            // spell_6 = chainlightning,
            // spell_7 = weaken,
            // spell_8 = masscurse,
            // TrueColor = 264,
            // virtue = 8,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BardImmune = true,
            BaseSoundID = 362,
            Body = 0x3b,
            CanFly = true,
            CorpseNameOverride = "corpse of a Poison Wyrm",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 155,
            DamageMin = 20,
            Dex = 340,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            HitPoison = Poison.Deadly,
            HitsMax = 800,
            Hue = 264,
            Int = 400,
            ManaMaxSeed = 200,
            MinTameSkill = 140,
            Name = "a Poison Wyrm",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Second.HarmSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.First.WeakenSpell),
                typeof(Spells.Sixth.MassCurseSpell),
            },
            ProvokeSkillOverride = 140,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 80 },
                { SkillName.MagicResist, 165 },
                { SkillName.Tactics, 120 },
                { SkillName.Macing, 140 },
                { SkillName.Poisoning, 140 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 140,
            Str = 1900,
            Tamable = true,
            VirtualArmor = 60,

        });


        [Constructible]
public PoisonWyrm() : base(CreatureProperties.Get<PoisonWyrm>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Poison Wyrm Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239,
            });


        }

        [Constructible]
public PoisonWyrm(Serial serial) : base(serial) {}



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
