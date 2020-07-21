

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
    public class TrollWarrior : BaseCreature
    {
        static TrollWarrior() => CreatureProperties.Register<TrollWarrior>(new CreatureProperties
        {
            // DataElementId = troll4,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = troll4,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x145 /* Weapon */,
            // hostile = 1,
            // lootgroup = 14,
            // MissSound = 0x23B /* Weapon */,
            // script = killpcs,
            // Speed = 25 /* Weapon */,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysMurderer = true,
            Body = 0x36,
            CorpseNameOverride = "corpse of a troll warrior",
            CreatureType = CreatureType.Troll,
            DamageMax = 20,
            DamageMin = 8,
            Dex = 80,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 225,
            Hue = 33784,
            Int = 40,
            ManaMaxSeed = 30,
            Name = "a troll warrior",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 115 },
                { SkillName.Macing, 115 },
            },
            StamMaxSeed = 60,
            Str = 225,
            VirtualArmor = 10,

        });


        [Constructible]
public TrollWarrior() : base(CreatureProperties.Get<TrollWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll4 Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x145,
                MissSound = 0x23B,
            });


        }

        [Constructible]
public TrollWarrior(Serial serial) : base(serial) {}



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
