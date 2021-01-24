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
    public class TaintedMage : BaseCreature
    {
        static TaintedMage()
        {
            CreatureProperties.Register<TaintedMage>(new CreatureProperties
            {
                // cast_pct = 100,
                // CProp_FinalDeath = i1,
                // DataElementId = taintedmage,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = taintedmage,
                // Graphic = 0x13F9 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                // lootgroup = 131,
                // Macefighting = 100,
                // MagicItemChance = 50,
                // Magicitemlevel = 4,
                // MissSound = 0x234 /* Weapon */,
                // num_casts = 90,
                // script = spellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = kill,
                // spell_1 = abyssalflame,
                // spell_2 = ebolt,
                // spell_3 = plague,
                // spell_4 = sorcerersbane,
                // spell_5 = wyvernstrike,
                // spell_6 = dispel,
                // spell_7 = massdispel,
                // spell_8 = wraithbreath,
                // spell_9 = darkness,
                // TrueColor = 1,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                ClassLevel = 3,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of <random> the Tainted Mage",
                CreatureType = CreatureType.Human,
                DamageMax = 15,
                DamageMin = 3,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 200,
                Hue = 1,
                Int = 1000,
                ManaMaxSeed = 1000,
                Name = "<random> the Tainted Mage",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(WyvernStrikeSpell),
                    typeof(AbyssalFlameSpell),
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(PlagueSpell),
                    typeof(SorcerorsBaneSpell),
                    typeof(WyvernStrikeSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(Spells.Seventh.MassDispelSpell),
                    typeof(WraithBreathSpell),
                    typeof(DarknessSpell)
                },
                RiseCreatureDelay = TimeSpan.FromSeconds(2),
                RiseCreatureType = typeof(EvisceratedCarcass),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 120},
                    {SkillName.EvalInt, 120},
                    {SkillName.Magery, 140}
                },
                StamMaxSeed = 50,
                Str = 200,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public TaintedMage() : base(CreatureProperties.Get<TaintedMage>())
        {
            // Add customization here

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "an ebony staff",
                Hue = 1157,
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x234,
                Animation = (WeaponAnimation) 0xC
            });

            AddItem(new DeathShroud
            {
                Movable = false,
                Name = "a tattered mage's robe",
                Hue = 1302
            });

            AddItem(new LeatherGloves
            {
                Movable = false,
                Name = "a pair of black leather gloves",
                Hue = 1,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300
            });

            AddItem(new Boots
            {
                Movable = false,
                Name = "a pair of black leather boots",
                Hue = 1
            });
        }

        [Constructible]
        public TaintedMage(Serial serial) : base(serial)
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