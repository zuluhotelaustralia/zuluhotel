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
    public class FrenziedPoisonOstard : BaseCreature
    {
        static FrenziedPoisonOstard()
        {
            CreatureProperties.Register<FrenziedPoisonOstard>(new CreatureProperties
            {
                // CProp_noloot = 1,
                // DataElementId = frenziedpoisonostard,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // Equip = scorp,
                // food = meat,
                // Graphic = 0x0ec4 /* Weapon */,
                // guardignore = 1,
                // Hitscript = :combat:poisonhit /* Weapon */,
                // HitSound = 0x190 /* Weapon */,
                // hostile = 1,
                // MissSound = 0x239 /* Weapon */,
                // script = killpcs,
                // Speed = 30 /* Weapon */,
                // TrueColor = 264,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysMurderer = true,
                Body = 0xda,
                CorpseNameOverride = "corpse of a frenzied poison ostard",
                CreatureType = CreatureType.Animal,
                DamageMax = 24,
                DamageMin = 3,
                Dex = 400,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitPoison = Poison.Regular,
                HitsMax = 225,
                Hue = 264,
                Int = 185,
                ManaMaxSeed = 175,
                MinTameSkill = 110,
                Name = "a frenzied poison ostard",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 130,
                Resistances = new Dictionary<ElementalType, CreatureProp>
                {
                    {ElementalType.Poison, 3}
                },
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.Parry, 110},
                    {SkillName.MagicResist, 110},
                    {SkillName.Tactics, 110},
                    {SkillName.Macing, 160},
                    {SkillName.Magery, 225},
                    {SkillName.Poisoning, 110}
                },
                StamMaxSeed = 125,
                Str = 225,
                Tamable = true,
                VirtualArmor = 15
            });
        }


        [Constructible]
        public FrenziedPoisonOstard() : base(CreatureProperties.Get<FrenziedPoisonOstard>())
        {
            // Add customization here

            AddItem(new SkinningKnife
            {
                Movable = false,
                Name = "Scorp Weapon",
                Speed = 30,
                Skill = SkillName.Swords,
                MaxHitPoints = 250,
                HitPoints = 250,
                HitSound = 0x190,
                MissSound = 0x239
            });
        }

        [Constructible]
        public FrenziedPoisonOstard(Serial serial) : base(serial)
        {
        }


        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int) 0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}