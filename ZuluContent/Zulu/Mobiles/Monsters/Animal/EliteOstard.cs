

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
    public class EliteOstard : BaseCreature
    {
        static EliteOstard() => CreatureProperties.Register<EliteOstard>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i250,
            // CProp_EarthProtection = i2,
            // CProp_NecroProtection = i2,
            // CProp_PermMagicImmunity = i5,
            // CProp_SuperOsty = i1,
            // DataElementId = eliteostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = eliteostard,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16B /* Weapon */,
            // hostile = 0,
            // MissSound = 0x239 /* Weapon */,
            // script = firebreather,
            // Speed = 60 /* Weapon */,
            // TrueColor = 1298,
            // virtue = 7,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysAttackable = true,
            AutoDispel = true,
            BaseSoundID = 625,
            Body = 0xda,
            CorpseNameOverride = "corpse of Elite Ostard",
            CreatureType = CreatureType.Animal,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 300,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HasBreath = true,
            HitsMax = 600,
            Hue = 1298,
            Int = 650,
            ManaMaxSeed = 150,
            MinTameSkill = 115,
            Name = "Elite Ostard",
            PerceptionRange = 10,
            ProvokeSkillOverride = 150,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Energy, 50 },
                { ElementalType.Cold, 50 },
                { ElementalType.Fire, 50 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 130 },
                { SkillName.MagicResist, 100 },
                { SkillName.DetectHidden, 100 },
            },
            StamMaxSeed = 125,
            Str = 600,
            Tamable = true,
            VirtualArmor = 50,

        });


        [Constructible]
public EliteOstard() : base(CreatureProperties.Get<EliteOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Great Wyrm Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16B,
                MissSound = 0x239,
            });


        }

        [Constructible]
public EliteOstard(Serial serial) : base(serial) {}



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
