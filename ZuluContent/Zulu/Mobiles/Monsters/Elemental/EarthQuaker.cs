using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Spells.Earth;

namespace Server.Mobiles
{
    public class EarthQuaker : BaseCreature
    {
        static EarthQuaker()
        {
            CreatureProperties.Register<EarthQuaker>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_casts = 0,
                // CProp_BaseHpRegen = i1000,
                // CProp_massCastRange = i12,
                // DataElementId = earthquaker,
                // DataElementType = NpcTemplate,
                // Equip = behemoth,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x16D /* Weapon */,
                // hostile = 1,
                LootTable = "9",
                LootItemChance = 50,
                LootItemLevel = 6,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 40,
                // script = spellkillpcsTeleporter,
                // Speed = 50 /* Weapon */,
                // spell = MassCast shiftingearth,
                // spell_0 = MassCast shiftingearth,
                // spell_1 = MassCast shiftingearth,
                // spell_2 = MassCast shiftingearth,
                // TrueColor = 1000,
                AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
                AlwaysMurderer = true,
                AutoDispel = true,
                BardImmune = true,
                Body = 0x0e,
                ClassLevel = 4,
                ClassType = ZuluClassType.Mage,
                CorpseNameOverride = "corpse of an Earth Quaker",
                CreatureType = CreatureType.Elemental,
                DamageMax = 60,
                DamageMin = 10,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 2250,
                Hue = 1000,
                Int = 55,
                ManaMaxSeed = 2550,
                Name = "an Earth Quaker",
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(ShiftingEarthSpell),
                    typeof(ShiftingEarthSpell),
                    typeof(ShiftingEarthSpell),
                    typeof(ShiftingEarthSpell)
                },
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Fire, 100},
                    {ElementalType.Air, 100},
                    {ElementalType.Water, 100},
                    {ElementalType.Necro, 100},
                    {ElementalType.Earth, 100},
                    {ElementalType.MagicImmunity, 8}
                },
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 250},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 60},
                    {SkillName.DetectHidden, 200},
                    {SkillName.Hiding, 200},
                    {SkillName.Magery, 300}
                },
                StamMaxSeed = 200,
                Str = 2250,
                Tamable = false,
                TargetAcquireExhaustion = true,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public EarthQuaker() : base(CreatureProperties.Get<EarthQuaker>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Behemoth Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239
            });
        }

        [Constructible]
        public EarthQuaker(Serial serial) : base(serial)
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