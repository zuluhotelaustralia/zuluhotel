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
    public class Spectre : BaseCreature
    {
        static Spectre()
        {
            CreatureProperties.Register<Spectre>(new CreatureProperties
            {
                // CProp_AttackTypeImmunities = i256,
                // CProp_NecroProtection = i3,
                // DataElementId = spectre,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = spectre,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x181 /* Weapon */,
                // hostile = 1,
                LootTable = "3",
                LootItemChance = 5,
                LootItemLevel = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 60 /* Weapon */,
                // Swordsmanship = 75,
                // TrueColor = 25125,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x1a,
                CorpseNameOverride = "corpse of a spectre",
                CreatureType = CreatureType.Undead,
                DamageMax = 15,
                DamageMin = 3,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 135,
                Hue = 25125,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "a spectre",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 55},
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 120}
                },
                StamMaxSeed = 50,
                Str = 135,
                Tamable = false,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Spectre() : base(CreatureProperties.Get<Spectre>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Spectre Weapon",
                Speed = 60,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x181,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Spectre(Serial serial) : base(serial)
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