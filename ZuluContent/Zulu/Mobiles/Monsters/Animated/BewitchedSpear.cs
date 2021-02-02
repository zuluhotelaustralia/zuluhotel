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
    public class BewitchedSpear : BaseCreature
    {
        static BewitchedSpear()
        {
            CreatureProperties.Register<BewitchedSpear>(new CreatureProperties
            {
                // DataElementId = bewitchedspear,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = bewitchedspear,
                // HitSound = 0x23D /* Weapon */,
                // hostile = 1,
                LootTable = "48",
                LootItemChance = 1,
                // MissSound = 0x23B /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x272,
                CorpseNameOverride = "corpse of a Bewitched Spear",
                CreatureType = CreatureType.Animated,
                DamageMax = 21,
                DamageMin = 2,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 210,
                Hue = 0,
                Int = 110,
                ManaMaxSeed = 0,
                Name = "a Bewitched Spear",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 94,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Fencing, 140},
                    {SkillName.MagicResist, 70}
                },
                StamMaxSeed = 100,
                Str = 210,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public BewitchedSpear() : base(CreatureProperties.Get<BewitchedSpear>())
        {
            // Add customization here
        }

        [Constructible]
        public BewitchedSpear(Serial serial) : base(serial)
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