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
    public class Bandit : BaseCreature
    {
        static Bandit()
        {
            CreatureProperties.Register<Bandit>(new CreatureProperties
            {
                // DataElementId = bandit,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = bandit,
                // Graphic = 0x1440 /* Weapon */,
                // HitSound = 0x168 /* Weapon */,
                // lootgroup = 47,
                // MagicItemChance = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // Swordsmanship = 30,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                Body = 0x190,
                CorpseNameOverride = "corpse of a bandit",
                CreatureType = CreatureType.Human,
                DamageMax = 50,
                DamageMin = 5,
                Dex = 30,
                Female = false,
                FightMode = FightMode.None,
                FightRange = 1,
                HitsMax = 30,
                Hue = 0,
                Int = 20,
                ManaMaxSeed = 10,
                Name = "a bandit",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 50,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 30},
                    {SkillName.Fencing, 30}
                },
                StamMaxSeed = 30,
                Str = 30
            });
        }


        [Constructible]
        public Bandit() : base(CreatureProperties.Get<Bandit>())
        {
            // Add customization here

            AddItem(new Cutlass
            {
                Movable = false,
                Name = "Brigand1 Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x168,
                MissSound = 0x239
            });
        }

        [Constructible]
        public Bandit(Serial serial) : base(serial)
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