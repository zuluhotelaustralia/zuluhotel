

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
    public class ArachnidQueen : BaseCreature
    {
        static ArachnidQueen() => CreatureProperties.Register<ArachnidQueen>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i8,
            // CProp_NecroProtection = i8,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i8,
            // CProp_untamable_0 = i1,
            // DataElementId = mountedarachnidqueen,
            // DataElementType = NpcTemplate,
            // Equip = arachnidqueen,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x186 /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 80,
            // MagicItemLevel = 7,
            // MissSound = 0x187 /* Weapon */,
            // script = fastspiders,
            // Speed = 60 /* Weapon */,
            // TrueColor = 0x0497,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* fastspiders */,
            AlwaysMurderer = true,
            Body = 0x1c,
            CorpseNameOverride = "corpse of an Arachnid Queen",
            CreatureType = CreatureType.Elemental,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitPoison = Poison.Greater,
            HitsMax = 5250,
            Hue = 0x0497,
            Int = 55,
            ManaMaxSeed = 0,
            Name = "an Arachnid Queen",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 160,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
                { ElementalType.Poison, 10 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Poisoning, 70 },
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 200 },
                { SkillName.MagicResist, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Hiding, 200 },
            },
            StamMaxSeed = 200,
            Str = 2250,
            Tamable = false,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public ArachnidQueen() : base(CreatureProperties.Get<ArachnidQueen>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Arachnid Queen Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x187,
            });
  
  
        }

        public ArachnidQueen(Serial serial) : base(serial) {}

  

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