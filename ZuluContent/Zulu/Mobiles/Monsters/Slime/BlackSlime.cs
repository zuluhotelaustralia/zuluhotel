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
    public class BlackSlime : BaseCreature
    {
        static BlackSlime()
        {
            CreatureProperties.Register<BlackSlime>(new CreatureProperties
            {
                // DataElementId = blackslime,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = blackslime,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x1CB /* Weapon */,
                LootTable = "58",
                // MissSound = 0x239 /* Weapon */,
                // script = slime,
                // Speed = 25 /* Weapon */,
                // TrueColor = 0x0455,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* slime */,
                AlwaysAttackable = true,
                Body = 0x33,
                CorpseNameOverride = "corpse of a black slime",
                CreatureType = CreatureType.Slime,
                DamageMax = 10,
                DamageMin = 2,
                Dex = 50,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 230,
                Hue = 0x0455,
                Int = 15,
                ManaMaxSeed = 5,
                MinTameSkill = 75,
                Name = "a black slime",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 19,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 100},
                    {SkillName.MagicResist, 90}
                },
                StamMaxSeed = 50,
                Str = 230,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public BlackSlime() : base(CreatureProperties.Get<BlackSlime>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Black Slime Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1CB,
                MissSound = 0x239
            });
        }

        [Constructible]
        public BlackSlime(Serial serial) : base(serial)
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