

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
    public class IceElemental : BaseCreature
    {
        static IceElemental() => CreatureProperties.Register<IceElemental>(new CreatureProperties
        {
            // alignment_0 = neutral,
            // cast_pct = 35,
            // CProp_nocorpse = i1,
            // DataElementId = iceelemental,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = iceelemental,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x119 /* Weapon */,
            // hostile = 1,
            // lootgroup = 20,
            // MagicItemChance = 50,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 6,
            // script = spellkillpcs,
            // Speed = 30 /* Weapon */,
            // spell = masscurse,
            // spell_0 = lightning,
            // spell_1 = magicarrow,
            // spell_2 = meteorswarm,
            // spell_3 = flamestrike,
            // spell_4 = explosion,
            // spell_5 = chainlightning,
            // TrueColor = 0x0481,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x10,
            CorpseNameOverride = "corpse of an ice elemental",
            CreatureType = CreatureType.Elemental,
            DamageMax = 35,
            DamageMin = 20,
            Dex = 70,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 140,
            Hue = 0x0481,
            Int = 110,
            ManaMaxSeed = 125,
            Name = "an ice elemental",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.MassCurseSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.Sixth.ExplosionSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 170 },
                { SkillName.Tactics, 310 },
                { SkillName.Macing, 185 },
                { SkillName.Magery, 90 },
            },
            StamMaxSeed = 65,
            Str = 140,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public IceElemental() : base(CreatureProperties.Get<IceElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ice Elemental Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x119,
                MissSound = 0x239,
            });
  
  
        }

        public IceElemental(Serial serial) : base(serial) {}

  

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