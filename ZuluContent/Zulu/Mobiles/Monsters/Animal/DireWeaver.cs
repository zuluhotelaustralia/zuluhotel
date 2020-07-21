

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
    public class DireWeaver : BaseCreature
    {
        static DireWeaver() => CreatureProperties.Register<DireWeaver>(new CreatureProperties
        {
            // DataElementId = direweaver,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = direweaver,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // Hitscript = :combat:poisonhit /* Weapon */,
            // HitSound = 0x186 /* Weapon */,
            // hostile = 1,
            // MissSound = 0x187 /* Weapon */,
            // script = spiders,
            // Speed = 40 /* Weapon */,
            // Swordsmanship = 135,
            // TrueColor = 1409,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* spiders */,
            AlwaysMurderer = true,
            Body = 0x1c,
            CorpseNameOverride = "corpse of a Dire Weaver",
            CreatureType = CreatureType.Animal,
            DamageMax = 75,
            DamageMin = 25,
            Dex = 300,
            Female = false,
            FightMode = FightMode.Aggressor,
            FightRange = 1,
            HitPoison = Poison.Greater,
            HitsMax = 450,
            Hue = 1409,
            Int = 35,
            ManaMaxSeed = 25,
            Name = "a Dire Weaver",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            Resistances = new Dictionary<ElementalType, CreatureProp>
            {
                { ElementalType.Poison, 75 },
            },
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 85 },
                { SkillName.Poisoning, 70 },
                { SkillName.MagicResist, 80 },
                { SkillName.Tactics, 120 },
            },
            StamMaxSeed = 50,
            Str = 450,
            VirtualArmor = 40,

        });


        [Constructible]
public DireWeaver() : base(CreatureProperties.Get<DireWeaver>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Dire Weaver Weapon",
                Speed = 40,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x186,
                MissSound = 0x187,
            });

            AddItem(new HeaterShield
            {
                Movable = false,
                Name = "Shield AR20",
                BaseArmorRating = 20,
                MaxHitPoints = 400,
                HitPoints = 400,
            });


        }

        [Constructible]
public DireWeaver(Serial serial) : base(serial) {}



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
