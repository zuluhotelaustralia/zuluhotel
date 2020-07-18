

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
    public class OrcishKing : BaseCreature
    {
        static OrcishKing() => CreatureProperties.Register<OrcishKing>(new CreatureProperties
        {
            // DataElementId = orcking,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = orcking,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1B1 /* Weapon */,
            // hostile = 1,
            // lootgroup = 13,
            // MagicItemChance = 25,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 55 /* Weapon */,
            // TrueColor = 0x0465,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 7,
            CorpseNameOverride = "corpse of <random> the Orcish King",
            CreatureType = CreatureType.Orc,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 300,
            Hue = 0x0465,
            Int = 60,
            ManaMaxSeed = 50,
            Name = "<random> the Orcish King",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 150 },
                { SkillName.MagicResist, 85 },
            },
            StamMaxSeed = 150,
            Str = 300,
            VirtualArmor = 35,
  
        });

        [Constructable]
        public OrcishKing() : base(CreatureProperties.Get<OrcishKing>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc King Weapon",
                Speed = 55,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B1,
                MissSound = 0x239,
            });
  
  
        }

        public OrcishKing(Serial serial) : base(serial) {}

  

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