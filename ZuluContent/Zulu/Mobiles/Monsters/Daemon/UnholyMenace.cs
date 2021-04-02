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
    public class UnholyMenace : BaseCreature
    {
        static UnholyMenace()
        {
            CreatureProperties.Register<UnholyMenace>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i500,
                // CProp_looter = s1,
                // CProp_noanimate = i1,
                // CProp_NoReactiveArmour = i1,


                // CProp_Permmr = i6,
                // DataElementId = undeadmenace,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = undeadmenace,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x23D /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 80,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // script = firebreather,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1305,
                // virtue = 8,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysMurderer = true,
                AutoDispel = true,
                BardImmune = true,
                BaseSoundID = 362,
                Body = 309,
                CorpseNameOverride = "corpse of an Unholy Menace",
                CreatureType = CreatureType.Daemon,
                DamageMax = 150,
                DamageMin = 15,
                Dex = 450,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 1000,
                Hue = 1305,
                Int = 500,
                ManaMaxSeed = 500,
                Name = "an Unholy Menace",
                PerceptionRange = 10,
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 50},
                    {ElementalType.MagicImmunity, 7},
                    {ElementalType.Earth, 75},
                    {ElementalType.Necro, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 150},
                    {SkillName.MagicResist, 100},
                    {SkillName.Parry, 100},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 500,
                Str = 1250,
                Tamable = false,
                VirtualArmor = 75
            });
        }


        [Constructible]
        public UnholyMenace() : base(CreatureProperties.Get<UnholyMenace>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Menace Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500
            });
        }

        [Constructible]
        public UnholyMenace(Serial serial) : base(serial)
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