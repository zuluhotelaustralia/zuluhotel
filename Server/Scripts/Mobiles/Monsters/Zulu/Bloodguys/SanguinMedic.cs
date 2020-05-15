using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a sanguin medic corpse")]
    public class SanguinMedic : BaseCreature
    {
        [Constructable]
        public SanguinMedic()
            : base(AIType.AI_Healer, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Name = "Sanguin Medic";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            }

            SetStr(96, 120);
            SetDex(50, 55);
            SetInt(36, 60);

            SetHits(125, 150);

            SetDamage(10, 14); //Uses Weapon

            VirtualArmor = 15;

            SetSkill(SkillName.Tactics, 100.0, 100.0); //Uses Weapon

            SetSkill(SkillName.Macing, 65.0, 70.0);
            SetSkill(SkillName.Wrestling, 65.0, 70.0);

            SetSkill(SkillName.MagicResist, 30.0, 35.0);
            SetSkill(SkillName.Parry, 40.0, 45.0);

            SetSkill(SkillName.Healing, 20.0, 25.0);

            Fame = 1500;
            Karma = -1500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 1775;
            AddItem(Sandals);

            Item StuddedArms = new StuddedArms();
            StuddedArms.Movable = false;
            AddItem(StuddedArms);

            Item StuddedGloves = new StuddedGloves();
            StuddedGloves.Movable = false;
            AddItem(StuddedGloves);

            Item StuddedGorget = new StuddedGorget();
            StuddedGorget.Movable = false;
            AddItem(StuddedGorget);

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 1777;
            AddItem(Robe);

            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 1775;
            AddItem(Hood);

            switch (Utility.Random(5))
            {
                case 0: AddItem(new Club()); break;
                case 1: AddItem(new HammerPick()); break;
                case 2: AddItem(new Mace()); break;
                case 3: AddItem(new Maul()); break;
                case 4: AddItem(new WarMace()); break;
            }

            AddItem(new WoodenShield());

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich);
        }

        public override bool AlwaysMurderer { get { return true; } }

        public SanguinMedic(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
