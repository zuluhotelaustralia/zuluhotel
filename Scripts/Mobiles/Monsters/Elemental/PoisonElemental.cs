

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
    public class PoisonElemental : BaseCreature
    {
        static PoisonElemental() => CreatureProperties.Register<PoisonElemental>(new CreatureProperties
        {
            // DataElementId = poisonelemental,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = poisonelemental,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x1D5 /* Weapon */,
            // hostile = 1,
            // lootgroup = 46,
            // MagicItemChance = 50,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // speech = 7,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0x07d6,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x0d,
            CorpseNameOverride = "corpse of a poison elemental",
            CreatureType = CreatureType.Elemental,
            DamageMax = 50,
            DamageMin = 25,
            Dex = 160,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitPoison = Poison.Deadly,
            HitsMax = 350,
            Hue = 0x07d6,
            Int = 350,
            ManaMaxSeed = 200,
            Name = "a poison elemental",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 10 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 200 },
                { SkillName.MagicResist, 70 },
            },
            StamMaxSeed = 50,
            Str = 350,
            VirtualArmor = 40,
  
        });

        [Constructable]
        public PoisonElemental() : base(CreatureProperties.Get<PoisonElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Poisonelemental Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239,
            });
  
  
        }

        public PoisonElemental(Serial serial) : base(serial) {}

  

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