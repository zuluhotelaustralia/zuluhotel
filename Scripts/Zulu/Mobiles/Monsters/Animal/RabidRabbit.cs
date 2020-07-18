

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
    public class RabidRabbit : BaseCreature
    {
        static RabidRabbit() => CreatureProperties.Register<RabidRabbit>(new CreatureProperties
        {
            // DataElementId = rabidrabbit,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = rabidrabbit,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // Hitscript = :combat:piercingscript /* Weapon */,
            // HitSound = 0x1CB /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 199,
            Body = 0xCD,
            CorpseNameOverride = "corpse of a rabid rabbit",
            CreatureType = CreatureType.Animal,
            DamageMax = 12,
            DamageMin = 7,
            Dex = 550,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 90,
            Hue = 1154,
            Int = 25,
            ManaMaxSeed = 15,
            MinTameSkill = 55,
            Name = "a rabid rabbit",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 130,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 70 },
                { SkillName.Fencing, 75 },
            },
            StamMaxSeed = 50,
            Str = 90,
            Tamable = true,
            VirtualArmor = 20,
  
        });

        [Constructable]
        public RabidRabbit() : base(CreatureProperties.Get<RabidRabbit>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rabid Rabbit Weapon",
                Speed = 30,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1CB,
                MissSound = 0x239,
            });
  
  
        }

        public RabidRabbit(Serial serial) : base(serial) {}

  

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