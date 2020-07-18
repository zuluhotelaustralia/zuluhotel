

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
    public class WaterDrake : BaseCreature
    {
        static WaterDrake() => CreatureProperties.Register<WaterDrake>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i3,
            // DataElementId = waterdrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = waterdrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:piercingscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // script = killpcs,
            // Speed = 50 /* Weapon */,
            // TrueColor = 1165,
            // virtue = 7,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3d,
            CorpseNameOverride = "corpse of a Water Drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Dragon,
            HitsMax = 400,
            Hue = 1165,
            Int = 90,
            ManaMaxSeed = 80,
            MinTameSkill = 115,
            Name = "a Water Drake",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 130 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 100 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 130,
            Str = 400,
            Tamable = true,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public WaterDrake() : base(CreatureProperties.Get<WaterDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Water Drake Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public WaterDrake(Serial serial) : base(serial) {}

  

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