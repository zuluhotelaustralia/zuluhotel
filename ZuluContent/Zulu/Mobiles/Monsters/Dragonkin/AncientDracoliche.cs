using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class AncientDracoliche : BaseCreature
    {
        static AncientDracoliche()
        {
            CreatureProperties.Register<AncientDracoliche>(new CreatureProperties
            {
                // cast_pct = 20,
                // CProp_BaseHpRegen = i350,
                // CProp_BaseManaRegen = i1000,
                // CProp_Permmr = i5,
                // DataElementId = ancientdracoliche,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ancientdracoliche,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 70,
                LootItemLevel = 7,
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
                ClassType = ZuluClassType.Mage,
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
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(IceStrikeSpell),
                    typeof(PlagueSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Seventh.MassDispelSpell),
                    typeof(DarknessSpell)
                },
                ProvokeSkillOverride = 170,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Water, 50},
                    {ElementalType.Fire, 100},
                    {ElementalType.Physical, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                SaySpellMantra = false,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 200},
                    {SkillName.Tactics, 200},
                    {SkillName.Fencing, 150},
                    {SkillName.MagicResist, 200},
                    {SkillName.Magery, 200},
                    {SkillName.DetectHidden, 200},
                    {SkillName.EvalInt, 200}
                },
                StamMaxSeed = 175,
                Str = 3000,
                Tamable = true
            });
        }


        [Constructible]
        public AncientDracoliche() : base(CreatureProperties.Get<AncientDracoliche>())
        {
            // Add customization here
        }

        [Constructible]
        public AncientDracoliche(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}