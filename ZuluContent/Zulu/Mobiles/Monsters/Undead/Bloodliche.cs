

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
    public class Bloodliche : BaseCreature
    {
        static Bloodliche() => CreatureProperties.Register<Bloodliche>(new CreatureProperties
        {
            // cast_pct = 25,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i7,
            // DataElementId = bloodliche,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = bloodliche,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x19F /* Weapon */,
            // hostile = 1,
            // lootgroup = 22,
            // MagicItemChance = 25,
            // MagicItemLevel = 5,
            // MissSound = 0x19D /* Weapon */,
            // num_casts = 5,
            // script = spellkillpcs,
            // speech = 35,
            // Speed = 35 /* Weapon */,
            // spell = poison,
            // spell_0 = flamestrike,
            // spell_1 = ebolt,
            // spell_10 = paralyze,
            // spell_11 = masscurse,
            // spell_12 = summonskel,
            // spell_2 = lightning,
            // spell_3 = harm,
            // spell_4 = mindblast,
            // spell_5 = magicarrow,
            // spell_6 = explosion,
            // spell_7 = meteorswarm,
            // spell_8 = chainlightning,
            // spell_9 = summondaemon,
            // TrueColor = 0x04b9,
            // virtue = 4,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x18,
            CorpseNameOverride = "corpse of a Bloodliche",
            CreatureType = CreatureType.Undead,
            DamageMax = 45,
            DamageMin = 9,
            Dex = 210,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 3,
            HideType = HideType.Liche,
            HitsMax = 275,
            Hue = 0x04b9,
            Int = 900,
            ManaMaxSeed = 200,
            Name = "a Bloodliche",
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
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.Sixth.MassCurseSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Magery, 175 },
                { SkillName.MagicResist, 150 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 160 },
            },
            StamMaxSeed = 50,
            Str = 275,
            VirtualArmor = 30,

        });


        [Constructible]
public Bloodliche() : base(CreatureProperties.Get<Bloodliche>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Bloodliche Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x19F,
                MissSound = 0x19D,
            });


        }

        [Constructible]
public Bloodliche(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
