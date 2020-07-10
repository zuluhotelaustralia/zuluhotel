

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
    public class Marksman : BaseCreature
    {
        static Marksman() => CreatureProperties.Register<Marksman>(new CreatureProperties
        {
            // ammoamount = 30,
            // ammotype = 0x1bfb,
            // DataElementId = orcmarksman,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = direwolf,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0xE8 /* Weapon */,
            // hostile = 1,
            // lootgroup = 52,
            // missileweapon = xbowman,
            // MissSound = 0x239 /* Weapon */,
            // script = explosionkillpcs,
            // speech = 6,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* explosionkillpcs */,
            AlwaysMurderer = true,
            Body = 0x11,
            CorpseNameOverride = "corpse of <random> the marksman",
            CreatureType = CreatureType.Orc,
            DamageMax = 12,
            DamageMin = 3,
            Dex = 190,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 135,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 25,
            Name = "<random> the marksman",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 50 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 65 },
                { SkillName.Archery, 100 },
            },
            StamMaxSeed = 90,
            Str = 135,
            VirtualArmor = 10,
  
        });

        [Constructable]
        public Marksman() : base(CreatureProperties.Get<Marksman>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Direwolf Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0xE8,
                MissSound = 0x239,
            });
  
  
        }

        public Marksman(Serial serial) : base(serial) {}

  

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