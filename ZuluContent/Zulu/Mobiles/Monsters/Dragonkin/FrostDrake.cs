

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
    public class FrostDrake : BaseCreature
    {
        static FrostDrake() => CreatureProperties.Register<FrostDrake>(new CreatureProperties
        {
            // cast_pct = 20,
            // DataElementId = frostdrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = frostdrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 60,
            // MagicItemLevel = 3,
            // MissSound = 0x239 /* Weapon */,
            // num_casts = 2,
            // script = spellkillpcs,
            // Speed = 45 /* Weapon */,
            // spell = chainlightning,
            // spell_0 = ebolt,
            // spell_1 = flamestrike,
            // spell_2 = meteorswarm,
            // spell_3 = lightning,
            // TrueColor = 1154,
            // virtue = 6,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3c,
            CorpseNameOverride = "corpse of a frost drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 290,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.IceCrystal,
            HitsMax = 350,
            Hue = 1154,
            Int = 385,
            ManaMaxSeed = 85,
            MinTameSkill = 115,
            Name = "a frost drake",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Sixth.EnergyBoltSpell),
                typeof(Spells.Fourth.LightningSpell),
            },
            ProvokeSkillOverride = 130,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Cold, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 100 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 120 },
                { SkillName.Magery, 110 },
            },
            StamMaxSeed = 50,
            Str = 350,
            Tamable = true,
            VirtualArmor = 40,

        });


        [Constructible]
public FrostDrake() : base(CreatureProperties.Get<FrostDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frost Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public FrostDrake(Serial serial) : base(serial) {}



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
