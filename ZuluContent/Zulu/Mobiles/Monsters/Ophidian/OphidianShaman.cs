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
    public class OphidianShaman : BaseCreature
    {
        static OphidianShaman()
        {
            CreatureProperties.Register<OphidianShaman>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_cast = 1,
                // DataElementId = ophidianshaman,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = ophidianshaman,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // hostile = 1,
                // lootgroup = 69,
                // MaceFighting_0 = 120,
                // MagicItemChance = 30,
                // MagicItemLevel = 4,
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 3,
                // script = critterhealer,
                // Speed = 30 /* Weapon */,
                // spell = summonfire,
                // spell_0 = lightning,
                // spell_1 = ebolt,
                // spell_2 = magicreflection,
                // spell_3 = fireball,
                // spell_4 = explosion,
                // spell_5 = flamestrike,
                // TrueColor = 88,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* critterhealer */,
                AlwaysMurderer = true,
                Body = 0x55,
                CorpseNameOverride = "corpse of an Ophidian Shaman",
                CreatureType = CreatureType.Ophidian,
                DamageMax = 30,
                DamageMin = 3,
                Dex = 160,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 500,
                Hue = 88,
                Int = 350,
                ManaMaxSeed = 200,
                Name = "an Ophidian Shaman",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 75},
                    {ElementalType.Water, 75}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 120},
                    {SkillName.Magery, 120},
                    {SkillName.Macing, 120},
                    {SkillName.Tactics, 70},
                    {SkillName.MagicResist, 90}
                },
                StamMaxSeed = 160,
                Str = 500,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public OphidianShaman() : base(CreatureProperties.Get<OphidianShaman>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ophidian Shaman Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x169
            });
        }

        [Constructible]
        public OphidianShaman(Serial serial) : base(serial)
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