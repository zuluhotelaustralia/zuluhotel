using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Items
{
    public class HallMonitorSuit : BaseSuit
    {
	[Constructable]
	public HallMonitorSuit() : base( AccessLevel.Player, 0, 0x204E )
	{
	    this.Name = "a tattered robe";
	}

	public HallMonitorSuit( Serial serial ) : base( serial )
	{
	    this.Name = "a tattered robe";
	}

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); // version
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	}
    }
}

namespace Server.Mobiles
{
       
    [CorpseName("a furious hall monitor corpse")]
    public class FuriousHallMonitor : BaseCreature
    {
        public override bool ShowFameTitle
        {
            get
            {
                return false;
            }
        }

        [Constructable]
	public FuriousHallMonitor() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 )
	{
	    Name = "a furious hall monitor";
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

	    SetStr( 300, 300 );
	    SetDex( 100, 100 );
	    SetInt( 1400, 1600 );

	    SetHits( 350, 400 );
            SetMana( 1400, 1600);

	    SetDamage( 7, 14 );

            VirtualArmor = 5;
            
            SetSkill( SkillName.Tactics, 100.0, 130.0);
		
	    SetSkill( SkillName.EvalInt, 75.0, 80.0 );
	    SetSkill( SkillName.Magery, 75.0, 120.0 );			
            SetSkill( SkillName.Meditation, 100.0, 130.0);

            SetSkill( SkillName.MagicResist, 75.0, 120.0);

	    SetSkill( SkillName.Wrestling, 75.0, 120.0 );

	    Fame = 12500;
	    Karma = -12500;

            Item Sandals = new Sandals();
            Sandals.Movable = false;
            Sandals.Hue = 1775;
            AddItem(Sandals);

            Item Robe = new HallMonitorSuit();
            Robe.Movable = false;
            Robe.Hue = 1255;
            AddItem( Robe );

            Utility.AssignRandomHair(this);
	}

	public FuriousHallMonitor( Serial serial ) : base( serial )
	{
	}        

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.FilthyRich );
	    AddLoot( LootPack.LesserNecroScrolls );
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 1; } }

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
