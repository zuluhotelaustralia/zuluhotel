

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
    public class BahamutWyrmKing : BaseCreature
    {
        static BahamutWyrmKing() => CreatureProperties.Register<BahamutWyrmKing>(new CreatureProperties
        {
            // cast_pct = 90,
            // count_casts = 0,
            // CProp_BaseHpRegen = i250,
            // CProp_massCastRange = i15,
            // CProp_PermMagicImmunity = i5,
            // DataElementId = wyrmking,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = daemonwyrm,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:customanim /* Weapon */,
            // HitSound = 0x16B /* Weapon */,
            // hostile = 1,
            // lootgroup = 35,
            // MagicItemChance = 80,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // script = spellkillpcsTeleporter,
            // Speed = 60 /* Weapon */,
            // spell = MassCast paralyze,
            // spell_0 = MassCast gustofair,
            // spell_1 = MassCast icestrike,
            // TrueColor = 1297,
            // virtue = 7,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            BaseSoundID = 362,
            Body = 0x3b,
            ClassLevel = 6,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of Bahamut, the Wyrm King",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 2475,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 7,
            HitsMax = 1900,
            Hue = 1297,
            Int = 650,
            ManaMaxSeed = 150,
            MinTameSkill = 150,
            Name = "Bahamut, the Wyrm King",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.Earth.GustOfAirSpell),
                typeof(Spells.Earth.IceStrikeSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Fire, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 150 },
                { SkillName.MagicResist, 110 },
                { SkillName.Magery, 300 },
                { SkillName.DetectHidden, 150 },
            },
            StamMaxSeed = 2475,
            Str = 1900,
            Tamable = true,
            TargetAcquireExhaustion = true,
            VirtualArmor = 50,
  
        });

        [Constructable]
        public BahamutWyrmKing() : base(CreatureProperties.Get<BahamutWyrmKing>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Daemon Wyrm Weapon",
                Hue = 1645,
                Speed = 60,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239,
                MaxRange = 7,
                Animation = (WeaponAnimation)0x0009,
            });
  
  
        }

        public BahamutWyrmKing(Serial serial) : base(serial) {}

  

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