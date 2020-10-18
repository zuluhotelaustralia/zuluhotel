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
    public class Shadow : BaseCreature
    {
        static Shadow()
        {
            CreatureProperties.Register<Shadow>(new CreatureProperties
            {
                // AccuracyAdjustment = 95,
                // AttackAttribute = MaceFighting,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0x19F,
                // AttackMissSound = 0x239,
                // AttackSpeed = 30,
                // cast_pc = 98,
                // corpseamt = 4 2,
                // corpseitm = lichehides VialOfBlood,
                // CProp_Equipt = sliche,
                // damagedsound = 0x1a0,
                // DataElementId = shadowlord2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // elemental = undead,
                // hostile = 1,
                // idlesound1 = 0x19d,
                // idlesound2 = 0x19e,
                // lootgroup = 97,
                // MagicAdjustment = 100,
                // MagicItemChance = 100,
                // nopsych = 1,
                // num_casts = 45,
                // script = spellkillpcs,
                // spell = poison,
                // spell_0 = fstrike,
                // spell_1 = eBolt,
                // spell_10 = cure,
                // spell_11 = greaterheal,
                // spell_12 = earthquake,
                // spell_13 = manaDrain,
                // spell_2 = lightning,
                // spell_3 = harm,
                // spell_4 = mindBlast,
                // spell_5 = magicArrow,
                // spell_6 = explosion,
                // spell_7 = fireball,
                // spell_8 = paralyze,
                // spell_9 = dispel,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 412,
                Body = 0x18,
                CorpseNameOverride = "corpse of a shadow",
                DamageMax = 85,
                DamageMin = 13,
                Dex = 500,
                Fame = 5,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1200,
                Hue = 0,
                Int = 1200,
                Karma = 5,
                ManaMaxSeed = 1200,
                Name = "a shadow",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Seventh.FlameStrikeSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(Spells.Second.CureSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Fourth.ManaDrainSpell)
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 100},
                    {SkillName.EvalInt, 100}
                },
                StamMaxSeed = 500,
                Str = 1200,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public Shadow() : base(CreatureProperties.Get<Shadow>())
        {
            // Add customization here
        }

        [Constructible]
        public Shadow(Serial serial) : base(serial)
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