using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Necromancy;
using Server.Items;
using Server.Engines.Magic;

namespace Server.Mobiles
{
    public class EvilCodexDamnorum : BaseCreature
    {
        static EvilCodexDamnorum()
        {
            CreatureProperties.Register<EvilCodexDamnorum>(new CreatureProperties
            {
                // cast_pct = 60,
                // CProp_AttackTypeImmunities = i256,
                // CProp_BaseHpRegen = i350,
                // CProp_BaseManaRegen = i500,
                // CProp_Permmr = i4,
                // DataElementId = EvilCodexDamnorum,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = EvilCodexDamnorum,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:piercingscript /* Weapon */,
                // HitSound = 0x263 /* Weapon */,
                // hostile = 1,
                LootTable = "141",
                LootItemChance = 75,
                LootItemLevel = 6,
                // MissSound = 0x264 /* Weapon */,
                // num_casts = 20,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 30 /* Weapon */,
                // spell = darkness,
                // spell_0 = decayingray,
                // spell_1 = spectretouch,
                // spell_2 = abyssalflame,
                // spell_3 = wraithbreath,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = kill,
                // spell_7 = plague,
                // spell_8 = teletoplayer,
                // Swordsmanship = 150,
                // TrueColor = 1645,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x3d9,
                CorpseNameOverride = "corpse of an Evil Codex Damnorum",
                CreatureType = CreatureType.Animated,
                DamageMax = 65,
                DamageMin = 25,
                Dex = 600,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 850,
                Hue = 1645,
                Int = 910,
                ManaMaxSeed = 1600,
                Name = "an Evil Codex Damnorum",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(DarknessSpell),
                    typeof(DecayingRaySpell),
                    typeof(SpectresTouchSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(WraithBreathSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(PlagueSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Air, 75},
                    {ElementalType.Water, 75},
                    {ElementalType.Fire, 75},
                    {ElementalType.Earth, 75},
                    {ElementalType.Necro, 100},
                    {ElementalType.PermMagicImmunity, 6}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 100},
                    {SkillName.MagicResist, 120},
                    {SkillName.Tactics, 100},
                    {SkillName.Magery, 180},
                    {SkillName.Healing, 100}
                },
                StamMaxSeed = 600,
                Str = 600,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public EvilCodexDamnorum() : base(CreatureProperties.Get<EvilCodexDamnorum>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Evil Codex Damnorum Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x263,
                MissSound = 0x264
            });
        }

        [Constructible]
        public EvilCodexDamnorum(Serial serial) : base(serial)
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