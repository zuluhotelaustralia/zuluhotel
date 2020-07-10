

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
    public class MindFlayer : BaseCreature
    {
        static MindFlayer() => CreatureProperties.Register<MindFlayer>(new CreatureProperties
        {
            // CProp_NecroProtection = i8,
            // CProp_PermMagicImmunity = i8,
            // DataElementId = mindflayer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = mindflayer,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitScript = :combat:spellstrikescript /* Weapon */,
            // HitSound = 0x0214 /* Weapon */,
            // hostile = 1,
            // lootgroup = 35,
            // MagicItemChance = 90,
            // MagicItemChance_0 = 65,
            // Magicitemlevel = 6,
            // MagicItemLevel_0 = 6,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcsTeleporter,
            // speech = 35,
            // Speed = 60 /* Weapon */,
            // TrueColor = 2222,
            // virtue = 4,
            AiType = AIType.AI_Melee /* killpcsTeleporter */,
            AlwaysMurderer = true,
            BardImmune = true,
            Body = 0x18,
            ClassLevel = 6,
            ClassSpec = SpecName.Warrior,
            CorpseNameOverride = "corpse of a Mind Flayer",
            CreatureType = CreatureType.Daemon,
            DamageMax = 50,
            DamageMin = 14,
            Dex = 310,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 6,
            HitsMax = 1200,
            Hue = 2222,
            Int = 2750,
            ManaMaxSeed = 2750,
            Name = "a Mind Flayer",
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 100 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Magery, 375 },
                { SkillName.MagicResist, 150 },
                { SkillName.Tactics, 150 },
                { SkillName.Macing, 150 },
                { SkillName.DetectHidden, 200 },
            },
            StamMaxSeed = 50,
            Str = 1200,
            Tamable = false,
            TargetAcquireExhaustion = true,
            VirtualArmor = 60,
            WeaponAbility = new SpellStrike<Server.Spells.Necromancy.ControlUndeadSpell>(),
            WeaponAbilityChance = 1,
  
        });

        [Constructable]
        public MindFlayer() : base(CreatureProperties.Get<MindFlayer>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Speed = 60,
                MaxRange = 6,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x0214,
                MissSound = 0x239,
            });
  
  
        }

        public MindFlayer(Serial serial) : base(serial) {}

  

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