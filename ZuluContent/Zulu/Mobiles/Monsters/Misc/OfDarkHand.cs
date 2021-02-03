using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class OfDarkHand : BaseCreature
    {
        static OfDarkHand()
        {
            CreatureProperties.Register<OfDarkHand>(new CreatureProperties
            {
                // CProp_nomountatdeath = i1,
                // CProp_NoReactiveArmour = i1,
                // DataElementId = darkknightmounted,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = servantofcain,
                // guardignore = 1,
                // HitSound = 0x238 /* Weapon */,
                // hostile = 1,
                LootTable = "59",
                // MissSound = 0x233 /* Weapon */,
                // mount = 0x3e9f 1170,
                // script = killpcsTeleporter,
                // Speed = 15 /* Weapon */,
                // Swordsmanship = 200,
                // TrueColor = 0,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x190,
                ClassLevel = 6,
                ClassType = ZuluClassType.Warrior,
                CorpseNameOverride = "corpse of <random> of the Dark Hand",
                DamageMax = 35,
                DamageMin = 10,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 300,
                Hue = 1172,
                Int = 210,
                ManaMaxSeed = 200,
                Name = "<random> of the Dark Hand",
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 200,
                Str = 500,
                TargetAcquireExhaustion = true
            });
        }


        [Constructible]
        public OfDarkHand() : base(CreatureProperties.Get<OfDarkHand>())
        {
            // Add customization here
        }

        [Constructible]
        public OfDarkHand(Serial serial) : base(serial)
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