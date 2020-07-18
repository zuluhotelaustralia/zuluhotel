

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
    public class BewitchedGauntlets : BaseCreature
    {
        static BewitchedGauntlets() => CreatureProperties.Register<BewitchedGauntlets>(new CreatureProperties
        {
            // DataElementId = bewitchedgauntlets,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bewitchedgauntlets,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x17F /* Weapon */,
            // hostile = 1,
            // lootgroup = 48,
            // MagicItemChance = 1,
            // MissSound = 0x182 /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x212,
            CorpseNameOverride = "corpse of Bewitched Gauntlets",
            CreatureType = CreatureType.Animated,
            DamageMax = 36,
            DamageMin = 6,
            Dex = 110,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 210,
            Hue = 0,
            Int = 110,
            ManaMaxSeed = 0,
            Name = "Bewitched Gauntlets",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 94,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 140 },
                { SkillName.MagicResist, 70 },
            },
            StamMaxSeed = 100,
            Str = 210,
  
        });

        [Constructable]
        public BewitchedGauntlets() : base(CreatureProperties.Get<BewitchedGauntlets>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bewitched Armor Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17F,
                MissSound = 0x182,
            });
  
  
        }

        public BewitchedGauntlets(Serial serial) : base(serial) {}

  

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