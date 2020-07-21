

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class TwistedBramble : BaseCreature
    {
        static TwistedBramble() => CreatureProperties.Register<TwistedBramble>(new CreatureProperties
        {
            // cast_pct = 100,
            // CProp_EarthProtection = i4,
            // DataElementId = twistedbramble,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = twistedbramble,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1BD /* Weapon */,
            // hostile = 1,
            // lootgroup = 129,
            // MagicItemChance = 75,
            // Magicitemlevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 90,
            // script = spellkillpcs,
            // Speed = 30 /* Weapon */,
            // spell = flamestrike,
            // spell_0 = ebolt,
            // spell_1 = lightning,
            // spell_2 = mindblast,
            // spell_3 = explosion,
            // spell_4 = chainlightning,
            // spell_5 = paralyze,
            // spell_6 = weaken,
            // spell_7 = curse,
            // TrueColor = 1282,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            Body = 0x2f,
            ClassLevel = 3,
            ClassSpec = SpecName.Mage,
            CorpseNameOverride = "corpse of a Twisted Bramble",
            CreatureType = CreatureType.Plant,
            DamageMax = 50,
            DamageMin = 15,
            Dex = 150,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            HitsMax = 500,
            Hue = 1282,
            Int = 500,
            ManaMaxSeed = 500,
            Name = "a Twisted Bramble",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
                typeof(Spells.Fifth.MindBlastSpell),
                typeof(Spells.Sixth.ExplosionSpell),
                typeof(Spells.Fifth.ParalyzeSpell),
                typeof(Spells.First.WeakenSpell),
                typeof(Spells.Fourth.CurseSpell),
            },
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 150 },
                { SkillName.MagicResist, 75 },
                { SkillName.Magery, 120 },
                { SkillName.EvalInt, 100 },
            },
            StamMaxSeed = 100,
            Str = 500,
            VirtualArmor = 40,

        });


        [Constructible]
public TwistedBramble() : base(CreatureProperties.Get<TwistedBramble>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Twisted Bramble Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1BD,
                MissSound = 0x239,
            });


        }

        [Constructible]
public TwistedBramble(Serial serial) : base(serial) {}



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
