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
    public class NightWalker : BaseCreature
    {
        static NightWalker()
        {
            CreatureProperties.Register<NightWalker>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i7,
                // CProp_NecroProtection = i7,
                // CProp_noanimate = i1,
                // CProp_nomountatdeath = i1,
                // CProp_NoReactiveArmour = i1,
                // CProp_PermMagicImmunity = i8,
                // DataElementId = mountednightwalker,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = nightwalker,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x165 /* Weapon */,
                // hostile = 1,
                LootTable = "201",
                LootItemChance = 100,
                LootItemLevel = 9,
                // MissSound = 0x164 /* Weapon */,
                // mount = 793 1,
                // script = killpcsTeleporter,
                // Speed = 37 /* Weapon */,
                // TrueColor = 1157,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 311,
                CanSwim = true,
                CorpseNameOverride = "corpse of a Night Walker",
                CreatureType = CreatureType.Elemental,
                DamageMax = 46,
                DamageMin = 16,
                Dex = 600,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 4000,
                Hue = 1157,
                Int = 1000,
                ManaMaxSeed = 1000,
                Name = "a Night Walker",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.PermPoisonImmunity, 25}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 200},
                    {SkillName.Tactics, 100},
                    {SkillName.Fencing, 150},
                    {SkillName.Parry, 100},
                    {SkillName.DetectHidden, 130},
                    {SkillName.Hiding, 200}
                },
                StamMaxSeed = 500,
                Str = 4000,
                Tamable = false,
                TargetAcquireExhaustion = true,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public NightWalker() : base(CreatureProperties.Get<NightWalker>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Nightwalker weapon",
                Hue = 1,
                Speed = 37,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x165,
                MissSound = 0x164
            });

            AddItem(new MetalShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500
            });
        }

        [Constructible]
        public NightWalker(Serial serial) : base(serial)
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