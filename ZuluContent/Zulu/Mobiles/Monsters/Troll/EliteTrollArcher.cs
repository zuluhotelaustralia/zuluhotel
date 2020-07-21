

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
    public class EliteTrollArcher : BaseCreature
    {
        static EliteTrollArcher() => CreatureProperties.Register<EliteTrollArcher>(new CreatureProperties
        {
            // ammoamount = 35,
            // ammotype = 0xf3f,
            // DataElementId = trollarcher3,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = trollarcher3,
            // HitSound = 0x235 /* Weapon */,
            // hostile = 1,
            // lootgroup = 41,
            // missileweapon = archer,
            // MissSound = 0x239 /* Weapon */,
            // script = archerkillpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* archerkillpcs */,
            AlwaysMurderer = true,
            Body = 0x36,
            CorpseNameOverride = "corpse of an elite troll archer",
            CreatureType = CreatureType.Troll,
            DamageMax = 22,
            DamageMin = 10,
            Dex = 120,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 200,
            Hue = 33784,
            Int = 60,
            ManaMaxSeed = 0,
            Name = "an elite troll archer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 60 },
                { SkillName.Tactics, 95 },
                { SkillName.Macing, 120 },
                { SkillName.Archery, 105 },
            },
            StamMaxSeed = 60,
            Str = 200,
            VirtualArmor = 25,

        });


        [Constructible]
public EliteTrollArcher() : base(CreatureProperties.Get<EliteTrollArcher>())
        {
            // Add customization here


        }

        [Constructible]
public EliteTrollArcher(Serial serial) : base(serial) {}



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
