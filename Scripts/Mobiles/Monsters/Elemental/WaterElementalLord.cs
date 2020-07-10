

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
    public class WaterElementalLord : BaseCreature
    {
        static WaterElementalLord() => CreatureProperties.Register<WaterElementalLord>(new CreatureProperties
        {
            // CProp_nocorpse = i1,
            // DataElementId = waterelementallord,
            // DataElementType = NpcTemplate,
            // Equip = waterelementallord,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x119 /* Weapon */,
            // hostile = 1,
            // lootgroup = 73,
            // MagicItemChance = 60,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // script = spellkillpcs,
            // Speed = 35 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = ebolt,
            // spell_1 = lightning,
            // spell_2 = chainlighening,
            // spell_3 = meteorswarm,
            // spell_4 = summonwater,
            // spell_5 = archcure,
            // TrueColor = 102,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x10,
            CanSwim = true,
            CorpseNameOverride = "corpse of a water elemental lord",
            CreatureType = CreatureType.Elemental,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 350,
            Hue = 102,
            Int = 410,
            ManaMaxSeed = 900,
            Name = "a water elemental lord",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Fourth.ArchCureSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 160 },
                { SkillName.Magery, 100 },
                { SkillName.MagicResist, 75 },
            },
            StamMaxSeed = 50,
            Str = 350,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public WaterElementalLord() : base(CreatureProperties.Get<WaterElementalLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Water Elemental Lord Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x119,
                MissSound = 0x239,
            });
  
  
        }

        public WaterElementalLord(Serial serial) : base(serial) {}

  

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