using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

// this is a spawner that can go inside containers (i.e. chests) and spawn items inside them.
// future versions will support loading from/saving to disk so that you can quickly spawn a whole area
namespace Server.Mobiles {
    public class LeetSpawner : Spawner {
	[Constructable]
	public LeetSpawner() : base() {}
	public LeetSpawner( Serial serial ) : base( serial ) {}

	public override string DefaultName {
	    get { return "Leet Spawner"; }
	}

	public override void Spawn( int index ) {
	    Map map = Map;

	    if( map == null ||
		map == Map.Internal ||
		SpawnNamesCount == 0 ||
		index >= SpawnNamesCount ) {
		return;
	    }

	    if( Type.GetType("Server.Items." + SpawnNames[index]) == null || !(Parent is Container) ){
		//unless we're spawning an item AND inside a container, just use Spawner.Spawn()
		base.Spawn( index );
		return;
	    }		

	    Defrag();

	    if( CheckSpawnerFull() ){
		return;
	    }

	    ISpawnable spawned = CreateSpawnedObject( index );

	    if( spawned == null ){
		return;
	    }

	    spawned.Spawner = this;
	    Spawned.Add( spawned );

	    Container par = (Container)Parent;

	    //pseudo TryDropItem here to avoid having to supply the required Mobile
	    List<Item> list = par.Items;
	    foreach( Item item in list ){
		if( !(item is Container) ){
		    if( item.StackWith(null, (Item)spawned, false) ){
			// we stacked
			return;
		    }
		}
	    }

	    par.DropItem((Item)spawned);
	    InvalidateProperties();
	}

	public override void Defrag() {
	    if( !(Parent is Container) ){
		//if we're not in a container, then use base class' method
		base.Defrag();
		return;
	    }

	    bool removed = false;

	    for( int i = 0; i < Spawned.Count; ++i ){
		ISpawnable e = Spawned[i];
		
		if( e is Item ){
		    Item item = (Item)e;

		    if( item.Deleted || item.Parent != Parent ){
			//been looted or removed
			removed = true;
			--i;
		    }
		}
	    }
	    
	    if( removed ){
		InvalidateProperties();
	    }
	}

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );
	}

	public override void Deserialize( GenericReader reader ) {
	    base.Deserialize( reader );
	}
    }
}
