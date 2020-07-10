

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
    public class TrollGeneral : BaseCreature
    {
        static TrollGeneral() => CreatureProperties.Register<TrollGeneral>(new CreatureProperties
        {
            // DataElementId = troll6,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = troll6,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 46,
            // Macefighting = 130,
            // MissSound = 0x23B /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x37,
            CorpseNameOverride = "corpse of a troll General",
            CreatureType = CreatureType.Troll,
            DamageMax = 37,
            DamageMin = 9,
            Dex = 90,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 295,
            Hue = 33784,
            Int = 80,
            ManaMaxSeed = 70,
            Name = "a troll General",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 75,
            Str = 295,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public TrollGeneral() : base(CreatureProperties.Get<TrollGeneral>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll6 Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x23B,
            });
  
  
        }

        public TrollGeneral(Serial serial) : base(serial) {}

  

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