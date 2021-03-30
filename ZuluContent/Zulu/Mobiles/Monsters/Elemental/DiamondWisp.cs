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
    public class DiamondWisp : BaseCreature
    {
        static DiamondWisp()
        {
            CreatureProperties.Register<DiamondWisp>(new CreatureProperties
            {
                // cast_pct = 40,
                // DataElementId = diamondwisp,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = blackwisp,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x1D5 /* Weapon */,
                // hostile = 1,
                LootTable = "203",
                LootItemChance = 75,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 8,
                // script = spellkillpcs,
                // speech = 7,
                // Speed = 35 /* Weapon */,
                // spell = masscurse,
                // spell_0 = abyssalflame,
                // spell_1 = wyvernstrike,
                // spell_2 = spectretouch,
                // spell_3 = sorcerersbane,
                // spell_4 = wraithbreath,
                // spell_5 = decayingray,
                // spell_6 = kill,
                // TrueColor = 1176,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                AutoDispel = true,
                Body = 0x3a,
                CorpseNameOverride = "corpse of a diamond wisp",
                CreatureType = CreatureType.Elemental,
                DamageMax = 64,
                DamageMin = 8,
                Dex = 575,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 1400,
                Hue = 1176,
                Int = 1100,
                ManaMaxSeed = 1100,
                Name = "a diamond wisp",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.MassCurseSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(SpectresTouchSpell),
                    typeof(SorcerersBaneSpell),
                    typeof(WraithBreathSpell),
                    typeof(DecayingRaySpell),
                    typeof(WyvernStrikeSpell)
                },
                ProvokeSkillOverride = 120,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.PermMagicImmunity, 6}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 200},
                    {SkillName.MagicResist, 200},
                    {SkillName.Magery, 200},
                    {SkillName.EvalInt, 200}
                },
                StamMaxSeed = 175,
                Str = 1700,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public DiamondWisp() : base(CreatureProperties.Get<DiamondWisp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Blackwisp Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DiamondWisp(Serial serial) : base(serial)
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