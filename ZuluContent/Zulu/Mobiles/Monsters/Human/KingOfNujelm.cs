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
    public class KingOfNujelm : BaseCreature
    {
        static KingOfNujelm()
        {
            CreatureProperties.Register<KingOfNujelm>(new CreatureProperties
            {
                // ammoamount = 300,
                // ammotype = 0xEED,
                // DataElementId = kingnujelm,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = taintedranger,
                // graphic = 0x13B2 /* Weapon */,
                // HitSound = 0xFD /* Weapon */,
                // hostile = 1,
                // lootgroup = 131,
                // MagicItemChance = 50,
                // Magicitemlevel = 6,
                // missileweapon = xbowman,
                // MissSound = 0x239 /* Weapon */,
                // script = explosionkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 1300,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* explosionkillpcs */,
                AlwaysMurderer = true,
                Body = 0x03DF,
                ClassLevel = 4,
                ClassSpec = SpecName.Ranger,
                CorpseNameOverride = "corpse of King of Nujelm",
                CreatureType = CreatureType.Human,
                DamageMax = 57,
                DamageMin = 17,
                Dex = 250,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 7,
                HitsMax = 800,
                Hue = 1300,
                Int = 60,
                ManaMaxSeed = 0,
                Name = "King of Nujelm",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Archery, 130},
                    {SkillName.MagicResist, 90},
                    {SkillName.Hiding, 120},
                    {SkillName.DetectHidden, 100}
                },
                StamMaxSeed = 50,
                Str = 800,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public KingOfNujelm() : base(CreatureProperties.Get<KingOfNujelm>())
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
        public KingOfNujelm(Serial serial) : base(serial)
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