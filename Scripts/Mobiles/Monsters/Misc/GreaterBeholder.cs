

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
    public class GreaterBeholder : BaseCreature
    {
        static GreaterBeholder() => CreatureProperties.Register<GreaterBeholder>(new CreatureProperties
        {
            // cast_pct = 50,
            // count_casts = 0,
            // CProp_massCastRange = i12,
            // DataElementId = GreaterBeholder,
            // DataElementType = NpcTemplate,
            // Detect = Hidden 200,
            // dstart = 10,
            // Equip = beholder,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x17C /* Weapon */,
            // hostile = 1,
            // lootgroup = 57,
            // MagicItemChance = 75,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 40,
            // script = spellkillpcsTeleporter,
            // Speed = 33 /* Weapon */,
            // spell = MassCast	paralyze,
            // spell_0 = MassCast	fireball,
            // spell_1 = MassCast	lightning,
            // spell_2 = MassCast	Curse,
            // spell_3 = MassCast	flamestrike,
            // spell_4 = MassCast	kill,
            // spell_5 = MassCast	poison,
            // spell_6 = MassCast	mindblast,
            // TrueColor = 1173,
            AiType = AIType.AI_Mage /* spellkillpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x16,
            ClassLevel = 6,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of Greater Beholder",
            DamageMax = 34,
            DamageMin = 14,
            Dex = 500,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 1500,
            Hue = 1173,
            Int = 1600,
            ManaMaxSeed = 1500,
            Name = "Greater Beholder",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Fourth.CurseSpell),
                typeof(Spells.Necromancy.WyvernStrikeSpell),
                typeof(Spells.Third.PoisonSpell),
                typeof(Spells.Fifth.MindBlastSpell),
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 200 },
                { SkillName.Tactics, 200 },
                { SkillName.Macing, 200 },
                { SkillName.Magery, 300 },
            },
            StamMaxSeed = 500,
            Str = 1500,
            Tamable = false,
            TargetAcquireExhaustion = true,
            VirtualArmor = 20,
  
        });

        [Constructable]
        public GreaterBeholder() : base(CreatureProperties.Get<GreaterBeholder>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Beholder Weapon",
                Speed = 33,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x17C,
                MissSound = 0x239,
            });
  
  
        }

        public GreaterBeholder(Serial serial) : base(serial) {}

  

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