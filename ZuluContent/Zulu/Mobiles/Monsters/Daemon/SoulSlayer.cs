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
    public class SoulSlayer : BaseCreature
    {
        static SoulSlayer()
        {
            CreatureProperties.Register<SoulSlayer>(new CreatureProperties
            {
                // cast_pct = 80,
                // CProp_BaseHpRegen = i500,
                // CProp_BaseManaRegen = i1000,
                // CProp_EarthProtection = i6,
                // CProp_massCastRange = i15,
                // CProp_NecroProtection = i8,
                // CProp_PermMagicImmunity = i8,
                // CProp_Permmr = i5,
                // DataElementId = hiddensoulslayer,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = soulslayer,
                // Gender_0 = 0,
                // Graphic = 0x0ec4 /* Weapon */,
                // hiding = 200,
                // Hitscript = :combat:voidscript /* Weapon */,
                // HitSound = 0x25F /* Weapon */,
                // hostile = 1,
                // lootgroup = 9,
                // MagicItemChance = 100,
                // MagicItemLevel = 6,
                // MissSound = 0x169 /* Weapon */,
                // num_casts = 800,
                // Parry_0 = 100,
                // script = spellkillpcs,
                // speech = 35,
                // Speed = 65 /* Weapon */,
                // spell = MassCast plague,
                // spell_0 = MassCast dispel,
                // spell_1 = MassCast spectretouch,
                // spell_2 = MassCast mindblast,
                // spell_3 = MassCast curse,
                // TrueColor = 1302,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 792,
                CanFly = true,
                CorpseNameOverride = "corpse of a Soul Slayer",
                CreatureType = CreatureType.Daemon,
                DamageMax = 99,
                DamageMin = 57,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 5,
                HitsMax = 1000,
                Hue = 1302,
                Int = 3000,
                ManaMaxSeed = 2000,
                Name = "a Soul Slayer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(PlagueSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(SpectresTouchSpell),
                    typeof(Spells.Fifth.MindBlastSpell),
                    typeof(Spells.Fourth.CurseSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Poison, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 200},
                    {SkillName.MagicResist, 200},
                    {SkillName.Tactics, 200},
                    {SkillName.Macing, 150},
                    {SkillName.Magery, 125},
                    {SkillName.EvalInt, 200},
                    {SkillName.DetectHidden, 200}
                },
                StamMaxSeed = 70,
                Str = 1100,
                VirtualArmor = 60
            });
        }


        [Constructible]
        public SoulSlayer() : base(CreatureProperties.Get<SoulSlayer>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Soul Slayer Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169,
                MaxRange = 5
            });
        }

        [Constructible]
        public SoulSlayer(Serial serial) : base(serial)
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