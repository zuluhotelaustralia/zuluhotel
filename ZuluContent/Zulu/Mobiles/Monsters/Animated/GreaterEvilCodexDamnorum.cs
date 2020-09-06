

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
    public class GreaterEvilCodexDamnorum : BaseCreature
    {
        static GreaterEvilCodexDamnorum() => CreatureProperties.Register<GreaterEvilCodexDamnorum>(new CreatureProperties
        {
            // cast_pct = 80,
            // count_casts = 0,
            // CProp_AttackTypeImmunities = i256,
            // CProp_BaseHpRegen = i350,
            // CProp_BaseManaRegen = i500,
            // CProp_EarthProtection = i3,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i6,
            // CProp_Permmr = i4,
            // DataElementId = GreaterEvilCodexDamnorum,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = EvilCodexDamnorum,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:piercingscript /* Weapon */,
            // HitSound = 0x263 /* Weapon */,
            // hostile = 1,
            // lootgroup = 141,
            // MagicItemChance = 75,
            // MagicItemLevel = 6,
            // MissSound = 0x264 /* Weapon */,
            // num_casts = 20,
            // script = spellkillpcsTeleporter,
            // speech = 35,
            // Speed = 30 /* Weapon */,
            // spell = MassCast	darkness,
            // spell_0 = MassCast	decayingray,
            // spell_1 = MassCast	spectretouch,
            // spell_2 = MassCast	sorcerersbane,
            // spell_3 = MassCast	wyvernstrike,
            // spell_4 = MassCast	kill,
            // spell_5 = MassCast	plague,
            // spell_6 = teletoplayer,
            // Swordsmanship = 150,
            // TrueColor = 1645,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x3d9,
            CorpseNameOverride = "corpse of a Greater Evil Codex Damnorum",
            CreatureType = CreatureType.Animated,
            DamageMax = 65,
            DamageMin = 25,
            Dex = 1600,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 1850,
            Hue = 1645,
            Int = 1910,
            ManaMaxSeed = 1600,
            Name = "a Greater Evil Codex Damnorum",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(DarknessSpell),
                typeof(DecayingRaySpell),
                typeof(SpectresTouchSpell),
                typeof(SorcerorsBaneSpell),
                typeof(WyvernStrikeSpell),
                typeof(WyvernStrikeSpell),
                typeof(PlagueSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Energy, 75 },
                { ElementalType.Cold, 75 },
                { ElementalType.Fire, 75 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 100 },
                { SkillName.MagicResist, 120 },
                { SkillName.Tactics, 100 },
                { SkillName.Magery, 180 },
                { SkillName.Healing, 100 },
            },
            StamMaxSeed = 600,
            Str = 1600,
            TargetAcquireExhaustion = true,
            VirtualArmor = 25,

        });


        [Constructible]
public GreaterEvilCodexDamnorum() : base(CreatureProperties.Get<GreaterEvilCodexDamnorum>())
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
                MissSound = 0x264,
            });


        }

        [Constructible]
public GreaterEvilCodexDamnorum(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
