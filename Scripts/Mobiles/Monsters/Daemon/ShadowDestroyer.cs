

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
    public class ShadowDestroyer : BaseCreature
    {
        static ShadowDestroyer() => CreatureProperties.Register<ShadowDestroyer>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_BaseManaRegen = i500,
            // CProp_EarthProtection = i5,
            // CProp_looter = s1,
            // CProp_NecroProtection = i5,
            // CProp_noanimate = i1,
            // CProp_NoReactiveArmour = i1,
            // CProp_PermMagicImmunity = i7,
            // CProp_Permmr = i6,
            // DataElementId = shadowdestroyer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = undeadmenace,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x23D /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 90,
            // MagicItemLevel = 7,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcsTeleporterfast,
            // Speed = 60 /* Weapon */,
            // TrueColor = 1172,
            // virtue = 8,
            ActiveSpeed = 0.150,
            AiType = AIType.AI_Melee /* killpcsTeleporterfast */,
            AlwaysMurderer = true,
            AutoDispel = true,
            BardImmune = true,
            Body = 311,
            CorpseNameOverride = "corpse of Shadow Destroyer",
            CreatureType = CreatureType.Daemon,
            DamageMax = 150,
            DamageMin = 15,
            Dex = 450,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 1200,
            Hue = 1172,
            Int = 500,
            ManaMaxSeed = 500,
            Name = "Shadow Destroyer",
            PassiveSpeed = 0.300,
            PerceptionRange = 10,
            ProvokeSkillOverride = 160,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 150 },
                { SkillName.Magery, 150 },
                { SkillName.MagicResist, 100 },
                { SkillName.Parry, 100 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 500,
            Str = 1550,
            Tamable = false,
            TargetAcquireExhaustion = true,
            VirtualArmor = 75,
  
        });

        [Constructable]
        public ShadowDestroyer() : base(CreatureProperties.Get<ShadowDestroyer>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Undead Menace Weapon",
                Speed = 60,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x23D,
                MissSound = 0x239,
            });
  
            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR50",
                BaseArmorRating = 50,
                MaxHitPoints = 500,
                HitPoints = 500,
            });
  
  
        }

        public ShadowDestroyer(Serial serial) : base(serial) {}

  

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