using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;

namespace Server.Mobiles
{
    public class AngryGuest : BaseCreature
    {
	private bool m_DinnerGuest;
	public bool DinnerGuest{
	    get { return m_DinnerGuest; }
	    set { m_DinnerGuest = value; }
	}
	
        [Constructable] 
	public AngryGuest() : base( AIType.AI_Generic, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
	{
	    m_DinnerGuest = Utility.RandomBool();
            Body = Utility.RandomList(0x190, 0x191);
            Hue = Utility.RandomSkinHue();
            Utility.AssignRandomHair(this); 

            //Dinner Guest
            if (DinnerGuest)
            {
                Name = "angry dinner guest";

                //Male
                if (Body == 0x190)
                {
                    AddItem(new FancyShirt(Utility.RandomBlueHue()));
                    AddItem(new LongPants());
                    AddItem( new Shoes() );
                }
                
                //Female
                else
                {
                    AddItem(new FancyDress(Utility.RandomBlueHue()));
                    AddItem( new Shoes() );
                }
            }

            //Faculty
            else
            {
                Name = "angry faculty member";

                AddItem(new Robe(Utility.RandomBlueHue()));
                AddItem(new WizardsHat(Utility.RandomBlueHue()));
                AddItem(new Sandals());	
            }   				
        
	    SetStr( 80, 100 );
	    SetDex( 45, 50 );
	    SetInt( 600, 650 );

	    SetHits( 175, 200 );
            SetMana( 600, 650 );

	    SetDamage( 5, 10 );

            VirtualArmor = 0;            

            SetSkill( SkillName.Tactics, 100.0, 100.0);
			
            SetSkill( SkillName.Meditation, 100.0, 100.0);
            SetSkill( SkillName.EvalInt, 75.0, 80.0);			
            SetSkill( SkillName.Magery, 75.0, 80.0);

	    SetSkill( SkillName.MagicResist, 75.0, 80.0 );	
		
	    SetSkill( SkillName.Wrestling, 65.0, 70.0 );

	    Fame = 8500;
	    Karma = -8500;
	}

        public override void GenerateLoot()
        {
	    AddLoot( LootPack.Rich );
        }

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }
	public override int Meat{ get{ return 1; } }		

	public AngryGuest( Serial serial ) : base( serial ) 
	{ 
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
