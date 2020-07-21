

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
    public class GlacierGiant : BaseCreature
    {
        static GlacierGiant() => CreatureProperties.Register<GlacierGiant>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i8,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // DataElementId = glaciergiant,
            // DataElementType = NpcTemplate,
            // Equip = behemoth,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 85,
            // MagicItemLevel = 7,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 50 /* Weapon */,
            // TrueColor = 1152,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 0x02,
            CorpseNameOverride = "corpse of a Glacier Giant",
            CreatureType = CreatureType.Elemental,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 410,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 2250,
            Hue = 1152,
            Int = 55,
            ManaMaxSeed = 0,
            Name = "a Glacier Giant",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 160 },
                { SkillName.Macing, 190 },
                { SkillName.MagicResist, 60 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 200,
            Str = 2320,
            VirtualArmor = 45,

        });


        [Constructible]
public GlacierGiant() : base(CreatureProperties.Get<GlacierGiant>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Behemoth Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public GlacierGiant(Serial serial) : base(serial) {}



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
