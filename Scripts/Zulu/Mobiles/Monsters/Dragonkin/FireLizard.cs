

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
    public class FireLizard : BaseCreature
    {
        static FireLizard() => CreatureProperties.Register<FireLizard>(new CreatureProperties
        {
            // DataElementId = firelizard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = firelizard,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x28C /* Weapon */,
            // MissSound = 0x28A /* Weapon */,
            // script = firebreather,
            // Speed = 30 /* Weapon */,
            // TrueColor = 0x075a,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 417,
            Body = 0xca,
            CanSwim = true,
            CorpseNameOverride = "corpse of a fire lizard",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 28,
            DamageMin = 4,
            Dex = 80,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HasBreath = true,
            Hides = 5,
            HideType = HideType.Lizard,
            HitsMax = 140,
            Hue = 0x075a,
            Int = 30,
            ManaMaxSeed = 20,
            MinTameSkill = 90,
            Name = "a fire lizard",
            PerceptionRange = 10,
            ProvokeSkillOverride = 110,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 100 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 130 },
            },
            StamMaxSeed = 50,
            Str = 140,
            Tamable = true,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public FireLizard() : base(CreatureProperties.Get<FireLizard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Fire Lizard Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x28C,
                MissSound = 0x28A,
            });
  
  
        }

        public FireLizard(Serial serial) : base(serial) {}

  

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