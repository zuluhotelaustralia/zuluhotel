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
    public class Vampire : BaseCreature
    {
        static Vampire()
        {
            CreatureProperties.Register<Vampire>(new CreatureProperties
            {
                // cast_pct = 80,
                // DataElementId = Vampire2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = Vampire2,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:lifedrainscript /* Weapon */,
                // HitSound = 0x16C /* Weapon */,
                // hostile = 1,
                LootTable = "71",
                LootItemChance = 75,
                LootItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 14,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 30 /* Weapon */,
                // spell = poison,
                // spell_0 = lightning,
                // spell_1 = summonskel,
                // spell_2 = paralyze,
                // spell_3 = manadrain,
                // spell_4 = explosion,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of Vampire",
                CreatureType = CreatureType.Undead,
                DamageMax = 40,
                DamageMin = 12,
                Dex = 350,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 350,
                Hue = 0,
                Int = 350,
                ManaMaxSeed = 350,
                Name = "Vampire",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Fourth.ManaDrainSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.PermMagicImmunity, 3}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 110},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 350,
                Str = 350,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public Vampire() : base(CreatureProperties.Get<Vampire>())
        {
            // Add customization here

            AddItem(new ShortHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1
            });

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Vampire2 Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Vampire(Serial serial) : base(serial)
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