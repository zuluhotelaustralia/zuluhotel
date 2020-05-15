using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a vampire corpse")]
    public class BloodthirstyVampire : BaseCreature
    {
        [Constructable]
        public BloodthirstyVampire()
            : base(AIType.AI_Berserk, FightMode.Closest, 10, 1, 0.1, 0.4)
        {
            Name = "a bloodthirsty vampire";
            Body = Utility.RandomBool() ? 0x190 : 0x191;
            Hue = 1882;



            Int32 sounds = 21;

            switch (Utility.Random(2))
            {
                case 0:
                    sounds = 0x286;
                    break;

                case 1:
                    sounds = 0x288;
                    break;
            }

            BaseSoundID = sounds;


            SetStr(500, 550);
            SetDex(110, 115);
            SetInt(1800, 2000);

            SetHits(1000, 1200);
            SetMana(1000, 1200);

            SetDamage(25, 40);

            VirtualArmor = 10;

            SetSkill(SkillName.Tactics, 100.0, 100.0);

            SetSkill(SkillName.EvalInt, 90.0, 95.0);
            SetSkill(SkillName.Meditation, 100.0, 100.0);
            SetSkill(SkillName.Magery, 90.0, 95.0);

            SetSkill(SkillName.MagicResist, 90.0, 95.0);

            SetSkill(SkillName.Wrestling, 90.0, 95.0);
            SetSkill(SkillName.Swords, 90.0, 95.0);
            SetSkill(SkillName.Fencing, 90.0, 95.0);

            Fame = 500;
            Karma = -500;


            Item ShortPants = new ShortPants();
            ShortPants.Movable = false;
            ShortPants.Hue = 2019;
            AddItem(ShortPants);


            switch (Utility.Random(3))
            {
                case 0:
                    {
                        AddItem(new Dagger());
                    }
                    break;

                case 1:
                    {
                        AddItem(new Kryss());
                    }
                    break;

                case 2:
                    {
                        AddItem(new Katana());
                    }
                    break;


            }
        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);

            PackItem(new BonePile());

            if (0.01 > Utility.RandomDouble())
                PackItem(new EmptyVialsWRack());
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 1; } }

        public BloodthirstyVampire(Serial serial)
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
