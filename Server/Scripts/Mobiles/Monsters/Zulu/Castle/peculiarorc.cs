using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server;

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class PeculiarOrc : BaseCreature
    {
        public override InhumanSpeech SpeechType { get { return InhumanSpeech.Orc; } }

        [Constructable]
        public PeculiarOrc()
            : base(AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a peculiar orc";
            Body = 17;
            BaseSoundID = 0x45A;

            SetStr(100, 105);
            SetDex(45, 50);
            SetInt(20, 25);

            SetHits(75, 100);

            SetDamage(4, 8);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 25.0, 30.0);
            SetSkill(SkillName.Wrestling, 50.0, 55.0);

            Fame = 1500;
            Karma = -1500;

            Item blackcloth = new OrcishKinMask ();
            blackcloth.Movable = true;
            blackcloth.Hue = 0;
            AddItem(blackcloth);
        }

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Meager );
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 1; } }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.SavagesAndOrcs; }
        }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is OrcishKinMask)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            if (aggressor.HueMod == 1451)
            {
                aggressor.Damage(50, this, DamageType.Fire, AttackType.Physical);
                aggressor.BodyMod = 0;
                aggressor.HueMod = -1;
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
                aggressor.SendMessage("Your skin is scorched as the orcish paint burns away!"); // Your skin is scorched as the tribal paint burns away!
            }
        }

        public PeculiarOrc(Serial serial)
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
