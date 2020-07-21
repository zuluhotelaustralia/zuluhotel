

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class Juggernaught : BaseCreature
    {
        static Juggernaught() => CreatureProperties.Register<Juggernaught>(new CreatureProperties
        {
            // cast_pct = 100,
            // count_casts = 0,
            // CProp_BaseHpRegen = i3000,
            // CProp_EarthProtection = i4,
            // CProp_massCastRange = i15,
            // CProp_NecroProtection = i4,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i4,
            // CProp_SpecialAttack = i14360,
            // DataElementId = juggernaught,
            // DataElementType = NpcTemplate,
            // equip = juggernaught,
            // Graphic = 0x0f63 /* Weapon */,
            // HitScript = :combat:spellstrikescript /* Weapon */,
            // HitSound = 0x2F5 /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 50,
            // MagicItemLevel = 6,
            // MissSound = 0x219 /* Weapon */,
            // num_casts = 500,
            // script = spellkillpcs,
            // Speed = 99 /* Weapon */,
            // spell = MassCast wallofstone,
            // spell_0 = MassCast blade_spirit,
            // spell_1 = MassCast dispel,
            // spell_2 = summonjuggernaught,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            BardImmune = true,
            BaseSoundID = 532,
            Body = 0x0e,
            CorpseNameOverride = "corpse of a Juggernaught",
            CreatureType = CreatureType.Elemental,
            DamageMax = 70,
            DamageMin = 7,
            Dex = 10,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 6,
            HitsMax = 2000,
            Hue = 0,
            Int = 1000,
            ManaMaxSeed = 1000,
            Name = "a Juggernaught",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.WallOfStoneSpell),
                typeof(Spells.Fifth.BladeSpiritsSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 250 },
                { SkillName.Fencing, 200 },
                { SkillName.MagicResist, 200 },
                { SkillName.Magery, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Meditation, 200 },
            },
            StamMaxSeed = 200,
            Str = 1000,
            Tamable = false,
            VirtualArmor = 100,
            WeaponAbility = new SpellStrike<Server.Spells.Fourth.LightningSpell>(),
            WeaponAbilityChance = 1,

        });


        [Constructible]
public Juggernaught() : base(CreatureProperties.Get<Juggernaught>())
        {
            // Add customization here

            AddItem(new Spear
            {
                Movable = false,
                Name = "Juggernaught Weapon",
                Speed = 99,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x2F5,
                MissSound = 0x219,
                MaxRange = 6,
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR40",
                BaseArmorRating = 40,
                MaxHitPoints = 500,
                HitPoints = 500,
            });


        }

        [Constructible]
public Juggernaught(Serial serial) : base(serial) {}



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
