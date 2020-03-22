using System;
using Server;
using Server.Mobiles;

// these items should drop in loot at a specified percentage, but only when
// the monster that died is in a specific region

namespace Server.Items {

    public class ChicaneBossStone : Item {
	// transient gate should pop up at 5729, 1023, 56 and target 5844, 1027, 5
	// should be deleted after 4-5 minutes
	// when regions done, announce everyone in DoomRegion that they hear the roar of
	//  an ancient dragon or something.

	private Point3D _loc;
	private Point3D _tgt;
	private int _hue;
	    
	[Constructable]
	public ChicaneBossStone() : base( 0xF8B ){
	    this.Name = "A twisted moonstone";
	    _hue = 2759; //crystal
	    this.Hue = _hue;
	    _loc = new Point3D( 5461, 141, 20 );
	    _tgt = new Point3D( 5168, 74, 20 );
	}

	public ChicaneBossStone( Serial serial ) : base( serial ){
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); //version
	    writer.Write( _loc );
	    writer.Write( _tgt );
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	    _loc = reader.ReadPoint3D();
	    _tgt = reader.ReadPoint3D();
	}

	public override void OnDoubleClick( Mobile from ){
	    // create a gate at location, have it target location
	    // set a 5 min timer, delete gate ontick
	    from.SendMessage("The stone dissolves into a mist of magical essence.");
	    this.Delete();
	    Moongate gate = new Moongate();
	    gate.TargetMap = Map.Felucca;
	    gate.Target = _tgt;
	    gate.Hue = _hue;
	    gate.Name = "a dark moongate";
	    gate.MoveToWorld( _loc, Map.Felucca);

	    new BossStoneTimer( gate ).Start();
	    
	    //send message to everyone in the dungeon here about a dragon roaring or smth
	}
    }
    
    public class DoomBossStone : Item {
	// transient gate should pop up at 5729, 1023, 56 and target 5844, 1027, 5
	// should be deleted after 4-5 minutes
	// when regions done, announce everyone in DoomRegion that they hear the roar of
	//  an ancient dragon or something.

	private Point3D _loc;
	private Point3D _tgt;
	    
	[Constructable]
	public DoomBossStone() : base( 0xF8B ){
	    this.Name = "A draconic moonstone";
	    this.Hue = 2748; //malachite
	    _loc = new Point3D( 5729, 1023, 56 );
	    _tgt = new Point3D( 5844, 1027, 5 );
	}

	public DoomBossStone( Serial serial ) : base( serial ){
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize( writer );

	    writer.Write( (int) 0 ); //version
	    writer.Write( _loc );
	    writer.Write( _tgt );
	}

	public override void Deserialize( GenericReader reader ){
	    base.Deserialize( reader );

	    int version = reader.ReadInt();
	    _loc = reader.ReadPoint3D();
	    _tgt = reader.ReadPoint3D();
	}

	public override void OnDoubleClick( Mobile from ){
	    // create a gate at location, have it target location
	    // set a 5 min timer, delete gate ontick
	    from.SendMessage("The stone dissolves into a mist of magical essence.");
	    this.Delete();
	    Moongate gate = new Moongate();
	    gate.TargetMap = Map.Felucca;
	    gate.Target = _tgt;
	    gate.Hue = 2748;
	    gate.Name = "a dark moongate";
	    gate.MoveToWorld( _loc, Map.Felucca);

	    new BossStoneTimer( gate ).Start();
	    
	    //send message to everyone in the dungeon here about a dragon roaring or smth
	}
    }

    public class BossStoneTimer : Timer {
	private Moongate _gate;

	public BossStoneTimer( Moongate gate ) : base( TimeSpan.FromMinutes( 5 ) )
	{
	    _gate = gate;
	}

	protected override void OnTick()
	{
	    _gate.Delete();
	}
    }
}
