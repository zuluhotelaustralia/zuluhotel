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
    public class OrcMage : BaseCreature
    {
        static OrcMage()
        {
            CreatureProperties.Register<OrcMage>(new CreatureProperties
            {
                // cast_pct = 30,
                // DataElementId = orcmage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = orcmage,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1B3 /* Weapon */,
                // hostile = 1,
                // lootgroup = 31,
                // MagicItemChance = 75,
                // MagicItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 6,
                // script = spellkillpcs,
                // speech = 6,
                // Speed = 30 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = harm,
                // spell_2 = poison,
                // spell_3 = magicarrow,
                // spell_4 = fireball,
                // spell_5 = paralyze,
                // spell_6 = curse,
                // TrueColor = 201,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x11,
                CorpseNameOverride = "corpse of <random> the Orcmage",
                CreatureType = CreatureType.Orc,
                DamageMax = 8,
                DamageMin = 2,
                Dex = 90,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 195,
                Hue = 201,
                Int = 300,
                ManaMaxSeed = 90,
                Name = "<random> the Orcmage",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.First.MagicArrowSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Fourth.CurseSpell)
                },
                ProvokeSkillOverride = 105,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 75},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 80,
                Str = 195,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public OrcMage() : base(CreatureProperties.Get<OrcMage>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Orc Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B3,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcMage(Serial serial) : base(serial)
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