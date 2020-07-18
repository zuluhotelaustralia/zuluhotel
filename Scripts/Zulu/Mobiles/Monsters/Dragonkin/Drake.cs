

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
    public class Drake : BaseCreature
    {
        static Drake() => CreatureProperties.Register<Drake>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i3,
            // DataElementId = drake2,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = drake2,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // script = firebreather,
            // Speed = 45 /* Weapon */,
            // TrueColor = 0,
            // virtue = 7,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3d,
            CorpseNameOverride = "corpse of a drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            Hides = 2,
            HideType = HideType.Dragon,
            HitsMax = 500,
            Hue = 0,
            Int = 90,
            ManaMaxSeed = 80,
            MinTameSkill = 110,
            Name = "a drake",
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 90 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 120 },
            },
            StamMaxSeed = 130,
            Str = 500,
            Tamable = true,
            VirtualArmor = 40,
  
        });

        [Constructable]
        public Drake() : base(CreatureProperties.Get<Drake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Drake1 Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public Drake(Serial serial) : base(serial) {}

  

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