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
    public class IceTroll : BaseCreature
    {
        static IceTroll()
        {
            CreatureProperties.Register<IceTroll>(new CreatureProperties
            {
                // DataElementId = icetroll,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = icetroll,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x233 /* Weapon */,
                // hostile = 1,
                // lootgroup = 14,
                // MissSound = 0x23B /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // Swordsmanship = 130,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x35,
                CorpseNameOverride = "corpse of an ice troll",
                CreatureType = CreatureType.Troll,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 185,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 250,
                Hue = 1154,
                Int = 70,
                ManaMaxSeed = 60,
                Name = "an ice troll",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 100,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 150,
                Str = 250,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public IceTroll() : base(CreatureProperties.Get<IceTroll>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ice Troll Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x233,
                MissSound = 0x23B
            });
        }

        [Constructible]
        public IceTroll(Serial serial) : base(serial)
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