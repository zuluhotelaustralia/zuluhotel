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
    public class GreenSlime : BaseCreature
    {
        static GreenSlime()
        {
            CreatureProperties.Register<GreenSlime>(new CreatureProperties
            {
                // DataElementId = greenslime,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = greenslime,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x1CB /* Weapon */,
                // MissSound = 0x239 /* Weapon */,
                // script = slime,
                // Speed = 25 /* Weapon */,
                // TrueColor = 0x07D1,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* slime */,
                AlwaysAttackable = true,
                Body = 0x33,
                CorpseNameOverride = "corpse of a green slime",
                CreatureType = CreatureType.Slime,
                DamageMax = 30,
                DamageMin = 2,
                Dex = 50,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 140,
                Hue = 0x07D1,
                Int = 15,
                ManaMaxSeed = 0,
                MinTameSkill = 40,
                Name = "a green slime",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 19,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 80},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 100}
                },
                StamMaxSeed = 50,
                Str = 140,
                Tamable = true,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public GreenSlime() : base(CreatureProperties.Get<GreenSlime>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Green Slime Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1CB,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GreenSlime(Serial serial) : base(serial)
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