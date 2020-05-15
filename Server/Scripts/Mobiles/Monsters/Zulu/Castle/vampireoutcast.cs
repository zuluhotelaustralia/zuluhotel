using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{

    [CorpseName("a vampire corpse")]
    public class VampireOutcast : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
        public VampireOutcast()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a vampire outcast";
            Title = string.Empty;
            SpeechHue = Utility.RandomDyedHue();
            Hue = 1882;

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(550, 650);
            SetDex(110, 140);
            SetInt(100, 120);

            SetHits(675, 750);
            SetMana(100, 120);

            SetDamage(25, 34);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 55.0, 60.0);
            SetSkill(SkillName.Magery, 55.0, 60.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);

            SetSkill(SkillName.MagicResist, 100.0, 110.0);

            SetSkill(SkillName.Wrestling, 75.0, 90.0);

            Fame = 3500;
            Karma = -3500;

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 2019;
            AddItem(Robe);

        }

        public VampireOutcast(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 2);

            PackItem(new Bone());

        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

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
