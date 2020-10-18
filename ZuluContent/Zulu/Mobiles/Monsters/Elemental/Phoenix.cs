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
    public class Phoenix : BaseCreature
    {
        static Phoenix()
        {
            CreatureProperties.Register<Phoenix>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_cast = 1,
                // CProp_FinalDeath = i5,
                // DataElementId = phoenix,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = phoenix,
                // food = veggie,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x90 /* Weapon */,
                // MagicItemChance = 95,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // noloot = 1,
                // num_casts = 3,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = lightning,
                // spell_0 = ebolt,
                // spell_1 = magicreflection,
                // spell_2 = fireball,
                // spell_3 = explosion,
                // spell_4 = flamestrike,
                // Swordsmanship = 140,
                // TrueColor = 1645,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                AutoDispel = true,
                Body = 0x05,
                CorpseNameOverride = "corpse of a Phoenix",
                CreatureType = CreatureType.Elemental,
                DamageMax = 52,
                DamageMin = 17,
                Dex = 400,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 2,
                HideType = HideType.Lava,
                HitsMax = 450,
                Hue = 1645,
                Int = 500,
                ManaMaxSeed = 10,
                MinTameSkill = 105,
                Name = "a Phoenix",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                ProvokeSkillOverride = 120,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100}
                },
                RiseCreatureDelay = TimeSpan.FromSeconds(4),
                RiseCreatureType = typeof(Phoenix),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 85},
                    {SkillName.Tactics, 110},
                    {SkillName.Magery, 130},
                    {SkillName.EvalInt, 120},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 60,
                Str = 450,
                Tamable = true,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public Phoenix() : base(CreatureProperties.Get<Phoenix>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Phoenix Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x90,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Phoenix(Serial serial) : base(serial)
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