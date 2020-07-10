

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
    public class AncientDracoliche : BaseCreature
    {
        static AncientDracoliche() => CreatureProperties.Register<AncientDracoliche>(new CreatureProperties
        {
            // cast_pct = 20,
            // CProp_BaseHpRegen = i350,
            // CProp_BaseManaRegen = i1000,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // CProp_Permmr = i5,
            // DataElementId = ancientdracoliche,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ancientdracoliche,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 70,
            // MagicItemLevel = 7,
            // num_casts = 4,
            // script = spellkillpcs,
            // spell = mindblast,
            // spell_0 = flamestrike,
            // spell_1 = kill,
            // spell_2 = abyssalflame,
            // spell_3 = icestrike,
            // spell_4 = plague,
            // spell_5 = sorcerersbane,
            // spell_6 = earthquake,
            // spell_7 = massdispel,
            // spell_8 = darkness,
            // TrueColor = 1282,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 104,
            ClassLevel = 5,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of an Ancient Dracoliche",
            CreatureType = CreatureType.Dragonkin,
            Dex = 175,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 2000,
            Hue = 1282,
            Int = 1500,
            ManaMaxSeed = 1500,
            MinTameSkill = 170,
            Name = "an Ancient Dracoliche",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Earth.IceStrikeSpell),
                typeof(Spells.Necromancy.PlagueSpell),
                typeof(Spells.Necromancy.SorcerorsBaneSpell),
                typeof(Spells.Eighth.EarthquakeSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(Spells.Necromancy.DarknessSpell),
            },
            ProvokeSkillOverride = 170,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Cold, 50 },
                { ElementalType.Fire, 100 },
                { ElementalType.Physical, 100 },
            },
            SaySpellMantra = false,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 200 },
                { SkillName.Tactics, 200 },
                { SkillName.Fencing, 150 },
                { SkillName.MagicResist, 200 },
                { SkillName.Magery, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.EvalInt, 200 },
            },
            StamMaxSeed = 175,
            Str = 3000,
            Tamable = true,
  
        });

        [Constructable]
        public AncientDracoliche() : base(CreatureProperties.Get<AncientDracoliche>())
        {
            // Add customization here

  
        }

        public AncientDracoliche(Serial serial) : base(serial) {}

  

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