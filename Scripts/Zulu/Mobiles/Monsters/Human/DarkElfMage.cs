

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
    public class DarkElfMage : BaseCreature
    {
        static DarkElfMage() => CreatureProperties.Register<DarkElfMage>(new CreatureProperties
        {
            // cast_pct = 10,
            // count_casts = 1,
            // CProp_looter = s1,
            // CProp_PermMagicImmunity = i6,
            // DataElementId = drow,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = drow,
            // Graphic = 0x0f60 /* Weapon */,
            // HitSound = 0x13C /* Weapon */,
            // hostile = 1,
            // lootgroup = 57,
            // MagicItemChance = 60,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 2,
            // script = spellkillpcs,
            // Speed = 37 /* Weapon */,
            // spell = fireball,
            // spell_0 = lightning,
            // spell_1 = magicarrow,
            // spell_2 = paralyze,
            // spell_3 = flamestrike,
            // spell_4 = ebolt,
            // Swordsmanship = 110,
            // TrueColor = 0x0455,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x191,
            CorpseNameOverride = "corpse of a dark elf mage",
            CreatureType = CreatureType.Human,
            DamageMax = 43,
            DamageMin = 8,
            Dex = 195,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 160,
            Hue = 0x0455,
            Int = 300,
            ManaMaxSeed = 85,
            Name = "a dark elf mage",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
            },
            ProvokeSkillOverride = 50,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Macing, 95 },
                { SkillName.Tactics, 75 },
                { SkillName.MagicResist, 120 },
                { SkillName.Hiding, 100 },
                { SkillName.Magery, 110 },
            },
            StamMaxSeed = 95,
            Str = 160,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public DarkElfMage() : base(CreatureProperties.Get<DarkElfMage>())
        {
            // Add customization here

            AddItem(new Longsword
            {
                Movable = false,
                Name = "Drow Weapon",
                Speed = 37,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x13C,
                MissSound = 0x239,
            });
  
  
        }

        public DarkElfMage(Serial serial) : base(serial) {}

  

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