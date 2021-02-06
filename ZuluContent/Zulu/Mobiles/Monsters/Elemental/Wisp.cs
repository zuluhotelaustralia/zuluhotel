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
    public class Wisp : BaseCreature
    {
        static Wisp()
        {
            CreatureProperties.Register<Wisp>(new CreatureProperties
            {
                // CProp_EarthProtection = i4,
                // CProp_Permmr = i8,
                // DataElementId = wisp,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = wisp,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x1D5 /* Weapon */,
                LootTable = "125",
                LootItemChance = 25,
                LootItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // script = goodcaster,
                // speech = 7,
                // Speed = 45 /* Weapon */,
                // spell = ebolt,
                // spell_0 = flamestrike,
                // spell_1 = lightning,
                // spell_2 = harm,
                // spell_3 = curse,
                // spell_4 = meteorswarm,
                // Swordsmanship = 80,
                // TrueColor = 0,
                // virtue = -5,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* goodcaster */,
                Body = 0x3a,
                CanFly = true,
                CanSwim = true,
                CorpseNameOverride = "corpse of a wisp",
                CreatureType = CreatureType.Elemental,
                DamageMax = 45,
                DamageMin = 21,
                Dex = 185,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 225,
                Hue = 0,
                InitialInnocent = true,
                Int = 500,
                ManaMaxSeed = 200,
                Name = "a wisp",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(Spells.Sixth.EnergyBoltSpell),
                    typeof(Spells.Fourth.LightningSpell),
                    typeof(Spells.Second.HarmSpell),
                    typeof(Spells.Fourth.CurseSpell)
                },
                ProvokeSkillOverride = 160,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.PermPoisonImmunity, 100}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.Tactics, 100},
                    {SkillName.MagicResist, 90},
                    {SkillName.Magery, 120}
                },
                StamMaxSeed = 50,
                Str = 225,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public Wisp() : base(CreatureProperties.Get<Wisp>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Wisp Weapon",
                Speed = 45,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D5,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Wisp(Serial serial) : base(serial)
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