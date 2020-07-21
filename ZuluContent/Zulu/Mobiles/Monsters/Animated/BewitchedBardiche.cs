

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
    public class BewitchedBardiche : BaseCreature
    {
        static BewitchedBardiche() => CreatureProperties.Register<BewitchedBardiche>(new CreatureProperties
        {
            // DataElementId = bewitchedbardiche,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bewitchedbardiche,
            // HitSound = 0x237 /* Weapon */,
            // hostile = 1,
            // lootgroup = 48,
            // MagicItemChance = 1,
            // MissSound = 0x233 /* Weapon */,
            // script = killpcs,
            // Speed = 22 /* Weapon */,
            // Swordsmanship = 140,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x266,
            CorpseNameOverride = "corpse of a Bewitched Bardiche",
            CreatureType = CreatureType.Animated,
            DamageMax = 33,
            DamageMin = 8,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 210,
            Hue = 0,
            Int = 110,
            ManaMaxSeed = 0,
            Name = "a Bewitched Bardiche",
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
public BewitchedBardiche() : base(CreatureProperties.Get<BewitchedBardiche>())
        {
            // Add customization here


        }

        [Constructible]
public BewitchedBardiche(Serial serial) : base(serial) {}



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
