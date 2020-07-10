

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
    public class RockLizard : BaseCreature
    {
        static RockLizard() => CreatureProperties.Register<RockLizard>(new CreatureProperties
        {
            // DataElementId = rocklizard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = rocklizard,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x28A /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 25 /* Weapon */,
            // TrueColor = 0x060a,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 417,
            Body = 0xca,
            CanSwim = true,
            CorpseNameOverride = "corpse of a rock lizard",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 28,
            DamageMin = 4,
            Dex = 80,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Lizard,
            HitsMax = 190,
            Hue = 0x060a,
            Int = 20,
            ManaMaxSeed = 20,
            MinTameSkill = 75,
            Name = "a rock lizard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 90,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 130 },
                { SkillName.MagicResist, 60 },
            },
            StamMaxSeed = 50,
            Str = 190,
            Tamable = true,
            VirtualArmor = 15,
  
        });

        [Constructable]
        public RockLizard() : base(CreatureProperties.Get<RockLizard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rock Lizard Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x28A,
                MissSound = 0x239,
            });
  
  
        }

        public RockLizard(Serial serial) : base(serial) {}

  

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