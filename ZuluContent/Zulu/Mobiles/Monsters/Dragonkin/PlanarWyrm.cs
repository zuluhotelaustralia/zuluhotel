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
    public class PlanarWyrm : BaseCreature
    {
        static PlanarWyrm()
        {
            CreatureProperties.Register<PlanarWyrm>(new CreatureProperties
            {
                // CProp_BaseHpRegen = i250,
                // DataElementId = planarwyrm,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = daemonwyrm,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:customanim /* Weapon */,
                // HitSound = 0x16B /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 80,
                LootItemLevel = 5,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcsTeleporter,
                // Speed = 60 /* Weapon */,
                // TrueColor = 1301,
                // virtue = 7,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                BaseSoundID = 362,
                Body = 0x3b,
                CorpseNameOverride = "corpse of a Planar Wyrm",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 75,
                DamageMin = 25,
                Dex = 1475,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 7,
                HitsMax = 3000,
                Hue = 1301,
                Int = 650,
                ManaMaxSeed = 150,
                MinTameSkill = 150,
                Name = "a Planar Wyrm",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Fire, 100},
                    {ElementalType.PermMagicImmunity, 5}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 200},
                    {SkillName.Magery, 150},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 175,
                Str = 1800,
                Tamable = true,
                TargetAcquireExhaustion = true,
                VirtualArmor = 50
            });
        }


        [Constructible]
        public PlanarWyrm() : base(CreatureProperties.Get<PlanarWyrm>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemon Wyrm Weapon",
                Hue = 1645,
                Speed = 60,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239,
                MaxRange = 7,
                Animation = (WeaponAnimation) 0x0009
            });
        }

        [Constructible]
        public PlanarWyrm(Serial serial) : base(serial)
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