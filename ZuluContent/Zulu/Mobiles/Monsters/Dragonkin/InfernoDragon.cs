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
    public class InfernoDragon : BaseCreature
    {
        static InfernoDragon()
        {
            CreatureProperties.Register<InfernoDragon>(new CreatureProperties
            {
                // cast_pct = 40,
                // DataElementId = infernodragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = infernodragon,
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
                // spell_9 = gheal,
                // TrueColor = 1172,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BaseSoundID = 362,
                Body = 0x3b,
                CorpseNameOverride = "corpse of an Inferno Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 160,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Lava,
                HitsMax = 500,
                Hue = 1172,
                Int = 110,
                ManaMaxSeed = 100,
                MinTameSkill = 140,
                Name = "an Inferno Dragon",
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
                    typeof(Spells.Fourth.GreaterHealSpell)
                },
                ProvokeSkillOverride = 140,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 200},
                    {ElementalType.PermMagicImmunity, 4}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 120},
                    {SkillName.Macing, 140},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 150,
                Str = 500,
                Tamable = true,
                VirtualArmor = 40,
                WeaponAbility = new SpellStrike<RisingFireSpell>(),
                WeaponAbilityChance = 0.5
            });
        }


        [Constructible]
        public InfernoDragon() : base(CreatureProperties.Get<InfernoDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Inferno Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public InfernoDragon(Serial serial) : base(serial)
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