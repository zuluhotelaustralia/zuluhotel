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
    public class GiantSandWorm : BaseCreature
    {
        static GiantSandWorm()
        {
            CreatureProperties.Register<GiantSandWorm>(new CreatureProperties
            {
                // DataElementId = sandworm,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = sandworm,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1C0 /* Weapon */,
                LootTable = "59",
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 351,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0x96,
                CorpseNameOverride = "corpse of a Giant Sand Worm",
                CreatureType = CreatureType.Animal,
                DamageMax = 55,
                DamageMin = 10,
                Dex = 210,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                Hides = 5,
                HideType = HideType.Serpent,
                HitsMax = 300,
                Hue = 351,
                Int = 30,
                ManaMaxSeed = 56,
                MinTameSkill = 130,
                Name = "a Giant Sand Worm",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 110,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Hiding, 200},
                    {SkillName.Stealth, 200},
                    {SkillName.Tactics, 80},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 50,
                Str = 300,
                Tamable = true,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public GiantSandWorm() : base(CreatureProperties.Get<GiantSandWorm>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Sandworm Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1C0,
                MissSound = 0x239
            });
        }

        [Constructible]
        public GiantSandWorm(Serial serial) : base(serial)
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