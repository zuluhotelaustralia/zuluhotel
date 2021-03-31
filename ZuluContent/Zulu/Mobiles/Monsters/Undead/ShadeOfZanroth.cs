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
    public class ShadeOfZanroth : BaseCreature
    {
        static ShadeOfZanroth()
        {
            CreatureProperties.Register<ShadeOfZanroth>(new CreatureProperties
            {
                // CProp_NoReactiveArmour = i1,
                // DataElementId = shadeofzanroth,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = shadeofzanroth,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:customanim /* Weapon */,
                // HitSound = 0x24f /* Weapon */,
                // hostile = 1,
                LootTable = "142",
                LootItemChance = 0,
                LootItemLevel = 0,
                // MissSound = 0x24e /* Weapon */,
                // RunSpeed = 500,
                // script = killpcsTeleporter,
                // Speed = 30 /* Weapon */,
                // Swordsmanship = 165,
                // TrueColor = 17969,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x3ca,
                CorpseNameOverride = "corpse of a Shade of Zanroth",
                CreatureType = CreatureType.Undead,
                DamageMax = 61,
                DamageMin = 31,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 8,
                HitsMax = 500,
                Hue = 17969,
                Int = 500,
                ManaMaxSeed = 500,
                Name = "a Shade of Zanroth",
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Necro, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 120},
                    {SkillName.Tactics, 180}
                },
                StamMaxSeed = 500,
                Str = 500,
                TargetAcquireExhaustion = true,
                VirtualArmor = 60
            });
        }


        [Constructible]
        public ShadeOfZanroth() : base(CreatureProperties.Get<ShadeOfZanroth>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Shade of Zanroth Weapon",
                Hue = 1100,
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x24f,
                MissSound = 0x24e,
                MaxRange = 8,
                Animation = (WeaponAnimation) 0x0009
            });
        }

        [Constructible]
        public ShadeOfZanroth(Serial serial) : base(serial)
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