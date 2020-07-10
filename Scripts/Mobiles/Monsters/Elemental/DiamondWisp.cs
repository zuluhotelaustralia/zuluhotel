

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
    public class DiamondWisp : BaseCreature
    {
        static DiamondWisp() => CreatureProperties.Register<DiamondWisp>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_EarthProtection = i4,
            // CProp_NecroProtection = i4,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = diamondwisp,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = blackwisp,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x1D5 /* Weapon */,
            // hostile = 1,
            // lootgroup = 203,
            // MagicItemChance = 75,
            // MagicItemLevel = 6,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = spellkillpcs,
            // speech = 7,
            // Speed = 35 /* Weapon */,
            // spell = masscurse,
            // spell_0 = abyssalflame,
            // spell_1 = wyvernstrike,
            // spell_2 = spectretouch,
            // spell_3 = sorcerersbane,
            // spell_4 = wraithbreath,
            // spell_5 = decayingray,
            // spell_6 = kill,
            // TrueColor = 1176,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 0x3a,
            CorpseNameOverride = "corpse of a diamond wisp",
            CreatureType = CreatureType.Elemental,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 575,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 1400,
            Hue = 1176,
            Int = 1100,
            ManaMaxSeed = 1100,
            Name = "a diamond wisp",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.MassCurseSpell),
                typeof(Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Necromancy.SpectresTouchSpell),
                typeof(Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Spells.Necromancy.WraithBreathSpell),
                typeof(Spells.Necromancy.DecayingRaySpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
            },
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Energy, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 200 },
                { SkillName.MagicResist, 200 },
                { SkillName.Magery, 200 },
                { SkillName.EvalInt, 200 },
            },
            StamMaxSeed = 175,
            Str = 1700,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public DiamondWisp() : base(CreatureProperties.Get<DiamondWisp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Blackwisp Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239,
            });
  
  
        }

        public DiamondWisp(Serial serial) : base(serial) {}

  

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