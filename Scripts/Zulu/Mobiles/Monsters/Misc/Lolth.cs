

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
    public class Lolth : BaseCreature
    {
        static Lolth() => CreatureProperties.Register<Lolth>(new CreatureProperties
        {
            // CProp_Dark-Elf = i1,
            // CProp_NecroProtection = i4,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_Permmr = i8,
            // DataElementId = lolth,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = lolth,
            // EvaluateIntelligence = 175,
            // graphic = 0x13B2 /* Weapon */,
            // Hitscript = :combat:poisonspit /* Weapon */,
            // HitSound = 0x1CB /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 60,
            // MagicItemLevel = 6,
            // MissSound = 0x1CA /* Weapon */,
            // script = elfspellkillpcs,
            // Speed = 20 /* Weapon */,
            // spell = summondarkelfqueen,
            // spell_0 = teletoplayer,
            // spell_1 = spitweb,
            // spell_2 = sorcerersbane,
            // spell_3 = kill,
            // spell_4 = decayingray,
            // Swordsmanship = 200,
            // TrueColor = 1109,
            // virtue = 2,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* elfspellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x48,
            ClassLevel = 4,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of Lolth",
            DamageMax = 66,
            DamageMin = 36,
            Dex = 400,
            Female = true,
            FightMode = FightMode.Closest,
            FightRange = 12,
            HitPoison = Poison.Greater,
            HitsMax = 3000,
            Hue = 1109,
            Int = 20000,
            ManaMaxSeed = 3000,
            Name = "Lolth",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.SorcerorsBaneSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.WyvernStrikeSpell),
                typeof(RunZH.Scripts.Zulu.Spells.Necromancy.DecayingRaySpell),
            },
            ProvokeSkillOverride = 160,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Energy, 50 },
                { ElementalType.Fire, 100 },
                { ElementalType.Poison, 10 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 150 },
                { SkillName.Tactics, 150 },
                { SkillName.Magery, 200 },
                { SkillName.MagicResist, 100 },
            },
            StamMaxSeed = 250,
            Str = 1000,
            VirtualArmor = 100,
  
        });

        [Constructable]
        public Lolth() : base(CreatureProperties.Get<Lolth>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Hue = 0,
                Speed = 20,
                Skill = SkillName.Swords,
                Animation = (WeaponAnimation)0x0009,
                MissSound = 0x1CA,
                HitSound = 0x1CB,
                MaxHitPoints = 65,
                HitPoints = 65,
                MaxRange = 12,
            });
  
  
        }

        public Lolth(Serial serial) : base(serial) {}

  

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