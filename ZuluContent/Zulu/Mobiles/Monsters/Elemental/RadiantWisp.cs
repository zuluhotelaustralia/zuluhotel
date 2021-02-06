using System;
using System.Collections.Generic;
using Scripts.Zulu.Spells.Earth;
using Scripts.Zulu.Spells.Necromancy;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;

namespace Server.Mobiles
{
    public class RadiantWisp : BaseCreature
    {
        static RadiantWisp()
        {
            CreatureProperties.Register<RadiantWisp>(new CreatureProperties
            {
                // CProp_EarthProtection = i4,
                // CProp_NecroProtection = i8,
                // CProp_Permmr = i8,
                // DataElementId = radiantwisp,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = radiantwisp,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:banishscript /* Weapon */,
                // HitSound = 0x1D5 /* Weapon */,
                LootTable = "80",
                LootItemChance = 25,
                LootItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // script = goodcaster,
                // speech = 7,
                // Speed = 35 /* Weapon */,
                // spell = ebolt,
                // spell_0 = flamestrike,
                // spell_1 = explosion,
                // spell_2 = gheal,
                // spell_3 = calllightning,
                // spell_4 = gustofair,
                // spell_5 = icestrike,
                // spell_6 = shiftingearth,
                // spell_7 = risingfire,
                // spell_8 = darkness,
                // spell_9 = wraithbreath,
                // Swordsmanship = 140,
                // TrueColor = 1154,
                // virtue = -5,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* goodcaster */,
                AutoDispel = true,
                Body = 0x3a,
                CanFly = true,
                CanSwim = true,
                CorpseNameOverride = "corpse of a Radiant Wisp",
                CreatureType = CreatureType.Elemental,
                DamageMax = 80,
                DamageMin = 10,
                Dex = 275,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 350,
                Hue = 1154,
                InitialInnocent = true,
                Int = 550,
                ManaMaxSeed = 200,
                Name = "a Radiant Wisp",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Fourth.GreaterHealSpell),
                    typeof(CallLightningSpell),
                    typeof(GustOfAirSpell),
                    typeof(IceStrikeSpell),
                    typeof(ShiftingEarthSpell),
                    typeof(RisingFireSpell),
                    typeof(DarknessSpell),
                    typeof(WraithBreathSpell)
                },
                ProvokeSkillOverride = 125,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100},
                    {ElementalType.Fire, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 120},
                    {SkillName.MagicResist, 130},
                    {SkillName.Magery, 150},
                    {SkillName.EvalInt, 150}
                },
                StamMaxSeed = 50,
                Str = 350,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public RadiantWisp() : base(CreatureProperties.Get<RadiantWisp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Radiant Wisp Weapon",
                Speed = 35,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public RadiantWisp(Serial serial) : base(serial)
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