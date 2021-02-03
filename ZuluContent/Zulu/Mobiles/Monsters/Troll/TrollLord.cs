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
    public class TrollLord : BaseCreature
    {
        static TrollLord()
        {
            CreatureProperties.Register<TrollLord>(new CreatureProperties
            {
                // DataElementId = troll_lord,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = troll_lord,
                // Graphic = 0x0ec4 /* Weapon */,
                // HitSound = 0x1D0 /* Weapon */,
                // hostile = 1,
                LootTable = "19",
                LootItemChance = 25,
                LootItemLevel = 2,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 25 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0x37,
                CorpseNameOverride = "corpse of a troll lord",
                CreatureType = CreatureType.Troll,
                DamageMax = 58,
                DamageMin = 18,
                Dex = 150,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 350,
                Hue = 33784,
                Int = 65,
                ManaMaxSeed = 0,
                Name = "a troll lord",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 99,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 130},
                    {SkillName.Macing, 150},
                    {SkillName.MagicResist, 80}
                },
                StamMaxSeed = 50,
                Str = 350,
                VirtualArmor = 35
            });
        }


        [Constructible]
        public TrollLord() : base(CreatureProperties.Get<TrollLord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll Lord Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1D0,
                MissSound = 0x239
            });
        }

        [Constructible]
        public TrollLord(Serial serial) : base(serial)
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