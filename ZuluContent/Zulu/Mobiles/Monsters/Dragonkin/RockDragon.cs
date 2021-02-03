using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class RockDragon : BaseCreature
    {
        static RockDragon()
        {
            CreatureProperties.Register<RockDragon>(new CreatureProperties
            {
                // cast_pct = 40,
                // CProp_EarthProtection = i8,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = rockdragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rockdragon,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "37",
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
                // TrueColor = 1160,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0xc,
                CanFly = true,
                CorpseNameOverride = "corpse of a Rock Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 60,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Dragon,
                HitsMax = 800,
                Hue = 1160,
                Int = 400,
                ManaMaxSeed = 200,
                MinTameSkill = 140,
                Name = "a Rock Dragon",
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
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 140},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 140,
                Str = 800,
                Tamable = true,
                VirtualArmor = 50,
                WeaponAbility = new SpellStrike<ShiftingEarthSpell>(),
                WeaponAbilityChance = 0.5
            });
        }


        [Constructible]
        public RockDragon() : base(CreatureProperties.Get<RockDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rock Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public RockDragon(Serial serial) : base(serial)
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