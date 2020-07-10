

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
    public class Strangler : BaseCreature
    {
        static Strangler() => CreatureProperties.Register<Strangler>(new CreatureProperties
        {
            // cast_pct = 75,
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i8,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i8,
            // CProp_noanimate = i1,
            // CProp_PermMagicImmunity = i8 ,
            // CProp_untamable_0 = i1,
            // DataElementId = strangler,
            // DataElementType = NpcTemplate,
            // Equip = ophidianwarrior,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 201   ,
            // MagicItemChance = 100 ,
            // MagicItemLevel = 8 ,
            // MissSound = 0x169 /* Weapon */,
            // num_casts = 400,
            // script = killpcssprinters,
            // Speed = 37 /* Weapon */,
            // spell = chainlightning,
            // spell_0 = MassCast shiftingearth,
            // spell_1 = MassCast gustofair,
            // spell_2 = wraithbreath,
            // spell_3 = massdispel,
            // spell_4 = teletoplayer,
            // TrueColor = 0x0497 ,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcssprinters */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x1c ,
            CorpseNameOverride = "corpse of a Strangler",
            CreatureType = CreatureType.Plant,
            DamageMax = 41,
            DamageMin = 11,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 2000,
            Hue = 0x0497 ,
            Int = 255,
            ManaMaxSeed = 0,
            Name = "a Strangler",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Earth.ShiftingEarthSpell),
                typeof(Spells.Earth.GustOfAirSpell),
                typeof(Spells.Necromancy.WraithBreathSpell),
                typeof(Spells.Seventh.MassDispelSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
                { ElementalType.Poison, 10 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Poisoning, 90 },
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 200 },
                { SkillName.MagicResist, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Hiding, 200 },
                { SkillName.Magery, 200 },
            },
            StamMaxSeed = 500,
            Str = 1500,
            Tamable = false,
            VirtualArmor = 30,
  
        });

        [Constructable]
        public Strangler() : base(CreatureProperties.Get<Strangler>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Warrior Weapon",
                Speed = 37,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169,
            });
  
  
        }

        public Strangler(Serial serial) : base(serial) {}

  

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