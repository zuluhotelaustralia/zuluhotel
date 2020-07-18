

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
    public class TerathanWarrior : BaseCreature
    {
        static TerathanWarrior() => CreatureProperties.Register<TerathanWarrior>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i5,
            // DataElementId = terathanwarrior,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = terathanwarrior,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x24D /* Weapon */,
            // hostile = 1,
            // lootgroup = 63,
            // MissSound = 0x24E /* Weapon */,
            // script = killpcs,
            // speech = 6,
            // Speed = 37 /* Weapon */,
            // Swordsmanship = 90,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x46,
            CorpseNameOverride = "corpse of a Terathan Warrior",
            CreatureType = CreatureType.Terathan,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 105,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 550,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "a Terathan Warrior",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 90 },
                { SkillName.MagicResist, 90 },
                { SkillName.Parry, 75 },
            },
            StamMaxSeed = 70,
            Str = 550,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public TerathanWarrior() : base(CreatureProperties.Get<TerathanWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Warrior Weapon",
                Speed = 37,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x24D,
                MissSound = 0x24E,
            });
  
  
        }

        public TerathanWarrior(Serial serial) : base(serial) {}

  

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