using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class TaintedRanger : BaseCreature
    {
        static TaintedRanger()
        {
            CreatureProperties.Register<TaintedRanger>(new CreatureProperties
            {
                // ammoamount = 300,
                // ammotype = 0xEED,
                // CProp_FinalDeath = i1,
                // DataElementId = taintedranger,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = taintedranger,
                // graphic = 0x13B2 /* Weapon */,
                // HitSound = 0xFD /* Weapon */,
                // hostile = 1,
                LootTable = "131",
                LootItemChance = 50,
                LootItemLevel = 4,
                // missileweapon = archer,
                // MissSound = 0x239 /* Weapon */,
                // script = explosionkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1157,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* explosionkillpcs */,
                AlwaysMurderer = true,
                Body = 0x1B3,
                ClassLevel = 1,
                ClassType = ZuluClassType.Ranger,
                CorpseNameOverride = "corpse of <random> the Tainted Ranger",
                CreatureType = CreatureType.Human,
                DamageMax = 57,
                DamageMin = 17,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 7,
                HitsMax = 300,
                Hue = 1157,
                Int = 60,
                ManaMaxSeed = 0,
                Name = "<random> the Tainted Ranger",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                RiseCreatureDelay = TimeSpan.FromSeconds(2),
                RiseCreatureType = typeof(EvisceratedCarcass),
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Archery, 130},
                    {SkillName.MagicResist, 90},
                    {SkillName.Hiding, 120},
                    {SkillName.DetectHidden, 100}
                },
                StamMaxSeed = 150,
                Str = 300,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public TaintedRanger() : base(CreatureProperties.Get<TaintedRanger>())
        {
            // Add customization here

            AddItem(new Bow
            {
                Movable = false,
                Name = "Tainted Ranger Weapon",
                Hue = 0x0493,
                Speed = 35,
                Skill = SkillName.Archery,
                EffectId = 0x37C3,
                Animation = (WeaponAnimation) 0x12,
                MissSound = 0x239,
                HitSound = 0xFD,
                MaxHitPoints = 300,
                HitPoints = 300,
                MaxRange = 7
            });

            AddItem(new LeatherGloves
            {
                Movable = false,
                Name = "a pair of dark leather gloves",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300
            });

            AddItem(new Boots
            {
                Movable = false,
                Name = "a pair of black leather boots",
                Hue = 1
            });

            AddItem(new LongPants
            {
                Movable = false,
                Name = "a pair of black leather pants",
                Hue = 1157
            });

            AddItem(new RangerChest
            {
                Movable = false,
                Name = "a Tunic of Woven Shadows",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300
            });

            AddItem(new BoneArms
            {
                Movable = false,
                Name = "a Bracer of Woven Shadows",
                Hue = 1302,
                BaseArmorRating = 1,
                MaxHitPoints = 300,
                HitPoints = 300
            });
        }

        [Constructible]
        public TaintedRanger(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}