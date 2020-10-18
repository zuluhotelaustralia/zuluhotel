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
    public class DarkSteed : BaseCreature
    {
        static DarkSteed()
        {
            CreatureProperties.Register<DarkSteed>(new CreatureProperties
            {
                // AccuracyAdjustment = 90,
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0xAB,
                // AttackMissSound = 0x239,
                // AttackSpeed = 55,
                // cast_pct = 95,
                // corpseamt = 7,
                // corpseitm = rawrib,
                // CProp_noloot = i1,
                // damagedsound = 0xac,
                // DataElementId = darksteed,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // elemental = fire,
                // flamestrength = 50,
                // food = meat,
                // herdskill = 150,
                // hostile = 1,
                // idlesound1 = 0xa9,
                // idlesound2 = 0xaa,
                // lootgroup = 26,
                // MagicAdjustment = 80,
                // MagicItemChance = 100,
                // nofear = 1,
                // num_casts = 45,
                // orneriness = 5,
                // script = spellkillpcs,
                // spell = magicArrow,
                // spell_0 = harm,
                // spell_1 = fireball,
                // spell_2 = poison,
                // spell_3 = lightning,
                // spell_4 = manaDrain,
                // spell_5 = mindBlast,
                // spell_6 = paralyze,
                // spell_7 = eBolt,
                // spell_8 = explosion,
                // spell_9 = fstrike,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 168,
                Body = 179,
                CorpseNameOverride = "corpse of a dark steed",
                DamageMax = 16,
                DamageMin = 2,
                Dex = 120,
                Fame = 4,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1200,
                Hue = 0,
                Int = 600,
                Karma = 4,
                ManaMaxSeed = 600,
                MinTameSkill = 150,
                Name = "a dark steed",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fourth.ManaDrainSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Seventh.FlameStrikeSpell)
                },
                ProvokeSkillOverride = 150,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Anatomy, 110},
                    {SkillName.MagicResist, 120},
                    {SkillName.EvalInt, 100},
                    {SkillName.Tactics, 120},
                    {SkillName.Wrestling, 110},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 120,
                Str = 1200,
                Tamable = true,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public DarkSteed() : base(CreatureProperties.Get<DarkSteed>())
        {
            // Add customization here
        }

        [Constructible]
        public DarkSteed(Serial serial) : base(serial)
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