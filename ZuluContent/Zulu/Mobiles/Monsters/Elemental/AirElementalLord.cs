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
    public class AirElementalLord : BaseCreature
    {
        static AirElementalLord()
        {
            CreatureProperties.Register<AirElementalLord>(new CreatureProperties
            {
                // CProp_nocorpse = i1,
                // DataElementId = airelementallord,
                // DataElementType = NpcTemplate,
                // Equip = airelementallord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x10A /* Weapon */,
                // hostile = 1,
                // lootgroup = 73,
                // MagicItemChance = 60,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = spellkillpcs,
                // Speed = 50 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = summonair,
                // spell_3 = blade_spirit,
                // spell_4 = chainlightning,
                // TrueColor = 1050,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x0d,
                CorpseNameOverride = "corpse of an air elemental lord",
                CreatureType = CreatureType.Elemental,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 500,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 210,
                Hue = 1050,
                Int = 600,
                ManaMaxSeed = 900,
                Name = "an air elemental lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fifth.BladeSpiritsSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 170},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 200,
                Str = 210,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public AirElementalLord() : base(CreatureProperties.Get<AirElementalLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Air Elemental Lord Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x10A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public AirElementalLord(Serial serial) : base(serial)
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