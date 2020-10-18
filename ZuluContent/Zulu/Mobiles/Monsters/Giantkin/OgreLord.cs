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
    public class OgreLord : BaseCreature
    {
        static OgreLord()
        {
            CreatureProperties.Register<OgreLord>(new CreatureProperties
            {
                // DataElementId = ogrelord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ogrelord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1AE /* Weapon */,
                // hostile = 1,
                // lootgroup = 59,
                // MagicItemChance = 5,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 6,
                // Speed = 40 /* Weapon */,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x01,
                CorpseNameOverride = "corpse of <random> the Ogre Lord",
                CreatureType = CreatureType.Giantkin,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 230,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 500,
                Hue = 0,
                Int = 75,
                ManaMaxSeed = 65,
                Name = "<random> the Ogre Lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 80,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 80,
                Str = 500,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public OgreLord() : base(CreatureProperties.Get<OgreLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ogre Lord Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1AE,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OgreLord(Serial serial) : base(serial)
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