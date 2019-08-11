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
	    Playing, // game in progress
	}

	private static BattleState _state = BattleState.Idle;

	public const int PlayerCap = 30;
	public const double HoursTilNextGame = 2;
	
        private static Map _Map = Map.Felucca;
        
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

	    _Players = new List<PlayerMobile>();
	    _AlivePlayers = new List<PlayerMobile>();

	    CommandSystem.Register("Escape", AccessLevel.Player, new CommandEventHandler(Escape_OnCommand) );
            CommandSystem.Register("StartBRGame", AccessLevel.Developer, new CommandEventHandler(StartBRGame_OnCommand));


            // Delete any zone walls that were persisted in the world save
            ClearZone();
	}

        public static void StartBRGame_OnCommand( CommandEventArgs e ) {
            BeginJoining();
        }

	public static void Escape_OnCommand( CommandEventArgs e){
	    if( e.Mobile.Alive ) {
		e.Mobile.SendMessage("You cannot do this unless dead.");
		return;
	    }
	    else {
		e.Mobile.SendMessage("You will now be moved from the battle royale arena as an observer and removed from the minigame.");
		e.Mobile.MoveToWorld(GameController.EscapeLoc, _Map);
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
		//GameTimer jt = new GameTimer( TimeSpan.FromMinutes(10), EndJoining );
		GameTimer jt = new GameTimer( TimeSpan.FromSeconds(10), EndJoining );
		jt.Start();
	    }
	}

	public static void EndJoining(){
            Announce("End Joining");
            
	    _state = BattleState.Parachuting;
	    // kill everyone, move em to arena, set a res timer that calls BeginPlay()

	    foreach( PlayerMobile pm in _Players ){
		pm.Kill();
		pm.MoveToWorld(_StartLoc, _Map);
		pm.SendMessage("You will be automatically resurrected in 60 seconds, at which point the battle royale will begin!"); //TODO cliloc this
	    }
	    
	    Announce("BattleRoyale has started!");
	    GameTimer rt = new GameTimer( TimeSpan.FromMinutes(1) , BeginPlay);
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
		PlayerMobile victor = _AlivePlayers[0];
		victor.SendMessage("Congratulations!  You will be teleported out of the arena in 15 seconds.");
		GameTimer repatriator = new GameTimer( TimeSpan.FromSeconds(15), EndGame );
		repatriator.Start();
	    }
	}

	public static void EndGame() {
	    PlayerMobile victor = _AlivePlayers[0];

	    _Players.Clear();
	    _AlivePlayers.Clear();

	    victor.MoveToWorld(EscapeLoc, Map.Felucca);

	    //set a timer for next game opening
	    GameTimer nextgame = new GameTimer( TimeSpan.FromHours(HoursTilNextGame), BeginJoining );
	    nextgame.Start();
	}

	public static bool CheckVictory() {
	    //returns true if someone has won the game
	    if( _state == BattleState.Playing ){
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
	
        public class ZoneStage {
            private int _size;
            private TimeSpan _duration;

            public ZoneStage(int size, TimeSpan duration)
            {
                _size = size;
                _duration = duration;
            }

            public TimeSpan Duration{ get{ return _duration; } }
            public int Size{ get{ return _size; } }

            public Rectangle2D ToRect(Map map, Point2D center)
            {
                return new Rectangle2D(new Point2D(Math.Max(0, center.X - Size),
                                                   Math.Max(0, center.Y - Size)),
                                       new Point2D(Math.Min(map.Width, center.X + Size),
                                                   Math.Min(map.Height, center.Y + Size)));
            }
        }

        /**
         * Moonglow outer dimensions
         *
         *  Top: 797
         *  Bottom: 1521
         *  Left 4249
         *  Right: 4734
         */
        private static Point2D _ZoneCenter;

        private static Rectangle2D _CurrentZone;
        private static Rectangle2D _NextZone;

        private static ZoneStage[] _ZoneStages = {
            new ZoneStage( 500, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 250, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 125, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 60, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 30, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 10, new TimeSpan(0, 0, 30) )
        };

        // ItemIds to draw the "current zone" outside of which you
        // will be taking damage. First is the horizontal item
        // (East-West) second is the vertical item (North to South)
        private static int[] _NextZoneItemIds = { 0x3967, 0x3979 };

        // ItemIds to draw the "current zone" outside of which you
        // will be taking damage. First is the horizontal item
        // (East-West) second is the vertical item (North to South)
        private static int[] _CurrentZoneItemIds = { 0x3946, 0x3956 };

        private static int _CurrentStage = 0;

        public static void AdjustZone() {
            ZoneStage stage = _ZoneStages[_CurrentStage];

            _CurrentZone = stage.ToRect(_Map, _ZoneCenter);

            ClearZone();
            DrawZone(_CurrentZone, _CurrentZoneItemIds);

            if ( _CurrentStage < _ZoneStages.Length - 1 ) {
                _NextZone = _ZoneStages[_CurrentStage + 1].ToRect(_Map, _ZoneCenter);
                DrawZone(_NextZone, _NextZoneItemIds);
            }

            Announce("Zone has collapsed, the zone will shrink again in " + stage.Duration.TotalSeconds + " seconds.");

            new GameTimer(stage.Duration, ShrinkZone).Start();
        }

        public static void BeginPlay() {
	    foreach( PlayerMobile pm in _Players ){
		pm.Resurrect();
		_AlivePlayers.Add(pm);
		pm.OnDeathEvent += OnPlayerDeath;
		pm.SendMessage("Last one standing in Moonglow wins!");
	    }

	    _state = BattleState.Playing;
            _CurrentStage = 0;

            // TODO: This is same as the "start location" should be
            // randomly selected from the available play area.
            _ZoneCenter = new Point2D(4420, 1155);

            AdjustZone();
        }

        public static void ShrinkZone() {
            if ( _CurrentStage < _ZoneStages.Length - 1) {
                _CurrentStage++; 
                AdjustZone();
            } else {
                Announce("This is the final zone, last man standing wins.");
            }
        }

        public static void ClearZone() {
            List<Item> oldZone = _ZoneList;
            _ZoneList = new List<Item>();

            foreach ( Item i in oldZone ) {
                i.Delete();
            }
        }

        public static void DrawZone(Rectangle2D zone, int[] itemIds) {
            int x, y, z;
            Item item;

            for ( x = zone.Left ; x < zone.Right ; x++ ) {
                y = zone.Top;
                z = _Map.GetAverageZ(x, y);

                item = new ZoneWall(new Point3D(x, y, z), _Map, itemIds[0]);
                _ZoneList.Add(item);

                y = zone.Bottom;
                z = _Map.GetAverageZ(x, y);

                item = new ZoneWall(new Point3D(x, y, z), _Map, itemIds[0]);
                _ZoneList.Add(item);
            }

            for ( y = zone.Bottom ; y < zone.Top ; y++ ) {
                x = zone.Left;
                z = _Map.GetAverageZ(x, y);

                item = new ZoneWall(new Point3D(x, y, z), _Map, itemIds[1]);
                _ZoneList.Add(item);

                x = zone.Right;
                z = _Map.GetAverageZ(x, y);

                item = new ZoneWall(new Point3D(x, y, z), _Map, itemIds[1]);
                _ZoneList.Add(item);
            }
        }

        private static List<Item> _ZoneList = new List<Item>();

        public class ZoneWall : Item
        {
            public ZoneWall(Point3D loc, Map map, int itemId) : base ( itemId ) {
                Movable = false;
                Visible = true;
                MoveToWorld( loc, map );
            }

            public ZoneWall( Serial serial ) : base( serial ) {}

            public override bool Decays { get{ return false; } }

            public override void Deserialize( GenericReader reader )
            {
                base.Deserialize(reader);
                GameController._ZoneList.Add(this);
            }
        }
    }
}
