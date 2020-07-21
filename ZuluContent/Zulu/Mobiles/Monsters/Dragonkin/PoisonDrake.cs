

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
    public class PoisonDrake : BaseCreature
    {
        static PoisonDrake() => CreatureProperties.Register<PoisonDrake>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i4,
            // DataElementId = poisondrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = poisondrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // script = killpcs,
            // Speed = 45 /* Weapon */,
            // TrueColor = 264,
            // virtue = 7,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3c,
            CorpseNameOverride = "corpse of a Poison Drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Dragon,
            HitPoison = Poison.Greater,
            HitsMax = 350,
            Hue = 264,
            Int = 90,
            ManaMaxSeed = 80,
            MinTameSkill = 120,
            Name = "a Poison Drake",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 70 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 120 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 100,
            Str = 350,
            Tamable = true,
            VirtualArmor = 20,

        });


        [Constructible]
public PoisonDrake() : base(CreatureProperties.Get<PoisonDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Poison Drake Weapon",
                Speed = 45,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });


        }

        [Constructible]
public PoisonDrake(Serial serial) : base(serial) {}



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
