

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
    public class Ratlord : BaseCreature
    {
        static Ratlord() => CreatureProperties.Register<Ratlord>(new CreatureProperties
        {
            // DataElementId = ratman_lord,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = ratman_lord,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x1B8 /* Weapon */,
            // hostile = 1,
            // lootgroup = 27,
            // MagicItemChance = 50,
            // MagicItemLevel = 1,
            // MissSound = 0x239 /* Weapon */,
            // script = killpcs,
            // Speed = 55 /* Weapon */,
            // Swordsmanship = 105,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x2d,
            CorpseNameOverride = "corpse of <random> the Ratlord",
            CreatureType = CreatureType.Ratkin,
            DamageMax = 64,
            DamageMin = 8,
            Dex = 450,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 5,
            HideType = HideType.Rat,
            HitsMax = 300,
            Hue = 0,
            Int = 65,
            ManaMaxSeed = 0,
            Name = "<random> the Ratlord",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 95,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Tactics, 100 },
                { SkillName.MagicResist, 75 },
            },
            StamMaxSeed = 50,
            Str = 300,
            VirtualArmor = 30,

        });


        [Constructible]
public Ratlord() : base(CreatureProperties.Get<Ratlord>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Ratman Lord Weapon",
                Speed = 55,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x1B8,
                MissSound = 0x239,
            });


        }

        [Constructible]
public Ratlord(Serial serial) : base(serial) {}



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
