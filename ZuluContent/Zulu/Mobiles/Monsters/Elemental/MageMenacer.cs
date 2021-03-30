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
    public class MageMenacer : BaseCreature
    {
        static MageMenacer()
        {
            CreatureProperties.Register<MageMenacer>(new CreatureProperties
            {
                // alignment_0 = evil,
                // CProp_AttackTypeImmunities = i639,
                // CProp_noanimate = i1,
                // CProp_NoReactiveArmour = i1,
                // DataElementId = hiddenmagemenacer,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = magemenacer,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonblackrockscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                // hostile_0 = 1,
                LootTable = "9",
                LootItemChance = 80,
                LootItemLevel = 7,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 35,
                // Speed = 50 /* Weapon */,
                // TrueColor = 1306,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x23e,
                CorpseNameOverride = "corpse of a Mage Menacer",
                CreatureType = CreatureType.Elemental,
                DamageMax = 80,
                DamageMin = 10,
                Dex = 600,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitPoison = Poison.Deadly,
                HitsMax = 3000,
                Hue = 1306,
                Int = 25,
                ManaMaxSeed = 0,
                Name = "a Mage Menacer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Physical, 100},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Poisoning, 100},
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 160},
                    {SkillName.DetectHidden, 130},
                    {SkillName.Hiding, 130}
                },
                StamMaxSeed = 500,
                Str = 2500,
                Tamable = false,
                VirtualArmor = 60,
                WeaponAbility = new BlackrockStrike(),
                WeaponAbilityChance = 1.0
            });
        }


        [Constructible]
        public MageMenacer() : base(CreatureProperties.Get<MageMenacer>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Mage Menacer Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public MageMenacer(Serial serial) : base(serial)
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