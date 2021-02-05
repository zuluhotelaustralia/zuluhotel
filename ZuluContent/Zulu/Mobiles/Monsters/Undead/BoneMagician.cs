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
    public class BoneMagician : BaseCreature
    {
        static BoneMagician()
        {
            CreatureProperties.Register<BoneMagician>(new CreatureProperties
            {
                // cast_pct = 40,
                // DataElementId = bonemage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = bonemage,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x16C /* Weapon */,
                // hostile = 1,
                LootTable = "39",
                LootItemChance = 50,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 35 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = summonskel,
                // spell_3 = fireball,
                // spell_4 = paralyze,
                // spell_5 = masscurse,
                // spell_6 = meteorswarm,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x38,
                CorpseNameOverride = "corpse of a bone magician",
                CreatureType = CreatureType.Undead,
                DamageMax = 32,
                DamageMin = 4,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 200,
                Hue = 0,
                Int = 295,
                ManaMaxSeed = 175,
                Name = "a bone magician",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 70},
                    {SkillName.Tactics, 60},
                    {SkillName.Macing, 80},
                    {SkillName.Magery, 95}
                },
                StamMaxSeed = 50,
                Str = 200,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public BoneMagician() : base(CreatureProperties.Get<BoneMagician>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bone Mage Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public BoneMagician(Serial serial) : base(serial)
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