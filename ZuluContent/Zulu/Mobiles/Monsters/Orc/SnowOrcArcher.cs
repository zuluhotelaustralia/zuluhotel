

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
    public class SnowOrcArcher : BaseCreature
    {
        static SnowOrcArcher() => CreatureProperties.Register<SnowOrcArcher>(new CreatureProperties
        {
            // ammoamount = 30,
            // ammotype = 0x1bfb,
            // DataElementId = snoworcarcher,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = snoworcarcher,
            // HitSound = 0x235 /* Weapon */,
            // hostile = 1,
            // lootgroup = 72,
            // missileweapon = xbowman,
            // MissSound = 0x239 /* Weapon */,
            // script = explosionkillpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* explosionkillpcs */,
            AlwaysMurderer = true,
            Body = 0x11,
            CorpseNameOverride = "corpse of <random> the snow orc archer",
            CreatureType = CreatureType.Orc,
            DamageMax = 27,
            DamageMin = 12,
            Dex = 250,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 125,
            Hue = 1154,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "<random> the snow orc archer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 95,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 90 },
                { SkillName.Macing, 85 },
                { SkillName.Archery, 90 },
            },
            StamMaxSeed = 50,
            Str = 125,
            VirtualArmor = 25,

        });


        [Constructible]
public SnowOrcArcher() : base(CreatureProperties.Get<SnowOrcArcher>())
        {
            // Add customization here


        }

        [Constructible]
public SnowOrcArcher(Serial serial) : base(serial) {}



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
