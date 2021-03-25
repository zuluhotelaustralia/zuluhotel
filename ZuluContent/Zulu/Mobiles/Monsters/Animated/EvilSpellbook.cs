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
    public class EvilSpellbook : BaseCreature
    {
        static EvilSpellbook()
        {
            CreatureProperties.Register<EvilSpellbook>(new CreatureProperties
            {
                // cast_pct = 60,
                // DataElementId = evilspellbook,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = evilspellbook,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x263 /* Weapon */,
                // hostile = 1,
                LootTable = "35",
                LootItemChance = 50,
                LootItemLevel = 5,
                // MissSound = 0x264 /* Weapon */,
                // num_casts = 20,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 35 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_10 = paralyze,
                // spell_11 = gheal,
                // spell_2 = chainlightning,
                // spell_3 = fireball,
                // spell_4 = explosion,
                // spell_5 = masscurse,
                // spell_6 = meteorswarm,
                // spell_7 = summonskel,
                // spell_8 = earthquake,
                // spell_9 = reflect,
                // Swordsmanship = 90,
                // TrueColor = 0x486,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x3d9,
                CorpseNameOverride = "corpse of evil spellbook",
                CreatureType = CreatureType.Animated,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 600,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 400,
                Hue = 0x486,
                Int = 900,
                ManaMaxSeed = 900,
                Name = "evil spellbook",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Third.FireballSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(Spells.Eighth.EarthquakeSpell),
                    typeof(Spells.Fifth.MagicReflectSpell),
                    typeof(Spells.Fifth.ParalyzeSpell),
                    typeof(Spells.Fourth.GreaterHealSpell)
                },
                SaySpellMantra = true,
                Resistances = new Dictionary<ElementalType, CreatureProp>()
                {
                    {ElementalType.PermMagicImmunity, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 65},
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 85},
                    {SkillName.Macing, 175},
                    {SkillName.Magery, 150},
                    {SkillName.Healing, 99}
                },
                StamMaxSeed = 100,
                Str = 400,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public EvilSpellbook() : base(CreatureProperties.Get<EvilSpellbook>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Evil Spellbook Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x263,
                MissSound = 0x264
            });
        }

        [Constructible]
        public EvilSpellbook(Serial serial) : base(serial)
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