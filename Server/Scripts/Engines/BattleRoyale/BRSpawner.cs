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
	}

	public static void Respawn_OnCommand( CommandEventArgs e ){
	    e.Mobile.Target = new RespawnSpawnerTarget();
	}
	    
	[Constructable]
	public BRSpawner() : base() {}
	public BRSpawner( Serial serial ) : base( serial ){}

	public override string DefaultName {
	    get { return "Battle Royale Spawner"; }
	}

	private override void InitSpawner( ){

	    SpawnNames.Add(""); //add a uge list of everything to be spawned
	    base.InitSpawner( 1, 5, 10, 0, 4, spawnNames );
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
