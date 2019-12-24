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
	    CommandSystem.Register("Respawn", AccessLevel.GameMaster, new CommandEventHandler( Respawn_OnCommand ) );
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
	    spawnNames.Add("katana");
	    spawnNames.Add("katana set damagelevel power set identified true");
	    spawnNames.Add("katana set damagelevel force set identified true");
	    spawnNames.Add("warmace");
	    spawnNames.Add("warmace set damagelevel power set identified true");
	    spawnNames.Add("warmace set damagelevel force set identified true");
	    spawnNames.Add("warmace set damagelevel vanq set identified true");
	    spawnNames.Add("kryss");
	    spawnNames.Add("kryss set damagelevel force set identified true");
	    spawnNames.Add("kryss set damagelevel power set identified true");
	    spawnNames.Add("kryss set damagelevel vanq set identified true");
	    spawnNames.Add("bandage 20");
	    spawnNames.Add("chainchest");
	    spawnNames.Add("chainlegs");
	    spawnNames.Add("chaincoif");
	    spawnNames.Add("metalshield");
	    spawnNames.Add("bronzeshield");
	    spawnNames.Add("metalkiteshield");
	    spawnNames.Add("ringmailchest");
	    spawnNames.Add("ringmaillegs");
	    spawnNames.Add("ringmailgloves");
	    spawnNames.Add("ringmailarms");
	    spawnNames.Add("leatherarms");
	    spawnNames.Add("leatherchest");
	    spawnNames.Add("leathergloves");
	    spawnNames.Add("leathergorget");
	    spawnNames.Add("leatherlegs");
	    spawnNames.Add("platehelm");
	    spawnNames.Add("platechest");
	    spawnNames.Add("platearms");
	    spawnNames.Add("platelegs");
	    spawnNames.Add("plategorget");
	    spawnNames.Add("plategloves");
	    spawnNames.Add("halberd");
	    spawnNames.Add("halberd set damagelevel force set identified true");
	    spawnNames.Add("halberd set damagelevel power set identified true");
	    spawnNames.Add("halberd set damagelevel vanq set identified true");
	    spawnNames.Add("spear");
	    spawnNames.Add("spear set damagelevel force set identified true");
	    spawnNames.Add("spear set damagelevel power set identified true");
	    spawnNames.Add("spear set damagelevel vanq set identified true");
	    spawnNames.Add("closehelm");
	    spawnNames.Add("bascinet");
	    spawnNames.Add("bonehelm");
	    spawnNames.Add("bonearms");
	    spawnNames.Add("bonechest");
	    spawnNames.Add("bonegloves");
	    spawnNames.Add("bonelegs");
	    spawnNames.Add("blackstaff");
	    spawnNames.Add("blackstaff set damagelevel force set identified true");
	    spawnNames.Add("blackstaff set damagelevel power set identified true");
	    spawnNames.Add("blackstaff set damagelevel vanq set identified true");

	    for( int i=0; i<30; i++ ){
		spawnNames.Add("bagofpotions");
	    }

	    for( int i=0; i<30; i++ ){
		spawnNames.Add("bagofallreagents 30");
	    }

	    for( int i=0; i<30; i++ ){
		spawnNames.Add("bow");
		spawnNames.Add("bow set damagelevel force set identified true");
		spawnNames.Add("bow set damagelevel power set identified true");
		spawnNames.Add("bow set damagelevel vanq set identified true");
		spawnNames.Add("arrow 60");
	    }
		
	    base.InitSpawner( amount, minDelay, maxDelay, team, homeRange, spawnNames );
	}
	    

	public override void Serialize( GenericWriter writer ) {
	    base.Serialize( writer );

	    int version = 1;
	    
	    writer.Write( version );
	}

	public override void Deserialize( GenericReader reader ) {
	    base.Deserialize( reader );

	    int version;
	    
            version = reader.ReadInt();

	    switch( version ){
		case 1:
		    HomeRange = 1;
		    break;
		default:
		    HomeRange = 1;
		    break;
	    }
	       
	}

	public override void OnDoubleClick( Mobile from ){
	    //intentional - don't send the gump for this spawner
	    return;
	}
    }
}
