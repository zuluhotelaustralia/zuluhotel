using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName ("a hall monitor corpse")]
    public class HallMonitor : BaseCreature
    {

        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
	public HallMonitor() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a hall monitor";
            Title = string.Empty;
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                this.Body = 0x191;
            }
            else
            {
                this.Body = 0x190;
            } 

	    SetStr( 100, 105 );
	    SetDex( 45, 50 );
	    SetInt( 500, 600 );

	    SetHits( 125, 150 );
            SetMana( 500, 600 );

	    SetDamage( 5, 10 );

            VirtualArmor = 5;
            
            SetSkill(SkillName.Tactics, 100.0, 100.0);

	    SetSkill( SkillName.EvalInt, 50.0, 55.0 );
            SetSkill( SkillName.Meditation, 100.0, 100.0);
	    SetSkill( SkillName.Magery, 50.0, 55.0 );

	    SetSkill( SkillName.MagicResist, 50.0, 55.0 );

	    SetSkill( SkillName.Wrestling, 55.0, 60.0 );

	    Fame = 3500;
	    Karma = -3500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 1775;
            AddItem(Sandals);

            Item Robe = new Robe();
            Robe.Movable = false;
            Robe.Hue = 1246;
            AddItem(Robe);

            Item Hood = new Hood();
            Hood.Movable = false;
            Hood.Hue = 1246;
            AddItem(Hood);

            Utility.AssignRandomHair(this);
	}	 

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

	public HallMonitor( Serial serial ) : base( serial )
	{
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich );
        }

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );
	    writer.Write( (int) 0 );
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );
	    int version = reader.ReadInt();
	}
    }
}
