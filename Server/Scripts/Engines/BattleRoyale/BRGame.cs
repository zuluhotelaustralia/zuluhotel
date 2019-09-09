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

	private static bool _debug;
	
	[CommandProperty(AccessLevel.Developer)]
	public bool Debug {
	    get { return _debug; }
	    set { _debug = value; }
	}
       
	private static BattleState _state = BattleState.Idle;

       	public const int PlayerCap = 30;
	public const double HoursTilNextGame = 2;
	public const int ZoneDamageMultiplier = 10;
	public const int ZoneDamageInterval = 5;
	
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
	
	private static List<Mobile> _Players;
	private static List<Mobile> _AlivePlayers;

	public static void Initialize() {

	    //all commands go here, no more splitting it up between several files.
	    // I'm looking at *you*, sith.
	    // --sith

	    _Players = new List<Mobile>();
	    _AlivePlayers = new List<Mobile>();

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
	public static void TryUnregisterPlayer( Mobile pm ) {
	    if( _Players.Contains( pm ) ){
		_Players.Remove( pm );
		pm.SendMessage("You have been removed from the list of Battle Royale players.");
	    }
	}

	public static bool TryRegisterPlayer( Mobile pm ){
	    if( _Players.Count < PlayerCap ){
		pm.SendMessage("You have registered for Battle Royale.  Safely unequip and store your items before the game begins!"); //TODO cliloc this
		pm.SendMessage("To unregister, double-click the stone again.");
		_Players.Add(pm);

		if( _Players.Count == PlayerCap && _state == BattleState.Joining ){
		    //might as well start game early if it fills up.
		    foreach( PlayerMobile p in _Players ){
			p.SendMessage("Game queue is full, starting play in 30 seconds!");
		    }

		    new GameTimer(TimeSpan.FromSeconds(30), EndJoining ).Start();
		}
		
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
	    _debug = false;
	}

	public GameController( Serial serial ) : base( serial ){
	    // if the server goes down the game just ends and everyone can just deal with it
	    // not going to try to wrestle the complexity of having a minigame traverse server
	    // restarts
	}

	public override void Serialize( GenericWriter writer ){
	    base.Serialize(writer);
	    writer.Write( (int) 0 ); //version

	    writer.Write(_debug);
	}

	public override void Deserialize(GenericReader reader ){
	    base.Deserialize( reader );
	    int version = reader.ReadInt();

	    _debug = reader.ReadBool();
	}

	public override void OnDoubleClick( Mobile mob ){
	    Mobile from = mob as Mobile;

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
	    foreach( PlayerMobile p in _Players ){
		p.SendMessage( s );
		// later, change this to broadcast via the Town Criers TODO

		if( _debug ){
		    Console.WriteLine(s);
		}
	    }
	}
	
	public static void BeginJoining() {
	    if( _state != BattleState.Joining ){
		_state = BattleState.Joining;
		Announce("Battle Royale is now open for joining!  Game starts in 10 minutes!");

		TimeSpan ts;
		
		if( _debug ) {
		    ts = TimeSpan.FromSeconds(60);
		}
		else {
		    ts = TimeSpan.FromMinutes(10);
		}
		
		GameTimer jt = new GameTimer( ts, EndJoining );
		jt.Start();
	    }
	}

	public static void EndJoining(){
            if( _debug ){
		Announce("End Joining");
	    }

	    if( _Players.Count < 2 ) {
		Announce("Not enough players have joined the game.  Queue will open again in 1 hour.");
		_state = BattleState.Idle;
		_Players.Clear();
		GameTimer nextgame = new GameTimer( TimeSpan.FromHours(1), BeginJoining );
		nextgame.Start();
		
		return;
	    }
	    
	    _state = BattleState.Parachuting;
	    // kill everyone, move em to arena, set a res timer that calls BeginPlay()

	    foreach( Mobile pm in _Players ){
		if( pm.NetState != null ){
		    pm.Kill();
		    pm.MoveToWorld(_StartLoc, _Map);
		    pm.SendMessage("You will be automatically resurrected in 90 seconds, at which point the battle royale will begin!"); //TODO cliloc this
		}
		else{
		    TryUnregisterPlayer( pm );
		}
	    }
	    
	    Announce("Battle Royale has started!");
	    GameTimer rt = new GameTimer( TimeSpan.FromSeconds(90) , BeginPlay);
	    rt.Start();
	}
	
	private static void OnPlayerDeath( Mobile.OnDeathEventArgs a) {
	    Mobile pm = a.Mobile as Mobile;
	    pm.OnDeathEvent -= OnPlayerDeath;
	    _AlivePlayers.Remove(pm);
	    _Players.Remove(pm);
	    pm.SendMessage("You have died during Battle Royale.  You can continue to observe as a ghost.  Use the Escape command to leave the arena.");

	    foreach( Mobile player in _AlivePlayers ){
		player.SendMessage("{0} was killed by {1}.  {2} players remaining.", pm, pm.LastKiller, _AlivePlayers.Count );
	    }

	    if( CheckVictory() ){
		Mobile victor = _AlivePlayers[0];
		Announce("Winner winner, chicken dinner!  Battle Royale has ended!");
		_state = BattleState.Idle;
		victor.SendMessage("Congratulations!  You will be teleported out of the arena in 15 seconds.");
		GameTimer repatriator = new GameTimer( TimeSpan.FromSeconds(15), EndGame );
		repatriator.Start();
	    }
	}

	public static void EndGame() {
	    Mobile victor = _AlivePlayers[0];

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

	private static void Slap( Mobile pm ) {
	    pm.Damage( _CurrentStage.DamageLevel * ZoneDamageMultiplier );
	    pm.RevealingAction();
	}

	// should we proc the zone damage?
	private static void HandleZoneDamageTimer(){
	    if( _state == BattleState.Playing ){
		ProcZoneDamage();
		new GameTimer( TimeSpan.FromSeconds( ZoneDamageInterval ), HandleZoneDamageTimer ).Start();
	    }
	}
	    
	//check if every alive player is in the zone and if not, damage them
	private static void ProcZoneDamage(){
	    foreach( Mobile pm in _AlivePlayers ){
		if( !_CurrentZone.Contains( pm.Location )) {
		    //if they're not in the zone
		    Slap(pm);
		    pm.SendMessage("The acid rain outside the magic safe zone damages you!"); //TODO cliloc this
		}
	    }
	}

        public class ZoneStage {
            private int _size;
            private TimeSpan _duration;
            private int _damageLevel;

            public ZoneStage(int size, int damageLevel, TimeSpan duration)
            {
                _size = size;
                _duration = duration;
                _damageLevel = damageLevel;
            }

            public TimeSpan Duration{ get{ return _duration; } }
            public int Size{ get{ return _size; } }
            public int DamageLevel{ get{ return _damageLevel; } }

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

        private static List<ZoneStage> _ZoneStages = new List<ZoneStage> {
            new ZoneStage( 250, 1, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 125, 2, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 60, 2, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 30, 3, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 15, 3, new TimeSpan(0, 0, 30) ),
            new ZoneStage( 5, 4, new TimeSpan(0, 0, 300) ),
            new ZoneStage( 1, 5, new TimeSpan(0, 0, 300) )
        };

	// yes, these are hand picked.  Don't judge me.
	private static Point2D[] _FinalZoneCenters = {
	    new Point2D( 4548, 806 ),
	    new Point2D( 4545, 867 ),
	    new Point2D( 4543, 898 ),
	    new Point2D( 4542, 941 ),
	    new Point2D( 4567, 947 ),
	    new Point2D( 4527, 997 ),
	    new Point2D( 4513, 1042 ),
	    new Point2D( 4491, 1057 ),
	    new Point2D( 4453, 1102 ),
	    new Point2D( 4477, 1170 ),
	    new Point2D( 4551, 1145 ),
	    new Point2D( 4576, 1179 ),
	    new Point2D( 4633, 1202 ),
	    new Point2D( 4711, 1123 ),
	    new Point2D( 4645, 1184 ),
	    new Point2D( 4417, 1227 ),
	    new Point2D( 4387, 1260 ),
	    new Point2D( 4454, 1255 ),
	    new Point2D( 4496, 1218 ),
	    new Point2D( 4528, 1283 ),
	    new Point2D( 4536, 1314 ),
	    new Point2D( 4550, 1332 ),
	    new Point2D( 4546, 1353 ),
	    new Point2D( 4525, 1378 ),
	    new Point2D( 4522, 1407 ),
	    new Point2D( 4516, 1438 ),
	    new Point2D( 4490, 1470 ),
	    new Point2D( 4468, 1553 ),
	    new Point2D( 4415, 1492 ),
	    new Point2D( 4416, 1447 ),
	    new Point2D( 4587, 1457 ),
	    new Point2D( 4577, 1483 ),
	    new Point2D( 4653, 1424 ),
	    new Point2D( 4665, 1382 ),
	    new Point2D( 4624, 1297 ),
	    new Point2D( 4399, 1052 ),
	    new Point2D( 4419, 1111 ),
	    new Point2D( 4408, 1148 ),
	    new Point2D( 4378, 1180 ),
	    new Point2D( 4296, 1070 ),
	    new Point2D( 4326, 1002 ),
	    new Point2D( 4312, 974 ),
	    new Point2D( 4317, 954 ),
	    new Point2D( 4296, 962 ),
	    new Point2D( 4315, 918 )
	};

        // ItemIds to draw the "next zone," what will become the
        // "current zone" when the timer goes.  First is the
        // horizontal item (East-West) second is the vertical item
        // (North to South). Be careful to set these to an itemId that
        // the client believes it can walk through, otherwise people
        // will be unable to enter/exit the zone.

        private static int[] _NextZoneItemIds = { 0x3967, 0x3979 };

        // ItemIds to draw the "current zone" outside of which you
        // will be taking damage. First is the horizontal item
        // (East-West) second is the vertical item (North to
        // South). Be careful to set these to an itemId that the
        // client believes it can walk through, otherwise people will
        // be unable to enter/exit the zone.
        private static int[] _CurrentZoneItemIds = { 0x3915, 0x3922 };

        private static IEnumerator<ZoneStage> _CurrentStageEnumerator;
        private static IEnumerator<ZoneStage> _NextStageEnumerator;
        private static ZoneStage _CurrentStage;
        private static ZoneStage _NextStage;

        public static void BeginPlay() {
	    foreach( Mobile pm in _Players ){
		if( pm.NetState != null ){
		    pm.Resurrect();

		    pm.Hits = pm.Str;
		    pm.Stam = pm.Dex;
		    pm.Mana = pm.Int;

		    _AlivePlayers.Add(pm);
		    pm.OnDeathEvent += OnPlayerDeath;
		    pm.SendMessage("Last one standing in Moonglow wins!");
		}
		
	    }

            _state = BattleState.Playing;

            _ZoneCenter = _FinalZoneCenters[ Utility.RandomMinMax(0, _FinalZoneCenters.Length - 1) ];

            _CurrentStageEnumerator = _ZoneStages.GetEnumerator();
            _NextStageEnumerator = _ZoneStages.GetEnumerator();

            _NextStageEnumerator.MoveNext();

            ZoneTick();
	    HandleZoneDamageTimer();
        }

        public static void ZoneTick() {
            ClearZone();

            if ( _CurrentStageEnumerator.MoveNext() ) {
                _CurrentStage = _CurrentStageEnumerator.Current;
                _CurrentZone = _CurrentStage.ToRect(_Map, _ZoneCenter);

                if( _debug ){
		    Announce("Current zone is " + _CurrentStage.Size);
		}
                
                DrawZone(_CurrentZone, _CurrentZoneItemIds);

		if( _state == BattleState.Playing ){
		    new GameTimer(_CurrentStageEnumerator.Current.Duration, ZoneTick).Start();
		}
            }
            
            if ( _NextStageEnumerator.MoveNext() ) {
                _NextStage = _NextStageEnumerator.Current;
                _NextZone = _NextStage.ToRect(_Map, _ZoneCenter);

		if( _debug ){
		    Announce("Next zone is " + _NextStage.Size);
                }
		
                DrawZone(_NextZone, _NextZoneItemIds);
                
                Announce("Zone will collapse in " + _CurrentStage.Duration.TotalSeconds + " seconds");
            } else {
                Announce("This is the final zone.");
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

	    public override void Serialize( GenericWriter writer ) {
		base.Serialize( writer );
	    }

            public override void Deserialize( GenericReader reader )
            {
                base.Deserialize(reader);
                GameController._ZoneList.Add(this);
            }
        }
    }
}
