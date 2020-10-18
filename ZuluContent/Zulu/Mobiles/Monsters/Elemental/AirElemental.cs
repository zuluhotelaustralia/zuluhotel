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
    public class AirElemental : BaseCreature
    {
        static AirElemental()
        {
            CreatureProperties.Register<AirElemental>(new CreatureProperties
            {
                // cast_pct = 30,
                // CProp_nocorpse = i1,
                // DataElementId = airelemental,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = airelemental,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x10A /* Weapon */,
                // hostile = 1,
                // lootgroup = 20,
                // MagicItemChance = 25,
                // MagicItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // Speed = 50 /* Weapon */,
                // spell = ebolt,
                // spell_0 = lightning,
                // spell_1 = chainlightning,
                // Swordsmanship = 100,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x0d,
                CanFly = true,
                CorpseNameOverride = "corpse of an air elemental",
                CreatureType = CreatureType.Elemental,
                DamageMax = 30,
                DamageMin = 5,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 205,
                Hue = 33784,
                Int = 205,
                ManaMaxSeed = 195,
                Name = "an air elemental",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.MagicResist, 75},
                    {SkillName.Tactics, 100},
                    {SkillName.Magery, 90},
                    {SkillName.EvalInt, 75}
                },
                StamMaxSeed = 140,
                Str = 205,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public AirElemental() : base(CreatureProperties.Get<AirElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Air Elemental Weapon",
                Speed = 50,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x10A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public AirElemental(Serial serial) : base(serial)
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