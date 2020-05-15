using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an elite elder gargoyle corpse")]
    public class EliteGargoyleElder : BaseCreature
    {
        [Constructable]
        public EliteGargoyleElder()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an elite elder gargoyle";
            Body = 40;
            Hue = 2958;
            BaseSoundID = 357;

            SetStr(600, 605);
            SetDex(90, 100);
            SetInt(1400, 1650);

            SetHits(1450, 1600);
            SetMana(1400, 1450);

            SetDamage(25, 35);

            VirtualArmor = 25;

            SetSkill(SkillName.Tactics, 150.0, 150.0);

            SetSkill(SkillName.EvalInt, 120.0, 135.0);
            SetSkill(SkillName.Meditation, 150.0, 150.0);
            SetSkill(SkillName.Magery, 130.0, 135.0);

            SetSkill(SkillName.MagicResist, 90.0, 95.0);

            SetSkill(SkillName.Wrestling, 120.0, 125.0);

            Fame = 24000;
            Karma = -24000;
        }

        public override void GenerateLoot()
        {
            PackItem(new Longsword());

            AddLoot(LootPack.UltraRich);

            switch (Utility.Random(7))
            {
                case 0: AddItem(new RedCarpetSideOne()); break;
                case 1: AddItem(new RedCarpetSideOne()); break;
                case 2: AddItem(new RedCarpetSideTwo()); break;
                case 3: AddItem(new RedCarpetSideTwo()); break;
                case 4: AddItem(new RedCarpetCornerOne()); break;
                case 5: AddItem(new RedCarpetCornerTwo()); break;
                case 6: AddItem(new RedCarpetCenterThree()); break;

            }
        }

        public override bool CanRummageCorpses { get { return true; } }

        public override int Meat { get { return 1; } }

        public EliteGargoyleElder(Serial serial)
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
