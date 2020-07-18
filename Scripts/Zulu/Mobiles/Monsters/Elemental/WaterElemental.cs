

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
    public class WaterElemental : BaseCreature
    {
        static WaterElemental() => CreatureProperties.Register<WaterElemental>(new CreatureProperties
        {
            // cast_pct = 30,
            // CProp_nocorpse = i1,
            // DataElementId = waterelemental,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = waterelemental,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x119 /* Weapon */,
            // hostile = 1,
            // lootgroup = 20,
            // MagicItemChance = 25,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 10,
            // script = spellkillpcs,
            // Speed = 40 /* Weapon */,
            // spell = ebolt,
            // spell_0 = lightning,
            // spell_1 = harm,
            // spell_2 = mindblast,
            // spell_3 = magicarrow,
            // spell_4 = chainlightning,
            // Swordsmanship = 120,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x10,
            CanSwim = true,
            CorpseNameOverride = "corpse of a water elemental",
            CreatureType = CreatureType.Elemental,
            DamageMax = 45,
            DamageMin = 21,
            Dex = 80,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 210,
            Hue = 33784,
            Int = 250,
            ManaMaxSeed = 200,
            Name = "a water elemental",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Second.HarmSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.First.MagicArrowSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 55 },
                { SkillName.MagicResist, 30 },
                { SkillName.Tactics, 100 },
                { SkillName.Magery, 90 },
                { SkillName.EvalInt, 65 },
            },
            StamMaxSeed = 70,
            Str = 210,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public WaterElemental() : base(CreatureProperties.Get<WaterElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Water Elemental Weapon",
                Speed = 40,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x119,
                MissSound = 0x239,
            });
  
  
        }

        public WaterElemental(Serial serial) : base(serial) {}

  

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