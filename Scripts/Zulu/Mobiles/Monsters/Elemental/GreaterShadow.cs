

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
    public class GreaterShadow : BaseCreature
    {
        static GreaterShadow() => CreatureProperties.Register<GreaterShadow>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i500,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i5,
            // CProp_Permmr = i3,
            // DataElementId = greatershadow,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = greatershadow,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:voidscript /* Weapon */,
            // HitSound = 0x283 /* Weapon */,
            // hostile = 1,
            // lootgroup = 78,
            // MagicItemChance = 15,
            // MagicItemLevel = 4,
            // MissSound = 0x282 /* Weapon */,
            // Parrying = 100,
            // script = killpcs,
            // Speed = 37 /* Weapon */,
            // TrueColor = 1,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 573,
            CanSwim = true,
            CorpseNameOverride = "corpse of a Greater Shadow",
            CreatureType = CreatureType.Elemental,
            DamageMax = 45,
            DamageMin = 21,
            Dex = 275,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 350,
            Hue = 1,
            Int = 200,
            ManaMaxSeed = 125,
            Name = "a Greater Shadow",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            SaySpellMantra = true,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 125 },
                { SkillName.Tactics, 125 },
                { SkillName.Fencing, 150 },
                { SkillName.DetectHidden, 130 },
            },
            StamMaxSeed = 80,
            Str = 350,
            Tamable = false,
  
        });

        [Constructable]
        public GreaterShadow() : base(CreatureProperties.Get<GreaterShadow>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Greater Shadow Weapon",
                Hue = 1,
                Speed = 37,
                Skill = SkillName.Fencing,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x283,
                MissSound = 0x282,
            });
  
            AddItem(new BoneGloves
            {
                Movable = false,
                Name = "Green Bone Gloves AR10",
                Hue = 0x0491,
                BaseArmorRating = 10,
                MaxHitPoints = 200,
                HitPoints = 200,
            });
  
            AddItem(new BoneHelm
            {
                Movable = false,
                Name = "Red Bone Helm AR45",
                Hue = 0x0494,
                BaseArmorRating = 45,
                MaxHitPoints = 450,
                HitPoints = 450,
            });
  
  
        }

        public GreaterShadow(Serial serial) : base(serial) {}

  

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