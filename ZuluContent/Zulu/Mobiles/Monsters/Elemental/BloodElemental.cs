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
    public class BloodElemental : BaseCreature
    {
        static BloodElemental()
        {
            CreatureProperties.Register<BloodElemental>(new CreatureProperties
            {
                // DataElementId = bloodelemental,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = bloodelemental,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1D5 /* Weapon */,
                // hostile = 1,
                LootTable = "46",
                LootItemChance = 25,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = spellkillpcs,
                // speech = 7,
                // Speed = 45 /* Weapon */,
                // spell = ebolt,
                // spell_0 = flamestrike,
                // spell_1 = lightning,
                // spell_2 = harm,
                // TrueColor = 0x04b9,
                // virtue = 4,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x10,
                CorpseNameOverride = "corpse of a blood elemental",
                CreatureType = CreatureType.Elemental,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 185,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 450,
                Hue = 0x04b9,
                Int = 400,
                ManaMaxSeed = 200,
                Name = "a blood elemental",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell)
                },
                ProvokeSkillOverride = 115,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermMagicImmunity, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 200},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 50,
                Str = 450,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public BloodElemental() : base(CreatureProperties.Get<BloodElemental>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Blood Elemental Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public BloodElemental(Serial serial) : base(serial)
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