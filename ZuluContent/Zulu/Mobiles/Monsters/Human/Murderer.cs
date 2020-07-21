

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
    public class Murderer : BaseCreature
    {
        static Murderer() => CreatureProperties.Register<Murderer>(new CreatureProperties
        {
            // DataElementId = murderer,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = murderer,
            // Graphic = 0x0f51 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x23C /* Weapon */,
            // lootgroup = 47,
            // MagicItemChance = 1,
            // MissSound = 0x23A /* Weapon */,
            // Parrying = 80,
            // script = killpcs,
            // Speed = 50 /* Weapon */,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0x190,
            CorpseNameOverride = "corpse of a murderer",
            CreatureType = CreatureType.Human,
            DamageMax = 29,
            DamageMin = 23,
            Dex = 200,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitPoison = Poison.Regular,
            HitsMax = 160,
            Hue = 0,
            Int = 105,
            ManaMaxSeed = 95,
            Name = "a murderer",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 104,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 85 },
                { SkillName.Fencing, 100 },
                { SkillName.Hiding, 100 },
                { SkillName.MagicResist, 50 },
            },
            StamMaxSeed = 50,
            Str = 650,

        });


        [Constructible]
public Murderer() : base(CreatureProperties.Get<Murderer>())
        {
            // Add customization here

            AddItem(new ShortHair(Race.RandomHairHue())
            {
                Movable = false,
                Hue = 0x1,
            });

            AddItem(new Dagger
            {
                Movable = false,
                Name = "Murderer's Dagger",
                Speed = 50,
                Skill = SkillName.Fencing,
                MaxHitPoints = 70,
                HitPoints = 70,
                HitSound = 0x23C,
                MissSound = 0x23A,
                Animation = (WeaponAnimation)0x000a,
            });


        }

        [Constructible]
public Murderer(Serial serial) : base(serial) {}



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
