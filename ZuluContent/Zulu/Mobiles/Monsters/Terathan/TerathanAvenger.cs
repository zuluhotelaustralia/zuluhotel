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
    public class TerathanAvenger : BaseCreature
    {
        static TerathanAvenger()
        {
            CreatureProperties.Register<TerathanAvenger>(new CreatureProperties
            {
                // CProp_EarthProtection = i3,
                // DataElementId = terathanavenger,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = terathanavenger,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x24D /* Weapon */,
                // hostile = 1,
                LootTable = "68",
                LootItemChance = 10,
                LootItemLevel = 2,
                // MissSound = 0x24E /* Weapon */,
                // script = killpcssprinters,
                // speech = 6,
                // Speed = 47 /* Weapon */,
                // spell = flamestrike,
                // spell_0 = ebolt,
                // spell_1 = lightning,
                // spell_2 = dispel,
                // spell_3 = explosion,
                // spell_4 = masscurse,
                // TrueColor = 0,
                // virtue = 3,
                ActiveSpeed = 0.150,
                AiType = AIType.AI_Melee /* killpcssprinters */,
                AlwaysMurderer = true,
                Body = 0x46,
                CorpseNameOverride = "corpse of a Terathan Avenger",
                CreatureType = CreatureType.Terathan,
                DamageMax = 44,
                DamageMin = 8,
                Dex = 160,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitPoison = Poison.Regular,
                HitsMax = 225,
                Hue = 0,
                Int = 250,
                ManaMaxSeed = 0,
                Name = "a Terathan Avenger",
                PassiveSpeed = 0.300,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Fifth.DispelFieldSpell),
                    typeof(Spells.Sixth.ExplosionSpell),
                    typeof(Spells.Sixth.MassCurseSpell)
                },
                ProvokeSkillOverride = 90,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Air, 75}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Magery, 80},
                    {SkillName.Macing, 95},
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 130},
                    {SkillName.Parry, 75}
                },
                StamMaxSeed = 70,
                Str = 225,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public TerathanAvenger() : base(CreatureProperties.Get<TerathanAvenger>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Terathan Avenger Weapon",
                Speed = 47,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x24D,
                MissSound = 0x24E
            });
        }

        [Constructible]
        public TerathanAvenger(Serial serial) : base(serial)
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