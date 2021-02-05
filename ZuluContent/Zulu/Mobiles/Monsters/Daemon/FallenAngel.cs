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
    public class FallenAngel : BaseCreature
    {
        static FallenAngel()
        {
            CreatureProperties.Register<FallenAngel>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i500,
                // CProp_EarthProtection = i2,
                // CProp_NecroProtection = i4,
                // CProp_PermMagicImmunity = i6,
                // CProp_Permmr = i8,
                // DataElementId = fallenangel,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = fallenangel,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:trielementalscript /* Weapon */,
                // HitSound = 0x283 /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 25,
                LootItemLevel = 2,
                // MissSound = 0x282 /* Weapon */,
                // Parrying = 130,
                // script = killpcs,
                // speech = 54,
                // Speed = 40 /* Weapon */,
                // TrueColor = 1174,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 372,
                Body = 0x4,
                CanSwim = true,
                CorpseNameOverride = "corpse of a fallen angel",
                CreatureType = CreatureType.Daemon,
                DamageMax = 51,
                DamageMin = 21,
                Dex = 285,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 600,
                Hue = 1174,
                Int = 285,
                ManaMaxSeed = 85,
                Name = "a fallen angel",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 150,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 100},
                    {SkillName.Fencing, 150},
                    {SkillName.DetectHidden, 130}
                },
                StamMaxSeed = 75,
                Str = 600,
                Tamable = false,
                VirtualArmor = 40,
                WeaponAbility = new TriElementalStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public FallenAngel() : base(CreatureProperties.Get<FallenAngel>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "fallen angel Weapon",
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
        public FallenAngel(Serial serial) : base(serial)
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