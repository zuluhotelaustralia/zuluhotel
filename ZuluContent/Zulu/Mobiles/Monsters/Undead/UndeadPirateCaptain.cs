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
    public class UndeadPirateCaptain : BaseCreature
    {
        static UndeadPirateCaptain()
        {
            CreatureProperties.Register<UndeadPirateCaptain>(new CreatureProperties
            {
                // cast_pct = 50,
                // DataElementId = undeadpirate2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = undeadpirate2,
                // HitSound = 0x23C /* Weapon */,
                // hostile = 1,
                LootTable = "23",
                LootItemChance = 1,
                // LootItemChance_0 = 10,
                LootItemLevel = 1,
                // MissSound = 0x23A /* Weapon */,
                // num_casts = 3,
                // script = spellkillpcs,
                // speech = 54,
                // Speed = 45 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = fireball,
                // spell_3 = explosion,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of <random>, undead pirate captain",
                CreatureType = CreatureType.Undead,
                DamageMax = 16,
                DamageMin = 4,
                Dex = 250,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 250,
                Hue = 0,
                Int = 300,
                ManaMaxSeed = 400,
                Name = "<random>, undead pirate captain",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 100},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 150,
                Str = 250,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public UndeadPirateCaptain() : base(CreatureProperties.Get<UndeadPirateCaptain>())
        {
            // Add customization here
        }

        [Constructible]
        public UndeadPirateCaptain(Serial serial) : base(serial)
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