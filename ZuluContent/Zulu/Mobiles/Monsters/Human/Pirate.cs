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
    public class Pirate : BaseCreature
    {
        static Pirate()
        {
            CreatureProperties.Register<Pirate>(new CreatureProperties
            {
                // DataElementId = pirate,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = pirate,
                // HitSound = 0x23C /* Weapon */,
                // lootgroup = 47,
                // MissSound = 0x23A /* Weapon */,
                // script = killpcs,
                // speech = 46,
                // Speed = 45 /* Weapon */,
                // Swordsmanship = 75,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 342,
                Body = 0x190,
                CorpseNameOverride = "corpse of <random> the pirate",
                CreatureType = CreatureType.Human,
                DamageMax = 16,
                DamageMin = 4,
                Dex = 210,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 110,
                Hue = 33784,
                Int = 60,
                ManaMaxSeed = 0,
                Name = "<random> the pirate",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 80}
                },
                StamMaxSeed = 50,
                Str = 110,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public Pirate() : base(CreatureProperties.Get<Pirate>())
        {
            // Add customization here
        }

        [Constructible]
        public Pirate(Serial serial) : base(serial)
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