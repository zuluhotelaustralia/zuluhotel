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
    public class DamageTester : BaseCreature
    {
        static DamageTester()
        {
            CreatureProperties.Register<DamageTester>(new CreatureProperties
            {
                // AttackAttribute = Wrestling,
                // AttackSpeed = 1,
                // CProp_MerchantType = sgardener,
                // DataElementId = damagetester,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = gardener,
                // guardignore = 1,
                // Macefighting = 1,
                // noloot = 1,
                // script = animal,
                // speech = 28,
                // Swordsmanship = 1,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Animal /* animal */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of <random> the damage tester",
                DamageMax = 0,
                DamageMin = 0,
                Dex = 200,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 100000,
                Hue = 33784,
                Int = 200,
                ManaMaxSeed = 200,
                Name = "<random> the damage tester",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 200},
                    {SkillName.Mining, 1},
                    {SkillName.Fencing, 1},
                    {SkillName.Tactics, 1},
                    {SkillName.Wrestling, 1}
                },
                StamMaxSeed = 200,
                Str = 60000
            });
        }


        [Constructible]
        public DamageTester() : base(CreatureProperties.Get<DamageTester>())
        {
            // Add customization here
        }

        [Constructible]
        public DamageTester(Serial serial) : base(serial)
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