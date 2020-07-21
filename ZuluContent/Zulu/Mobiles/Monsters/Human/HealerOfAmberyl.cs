

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
    public class HealerOfAmberyl : BaseCreature
    {
        static HealerOfAmberyl() => CreatureProperties.Register<HealerOfAmberyl>(new CreatureProperties
        {
            // CProp_equipt = sacleric,
            // DataElementId = healer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = healer,
            // guardignore = 1,
            // Macefighting = 200,
            // Piety = 90,
            // Privs = invul,
            // script = daves_healer,
            // Settings = invul,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Healer /* daves_healer */,
            Body = 0x191,
            CorpseNameOverride = "corpse of <random>, Healer of Amberyl",
            CreatureType = CreatureType.Human,
            Dex = 200,
            Female = true,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 300,
            Hue = 33784,
            InitialInnocent = true,
            Int = 300,
            ManaMaxSeed = 200,
            Name = "<random>, Healer of Amberyl",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Magery, 200 },
            },
            StamMaxSeed = 100,
            Str = 300,
            VirtualArmor = 100,

        });


        [Constructible]
public HealerOfAmberyl() : base(CreatureProperties.Get<HealerOfAmberyl>())
        {
            // Add customization here


        }

        [Constructible]
public HealerOfAmberyl(Serial serial) : base(serial) {}



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
