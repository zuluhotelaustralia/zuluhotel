using System;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
    [CorpseName("an orcish corpse")]
    public class BridgeOrcCaptain : BaseCreature
    {
        public override InhumanSpeech SpeechType { get { return InhumanSpeech.Orc; } }

        [Constructable]
        public BridgeOrcCaptain()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {



            Name = NameList.RandomName("orc");
            Body = 7;
            BaseSoundID = 0x45A;

            SetStr(600, 605);
            SetDex(45, 50);
            SetInt(25, 30);

            SetHits(400, 500);

            SetDamage(25, 35);

            VirtualArmor = 5;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 45.0, 50.0);
            SetSkill(SkillName.Swords, 55.0, 60.0);
            SetSkill(SkillName.Wrestling, 55.0, 60.0);

            Fame = 7500;
            Karma = -7500;
        }


        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }

        public override int Meat { get { return 2; } }

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
                aggressor.Damage( 50, this, DamageType.Fire, AttackType.Physical );
                aggressor.BodyMod = 0;
                aggressor.HueMod = -1;
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
                aggressor.SendMessage("Your skin is scorched as the orcish paint burns away!"); // Your skin is scorched as the tribal paint burns away!
            }
        }

        public BridgeOrcCaptain(Serial serial)
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
