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
    public class OphidianApprenticeMage : BaseCreature
    {
        static OphidianApprenticeMage()
        {
            CreatureProperties.Register<OphidianApprenticeMage>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_cast = 1,
                // DataElementId = ophidianapprenticemage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianapprenticemage,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                LootTable = "76",
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 3,
                // script = spellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = summonfire,
                // spell_0 = lightning,
                // spell_1 = ebolt,
                // spell_2 = magicreflection,
                // spell_3 = fireball,
                // spell_4 = explosion,
                // spell_5 = flamestrike,
                // spell_6 = dispel,
                // TrueColor = 0,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 85,
                CorpseNameOverride = "corpse of an Ophidian Apprentice Mage",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 26,
                DamageMin = 8,
                Dex = 210,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 550,
                Hue = 0,
                Int = 400,
                ManaMaxSeed = 400,
                Name = "an Ophidian Apprentice Mage",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fifth.DispelFieldSpell)
                },
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 70},
                    {SkillName.Magery, 80},
                    {SkillName.Macing, 70},
                    {SkillName.Tactics, 70},
                    {SkillName.MagicResist, 80},
                    {SkillName.EvalInt, 80}
                },
                StamMaxSeed = 70,
                Str = 150,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public OphidianApprenticeMage() : base(CreatureProperties.Get<OphidianApprenticeMage>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Apprentice Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianApprenticeMage(Serial serial) : base(serial)
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