

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
    public class BlackGateDaemon : BaseCreature
    {
        static BlackGateDaemon() => CreatureProperties.Register<BlackGateDaemon>(new CreatureProperties
        {
            // AttackAttribute = Wrestling,
            // AttackHitScript = :combat:wrestlinghitscript,
            // AttackHitSound = 0xAB,
            // AttackMissSound = 0x239,
            // AttackSpeed = 35,
            // corpseamt = 7,
            // corpseitm = rawrib,
            // CProp_noloot = i1,
            // damagedsound = 0xac,
            // DataElementId = blackgatedaemon,
            // DataElementType = NpcTemplate,
            // dstart = 10,
            // guardignore = 1,
            // idlesound1 = 0xa9,
            // idlesound2 = 0xaa,
            // script = spellkillpcs,
            // spell = summon_daemon,
            // TrueColor = 0,
            ActiveSpeed = 0.2,
            AiType = AIType.AI_Mage /* spellkillpcs */,
            AlwaysAttackable = true,
            BaseSoundID = 168,
            Body = 0xe4,
            CorpseNameOverride = "corpse of a black gate daemon",
            DamageMax = 16,
            DamageMin = 2,
            Dex = 165,
            Fame = 4,
            Female = false,
            FightMode = FightMode.None,
            FightRange = 1,
            HitsMax = 1500,
            Hue = 0,
            Int = 290,
            Karma = 5,
            ManaMaxSeed = 465,
            Name = "a black gate daemon",
            PassiveSpeed = 0.4,
            PerceptionRange = 10,
            PreferredSpells = new List<Type>
            {
                typeof(Spells.Eighth.SummonDaemonSpell),
            },
            ProvokeSkillOverride = 150,
            Skills = new Dictionary<SkillName, CreatureProp>
            {
                { SkillName.MagicResist, 120 },
                { SkillName.Tactics, 90 },
                { SkillName.Wrestling, 90 },
                { SkillName.Magery, 100 },
                { SkillName.EvalInt, 90 },
                { SkillName.Anatomy, 25 },
            },
            StamMaxSeed = 165,
            Str = 1500,
            VirtualArmor = 20,

        });


        [Constructible]
public BlackGateDaemon() : base(CreatureProperties.Get<BlackGateDaemon>())
        {
            // Add customization here


        }

        [Constructible]
public BlackGateDaemon(Serial serial) : base(serial) {}



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
