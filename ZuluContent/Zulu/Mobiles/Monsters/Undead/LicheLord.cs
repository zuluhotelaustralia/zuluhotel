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
    public class LicheLord : BaseCreature
    {
        static LicheLord()
        {
            CreatureProperties.Register<LicheLord>(new CreatureProperties
            {
                // cast_pct = 42,
                // DataElementId = lichelord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = lichelord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x19F /* Weapon */,
                // hostile = 1,
                LootTable = "75",
                LootItemChance = 40,
                LootItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 30 /* Weapon */,
                // spell = ebolt,
                // spell_0 = flamestrike,
                // spell_1 = explosion,
                // spell_2 = chainlightning,
                // spell_3 = masscurse,
                // spell_4 = lightning,
                // TrueColor = 0x4631,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x18,
                CorpseNameOverride = "corpse of a Liche Lord",
                CreatureType = CreatureType.Undead,
                DamageMax = 55,
                DamageMin = 19,
                Dex = 85,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 3,
                HideType = HideType.Liche,
                HitsMax = 450,
                Hue = 0x4631,
                Int = 600,
                ManaMaxSeed = 250,
                Name = "a Liche Lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Fourth.LightningSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 6},
                    {ElementalType.Necro, 100},
                    {ElementalType.MagicImmunity, 6}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 100},
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 125},
                    {SkillName.Magery, 120}
                },
                StamMaxSeed = 80,
                Str = 450,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public LicheLord() : base(CreatureProperties.Get<LicheLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Lichelord Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x19F,
                MissSound = 0x239
            });
        }

        [Constructible]
        public LicheLord(Serial serial) : base(serial)
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