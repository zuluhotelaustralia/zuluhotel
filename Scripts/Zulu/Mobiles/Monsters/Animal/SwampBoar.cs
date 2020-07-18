

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
    public class SwampBoar : BaseCreature
    {
        static SwampBoar() => CreatureProperties.Register<SwampBoar>(new CreatureProperties
        {
            // DataElementId = SwampBoar,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = SwampBoar,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0xC9 /* Weapon */,
            // lootgroup = 64,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 0x07d6,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0xcb,
            CorpseNameOverride = "corpse of a swamp boar",
            CreatureType = CreatureType.Animal,
            DamageMax = 9,
            DamageMin = 3,
            Dex = 50,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 60,
            Hue = 0x07d6,
            Int = 25,
            ManaMaxSeed = 0,
            MinTameSkill = 45,
            Name = "a swamp boar",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 20,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 40 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 80 },
            },
            StamMaxSeed = 40,
            Str = 60,
            Tamable = true,
            VirtualArmor = 5,
  
        });

        [Constructable]
        public SwampBoar() : base(CreatureProperties.Get<SwampBoar>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "SwampBoar Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xC9,
                MissSound = 0x239,
            });
  
  
        }

        public SwampBoar(Serial serial) : base(serial) {}

  

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