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
    public class Cyclops : BaseCreature
    {
        static Cyclops()
        {
            CreatureProperties.Register<Cyclops>(new CreatureProperties
            {
                // DataElementId = cyclops,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = cyclops,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x25F /* Weapon */,
                // hostile = 1,
                LootTable = "40",
                // Macefighting = 110,
                LootItemChance = 50,
                LootItemLevel = 3,
                // MissSound = 0x169 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // TrueColor = 33784,
                // virtue = 2,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x4b,
                CorpseNameOverride = "corpse of a cyclops",
                CreatureType = CreatureType.Giantkin,
                DamageMax = 75,
                DamageMin = 15,
                Dex = 130,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 350,
                Hue = 33784,
                Int = 65,
                ManaMaxSeed = 0,
                Name = "a cyclops",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 99,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 80},
                    {SkillName.Tactics, 110},
                    {SkillName.MagicResist, 85}
                },
                StamMaxSeed = 50,
                Str = 350,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public Cyclops() : base(CreatureProperties.Get<Cyclops>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Cyclops Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x25F,
                MissSound = 0x169
            });
        }

        [Constructible]
        public Cyclops(Serial serial) : base(serial)
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