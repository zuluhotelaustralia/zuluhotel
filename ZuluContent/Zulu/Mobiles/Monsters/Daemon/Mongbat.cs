

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
    public class Mongbat : BaseCreature
    {
        static Mongbat() => CreatureProperties.Register<Mongbat>(new CreatureProperties
        {
            // DataElementId = mongbat,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = mongbat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1A9 /* Weapon */,
            // hostile = 1,
            // lootgroup = 33,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // Swordsmanship = 40,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 422,
            Body = 0x27,
            CorpseNameOverride = "corpse of a mongbat",
            CreatureType = CreatureType.Daemon,
            DamageMax = 6,
            DamageMin = 2,
            Dex = 60,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 20,
            Hue = 0,
            Int = 35,
            ManaMaxSeed = 0,
            MinTameSkill = 25,
            Name = "a mongbat",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 55,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 55 },
                { SkillName.Tactics, 50 },
                { SkillName.MagicResist, 10 },
            },
            StamMaxSeed = 50,
            Str = 20,
            Tamable = true,
            VirtualArmor = 5,

        });


        [Constructible]
public Mongbat() : base(CreatureProperties.Get<Mongbat>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Mongbat Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1A9,
                MissSound = 0x239,
            });


        }

        [Constructible]
public Mongbat(Serial serial) : base(serial) {}



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
