

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
    public class Liche : BaseCreature
    {
        static Liche() => CreatureProperties.Register<Liche>(new CreatureProperties
        {
            // cast_pct = 40,
            // CProp_NecroProtection = i3,
            // CProp_PermMagicImmunity = i3,
            // DataElementId = liche,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = liche,
            // Equip_0 = liche,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x19F /* Weapon */,
            // hostile = 1,
            // lootgroup = 23,
            // MagicItemChance = 80,
            // Magicitemlevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 8,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 30 /* Weapon */,
            // spell = poison,
            // spell_0 = flamestrike,
            // spell_1 = ebolt,
            // spell_10 = paralyze,
            // spell_11 = weaken,
            // spell_2 = lightning,
            // spell_3 = harm,
            // spell_4 = mindblast,
            // spell_5 = magicarrow,
            // spell_6 = explosion,
            // spell_7 = fireball,
            // spell_8 = chainlightning,
            // spell_9 = masscurse,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x18,
            CorpseNameOverride = "corpse of a liche",
            CreatureType = CreatureType.Undead,
            DamageMax = 32,
            DamageMin = 4,
            Dex = 80,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 3,
            HideType = HideType.Liche,
            HitsMax = 180,
            Hue = 0,
            Int = 300,
            ManaMaxSeed = 200,
            Name = "a liche",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Third.PoisonSpell),
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Second.HarmSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.First.MagicArrowSpell),
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(Spells.Third.FireballSpell),
                typeof(Spells.Sixth.MassCurseSpell),
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.First.WeakenSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 65 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 100 },
                { SkillName.MagicResist, 80 },
                { SkillName.EvalInt, 80 },
                { SkillName.Magery, 150 },
            },
            StamMaxSeed = 70,
            Str = 180,
            VirtualArmor = 25,
  
        });

        [Constructable]
        public Liche() : base(CreatureProperties.Get<Liche>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Liche Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x19F,
                MissSound = 0x239,
            });
  
  
        }

        public Liche(Serial serial) : base(serial) {}

  

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