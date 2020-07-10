

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
    public class MediumHoardeDemon : BaseCreature
    {
        static MediumHoardeDemon() => CreatureProperties.Register<MediumHoardeDemon>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i3,
            // CProp_NecroProtection = i3,
            // CProp_PermMagicImmunity = i3,
            // DataElementId = mediumhoardedemon,
            // DataElementType = NpcTemplate,
            // Equip = behemoth,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 37,
            // MagicItemChance = 10,
            // MagicItemLevel = 5,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 50 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 795,
            CorpseNameOverride = "corpse of a Medium Hoarde Demon",
            CreatureType = CreatureType.Daemon,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 200,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 1200,
            Hue = 0,
            Int = 200,
            ManaMaxSeed = 200,
            Name = "a Medium Hoarde Demon",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 75 },
                { ElementalType.Fire, 75 },
                { ElementalType.Energy, 75 },
                { ElementalType.Cold, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 175 },
                { SkillName.MagicResist, 60 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 200,
            Str = 1000,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public MediumHoardeDemon() : base(CreatureProperties.Get<MediumHoardeDemon>())
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

        public MediumHoardeDemon(Serial serial) : base(serial) {}

  

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