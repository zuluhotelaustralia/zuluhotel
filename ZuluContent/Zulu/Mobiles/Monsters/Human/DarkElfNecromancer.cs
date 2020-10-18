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
    public class DarkElfNecromancer : BaseCreature
    {
        static DarkElfNecromancer()
        {
            CreatureProperties.Register<DarkElfNecromancer>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_casts = 1,
                // CProp_Dark-Elf = i1,
                // CProp_leavecorpse = i1,
                // CProp_looter = s1,
                // CProp_PermMagicImmunity = i6,
                // DataElementId = darkelfnecromancer,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = darkelfnecromancer,
                // Graphic = 0x13f9 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                // lootgroup = 138,
                // MagicItemChance = 60,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = elfspellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = decayingray,
                // spell_0 = sorcerersbane,
                // spell_1 = wyvernstrike,
                // spell_2 = summonundead,
                // Swordsmanship = 110,
                // TrueColor = 1109,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* elfspellkillpcs */,
                AlwaysMurderer = true,
                Body = 0x190,
                ClassLevel = 2,
                ClassSpec = SpecName.Mage,
                CorpseNameOverride = "corpse of a Dark-Elf Necromancer",
                CreatureType = CreatureType.Human,
                DamageMax = 40,
                DamageMin = 4,
                Dex = 195,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 500,
                Hue = 1109,
                Int = 1000,
                ManaMaxSeed = 1000,
                Name = "a Dark-Elf Necromancer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(DecayingRaySpell),
                    typeof(SorcerorsBaneSpell),
                    typeof(WyvernStrikeSpell)
                },
                ProvokeSkillOverride = 120,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 95},
                    {SkillName.Tactics, 75},
                    {SkillName.MagicResist, 150},
                    {SkillName.Magery, 150}
                },
                StamMaxSeed = 195,
                Str = 200,
                VirtualArmor = 25
            });
        }


        [Constructible]
        public DarkElfNecromancer() : base(CreatureProperties.Get<DarkElfNecromancer>())
        {
            // Add customization here

            AddItem(new LongHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1
            });

            AddItem(new GnarledStaff
            {
                Movable = false,
                Name = "Evil Mage Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239
            });
        }

        [Constructible]
        public DarkElfNecromancer(Serial serial) : base(serial)
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