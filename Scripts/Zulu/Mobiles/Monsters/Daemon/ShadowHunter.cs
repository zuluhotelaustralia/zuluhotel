

using System;
using System.Collections.Generic;
using Server;

using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using RunZH.Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class ShadowHunter : BaseCreature
    {
        static ShadowHunter() => CreatureProperties.Register<ShadowHunter>(new CreatureProperties
        {
            // ammoamount = 300,
            // ammotype = 0xEED,
            // DataElementId = shadowhunter,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = taintedranger,
            // graphic = 0x13B2 /* Weapon */,
            // HitSound = 0xFD /* Weapon */,
            // hostile = 1,
            // lootgroup = 131,
            // MagicItemChance = 50,
            // Magicitemlevel = 5,
            // missileweapon = archer,
            // MissSound = 0x239 /* Weapon */,
            // script = explosionkillpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 1157,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Archer /* explosionkillpcs */,
            AlwaysMurderer = true,
            Body = 0x190,
            ClassLevel = 4,
            ClassSpec = SpecName.Ranger,
            CorpseNameOverride = "corpse of <random> the Shadow Hunter",
            CreatureType = CreatureType.Daemon,
            DamageMax = 57,
            DamageMin = 17,
            Dex = 350,
            Female = false,
            FightMode = FightMode.Closest,
            FightRange = 7,
            HitsMax = 1000,
            Hue = 1157,
            Int = 60,
            ManaMaxSeed = 0,
            Name = "<random> the Shadow Hunter",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.Archery, 130 },
                { SkillName.MagicResist, 90 },
                { SkillName.Hiding, 120 },
                { SkillName.DetectHidden, 100 },
            },
            StamMaxSeed = 150,
            Str = 350,
            VirtualArmor = 40,
  
        });

        [Constructable]
        public ShadowHunter() : base(CreatureProperties.Get<ShadowHunter>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Name = "Tainted Ranger Weapon",
                Hue = 0x0493,
                Speed = 35,
                Skill = SkillName.Archery,
                EffectID = 0x37C3,
                Animation = (WeaponAnimation)0x12,
                MissSound = 0x239,
                HitSound = 0xFD,
                MaxHitPoints = 300,
                HitPoints = 300,
                MaxRange = 7,
            });
  
            AddItem(new LeatherGloves
            {
                Movable = false,
                Name = "a pair of dark leather gloves",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300,
            });
  
            AddItem(new Boots
            {
                Movable = false,
                Name = "a pair of black leather boots",
                Hue = 1,
            });
  
            AddItem(new LongPants
            {
                Movable = false,
                Name = "a pair of black leather pants",
                Hue = 1157,
            });
  
            AddItem(new RangerChest
            {
                Movable = false,
                Name = "a Tunic of Woven Shadows",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300,
            });
  
            AddItem(new BoneArms
            {
                Movable = false,
                Name = "a Bracer of Woven Shadows",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300,
            });
  
  
        }

        public ShadowHunter(Serial serial) : base(serial) {}

  

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