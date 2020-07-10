

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
    public class TrollChieftan : BaseCreature
    {
        static TrollChieftan() => CreatureProperties.Register<TrollChieftan>(new CreatureProperties
        {
            // DataElementId = trollking,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = trollking,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x233 /* Weapon */,
            // hostile = 1,
            // lootgroup = 60,
            // MagicItemChance = 30,
            // MagicItemLevel = 3,
            // MissSound = 0x23B /* Weapon */,
            // script = killpcs,
            // Speed = 40 /* Weapon */,
            // Swordsmanship = 160,
            // TrueColor = 0x0465,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x35,
            CorpseNameOverride = "corpse of a Troll Chieftan",
            CreatureType = CreatureType.Troll,
            DamageMax = 45,
            DamageMin = 21,
            Dex = 220,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 350,
            Hue = 0x0465,
            Int = 90,
            ManaMaxSeed = 80,
            Name = "a Troll Chieftan",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 140 },
                { SkillName.MagicResist, 80 },
            },
            StamMaxSeed = 90,
            Str = 350,
            VirtualArmor = 35,
  
        });

        [Constructable]
        public TrollChieftan() : base(CreatureProperties.Get<TrollChieftan>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll King Weapon",
                Speed = 40,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x233,
                MissSound = 0x23B,
            });
  
  
        }

        public TrollChieftan(Serial serial) : base(serial) {}

  

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