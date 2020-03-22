using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("an ettin corpse")]
    public class BridgeEttin : BaseCreature
    {
        [Constructable]
        public BridgeEttin()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ettin";
            Body = 18;
            BaseSoundID = 367;

            SetStr(150, 155);
            SetDex(45, 50);
            SetInt(20, 25);

            SetHits(300, 350);

            SetDamage(10, 20);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 25.0, 30.0);

            SetSkill(SkillName.Wrestling, 50.0, 55.0);

            Fame = 3000;
            Karma = -3000;
        }

        public override void GenerateLoot()
        {
            AddLoot( LootPack.Rich );
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 4; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.SavagesAndOrcs; }
        }

        public BridgeEttin(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
