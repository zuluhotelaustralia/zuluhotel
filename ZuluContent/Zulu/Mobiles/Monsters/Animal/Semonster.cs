

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
    public class Semonster : BaseCreature
    {
        static Semonster() => CreatureProperties.Register<Semonster>(new CreatureProperties
        {
            // DataElementId = seamonster,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = seamonster,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1BF /* Weapon */,
            // lootgroup = 50,
            // MagicItemChance = 1,
            // MissSound = 0x1C0 /* Weapon */,
            // script = killpcs,
            // speech = 6,
            // Speed = 25 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0x5f,
            CanSwim = true,
            CorpseNameOverride = "corpse of a sea monster",
            CreatureType = CreatureType.Animal,
            DamageMax = 45,
            DamageMin = 9,
            Dex = 160,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 250,
            Hue = 33784,
            Int = 35,
            ManaMaxSeed = 0,
            Name = "a sea monster",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 60,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 70 },
                { SkillName.Tactics, 100 },
                { SkillName.Macing, 120 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 50,
            Str = 250,
            VirtualArmor = 30,

        });


        [Constructible]
public Semonster() : base(CreatureProperties.Get<Semonster>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Sea Monster Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1BF,
                MissSound = 0x1C0,
            });


        }

        [Constructible]
public Semonster(Serial serial) : base(serial) {}



        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
