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
    public class Troll : BaseCreature
    {
        static Troll()
        {
            CreatureProperties.Register<Troll>(new CreatureProperties
            {
                // DataElementId = troll3,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = troll3,
                // Equip_0 = warmace,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1D0 /* Weapon */,
                // hostile = 1,
                LootTable = "14",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // Swordsmanship = 60,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x37,
                CorpseNameOverride = "corpse of a troll",
                CreatureType = CreatureType.Troll,
                DamageMax = 35,
                DamageMin = 10,
                Dex = 70,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 185,
                Hue = 33784,
                Int = 35,
                ManaMaxSeed = 0,
                Name = "a troll",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 90,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 60},
                    {SkillName.Macing, 90},
                    {SkillName.Tactics, 90},
                    {SkillName.MagicResist, 60}
                },
                StamMaxSeed = 50,
                Str = 185,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public Troll() : base(CreatureProperties.Get<Troll>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll3 Weapon",
                Speed = 25,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D0,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Troll(Serial serial) : base(serial)
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