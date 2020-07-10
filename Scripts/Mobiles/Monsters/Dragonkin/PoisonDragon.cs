

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
    public class PoisonDragon : BaseCreature
    {
        static PoisonDragon() => CreatureProperties.Register<PoisonDragon>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_PermMagicImmunity = i3,
            // DataElementId = poisondragon,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = poisondragon,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 75,
            // Magicitemlevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = firebreather,
            // Speed = 65 /* Weapon */,
            // spell = fireball,
            // spell_0 = flamestrike,
            // spell_1 = ebolt,
            // spell_2 = lightning,
            // spell_3 = harm,
            // spell_4 = mindblast,
            // spell_5 = magicarrow,
            // spell_6 = chainlightning,
            // spell_7 = weaken,
            // spell_8 = masscurse,
            // TrueColor = 264,
            // virtue = 8,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0xc,
            CanFly = true,
            CorpseNameOverride = "corpse of a Poison Dragon",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 340,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            Hides = 5,
            HideType = HideType.Dragon,
            HitPoison = Poison.Deadly,
            HitsMax = 600,
            Hue = 264,
            Int = 400,
            ManaMaxSeed = 200,
            MinTameSkill = 140,
            Name = "a Poison Dragon",
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Second.HarmSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.First.WeakenSpell),
                typeof(Spells.Sixth.MassCurseSpell),
            },
            ProvokeSkillOverride = 140,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 80 },
                { SkillName.MagicResist, 95 },
                { SkillName.Tactics, 120 },
                { SkillName.Macing, 140 },
                { SkillName.Poisoning, 140 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 140,
            Str = 600,
            Tamable = true,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public PoisonDragon() : base(CreatureProperties.Get<PoisonDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Poison Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public PoisonDragon(Serial serial) : base(serial) {}

  

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