

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
    public class WarDog : BaseCreature
    {
        static WarDog() => CreatureProperties.Register<WarDog>(new CreatureProperties
        {
            // DataElementId = wardog,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = wardog,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // guardignore = 1,
            // HitSound = 0x88 /* Weapon */,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 30 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 133,
            Body = 0xd9,
            CorpseNameOverride = "corpse of a war dog",
            CreatureType = CreatureType.Animal,
            DamageMax = 12,
            DamageMin = 3,
            Dex = 75,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 80,
            Hue = 33784,
            Int = 35,
            ManaMaxSeed = 0,
            MinTameSkill = 50,
            Name = "a war dog",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 60 },
                { SkillName.Macing, 75 },
                { SkillName.MagicResist, 40 },
            },
            StamMaxSeed = 50,
            Str = 80,
            Tamable = true,
            VirtualArmor = 15,

        });


        [Constructible]
public WarDog() : base(CreatureProperties.Get<WarDog>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Wardog Weapon",
                Speed = 30,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x88,
                MissSound = 0x239,
            });


        }

        [Constructible]
public WarDog(Serial serial) : base(serial) {}



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
