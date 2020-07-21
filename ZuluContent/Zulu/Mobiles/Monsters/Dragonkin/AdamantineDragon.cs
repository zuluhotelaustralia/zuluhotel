

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
    public class AdamantineDragon : BaseCreature
    {
        static AdamantineDragon() => CreatureProperties.Register<AdamantineDragon>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_EarthProtection = i8,
            // CProp_looter = s1,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = adamantinedragon,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = adamantinedragon,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:blackrockscript /* Weapon */,
            // HitSound = 0x23D /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 80,
            // Magicitemlevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = firebreather,
            // Speed = 65 /* Weapon */,
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
            // TrueColor = 1006,
            // virtue = 8,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 46,
            CorpseNameOverride = "corpse of an Adamantine Dragon",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 450,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            HitsMax = 2000,
            Hue = 1006,
            Int = 600,
            ManaMaxSeed = 2000,
            MinTameSkill = 140,
            Name = "an Adamantine Dragon",
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
            ProvokeSkillOverride = 200,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 50 },
                { ElementalType.Cold, 100 },
                { ElementalType.Physical, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 80 },
                { SkillName.MagicResist, 140 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 140 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 140,
            Str = 600,
            Tamable = true,
            VirtualArmor = 100,
            WeaponAbility = new BlackrockStrike(),
            WeaponAbilityChance = 1.0,

        });


        [Constructible]
public AdamantineDragon() : base(CreatureProperties.Get<AdamantineDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "adamantine dragon weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public AdamantineDragon(Serial serial) : base(serial) {}



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
