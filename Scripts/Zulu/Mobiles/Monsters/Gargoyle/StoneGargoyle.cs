

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
    public class StoneGargoyle : BaseCreature
    {
        static StoneGargoyle() => CreatureProperties.Register<StoneGargoyle>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i1,
            // DataElementId = stonegargoyle,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = stonegargoyle,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x177 /* Weapon */,
            // hostile = 1,
            // lootgroup = 40,
            // MagicItemChance = 15,
            // MagicItemLevel = 2,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 40 /* Weapon */,
            // TrueColor = 1154,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 372,
            Body = 0x4,
            CorpseNameOverride = "corpse of a stone gargoyle",
            CreatureType = CreatureType.Gargoyle,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 80,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 495,
            Hue = 1154,
            Int = 90,
            ManaMaxSeed = 80,
            Name = "a stone gargoyle",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 105,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 135 },
                { SkillName.MagicResist, 60 },
            },
            StamMaxSeed = 65,
            Str = 495,
            VirtualArmor = 20,
  
        });

        [Constructable]
        public StoneGargoyle() : base(CreatureProperties.Get<StoneGargoyle>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Stone Gargoyle Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x177,
                MissSound = 0x239,
            });
  
  
        }

        public StoneGargoyle(Serial serial) : base(serial) {}

  

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