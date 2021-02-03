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
    public class BewitchedPeasant : BaseCreature
    {
        static BewitchedPeasant()
        {
            CreatureProperties.Register<BewitchedPeasant>(new CreatureProperties
            {
                // DataElementId = bewitchedpeasant,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // equip = cothes,
                // Equip_0 = bewitchedpeasant,
                // guardignore = 1,
                LootTable = "47",
                // script = killpcs,
                // Swordsmanship = 75,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of <random> the bewitched peasant",
                CreatureType = CreatureType.Human,
                Dex = 60,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 80,
                Hue = 33784,
                Int = 60,
                ManaMaxSeed = 0,
                Name = "<random> the bewitched peasant",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                RiseCreatureDelay = TimeSpan.FromSeconds(25),
                RiseCreatureType = typeof(Spectre),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 50}
                },
                StamMaxSeed = 50,
                Str = 80
            });
        }


        [Constructible]
        public BewitchedPeasant() : base(CreatureProperties.Get<BewitchedPeasant>())
        {
            // Add customization here
        }

        [Constructible]
        public BewitchedPeasant(Serial serial) : base(serial)
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