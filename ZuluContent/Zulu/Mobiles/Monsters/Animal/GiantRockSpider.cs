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
    public class GiantRockSpider : BaseCreature
    {
        static GiantRockSpider()
        {
            CreatureProperties.Register<GiantRockSpider>(new CreatureProperties
            {
                // DataElementId = rockspider,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = rockspider,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x186 /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = spiders,
                // Speed = 38 /* Weapon */,
                // TrueColor = 1118,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* spiders */,
                AlwaysMurderer = true,
                Body = 0x1c,
                CorpseNameOverride = "corpse of a giant rock spider",
                CreatureType = CreatureType.Animal,
                DamageMax = 25,
                DamageMin = 9,
                Dex = 110,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HasWebs = true,
                HitPoison = Poison.Regular,
                HitsMax = 185,
                Hue = 1118,
                Int = 50,
                ManaMaxSeed = 40,
                MinTameSkill = 80,
                Name = "a giant rock spider",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 110},
                    {SkillName.MagicResist, 40}
                },
                StamMaxSeed = 70,
                Str = 185,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public GiantRockSpider() : base(CreatureProperties.Get<GiantRockSpider>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Rock Spider Weapon",
                Speed = 38,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantRockSpider(Serial serial) : base(serial)
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