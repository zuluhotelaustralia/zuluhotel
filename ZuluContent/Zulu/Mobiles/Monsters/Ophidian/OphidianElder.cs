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
    public class OphidianElder : BaseCreature
    {
        static OphidianElder()
        {
            CreatureProperties.Register<OphidianElder>(new CreatureProperties
            {
                // CProp_PermMagicImmunity = i4,
                // DataElementId = ophidianelder,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianmage,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "66",
                LootItemChance = 50,
                LootItemLevel = 3,
                // MissSound = 0x169 /* Weapon */,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = poison,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_10 = paralyze,
                // spell_11 = weaken,
                // spell_2 = lightning,
                // spell_3 = harm,
                // spell_4 = mindblast,
                // spell_5 = magicarrow,
                // spell_6 = explosion,
                // spell_7 = fireball,
                // spell_8 = chainlightning,
                // spell_9 = masscurse,
                // TrueColor = 1250,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x55,
                ClassLevel = 3,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of an Ophidian Elder",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 30,
                DamageMin = 3,
                Dex = 70,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 960,
                Hue = 1250,
                Int = 350,
                ManaMaxSeed = 800,
                Name = "an Ophidian Elder",
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
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.First.WeakenSpell)
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 130},
                    {SkillName.Magery, 150},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 100},
                    {SkillName.MagicResist, 150}
                },
                StamMaxSeed = 500,
                Str = 160,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public OphidianElder() : base(CreatureProperties.Get<OphidianElder>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Mage Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianElder(Serial serial) : base(serial)
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