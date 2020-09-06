

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
    public class IceBalron : BaseCreature
    {
        static IceBalron() => CreatureProperties.Register<IceBalron>(new CreatureProperties
        {
            // cast_pct = 25,
            // CProp_BaseHpRegen = i500,
            // CProp_BaseManaRegen = i1000,
            // CProp_EarthProtection = i4,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // CProp_Permmr = i5,
            // DataElementId = icebalron,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = balron1,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x168 /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 95,
            // MagicItemLevel = 7,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 6,
            // Parry_0 = 150,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 65 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = kill,
            // spell_1 = abyssalflame,
            // spell_2 = ebolt,
            // spell_3 = sorcerersbane,
            // spell_4 = earthquake,
            // spell_5 = dispel,
            // spell_6 = massdispel,
            // spell_7 = spectretouch,
            // spell_8 = wraithbreath,
            // spell_9 = darkness,
            // TrueColor = 1152,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 40,
            CanFly = true,
            CorpseNameOverride = "corpse of an Ice Balron",
            CreatureType = CreatureType.Daemon,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 150,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 1100,
            Hue = 1152,
            Int = 2200,
            ManaMaxSeed = 2000,
            Name = "an Ice Balron",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(WyvernStrikeSpell),
                typeof(AbyssalFlameSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(SorcerorsBaneSpell),
                typeof(Spells.Eighth.EarthquakeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(SpectresTouchSpell),
                typeof(WraithBreathSpell),
                typeof(DarknessSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Cold, 75 },
                { ElementalType.Energy, 100 },
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 200 },
                { SkillName.MagicResist, 150 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 150 },
                { SkillName.Magery, 220 },
                { SkillName.EvalInt, 200 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 70,
            Str = 1100,
            VirtualArmor = 75,

        });


        [Constructible]
public IceBalron() : base(CreatureProperties.Get<IceBalron>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Balron Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239,
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500,
            });


        }

        [Constructible]
public IceBalron(Serial serial) : base(serial) {}



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
