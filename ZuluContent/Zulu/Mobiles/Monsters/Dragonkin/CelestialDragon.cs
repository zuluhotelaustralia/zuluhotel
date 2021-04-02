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
    public class CelestialDragon : BaseCreature
    {
        static CelestialDragon()
        {
            CreatureProperties.Register<CelestialDragon>(new CreatureProperties
            {
                // cast_pct = 40,
                // CProp_HolyProtection = i6,
                // DataElementId = celestialdragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = celestialdragon,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:blackrockscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 75,
                LootItemLevel = 5,
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
                // TrueColor = 1301,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                BaseSoundID = 362,
                Body = 103,
                CanFly = true,
                CorpseNameOverride = "corpse of a Celestial Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Wyrm,
                HitsMax = 850,
                Hue = 1301,
                InitialInnocent = true,
                Int = 400,
                ManaMaxSeed = 200,
                MinTameSkill = 145,
                Name = "a Celestial Dragon",
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
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 140,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.MagicImmunity, 5}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 140},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 140,
                Str = 850,
                Tamable = true,
                VirtualArmor = 50,
                WeaponAbility = new BlackrockStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public CelestialDragon() : base(CreatureProperties.Get<CelestialDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Celestial Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public CelestialDragon(Serial serial) : base(serial)
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