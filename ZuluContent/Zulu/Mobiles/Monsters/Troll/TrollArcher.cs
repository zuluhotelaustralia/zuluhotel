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
    public class TrollArcher : BaseCreature
    {
        static TrollArcher()
        {
            CreatureProperties.Register<TrollArcher>(new CreatureProperties
            {
                // ammoamount = 35,
                // ammotype = 0xf3f,
                // DataElementId = trollarcher2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = trollarcher2,
                // HitSound = 0x235 /* Weapon */,
                // hostile = 1,
                // lootgroup = 41,
                // missileweapon = archer,
                // MissSound = 0x239 /* Weapon */,
                // script = archerkillpcs,
                // Speed = 35 /* Weapon */,
                // TrueColor = 33784,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Archer /* archerkillpcs */,
                AlwaysMurderer = true,
                Body = 0x36,
                CorpseNameOverride = "corpse of a troll archer",
                CreatureType = CreatureType.Troll,
                DamageMax = 22,
                DamageMin = 10,
                Dex = 105,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                Hides = 4,
                HideType = HideType.Troll,
                HitsMax = 190,
                Hue = 33784,
                Int = 50,
                ManaMaxSeed = 40,
                Name = "a troll archer",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 80,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 60},
                    {SkillName.Tactics, 95},
                    {SkillName.Macing, 100},
                    {SkillName.Archery, 95}
                },
                StamMaxSeed = 60,
                Str = 190,
                VirtualArmor = 20
            });
        }


        [Constructible]
        public TrollArcher() : base(CreatureProperties.Get<TrollArcher>())
        {
            // Add customization here
        }

        [Constructible]
        public TrollArcher(Serial serial) : base(serial)
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