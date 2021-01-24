using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class Enslaver : BaseCreature
    {
        static Enslaver()
        {
            CreatureProperties.Register<Enslaver>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i3,
                // CProp_NecroProtection = i3,
                // CProp_NoReactiveArmour = i1,
                // CProp_NoReactiveArmour_0 = i1,
                // CProp_untamemountatdeath = i1,
                // DataElementId = enslaver,
                // DataElementType = NpcTemplate,
                // Equip = enslaver,
                // Graphic = 0x1438 /* Weapon */,
                // Hitscript = :combat:enslavescript /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 50,
                // MagicItemLevel = 4,
                // MissSound = 0x234 /* Weapon */,
                // mount = 0x3ea4 1645,
                // mounttemplate = rubyfrenziedostard,
                // script = killpcsTeleporter,
                // Speed = 51 /* Weapon */,
                // TrueColor = 1645,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x190,
                ClassLevel = 3,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of <random> the Enslaver",
                CreatureType = CreatureType.Human,
                DamageMax = 50,
                DamageMin = 10,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 2000,
                Hue = 1645,
                Int = 55,
                ManaMaxSeed = 0,
                Name = "<random> the Enslaver",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 75},
                    {ElementalType.Air, 75},
                    {ElementalType.Water, 75},
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 250},
                    {SkillName.Macing, 175},
                    {SkillName.MagicResist, 60},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 200,
                Str = 2000,
                TargetAcquireExhaustion = true,
                VirtualArmor = 60
            });
        }


        [Constructible]
        public Enslaver() : base(CreatureProperties.Get<Enslaver>())
        {
            // Add customization here

            AddItem(new WarHammer
            {
                Movable = false,
                Hue = 1162,
                Name = "Enslavers Weapon",
                Speed = 51,
                HitSound = 0x13C,
                MissSound = 0x234,
                MaxHitPoints = 110,
                HitPoints = 110,
                Animation = (WeaponAnimation) 0x000b
            });
        }

        [Constructible]
        public Enslaver(Serial serial) : base(serial)
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