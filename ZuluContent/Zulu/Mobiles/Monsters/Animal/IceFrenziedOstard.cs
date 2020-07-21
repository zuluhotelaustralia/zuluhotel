

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
    public class IceFrenziedOstard : BaseCreature
    {
        static IceFrenziedOstard() => CreatureProperties.Register<IceFrenziedOstard>(new CreatureProperties
        {
            // DataElementId = icefrenziedostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = icefrenziedostard,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x254 /* Weapon */,
            // MissSound = 0x256 /* Weapon */,
            // script = killpcs,
            // Speed = 35 /* Weapon */,
            // TrueColor = 1152,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0xda,
            CorpseNameOverride = "corpse of an ice frenzied ostard",
            CreatureType = CreatureType.Animal,
            DamageMax = 48,
            DamageMin = 13,
            Dex = 400,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Ostard,
            HitsMax = 225,
            Hue = 1152,
            Int = 135,
            ManaMaxSeed = 125,
            MinTameSkill = 105,
            Name = "an ice frenzied ostard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 130,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 110 },
                { SkillName.MagicResist, 100 },
                { SkillName.Tactics, 110 },
                { SkillName.Macing, 160 },
            },
            StamMaxSeed = 175,
            Str = 225,
            Tamable = true,
            VirtualArmor = 35,

        });


        [Constructible]
public IceFrenziedOstard() : base(CreatureProperties.Get<IceFrenziedOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Tropical Frenzied Ostard Weapon",
                Speed = 35,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256,
            });


        }

        [Constructible]
public IceFrenziedOstard(Serial serial) : base(serial) {}



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
