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
    public class ElfDruid : BaseCreature
    {
        static ElfDruid()
        {
            CreatureProperties.Register<ElfDruid>(new CreatureProperties
            {
                // cast_pct = 50,
                // count_casts = 0,
                // CProp_Elf = i1,
                // CProp_leavecorpse = i1,
                // CProp_looter = s1,
                // CProp_PermMagicImmunity = i6,
                // DataElementId = elfdruid,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = elfdruid,
                // Graphic = 0x13f9 /* Weapon */,
                // HitSound = 0x13C /* Weapon */,
                // hostile = 1,
                // lootgroup = 133,
                // MagicItemChance = 60,
                // MagicItemLevel = 4,
                // MissSound = 0x239 /* Weapon */,
                // num_casts = 5,
                // script = elfspellkillpcs,
                // Speed = 30 /* Weapon */,
                // spell = shiftingearth,
                // spell_0 = calllightning,
                // spell_1 = flamestrike,
                // spell_2 = summonelelord,
                // Swordsmanship = 100,
                // TrueColor = 0x0302,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* elfspellkillpcs */,
                Body = 0x191,
                ClassLevel = 2,
                ClassSpec = SpecName.Mage,
                CorpseNameOverride = "corpse of an Elf Druid",
                CreatureType = CreatureType.Human,
                DamageMax = 40,
                DamageMin = 4,
                Dex = 195,
                Female = true,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 500,
                Hue = 0x0302,
                InitialInnocent = true,
                Int = 1000,
                ManaMaxSeed = 1000,
                Name = "an Elf Druid",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                PreferredSpells = new List<Type>
                {
                    typeof(ShiftingEarthSpell),
                    typeof(CallLightningSpell)
                },
                ProvokeSkillOverride = 120,
                SaySpellMantra = true,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Macing, 100},
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
        public ElfDruid() : base(CreatureProperties.Get<ElfDruid>())
        {
            // Add customization here

            AddItem(new DeathShroud
            {
                Movable = false,
                Name = "Druid's Robe",
                Hue = 0x0505
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
        public ElfDruid(Serial serial) : base(serial)
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