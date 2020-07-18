

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
    public class AnUndeadPirate : BaseCreature
    {
        static AnUndeadPirate() => CreatureProperties.Register<AnUndeadPirate>(new CreatureProperties
        {
            // DataElementId = undeadpirate1,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = undeadpirate1,
            // guardignore = 1,
            // HitSound = 0x23C /* Weapon */,
            // hostile = 1,
            // lootgroup = 47,
            // MagicItemChance = 10,
            // MagicItemLevel = 1,
            // MissSound = 0x23A /* Weapon */,
            // script = killpcs,
            // Speed = 45 /* Weapon */,
            // Swordsmanship = 100,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of <random>, an undead pirate",
            CreatureType = CreatureType.Undead,
            DamageMax = 16,
            DamageMin = 4,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 110,
            Hue = 0,
            Int = 20,
            ManaMaxSeed = 100,
            Name = "<random>, an undead pirate",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.MagicResist, 30 },
            },
            StamMaxSeed = 100,
            Str = 110,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public AnUndeadPirate() : base(CreatureProperties.Get<AnUndeadPirate>())
        {
            // Add customization here

  
        }

        public AnUndeadPirate(Serial serial) : base(serial) {}

  

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