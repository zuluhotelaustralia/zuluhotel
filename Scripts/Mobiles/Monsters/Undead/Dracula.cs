

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
    public class Dracula : BaseCreature
    {
        static Dracula() => CreatureProperties.Register<Dracula>(new CreatureProperties
        {
            // CProp_NecroProtection = i4,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = dracula,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = dracula,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // Hitscript = :combat:lifedrainscript /* Weapon */,
            // HitSound = 0x25A /* Weapon */,
            // hostile = 1,
            // lootgroup = 69,
            // MagicItemChance = 50,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of Dracula",
            CreatureType = CreatureType.Undead,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 600,
            Hue = 0,
            Int = 400,
            ManaMaxSeed = 400,
            Name = "Dracula",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 130 },
                { SkillName.Macing, 130 },
                { SkillName.MagicResist, 130 },
            },
            StamMaxSeed = 400,
            Str = 600,
            VirtualArmor = 35,
  
        });

        [Constructable]
        public Dracula() : base(CreatureProperties.Get<Dracula>())
        {
            // Add customization here

            AddItem(new ShortHair(Utility.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1,
            });
  
            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Dracula Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25A,
                MissSound = 0x239,
            });
  
  
        }

        public Dracula(Serial serial) : base(serial) {}

  

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