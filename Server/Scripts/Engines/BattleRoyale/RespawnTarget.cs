using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Mobiles{ //because spawner

    public class BRDebugTarget : Target {
	public BRDebugTarget() : base( -1, true, TargetFlags.None ) {}

	protected override void OnTarget( Mobile from, object targeted ){
	    if( targeted is Server.Mobiles.Spawner ){
		Spawner spnr = targeted as Spawner;
		foreach( String s in spnr.SpawnNames ){
		    Console.WriteLine(s);
		}
	    }
	}
    }
    
    public class RespawnSpawnerTarget : Target {
	public RespawnSpawnerTarget() : base( -1, true, TargetFlags.None ) {}

	protected override void OnTarget( Mobile from, object targeted ){
	    if( targeted is Server.Mobiles.Spawner ){
		((Spawner)targeted).Respawn();
	    }
	}
    }
}