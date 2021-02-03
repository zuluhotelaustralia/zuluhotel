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
    public class Warrior : BaseCreature
    {
        static Warrior()
        {
            CreatureProperties.Register<Warrior>(new CreatureProperties
            {
                // DataElementId = lizardman5,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = lizardman5,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                LootTable = "11",
                // Macefighting = 85,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 417,
                Body = 0x24,
                CorpseNameOverride = "corpse of <random> the warrior",
                CreatureType = CreatureType.Dragonkin,
                DamageMax = 30,
                DamageMin = 5,
                Dex = 103,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Lizard,
                HitsMax = 205,
                Hue = 0,
                Int = 60,
                ManaMaxSeed = 40,
                Name = "<random> the warrior",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 70,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 80}
                },
                StamMaxSeed = 75,
                Str = 205,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Warrior() : base(CreatureProperties.Get<Warrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lizardman5 Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Warrior(Serial serial) : base(serial)
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