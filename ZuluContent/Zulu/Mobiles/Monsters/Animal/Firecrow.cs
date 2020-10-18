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
    public class Firecrow : BaseCreature
    {
        static Firecrow()
        {
            CreatureProperties.Register<Firecrow>(new CreatureProperties
            {
                // DataElementId = firecrow,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = firecrow,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x7F /* Weapon */,
                // MissSound = 0x7E /* Weapon */,
                // script = firebreather,
                // Speed = 20 /* Weapon */,
                // TrueColor = 0x0455,
                AiType = AIType.AI_Melee /* firebreather */,
                AlwaysAttackable = true,
                Body = 0x06,
                CorpseNameOverride = "corpse of a firecrow",
                CreatureType = CreatureType.Animal,
                DamageMax = 6,
                DamageMin = 1,
                Dex = 60,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HasBreath = true,
                HitsMax = 18,
                Hue = 0x0455,
                Int = 15,
                ManaMaxSeed = 0,
                MinTameSkill = 25,
                Name = "a firecrow",
                PerceptionRange = 10,
                ProvokeSkillOverride = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 20},
                    {SkillName.Tactics, 50},
                    {SkillName.Macing, 10}
                },
                StamMaxSeed = 50,
                Str = 18,
                Tamable = true,
                VirtualArmor = 10
            });
        }


        [Constructible]
        public Firecrow() : base(CreatureProperties.Get<Firecrow>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Firecrow Weapon",
                Speed = 20,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x7F,
                MissSound = 0x7E
            });
        }

        [Constructible]
        public Firecrow(Serial serial) : base(serial)
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