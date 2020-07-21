

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
    public class EliteTrollWarrior : BaseCreature
    {
        static EliteTrollWarrior() => CreatureProperties.Register<EliteTrollWarrior>(new CreatureProperties
        {
            // DataElementId = troll5,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = troll5,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x233 /* Weapon */,
            // lootgroup = 43,
            // MissSound = 0x23B /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // Swordsmanship = 135,
            // TrueColor = 33784,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0x37,
            CorpseNameOverride = "corpse of an elite troll warrior",
            CreatureType = CreatureType.Troll,
            DamageMax = 20,
            DamageMin = 4,
            Dex = 85,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Troll,
            HitsMax = 235,
            Hue = 33784,
            Int = 65,
            ManaMaxSeed = 55,
            Name = "an elite troll warrior",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 80,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 120 },
            },
            StamMaxSeed = 65,
            Str = 235,
            VirtualArmor = 20,

        });


        [Constructible]
public EliteTrollWarrior() : base(CreatureProperties.Get<EliteTrollWarrior>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Troll5 Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x233,
                MissSound = 0x23B,
            });


        }

        [Constructible]
public EliteTrollWarrior(Serial serial) : base(serial) {}



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
