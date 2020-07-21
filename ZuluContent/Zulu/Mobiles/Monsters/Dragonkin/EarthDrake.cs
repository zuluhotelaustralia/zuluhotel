

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
    public class EarthDrake : BaseCreature
    {
        static EarthDrake() => CreatureProperties.Register<EarthDrake>(new CreatureProperties
        {
            // CProp_EarthProtection = i8,
            // CProp_PermMagicImmunity = i4,
            // DataElementId = earthdrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = earthdrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:staminadrainscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // script = firebreather,
            // Speed = 45 /* Weapon */,
            // TrueColor = 1134,
            // virtue = 7,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3c,
            CorpseNameOverride = "corpse of an Earth Drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 60,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            Hides = 5,
            HideType = HideType.Dragon,
            HitsMax = 500,
            Hue = 1134,
            Int = 90,
            ManaMaxSeed = 80,
            MinTameSkill = 115,
            Name = "an Earth Drake",
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 110 },
                { SkillName.Macing, 130 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 100,
            Str = 500,
            Tamable = true,
            VirtualArmor = 40,

        });


        [Constructible]
public EarthDrake() : base(CreatureProperties.Get<EarthDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Earth Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public EarthDrake(Serial serial) : base(serial) {}



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
