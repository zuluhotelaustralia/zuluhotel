using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Items{

    [DynamicFliping]
    [Flipable( 0x9AB, 0xE7C )]
    public class EasterEggChest : LockableContainer
    {
	public Dictionary<Serial, int> Leaderboard;
	    
	[Constructable]
	public EasterEggChest() : base( 0x9AB )
	{
	    Leaderboard = new Dictionary<Serial,int>();
	    Hue = Utility.RandomBrightHue();
	    Name = "Enchanted Chest of Easter";
	}

	public EasterEggChest( Serial serial ) : base( serial )
	{
	}

	public override bool OnDragDrop( Mobile from, Item dropped ){
	    if( dropped is EasterEggs ){
		EasterEggs eggs = dropped as EasterEggs;
		Serial them = from.Serial;
		
		if( Leaderboard.ContainsKey( from.Serial ) ){
		    Leaderboard[them] += eggs.Amount;
		}
		else{
		    Leaderboard[them] = eggs.Amount;
		}

		from.SendMessage("The box seems almost happy to accept your tasty eggs.");
		TownCrier.AddAnnouncement( from.Name + "'s Easter Egg total is " + Leaderboard[them] + "!");
		return true;
	    }
	    else {
		return base.OnDragDrop(from, dropped);
	    }
	}
	
	public override bool OnDragDropInto(Mobile from, Item item, Point3D p){
	    if( item is EasterEggs ){
		EasterEggs eggs = item as EasterEggs;
		Serial them = from.Serial;
		
		if( Leaderboard.ContainsKey( from.Serial ) ){
		    Leaderboard[them] += eggs.Amount;
		}
		else{
		    Leaderboard[them] = eggs.Amount;
		}

		from.SendMessage("The box seems almost happy to accept your tasty eggs.");
		TownCrier.AddAnnouncement( from.Name + "'s Easter Egg total is " + Leaderboard[them] + "!");
		return true;
	    }
	    else {
		return base.OnDragDropInto(from, item, p);
	    }
	}
		

	public override void Serialize( GenericWriter writer )
	{
	    base.Serialize( writer );

	    writer.Write( (int) 1 ); // version

	    writer.Write( Leaderboard.Count );
	    
	    foreach( KeyValuePair<Serial, int> entry in Leaderboard ){
		writer.Write( (int)entry.Key );
		writer.Write( entry.Value);
	    }
	}

	public override void Deserialize( GenericReader reader )
	{
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	    int buckets = reader.ReadInt();
	    Leaderboard = new Dictionary<Serial, int>();

	    for( int i=0; i<buckets; i++ ){
		Leaderboard.Add( (Serial)reader.ReadInt(), reader.ReadInt() );
	    }
	}
    }
}
