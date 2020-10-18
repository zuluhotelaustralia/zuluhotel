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
    public class MinorDaemon : BaseCreature
    {
        static MinorDaemon()
        {
            CreatureProperties.Register<MinorDaemon>(new CreatureProperties
            {
                // DataElementId = minordaemon,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = minordaemon,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x177 /* Weapon */,
                // hostile = 1,
                // lootgroup = 23,
                // MagicItemChance = 10,
                // MagicItemLevel = 3,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // speech = 54,
                // Speed = 40 /* Weapon */,
                // TrueColor = 0x23,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                BaseSoundID = 357,
                Body = 0x4,
                CorpseNameOverride = "corpse of a minor daemon",
                CreatureType = CreatureType.Daemon,
                DamageMax = 43,
                DamageMin = 8,
                Dex = 180,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 275,
                Hue = 0x23,
                Int = 85,
                ManaMaxSeed = 75,
                Name = "a minor daemon",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 115,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 90},
                    {SkillName.Tactics, 100},
                    {SkillName.Macing, 135}
                },
                StamMaxSeed = 50,
                Str = 275,
                VirtualArmor = 30
            });
        }


        [Constructible]
        public MinorDaemon() : base(CreatureProperties.Get<MinorDaemon>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Minor Daemon Weapon",
                Speed = 40,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x177,
                MissSound = 0x239
            });
        }

        [Constructible]
        public MinorDaemon(Serial serial) : base(serial)
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