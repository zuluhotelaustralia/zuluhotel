

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
    public class AirDrake : BaseCreature
    {
        static AirDrake() => CreatureProperties.Register<AirDrake>(new CreatureProperties
        {
            // CProp_PermMagicImmunity = i4,
            // DataElementId = airdrake,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = airdrake,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:spellstrikescript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 36,
            // MagicItemChance = 25,
            // MagicItemLevel = 4,
            // MissSound = 0x239 /* Weapon */,
            // noloot = 1,
            // script = firebreather,
            // Speed = 50 /* Weapon */,
            // TrueColor = 1170,
            // virtue = 7,
            AiType = AIType.AI_Melee /* firebreather */,
            AlwaysMurderer = true,
            BaseSoundID = 362,
            Body = 0x3d,
            CorpseNameOverride = "corpse of an Air Drake",
            CreatureType = CreatureType.Dragonkin,
            DamageMax = 73,
            DamageMin = 33,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HasBreath = true,
            Hides = 5,
            HideType = HideType.Dragon,
            HitsMax = 400,
            Hue = 1170,
            Int = 90,
            ManaMaxSeed = 80,
            MinTameSkill = 115,
            Name = "an Air Drake",
            PerceptionRange = 10,
            ProvokeSkillOverride = 130,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Energy, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.MagicResist, 95 },
                { SkillName.Tactics, 110 },
                { SkillName.Macing, 110 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 130,
            Str = 400,
            Tamable = true,
            VirtualArmor = 20,
            WeaponAbility = new SpellStrike<Server.Spells.Earth.CallLightningSpell>(),
            WeaponAbilityChance = 0.5,
  
        });

        [Constructable]
        public AirDrake() : base(CreatureProperties.Get<AirDrake>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Air Drake Weapon",
                Speed = 50,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x16D,
                MissSound = 0x239,
            });
  
  
        }

        public AirDrake(Serial serial) : base(serial) {}

  

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