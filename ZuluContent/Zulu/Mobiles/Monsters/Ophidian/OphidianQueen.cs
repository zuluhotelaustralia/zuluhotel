using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class OphidianQueen : BaseCreature
    {
        static OphidianQueen()
        {
            CreatureProperties.Register<OphidianQueen>(new CreatureProperties
            {
                // cast_pct = 10,
                // CProp_PhysicalResistance = i6,
                // DataElementId = ophidianqueen,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianqueen,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "37",
                LootItemChance = 60,
                LootItemLevel = 6,
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 2,
                // script = spellkillpcs,
                // Speed = 40 /* Weapon */,
                // spell = poison,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_10 = masscurse,
                // spell_2 = lightning,
                // spell_3 = harm,
                // spell_4 = mindblast,
                // spell_5 = magicarrow,
                // spell_6 = explosion,
                // spell_7 = meteorswarm,
                // spell_8 = chainlightning,
                // spell_9 = paralyze,
                // Swordsmanship = 70,
                // TrueColor = 1165,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x57,
                ClassLevel = 5,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of an Ophidian Queen",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 45,
                DamageMin = 9,
                Dex = 70,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 1950,
                Hue = 1165,
                Int = 700,
                ManaMaxSeed = 1000,
                Name = "an Ophidian Queen",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                SaySpellMantra = true,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.MagicImmunity, 8}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 160},
                    {SkillName.Magery, 160},
                    {SkillName.MagicResist, 95},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 100}
                },
                StamMaxSeed = 50,
                Str = 350,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public OphidianQueen() : base(CreatureProperties.Get<OphidianQueen>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Queen Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianQueen(Serial serial) : base(serial)
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