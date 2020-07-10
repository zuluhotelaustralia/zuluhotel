

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
    public class Raven : BaseCreature
    {
        static Raven() => CreatureProperties.Register<Raven>(new CreatureProperties
        {
            // DataElementId = evilbird,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = evilbird,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0xD4 /* Weapon */,
            // MissSound = 0xD5 /* Weapon */,
            // script = killpcs,
            // Speed = 20 /* Weapon */,
            // TrueColor = 0x0455,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0x06,
            CorpseNameOverride = "corpse of a Raven",
            CreatureType = CreatureType.Animal,
            DamageMax = 6,
            DamageMin = 1,
            Dex = 60,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 15,
            Hue = 0x0455,
            Int = 15,
            ManaMaxSeed = 0,
            MinTameSkill = 10,
            Name = "a Raven",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 50 },
                { SkillName.MagicResist, 20 },
                { SkillName.Macing, 20 },
            },
            StamMaxSeed = 50,
            Str = 15,
            Tamable = true,
            VirtualArmor = 15,
  
        });

        [Constructable]
        public Raven() : base(CreatureProperties.Get<Raven>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Evil Bird Weapon",
                Speed = 20,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xD4,
                MissSound = 0xD5,
            });
  
  
        }

        public Raven(Serial serial) : base(serial) {}

  

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