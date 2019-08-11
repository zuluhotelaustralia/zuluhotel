using Server;
using Server.Commands;
using Server.Mobiles;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Server.BattleRoyale{
    public class GameController : Item {

	public enum BattleState {
	    Idle, //game not running, nobody able to join
	    Joining, //players able to join
	    Parachuting, //if it was pubg you'd be in the herc or parachuting
	    Playing, // game actually started
	    Shrinking //same as playing only the zone is contracting
	}

	private static BattleState _state = BattleState.Idle;

	public const int PlayerCap = 30;
	
	private static Point3D _EscapeLoc = new Point3D(3033, 3406, 20); //serps, see AccountHandler
	public static Point3D EscapeLoc{
	    get { return _EscapeLoc; }
	}

	//TODO randomize this start loc by +/-5 in each x and y directions
	private static Point3D _StartLoc = new Point3D(4420, 1155, 0); //Moonglow, near the maze
	public static Point3D StartLoc{
	    get { return _StartLoc; }
	}
	
	private static List<PlayerMobile> _Players;
	private static List<PlayerMobile> _AlivePlayers;

	public static void Initialize() {
	    //all commands go here, no more splitting it up between several files.
	    // I'm looking at *you*, sith.
	    // --sith

	    CommandSystem.Register("Escape", AccessLevel.Player, new CommandEventHandler(Escape_OnCommand) );
	}

	public static void Escape_OnCommand( CommandEventArgs e){
	    if( e.Mobile.Alive ) {
		e.Mobile.SendMessage("You cannot do this unless dead.");
		return;
	    }
	    else {
		e.Mobile.SendMessage("You will now be moved from the battle royale arena as an observer and removed from the minigame.");
		e.Mobile.MoveToWorld(GameController.EscapeLoc, Map.Felucca);
		return;
	    }
	}

	// it's possible they're abandoned on the island due to a bug or server restart but not registered as a player any more.
	public static void TryUnregisterPlayer( PlayerMobile pm ) {
	    if( _Players.Contains( pm ) ){
		_Players.Remove( pm );
		pm.SendMessage("You have been removed from the list of Battle Royale players.");
	    }
	}

	public static bool TryRegisterPlayer( PlayerMobile pm ){
	    if( _Players.Count < PlayerCap ){
		pm.SendMessage("You have registered for Battle Royale.  Safely unequip and store your items before the game begins!"); //TODO cliloc this
		pm.SendMessage("To unregister, double-click the stone again.");
		_Players.Add(pm);
		return true;
	    }
	    else{
		pm.SendMessage("You may not join at this time.  The game queue is full.");
		return false;
	    }
	}
		
	[Constructable]
	public GameController() : base( 0xED4 ){
	    this.Name = "Battle Royale Control Stone";
	    this.Hue = 2771; //dripstone
	}

	public GameController( Serial serial ) : base( serial ){
	    // if the server goes down the game just ends and everyone can just deal with it
	    // not going to try to wrestle the complexity of having a minigame traverse server
	    // restarts
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);
	    writer.Write( (int) 0 ); //version
	}

	public override void Deserialize(GenericReader reader ){
	    base.Deserialize( reader );
	    int version = reader.ReadInt();
	}

	public override void OnDoubleClick( Mobile mob ){
	    PlayerMobile from = mob as PlayerMobile;

	    if( from.AccessLevel == AccessLevel.Player ){
		if( _state == BattleState.Joining ){
		    if( _Players.Contains(from) ){
			TryUnregisterPlayer(from);
		    }
		    else {
			TryRegisterPlayer( from );
		    }
		}
		else {
		    from.SendMessage("The game queue is not currently accepting new players; please try again later." );
		    return;
		}
	    }
	    else {
		from.SendMessage("Staff may not join Battle Royale.");
		return;
	    }
	}

	public static void Announce( String s ){
	    World.Broadcast( 0x35, true, s );
	    // later, change this to broadcast via the Town Criers TODO
	}
	
	public static void BeginJoining() {
	    if( _state != BattleState.Joining ){
		_state = BattleState.Joining;
		Announce("Battle Royale is now open for joining!  Game starts in 10 minutes!");
		GameTimer jt = new GameTimer( new TimeSpan(0, 10, 0), EndJoining );  //h:m:s
		jt.Start();
	    }
	}

	public static void EndJoining(){
	    _state = BattleState.Parachuting;
	    // kill everyone, move em to arena, set a res timer that calls BeginPlay()

	    foreach( PlayerMobile pm in _Players ){
		pm.Kill();
		pm.MoveToWorld(_StartLoc, Map.Felucca);
		pm.SendMessage("You will be automatically resurrected in 45 seconds, at which point the battle royale will begin!"); //TODO cliloc this
	    }
	    
	    Announce("BattleRoyale has started!");
	    GameTimer rt = new GameTimer( new TimeSpan(0, 1, 0), BeginPlay);
	    rt.Start();
	}
	
	private static void OnPlayerDeath( Mobile.OnDeathEventArgs a) {
	    PlayerMobile pm = a.Mobile as PlayerMobile;
	    pm.OnDeathEvent -= OnPlayerDeath;
	    _AlivePlayers.Remove(pm);
	    _Players.Remove(pm);
	    pm.SendMessage("You have died during Battle Royale.  You can continue to observe as a ghost.  Use the Escape command to leave the arena.");

	    foreach( PlayerMobile player in _AlivePlayers ){
		player.SendMessage("{0} was killed by {1}.  {2} players remaining.", pm, pm.LastKiller, _AlivePlayers.Count );
	    }

	    if( CheckVictory() ){
		Announce("Winner winner, chicken dinner!");
		_state = BattleState.Idle;
		_Players.Clear();
		_AlivePlayers.Clear();
		
		//set a timer for next game opening
	    }
	}

	public static bool CheckVictory() {
	    //returns true if someone has won the game
	    if( _state == BattleState.Playing || _state == BattleState.Shrinking ){
		if( _AlivePlayers.Count <= 1 ){
		    return true;
		}
		else {
		    // is it possible to be in this block and still be a win?
		    return false;
		}
	    }

	    return false;
	}
	
	public static void BeginPlay(){
	    foreach( PlayerMobile pm in _Players ){
		pm.Resurrect();
		_AlivePlayers.Add(pm);
		pm.OnDeathEvent += OnPlayerDeath;
		pm.SendMessage("Last one standing in Moonglow wins!");
	    }

	    _state = BattleState.Playing;
	}
    }
}
