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
    public class GiantFrostSpider : BaseCreature
    {
        static GiantFrostSpider()
        {
            CreatureProperties.Register<GiantFrostSpider>(new CreatureProperties
            {
                // DataElementId = frostspider,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = frostspider,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x186 /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = spiders,
                // Speed = 38 /* Weapon */,
                // TrueColor = 1154,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* spiders */,
                AlwaysMurderer = true,
                Body = 0x1c,
                CorpseNameOverride = "corpse of a giant frost spider",
                CreatureType = CreatureType.Animal,
                DamageMax = 21,
                DamageMin = 9,
                Dex = 125,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitPoison = Poison.Regular,
                HitsMax = 90,
                Hue = 1154,
                Int = 60,
                ManaMaxSeed = 0,
                MinTameSkill = 80,
                Name = "a giant frost spider",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 100,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 70},
                    {SkillName.Macing, 100},
                    {SkillName.MagicResist, 90}
                },
                StamMaxSeed = 70,
                Str = 90,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public GiantFrostSpider() : base(CreatureProperties.Get<GiantFrostSpider>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Frost Spider Weapon",
                Speed = 38,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantFrostSpider(Serial serial) : base(serial)
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