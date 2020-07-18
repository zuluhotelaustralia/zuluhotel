

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
    public class ScorpionKing : BaseCreature
    {
        static ScorpionKing() => CreatureProperties.Register<ScorpionKing>(new CreatureProperties
        {
            // cast_pct = 20,
            // CProp_BaseHpRegen = i500,
            // CProp_BaseManaRegen = i1000,
            // CProp_EarthProtection = i4,
            // CProp_NecroProtection = i4,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i7,
            // CProp_Permmr = i5,
            // DataElementId = scorpionking,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = scorpionking,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x190 /* Weapon */,
            // hostile = 1,
            // lootgroup = 150,
            // MagicItemChance = 90,
            // MagicItemLevel = 8,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 4,
            // Parry_0 = 200,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 55 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = kill,
            // spell_1 = abyssalflame,
            // spell_10 = wraithbreath,
            // spell_11 = darkness,
            // spell_2 = ebolt,
            // spell_3 = plague,
            // spell_4 = sorcerersbane,
            // spell_5 = wyvernstrike,
            // spell_6 = earthquake,
            // spell_7 = dispel,
            // spell_8 = massdispel,
            // spell_9 = spectretouch,
            // TrueColor = 1172,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x30,
            CanFly = true,
            CorpseNameOverride = "corpse of a Scorpion King",
            CreatureType = CreatureType.Daemon,
            DamageMax = 140,
            DamageMin = 14,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitPoison = Poison.Lethal,
            HitsMax = 5000,
            Hue = 1172,
            Int = 2000,
            ManaMaxSeed = 2000,
            Name = "a Scorpion King",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.AbyssalFlameSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.PlagueSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Eighth.EarthquakeSpell),
                typeof(Spells.Fifth.DispelFieldSpell),
                typeof(Spells.Seventh.MassDispelSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.SpectresTouchSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WraithBreathSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DarknessSpell),
            },
            ProvokeSkillOverride = 160,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 75 },
                { ElementalType.Cold, 75 },
                { ElementalType.Energy, 100 },
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 200 },
                { SkillName.MagicResist, 300 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 150 },
                { SkillName.Magery, 300 },
                { SkillName.EvalInt, 200 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Hiding, 200 },
            },
            StamMaxSeed = 300,
            Str = 1500,
            Tamable = false,
            VirtualArmor = 75,
  
        });

        [Constructable]
        public ScorpionKing() : base(CreatureProperties.Get<ScorpionKing>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Scorpion King Weapon",
                Speed = 55,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x190,
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

        public ScorpionKing(Serial serial) : base(serial) {}

  

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}