

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
    public class BewitchedSword : BaseCreature
    {
        static BewitchedSword() => CreatureProperties.Register<BewitchedSword>(new CreatureProperties
        {
            // DataElementId = bewitchedsword,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bewitchedsword,
            // HitSound = 0x23C /* Weapon */,
            // hostile = 1,
            // lootgroup = 48,
            // MagicItemChance = 1,
            // MissSound = 0x23A /* Weapon */,
            // script = killpcs,
            // Speed = 25 /* Weapon */,
            // Swordsmanship = 140,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x283,
            CorpseNameOverride = "corpse of a Bewitched Sword",
            CreatureType = CreatureType.Animated,
            DamageMax = 23,
            DamageMin = 1,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 210,
            Hue = 0,
            Int = 110,
            ManaMaxSeed = 0,
            Name = "a Bewitched Sword",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 94,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.MagicResist, 70 },
            },
            StamMaxSeed = 100,
            Str = 210,
            VirtualArmor = 35,

        });


        [Constructible]
public BewitchedSword() : base(CreatureProperties.Get<BewitchedSword>())
        {
            // Add customization here


        }

        [Constructible]
public BewitchedSword(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
