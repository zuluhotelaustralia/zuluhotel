

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
    public class BehemothLord : BaseCreature
    {
        static BehemothLord() => CreatureProperties.Register<BehemothLord>(new CreatureProperties
        {
            // CProp_BaseHpRegen = i1000,
            // CProp_EarthProtection = i8,
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // DataElementId = hiddenbehemothlord,
            // DataElementType = NpcTemplate,
            // Equip = behemoth,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:banishscript /* Weapon */,
            // HitSound = 0x16D /* Weapon */,
            // hostile = 1,
            // lootgroup = 9,
            // MagicItemChance = 80,
            // MagicItemLevel = 7,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcsTeleporter,
            // Speed = 50 /* Weapon */,
            // TrueColor = 54,
            AiType = AIType.AI_Melee /* killpcsTeleporter */,
            AlwaysMurderer = true,
            AutoDispel = true,
            Body = 0x0e,
            CorpseNameOverride = "corpse of a Behemoth Lord",
            CreatureType = CreatureType.Elemental,
            DamageMax = 60,
            DamageMin = 10,
            Dex = 400,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 2250,
            Hue = 54,
            Int = 55,
            ManaMaxSeed = 0,
            Name = "a Behemoth Lord",
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Fire, 100 },
                { ElementalType.Energy, 100 },
                { ElementalType.Cold, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 250 },
                { SkillName.Macing, 150 },
                { SkillName.MagicResist, 60 },
                { SkillName.DetectHidden, 200 },
                { SkillName.Hiding, 200 },
            },
            StamMaxSeed = 200,
            Str = 2950,
            TargetAcquireExhaustion = true,
            VirtualArmor = 45,
  
        });

        [Constructable]
        public BehemothLord() : base(CreatureProperties.Get<BehemothLord>())
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

        public BehemothLord(Serial serial) : base(serial) {}

  

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