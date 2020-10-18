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
    public class SlaveGuard : BaseCreature
    {
        static SlaveGuard()
        {
            CreatureProperties.Register<SlaveGuard>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i1000,
                // CProp_EarthProtection = i1,
                // CProp_NecroProtection = i1,
                // CProp_NoReactiveArmour = i1,
                // CProp_NoReactiveArmour_0 = i1,
                // CProp_untamemountatdeath = i1,
                // DataElementId = slaveguard,
                // DataElementType = NpcTemplate,
                // Equip = enslaver,
                // Graphic = 0x1438 /* Weapon */,
                // Hitscript = :combat:enslavescript /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 20,
                // MagicItemLevel = 1,
                // MissSound = 0x234 /* Weapon */,
                // mount = 0x3ea4 1159,
                // mounttemplate = emeraldfrenziedostard,
                // script = killpcs,
                // Speed = 51 /* Weapon */,
                // TrueColor = 1159,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x190,
                ClassLevel = 1,
                ClassSpec = SpecName.Warrior,
                CorpseNameOverride = "corpse of <random> the Slave Guard",
                CreatureType = CreatureType.Human,
                DamageMax = 50,
                DamageMin = 10,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 1000,
                Hue = 1159,
                Int = 55,
                ManaMaxSeed = 0,
                Name = "<random> the Slave Guard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 25},
                    {ElementalType.Air, 25},
                    {ElementalType.Water, 25},
                    {ElementalType.Poison, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 250},
                    {SkillName.Macing, 175},
                    {SkillName.MagicResist, 60},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 200,
                Str = 1000,
                VirtualArmor = 60
            });
        }


        [Constructible]
        public SlaveGuard() : base(CreatureProperties.Get<SlaveGuard>())
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
        public SlaveGuard(Serial serial) : base(serial)
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