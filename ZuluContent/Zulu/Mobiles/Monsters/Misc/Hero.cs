using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using static Server.Mobiles.CreatureProp;
using Server.Engines.Magic;
using Server.Engines.Harvest;
using Scripts.Zulu.Engines.Classes;

namespace Server.Mobiles
{
    public class Hero : BaseCreature
    {
        static Hero()
        {
            CreatureProperties.Register<Hero>(new CreatureProperties
            {
                // CProp_nomountatdeath = i1,
                // CProp_NoReactiveArmour = i1,
                // DataElementId = thehero,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = thehero,
                // Graphic = 0x0F60 /* Weapon */,
                // guardignore = 1,
                // HitSound = 0x23C /* Weapon */,
                // hostile = 1,
                // lootgroup = 59,
                // MissSound = 0x23A /* Weapon */,
                // mount = 0x3e9f 1182,
                // script = killpcsTeleporter,
                // Speed = 67 /* Weapon */,
                // Swordsmanship = 100,
                // TrueColor = 0,
                AiType = AIType.AI_Melee /* killpcsTeleporter */,
                AlwaysMurderer = true,
                BardImmune = true,
                Body = 0x190,
                ClassLevel = 6,
                ClassSpec = SpecName.Warrior,
                CorpseNameOverride = "corpse of <random> the Hero",
                DamageMax = 45,
                DamageMin = 33,
                Dex = 300,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 2000,
                Hue = 1002,
                Int = 210,
                ManaMaxSeed = 200,
                Name = "<random> the Hero",
                PerceptionRange = 10,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Tactics, 200},
                    {SkillName.MagicResist, 200}
                },
                StamMaxSeed = 200,
                Str = 300,
                TargetAcquireExhaustion = true,
                VirtualArmor = 80
            });
        }


        [Constructible]
        public Hero() : base(CreatureProperties.Get<Hero>())
        {
            // Add customization here

            AddItem(new Longsword
            {
                Movable = false,
                Name = "The Hero Longsword",
                Hue = 1182,
                Speed = 67,
                Skill = SkillName.Swords,
                MaxHitPoints = 70,
                HitPoints = 70,
                HitSound = 0x23C,
                MissSound = 0x23A,
                Animation = (WeaponAnimation) 0x0009
            });
        }

        [Constructible]
        public Hero(Serial serial) : base(serial)
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