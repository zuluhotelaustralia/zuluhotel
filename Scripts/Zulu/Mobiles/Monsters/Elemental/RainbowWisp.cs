

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using RunZH.Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class RainbowWisp : BaseCreature
    {
        static RainbowWisp() => CreatureProperties.Register<RainbowWisp>(new CreatureProperties
        {
            // CProp_Elf = i1,
            // CProp_NecroProtection = i2,
            // CProp_Permmr = i8,
            // DataElementId = rainbowwisp,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = rainbowwisp,
            // graphic = 0x13B2 /* Weapon */,
            // Hitscript = :combat:customanim /* Weapon */,
            // HitSound = 0x211 /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 60,
            // MagicItemLevel = 6,
            // MissSound = 0x212 /* Weapon */,
            // script = elfspellkillpcs,
            // speech = 7,
            // Speed = 20 /* Weapon */,
            // spell = calllightning,
            // spell_0 = gustofair,
            // spell_1 = icestrike,
            // spell_2 = shiftingearth,
            // spell_3 = summonelflord,
            // spell_4 = teletoplayer,
            // Swordsmanship = 200,
            // TrueColor = 1298,
            // virtue = -2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* elfspellkillpcs */,
            Body = 0x3a,
            CanFly = true,
            CanSwim = true,
            ClassLevel = 4,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of The Rainbow Wisp",
            CreatureType = CreatureType.Elemental,
            DamageMax = 66,
            DamageMin = 36,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 12,
            HitsMax = 3000,
            Hue = 1298,
            InitialInnocent = true,
            Int = 20000,
            ManaMaxSeed = 3000,
            Name = "The Rainbow Wisp",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(RunZH.Scripts.Zulu.Spells.Earth.CallLightningSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Earth.GustOfAirSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Earth.IceStrikeSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Earth.ShiftingEarthSpell),
            },
            ProvokeSkillOverride = 160,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 10 },
                { ElementalType.Cold, 100 },
                { ElementalType.Energy, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.MagicResist, 100 },
                { SkillName.Magery, 200 },
                { SkillName.EvalInt, 150 },
            },
            StamMaxSeed = 250,
            Str = 1000,
            VirtualArmor = 100,
  
        });

        [Constructable]
        public RainbowWisp() : base(CreatureProperties.Get<RainbowWisp>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Hue = 0,
                Speed = 20,
                Skill = SkillName.Swords,
                Animation = (WeaponAnimation)0x0009,
                MissSound = 0x212,
                HitSound = 0x211,
                MaxHitPoints = 65,
                HitPoints = 65,
                MaxRange = 12,
            });
  
  
        }

        public RainbowWisp(Serial serial) : base(serial) {}

  

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