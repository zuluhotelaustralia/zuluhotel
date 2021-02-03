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
    public class Efreeti : BaseCreature
    {
        static Efreeti()
        {
            CreatureProperties.Register<Efreeti>(new CreatureProperties
            {
                // cast_pct = 60,
                // CProp_nocorpse = i1,
                // CProp_PermMagicImmunity = i4,
                // DataElementId = efreeti,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = efreeti,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x10A /* Weapon */,
                LootTable = "46",
                LootItemChance = 50,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 12,
                // script = spellkillpcs,
                // Speed = 35 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = fireball,
                // spell_1 = lightning,
                // spell_2 = ebolt,
                // spell_3 = chainlightning,
                // TrueColor = 93,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x0d,
                CorpseNameOverride = "corpse of an efreeti",
                CreatureType = CreatureType.Elemental,
                DamageMax = 50,
                DamageMin = 5,
                Dex = 100,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 350,
                Hue = 93,
                Int = 355,
                ManaMaxSeed = 160,
                Name = "an efreeti",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 85},
                    {SkillName.Macing, 160},
                    {SkillName.Magery, 100},
                    {SkillName.MagicResist, 130}
                },
                StamMaxSeed = 90,
                Str = 350,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public Efreeti() : base(CreatureProperties.Get<Efreeti>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Efreeti Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x10A,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Efreeti(Serial serial) : base(serial)
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