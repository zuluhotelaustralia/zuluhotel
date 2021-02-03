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
    public class Zombie : BaseCreature
    {
        static Zombie()
        {
            CreatureProperties.Register<Zombie>(new CreatureProperties
            {
                // CProp_NecroProtection = i3,
                // DataElementId = zombie,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = zombie,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1DA /* Weapon */,
                // hostile = 1,
                LootTable = "24",
                LootItemChance = 0,
                LootItemLevel = 0,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // Swordsmanship = 65,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x03,
                CorpseNameOverride = "corpse of a zombie",
                CreatureType = CreatureType.Undead,
                DamageMax = 16,
                DamageMin = 2,
                Dex = 40,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 75,
                Hue = 0,
                Int = 15,
                ManaMaxSeed = 5,
                Name = "a zombie",
                PassiveSpeed = 0.4,
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
                StamMaxSeed = 30,
                Str = 75,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Zombie() : base(CreatureProperties.Get<Zombie>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Zombie Weapon",
                Speed = 25,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1DA,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Zombie(Serial serial) : base(serial)
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