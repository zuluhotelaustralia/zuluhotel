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
    public class Kraken : BaseCreature
    {
        static Kraken()
        {
            CreatureProperties.Register<Kraken>(new CreatureProperties
            {
                // AccuracyAdjustment = 100,
                // AttackAttribute = Wrestling,
                // AttackHitScript = :combat:wrestlinghitscript,
                // AttackHitSound = 0x163,
                // AttackMissSound = 0x239,
                // AttackSpeed = 35,
                // corpseamt = 1,
                // corpseitm = BrimStone,
                // damagedsound = 0x164,
                // DataElementId = kraken,
                // DataElementType = NpcTemplate,
                // dstart = 10,
                // hostile = 1,
                // hp_regen_rate = 150,
                // idlesound1 = 0x162,
                // idlesound2 = 0x162,
                // lootgroup = 100,
                // MagicAdjustment = 30,
                // MagicItemChance = 100,
                // MagicItemChance_0 = 100,
                // mana_regen_rate = 130,
                // nopsych = 1,
                // regenrate = 100,
                // script = spellkillpcs,
                // stamina_regen_rate = 100,
                // TrueColor = 0,
                ActiveSpeed = 0.2,
                AiType = AIType.AI_Mage /* spellkillpcs */,
                AlwaysAttackable = true,
                BaseSoundID = 357,
                Body = 0x8,
                CorpseNameOverride = "corpse of a kraken",
                DamageMax = 47,
                DamageMin = 11,
                Dex = 245,
                Fame = 5,
                Female = false,
                FightMode = FightMode.Closest,
                FightRange = 1,
                HitsMax = 468,
                Hue = 0,
                Int = 40,
                Karma = 5,
                ManaMaxSeed = 0,
                Name = "a kraken",
                PassiveSpeed = 0.4,
                PerceptionRange = 10,
                ProvokeSkillOverride = 140,
                Skills = new Dictionary<SkillName, CreatureProp>
                {
                    {SkillName.MagicResist, 20},
                    {SkillName.Tactics, 60},
                    {SkillName.Wrestling, 60}
                },
                StamMaxSeed = 245,
                Str = 780,
                VirtualArmor = 45
            });
        }


        [Constructible]
        public Kraken() : base(CreatureProperties.Get<Kraken>())
        {
            // Add customization here
        }

        [Constructible]
        public Kraken(Serial serial) : base(serial)
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