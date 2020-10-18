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
    public class Gargoyle : BaseCreature
    {
        static Gargoyle()
        {
            CreatureProperties.Register<Gargoyle>(new CreatureProperties
            {
                // cast_pct = 30,
                // CProp_PermMagicImmunity = i2,
                // DataElementId = gargoyle,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = gargoyle,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x177 /* Weapon */,
                // hostile = 1,
                // lootgroup = 38,
                // MagicItemChance = 10,
                // Magicitemlevel = 2,
                // MissSound = 0x233 /* Weapon */,
                // num_casts = 6,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 35 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = harm,
                // spell_2 = mindblast,
                // spell_3 = magicarrow,
                // spell_4 = fireball,
                // spell_5 = weaken,
                // spell_6 = masscurse,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 372,
                Body = 0x4,
                CorpseNameOverride = "corpse of a gargoyle",
                CreatureType = CreatureType.Gargoyle,
                DamageMax = 44,
                DamageMin = 8,
                Dex = 80,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 130,
                Hue = 0,
                Int = 210,
                ManaMaxSeed = 200,
                Name = "a gargoyle",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.First.WeakenSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 85,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Physical, 25}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 80},
                    {SkillName.Macing, 135},
                    {SkillName.Magery, 110},
                    {SkillName.EvalInt, 65}
                },
                StamMaxSeed = 70,
                Str = 130,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public Gargoyle() : base(CreatureProperties.Get<Gargoyle>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Gargoyle Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x177,
                MissSound = 0x233
            });
        }

        [Constructible]
        public Gargoyle(Serial serial) : base(serial)
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