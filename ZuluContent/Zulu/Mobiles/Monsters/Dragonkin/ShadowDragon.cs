using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class ShadowDragon : BaseCreature
    {
        static ShadowDragon()
        {
            CreatureProperties.Register<ShadowDragon>(new CreatureProperties
            {
                // CProp_AttackTypeImmunities = i256,
                // CProp_NecroProtection = i8,
                // CProp_noanimate = i1,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = shadowdragon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = shadowdragon,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:spellstrikescript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 75,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 65 /* Weapon */,
                // TrueColor = 17969,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                BardImmune = true,
                BaseSoundID = 362,
                Body = 0xc,
                CanFly = true,
                CorpseNameOverride = "corpse of a Shadow Dragon",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 440,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                Hides = 5,
                HideType = HideType.Dragon,
                HitsMax = 3000,
                Hue = 17969,
                Int = 400,
                ManaMaxSeed = 200,
                Name = "a Shadow Dragon",
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 200},
                    {SkillName.MagicResist, 250},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 140},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 140,
                Str = 3000,
                Tamable = false,
                VirtualArmor = 20,
                WeaponAbility = new SpellStrike<DecayingRaySpell>(),
                WeaponAbilityChance = 1
            });
        }


        [Constructible]
        public ShadowDragon() : base(CreatureProperties.Get<ShadowDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Shadow Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public ShadowDragon(Serial serial) : base(serial)
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