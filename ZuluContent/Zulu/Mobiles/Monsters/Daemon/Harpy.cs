

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
    public class Harpy : BaseCreature
    {
        static Harpy() => CreatureProperties.Register<Harpy>(new CreatureProperties
        {
            // DataElementId = harpy,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = harpy,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x195 /* Weapon */,
            // hostile = 1,
            // lootgroup = 29,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 65 /* Weapon */,
            // Swordsmanship = 65,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            BaseSoundID = 402,
            Body = 0x1e,
            CorpseNameOverride = "corpse of a harpy",
            CreatureType = CreatureType.Daemon,
            DamageMax = 18,
            DamageMin = 3,
            Dex = 70,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitsMax = 85,
            Hue = 0,
            Int = 75,
            ManaMaxSeed = 95,
            Name = "a harpy",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 75,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 80 },
                { SkillName.Tactics, 70 },
                { SkillName.MagicResist, 35 },
            },
            StamMaxSeed = 50,
            Str = 85,
            VirtualArmor = 10,

        });


        [Constructible]
public Harpy() : base(CreatureProperties.Get<Harpy>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Harpy Weapon",
                Speed = 65,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x195,
                MissSound = 0x239,
            });


        }

        [Constructible]
public Harpy(Serial serial) : base(serial) {}



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
