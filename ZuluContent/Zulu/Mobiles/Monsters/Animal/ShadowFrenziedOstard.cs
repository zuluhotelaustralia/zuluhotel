

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
    public class ShadowFrenziedOstard : BaseCreature
    {
        static ShadowFrenziedOstard() => CreatureProperties.Register<ShadowFrenziedOstard>(new CreatureProperties
        {
            // DataElementId = shadowfrenziedostard,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // Equip = shadowfrenziedostard,
            // food = meat,
            // Graphic = 0x0ec4 /* Weapon */,
            // HitSound = 0x254 /* Weapon */,
            // MissSound = 0x256 /* Weapon */,
            // script = killpcs,
            // Speed = 25 /* Weapon */,
            // TrueColor = 1904,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Melee /* killpcs */,
            AlwaysAttackable = true,
            Body = 0xda,
            CorpseNameOverride = "corpse of a shadow frenzied ostard",
            CreatureType = CreatureType.Animal,
            DamageMax = 45,
            DamageMin = 10,
            Dex = 320,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            Hides = 4,
            HideType = HideType.Ostard,
            HitsMax = 210,
            Hue = 1904,
            Int = 170,
            ManaMaxSeed = 160,
            MinTameSkill = 85,
            Name = "a shadow frenzied ostard",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            ProvokeSkillOverride = 120,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.Parry, 100 },
                { SkillName.MagicResist, 90 },
                { SkillName.Tactics, 120 },
                { SkillName.Macing, 160 },
            },
            StamMaxSeed = 160,
            Str = 210,
            Tamable = true,
            VirtualArmor = 20,

        });


        [Constructible]
public ShadowFrenziedOstard() : base(CreatureProperties.Get<ShadowFrenziedOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Golden Frenzied Ostard Weapon",
                Speed = 25,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x254,
                MissSound = 0x256,
            });


        }

        [Constructible]
public ShadowFrenziedOstard(Serial serial) : base(serial) {}



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
