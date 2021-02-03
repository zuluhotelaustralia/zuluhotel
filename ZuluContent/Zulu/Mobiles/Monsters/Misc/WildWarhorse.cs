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
    public class WildWarhorse : BaseCreature
    {
        static WildWarhorse()
        {
            CreatureProperties.Register<WildWarhorse>(new CreatureProperties
            {
                // AccuracyAdjustment = 90,
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0xAB,
                // AttackMissSound = 0x239,
                // AttackSpeed = 55,
                // corpseamt = 7,
                // corpseitm = rawrib,
                // CProp_noloot = i1,
                // damagedsound = 0xac,
                // DataElementId = warhorse2,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // flamestrength = 50,
                // food = meat,
                // herdskill = 105,
                // hostile = 1,
                // idlesound1 = 0xa9,
                // idlesound2 = 0xaa,
                LootTable = "26",
                // MagicAdjustment = 80,
                LootItemChance = 100,
                // nofear = 1,
                // orneriness = 5,
                // script = killpcs,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Melee /* killpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 168,
                Body = 119,
                CorpseNameOverride = "corpse of a wild warhorse",
                DamageMax = 16,
                DamageMin = 2,
                Dex = 500,
                Fame = 1,
                Female = false,
                FightMode = FightMode.Aggressor,
                FightRange = 1,
                HitsMax = 500,
                Hue = 0,
                Int = 500,
                Karma = 1,
                ManaMaxSeed = 500,
                MinTameSkill = 110,
                Name = "a wild warhorse",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 95,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 120},
                    {SkillName.EvalInt, 100},
                    {SkillName.Tactics, 120},
                    {SkillName.Wrestling, 110},
                    {SkillName.Magery, 100}
                },
                StamMaxSeed = 500,
                Str = 500,
                Tamable = true,
                VirtualArmor = 40
            });
        }


        [Constructible]
        public WildWarhorse() : base(CreatureProperties.Get<WildWarhorse>())
        {
            // Add customization here
        }

        [Constructible]
        public WildWarhorse(Serial serial) : base(serial)
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