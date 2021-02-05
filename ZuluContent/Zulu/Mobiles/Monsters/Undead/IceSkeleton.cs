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
    public class IceSkeleton : BaseCreature
    {
        static IceSkeleton()
        {
            CreatureProperties.Register<IceSkeleton>(new CreatureProperties
            {
                // CProp_NecroProtection = i3,
                // DataElementId = Iceskeleton,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = Iceskeleton,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1C5 /* Weapon */,
                // hostile = 1,
                LootTable = "14",
                LootItemChance = 2,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // Swordsmanship = 125,
                // targetText = "Ego apokteinou",
                // TrueColor = 0x0515,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x39,
                CorpseNameOverride = "corpse of an ice skeleton",
                CreatureType = CreatureType.Undead,
                DamageMax = 44,
                DamageMin = 8,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 200,
                Hue = 0x0515,
                Int = 35,
                ManaMaxSeed = 25,
                Name = "an ice skeleton",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 50,
                Str = 200,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public IceSkeleton() : base(CreatureProperties.Get<IceSkeleton>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ice Skeleton Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public IceSkeleton(Serial serial) : base(serial)
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