using System;
using System.Collections;
using System.Collections.Generic;

using Server;
using Server.Commands;
using Server.Items;

// this is a spawner for the BattleRoyale game only, meant to expediate (is that a word?) spawning the game arena with items

namespace Server.Mobiles{
    public class BRSpawner : Spawner {

	public static void Initialize() {
	    CommandSystem.Register("Respawn", AccessLevel.Developer, new CommandEventHandler( Respawn_OnCommand ) );
	    CommandSystem.Register("DumpSpawnNames", AccessLevel.Developer, new CommandEventHandler( DumpSpawnNames_OnCommand ) );
	}

	public static void Respawn_OnCommand( CommandEventArgs e ){
	    e.Mobile.Target = new RespawnSpawnerTarget();
	}

	public static void DumpSpawnNames_OnCommand( CommandEventArgs e ) {
	    e.Mobile.Target = new BRDebugTarget();
	}
	    
	[Constructable]
	public BRSpawner() : base() {}
	public BRSpawner( Serial serial ) : base( serial ){}

	public override string DefaultName {
	    get { return "Battle Royale Spawner"; }
	}

	protected override void InitSpawner( int amount, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, List<string> spawnNames ){

	    spawnNames.Clear();
	    spawnNames.Add("katana set damagelevel vanq"); //add a uge list of everything to be spawned
	    spawnNames.Add("chainlegs");
	    
	    base.InitSpawner( amount, minDelay, maxDelay, team, homeRange, spawnNames );
	}
	    

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );
	}

	public override void Deserialize( GenericReader reader ) {
	    base.Deserialize( reader );
	}

	public override void OnDoubleClick( Mobile from ){
	    //intentional - don't send the gump for this spawner
	    return;
	}
    }
}
