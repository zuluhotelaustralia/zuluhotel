

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
    public class BrigandArcher : BaseCreature
    {
        static BrigandArcher() => CreatureProperties.Register<BrigandArcher>(new CreatureProperties
        {
            // ammoamount = 30,
            // ammotype = 0xf3f,
            // DataElementId = mountedbrigandarcher,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = mountedbrigandarcher,
            // equip_0 = brigand1,
            // HitSound = 0x235 /* Weapon */,
            // lootgroup = 41,
            // Macefighting = 75,
            // MagicItemChance = 1,
            // missileweapon = archer,
            // MissSound = 0x239 /* Weapon */,
            // mount = 0x3e9f 1109,
            // script = archerkillpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* archerkillpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of a brigand archer",
            CreatureType = CreatureType.Human,
            DamageMax = 22,
            DamageMin = 10,
            Dex = 300,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 150,
            Hue = 0,
            Int = 60,
            ManaMaxSeed = 350,
            Name = "a brigand archer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 65 },
                { SkillName.Tactics, 60 },
                { SkillName.Archery, 140 },
            },
            StamMaxSeed = 350,
            Str = 150,
            VirtualArmor = 20,

        });


        [Constructible]
public BrigandArcher() : base(CreatureProperties.Get<BrigandArcher>())
        {
            // Add customization here


        }

        [Constructible]
public BrigandArcher(Serial serial) : base(serial) {}



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
