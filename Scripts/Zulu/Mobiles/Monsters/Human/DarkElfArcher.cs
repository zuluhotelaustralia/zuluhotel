

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
    public class DarkElfArcher : BaseCreature
    {
        static DarkElfArcher() => CreatureProperties.Register<DarkElfArcher>(new CreatureProperties
        {
            // ammoamount = 60,
            // ammotype = 0xf3f,
            // CProp_looter = s1,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = drowarcher,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = drowarcher,
            // HitSound = 0x235 /* Weapon */,
            // hostile = 1,
            // lootgroup = 41,
            // missileweapon = archer,
            // MissSound = 0x239 /* Weapon */,
            // script = archerkillpcs,
            // Speed = 35 /* Weapon */,
            // swordsmanship = 110,
            // TrueColor = 0x0455,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* archerkillpcs */,
            AlwaysMurderer = true,
            Body = 0x191,
            CorpseNameOverride = "corpse of a dark elf archer",
            CreatureType = CreatureType.Human,
            DamageMax = 22,
            DamageMin = 10,
            Dex = 130,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 165,
            Hue = 0x0455,
            Int = 95,
            ManaMaxSeed = 85,
            Name = "a dark elf archer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 60,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 95 },
                { SkillName.Tactics, 150 },
                { SkillName.MagicResist, 110 },
                { SkillName.Archery, 150 },
                { SkillName.Hiding, 100 },
            },
            StamMaxSeed = 95,
            Str = 165,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public DarkElfArcher() : base(CreatureProperties.Get<DarkElfArcher>())
        {
            // Add customization here

  
        }

        public DarkElfArcher(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}