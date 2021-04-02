using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Necromancy;

namespace Server.Mobiles
{
    public class Lolth : BaseCreature
    {
        static Lolth()
        {
            CreatureProperties.Register<Lolth>(new CreatureProperties
            {
                // CProp_Dark-Elf = i1,
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
                LootTable = "9",
                LootItemChance = 60,
                LootItemLevel = 6,
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
                ClassType = ZuluClassType.Mage,
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
                    typeof(SorcerersBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(DecayingRaySpell)
                },
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 50},
                    {ElementalType.Fire, 100},
                    {ElementalType.Poison, 1},
                    {ElementalType.Necro, 100}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 150},
                    {SkillName.Tactics, 150},
                    {SkillName.Magery, 200},
                    {SkillName.MagicResist, 100}
                },
                StamMaxSeed = 250,
                Str = 1000,
                VirtualArmor = 100
            });
        }


        [Constructible]
        public Lolth() : base(CreatureProperties.Get<Lolth>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Hue = 0,
                Speed = 20,
                Skill = SkillName.Swords,
                Animation = (WeaponAnimation) 0x0009,
                MissSound = 0x1CA,
                HitSound = 0x1CB,
                MaxHitPoints = 65,
                HitPoints = 65,
                MaxRange = 12
            });
        }

        [Constructible]
        public Lolth(Serial serial) : base(serial)
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