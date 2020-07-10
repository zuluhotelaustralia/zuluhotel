

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Server.Engines.Magic.HitScripts;

namespace Server.Mobiles
{
    public class TidalDragon : BaseCreature
    {
        static TidalDragon() => CreatureProperties.Register<TidalDragon>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = tidaldragon,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = tidaldragon,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:spellstrikescript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 75,
            // Magicitemlevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = killpcs,
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
            // TrueColor = 1165,
            // virtue = 8,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0xc,
            CanFly = true,
            CorpseNameOverride = "corpse of a Tidal Dragon",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 340,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.IceCrystal,
            HitsMax = 500,
            Hue = 1165,
            Int = 400,
            ManaMaxSeed = 200,
            MinTameSkill = 140,
            Name = "a Tidal Dragon",
            PassiveSpeed = 0.4,
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
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 80 },
                { SkillName.MagicResist, 130 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 120 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 140,
            Str = 500,
            Tamable = true,
            VirtualArmor = 40,
            WeaponAbility = new SpellStrike<Server.Spells.Necromancy.SorcerorsBaneSpell>(),
            WeaponAbilityChance = 0.8,
  
        });

        [Constructable]
        public TidalDragon() : base(CreatureProperties.Get<TidalDragon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tidal Dragon Weapon",
                Speed = 65,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public TidalDragon(Serial serial) : base(serial) {}

  

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