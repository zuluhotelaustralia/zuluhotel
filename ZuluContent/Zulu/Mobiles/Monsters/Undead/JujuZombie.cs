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
    public class JujuZombie : BaseCreature
    {
        static JujuZombie()
        {
            CreatureProperties.Register<JujuZombie>(new CreatureProperties
            {
                // CProp_NecroProtection = i8,
                // CProp_NoReactiveArmour = i1,
                // DataElementId = jujuzombie,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = jujuzombie,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:customanim /* Weapon */,
                // HitSound = 0x11C /* Weapon */,
                // hostile = 1,
                LootTable = "24",
                LootItemChance = 0,
                LootItemLevel = 0,
                // MissSound = 0x11D /* Weapon */,
                // RunSpeed = 500,
                // script = killpcsTeleporter,
                // Speed = 40 /* Weapon */,
                // Swordsmanship = 165,
                // TrueColor = 1300,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x03,
                CorpseNameOverride = "corpse of a juju zombie",
                CreatureType = CreatureType.Undead,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 4,
                HitsMax = 375,
                Hue = 1300,
                Int = 15,
                ManaMaxSeed = 5,
                Name = "a juju zombie",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 40},
                    {SkillName.Tactics, 80}
                },
                StamMaxSeed = 400,
                Str = 375,
                TargetAcquireExhaustion = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public JujuZombie() : base(CreatureProperties.Get<JujuZombie>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Juju Zombie Weapon",
                Hue = 1100,
                Speed = 40,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x11C,
                MissSound = 0x11D,
                MaxRange = 4,
                Animation = (WeaponAnimation) 0x0009
            });
        }

        [Constructible]
        public JujuZombie(Serial serial) : base(serial)
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