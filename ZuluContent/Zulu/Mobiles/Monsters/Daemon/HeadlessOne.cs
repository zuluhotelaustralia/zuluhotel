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
    public class HeadlessOne : BaseCreature
    {
        static HeadlessOne()
        {
            CreatureProperties.Register<HeadlessOne>(new CreatureProperties
            {
                // DataElementId = headless,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = headless,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x19A /* Weapon */,
                // hostile = 1,
                // lootgroup = 3,
                // MissSound = 0x19C /* Weapon */,
                // noloot = 1,
                // script = killpcs,
                // Speed = 45 /* Weapon */,
                // Swordsmanship = 85,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 407,
                Body = 0x1f,
                CorpseNameOverride = "corpse of a headless one",
                CreatureType = CreatureType.Daemon,
                DamageMax = 21,
                DamageMin = 3,
                Dex = 60,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 60,
                Hue = 0,
                Int = 25,
                ManaMaxSeed = 15,
                Name = "a headless one",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 40,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 40},
                    {SkillName.Tactics, 50},
                    {SkillName.MagicResist, 30}
                },
                StamMaxSeed = 50,
                Str = 60,
                VirtualArmor = 5
            });
        }


        [Constructible]
        public HeadlessOne() : base(CreatureProperties.Get<HeadlessOne>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Headless Weapon",
                Speed = 45,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x19A,
                MissSound = 0x19C
            });
        }

        [Constructible]
        public HeadlessOne(Serial serial) : base(serial)
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