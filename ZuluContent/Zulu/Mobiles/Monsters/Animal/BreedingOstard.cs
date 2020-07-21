

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
    public class BreedingOstard : BaseCreature
    {
        static BreedingOstard() => CreatureProperties.Register<BreedingOstard>(new CreatureProperties
        {
            // CProp_Mated = i0,
            // CProp_nomounting = i1,
            // CProp_noresurrect = i1,
            // CProp_nostabling = i1,
            // CProp_Rating = i10,
            // CProp_Temperament = i5,
            // DataElementId = femalebreedingostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = femalebreedingostard,
            // food = meat,
            // guardignore = 1,
            // script = animal,
            // TrueColor = 50,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Animal /* animal */,
            AlwaysAttackable = true,
            Body = 0xdb,
            CorpseNameOverride = "corpse of breeding ostard",
            CreatureType = CreatureType.Animal,
            Dex = 90,
            Female = true,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 100,
            Hue = 50,
            Int = 35,
            ManaMaxSeed = 0,
            MinTameSkill = 50,
            Name = "breeding ostard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 70,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 40 },
                { SkillName.MagicResist, 40 },
                { SkillName.Tactics, 50 },
                { SkillName.Macing, 60 },
            },
            StamMaxSeed = 50,
            Str = 100,
            Tamable = true,
            VirtualArmor = 20,

        });


        [Constructible]
public BreedingOstard() : base(CreatureProperties.Get<BreedingOstard>())
        {
            // Add customization here


        }

        [Constructible]
public BreedingOstard(Serial serial) : base(serial) {}



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
