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
    public class FieryGargoyle : BaseCreature
    {
        static FieryGargoyle()
        {
            CreatureProperties.Register<FieryGargoyle>(new CreatureProperties
            {
                // AccuracyAdjustment = 10,
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0x177,
                // AttackMissSound = 0x239,
                // AttackSpeed = 30,
                // corpseamt = 3 1,
                // corpseitm = ExecutionersCap blackheart,
                // damagedsound = 0x178,
                // DataElementId = firegargoyle,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // elemental = fire,
                // hostile = 1,
                // idlesound1 = 0x175,
                // idlesound2 = 0x176,
                // lootgroup = 42,
                // MagicAdjustment = 10,
                // MagicItemChance = 100,
                // provoke_0 = 80,
                // script = spellkillpcs,
                // spell = magicArrow,
                // spell_0 = harm,
                // spell_1 = fireball,
                // spell_2 = poison,
                // spell_3 = lightning,
                // spell_4 = mindBlast,
                // spell_5 = paralyze,
                // spell_6 = eBolt,
                // spell_7 = explosion,
                // spell_8 = fstrike,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 372,
                Body = 0x4,
                CorpseNameOverride = "corpse of a fiery gargoyle",
                DamageMax = 36,
                DamageMin = 6,
                Dex = 145,
                Fame = 3,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 600,
                Hue = 0,
                Int = 250,
                Karma = 3,
                ManaMaxSeed = 250,
                Name = "a fiery gargoyle",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Seventh.FlameStrikeSpell)
                },
                ProvokeSkillOverride = 120,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Wrestling, 100}
                },
                StamMaxSeed = 145,
                Str = 600,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public FieryGargoyle() : base(CreatureProperties.Get<FieryGargoyle>())
        {
            // Add customization here
        }

        [Constructible]
        public FieryGargoyle(Serial serial) : base(serial)
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