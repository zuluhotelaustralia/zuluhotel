

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
    public class BewitchedWarhammer : BaseCreature
    {
        static BewitchedWarhammer() => CreatureProperties.Register<BewitchedWarhammer>(new CreatureProperties
        {
            // DataElementId = bewitchedwarhammer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bewitchedwarhammer,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 48,
            // Macefighting = 140,
            // MagicItemChance = 1,
            // MissSound = 0x234 /* Weapon */,
            // script = killpcs,
            // Speed = 34 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x286,
            CorpseNameOverride = "corpse of a Bewitched Warhammer",
            CreatureType = CreatureType.Animated,
            DamageMax = 26,
            DamageMin = 6,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 210,
            Hue = 0,
            Int = 110,
            ManaMaxSeed = 0,
            Name = "a Bewitched Warhammer",
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
public BewitchedWarhammer() : base(CreatureProperties.Get<BewitchedWarhammer>())
        {
            // Add customization here


        }

        [Constructible]
public BewitchedWarhammer(Serial serial) : base(serial) {}



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
