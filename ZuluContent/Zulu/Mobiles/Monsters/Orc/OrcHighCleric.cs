using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class OrcHighCleric : BaseCreature
    {
        static OrcHighCleric()
        {
            CreatureProperties.Register<OrcHighCleric>(new CreatureProperties
            {
                // cast_pct = 25,

                // DataElementId = orchighcleric,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = undeadflayer,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:manadrainscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 100,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 50 /* Weapon */,
                // spell = poison,
                // spell_0 = flamestrike,
                // spell_1 = ebolt,
                // spell_10 = gheal,
                // spell_2 = lightning,
                // spell_3 = explosion,
                // spell_4 = wyvernstrike,
                // spell_5 = plague,
                // spell_6 = decayingray,
                // spell_7 = darkness,
                // spell_8 = kill,
                // spell_9 = abyssalflame,
                // TrueColor = 1170,
                // virtue = 4,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 17,
                CorpseNameOverride = "corpse of Orc High Cleric",
                CreatureType = CreatureType.Orc,
                DamageMax = 60,
                DamageMin = 10,
                Dex = 310,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1200,
                Hue = 1170,
                Int = 2750,
                ManaMaxSeed = 2750,
                Name = "Orc High Cleric",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Third.PoisonSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(PlagueSpell),
                    typeof(DecayingRaySpell),
                    typeof(DarknessSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Fourth.GreaterHealSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 8}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Magery, 175},
                    {SkillName.MagicResist, 150},
                    {SkillName.Tactics, 150},
                    {SkillName.Macing, 160},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 50,
                Str = 1200,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public OrcHighCleric() : base(CreatureProperties.Get<OrcHighCleric>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Flayer Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public OrcHighCleric(Serial serial) : base(serial)
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