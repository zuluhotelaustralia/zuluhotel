using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class Banshee : BaseCreature
    {
        static Banshee()
        {
            CreatureProperties.Register<Banshee>(new CreatureProperties
            {
                // cast_pct = 30,
                // count_casts = 0,
                // CProp_AttackTypeImmunities = i256,
                // CProp_massCastRange = i15,
                // DataElementId = banshee,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = wraith,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x283 /* Weapon */,
                // hostile = 1,
                // lootgroup = 35,
                // MagicItemChance = 80,
                // MagicItemLevel = 5,
                // MissSound = 0x282 /* Weapon */,
                // num_casts = 4,
                // script = spellkillpcsTeleporter,
                // Speed = 40 /* Weapon */,
                // spell = MassCast	kill,
                // spell_0 = MassCast	kill,
                // TrueColor = 0x4001,
                AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 310,
                ClassLevel = 6,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of a banshee",
                CreatureType = CreatureType.Undead,
                DamageMax = 40,
                DamageMin = 15,
                Dex = 380,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 925,
                Hue = 1176,
                Int = 1025,
                ManaMaxSeed = 1025,
                Name = "a banshee",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(WyvernStrikeSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.EvalInt, 100},
                    {SkillName.Magery, 150},
                    {SkillName.Parry, 80},
                    {SkillName.MagicResist, 105},
                    {SkillName.Tactics, 120},
                    {SkillName.Fencing, 120},
                    {SkillName.Hiding, 130}
                },
                StamMaxSeed = 80,
                Str = 925,
                Tamable = false,
                TargetAcquireExhaustion = true,
                VirtualArmor = 40,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public Banshee() : base(CreatureProperties.Get<Banshee>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "wraith Weapon",
                Hue = 1,
                Speed = 40,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x283,
                MissSound = 0x282
            });
        }

        [Constructible]
        public Banshee(Serial serial) : base(serial)
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