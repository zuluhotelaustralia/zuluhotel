using System;

using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server {

    // this is the object that goes onto Mobile to track prots.
    // the Prot class (no 's') is what we attach to items to give them a single prot.
    public class Prots {
	public static void Initialize(){
	    CommandSystem.Register("GetProts", AccessLevel.GameMaster, new CommandEventHandler( GetProts_OnCommand ) );
	    CommandSystem.Register("Prots", AccessLevel.Player, new CommandEventHandler( Prots_OnCommand ) );
	}

	public static void Prots_OnCommand( CommandEventArgs e ){
	    PlayerMobile pm = e.Mobile as PlayerMobile;
	    pm.Prots.UpdateProts();
	    pm.SendMessage("Air: {0}, Earth: {1}, Fire: {2}, Necro: {3}, Water: {4}, Poison: {5}", pm.Prots.Air, pm.Prots.Earth, pm.Prots.Fire, pm.Prots.Necro, pm.Prots.Water, pm.Prots.Poison );
	}
	    
	public static void GetProts_OnCommand( CommandEventArgs e ){
	    PlayerMobile pm = e.Mobile as PlayerMobile;

	    pm.Target = new InternalTarget();
	}
	private Mobile _parent;

	private int _fire;
	private int _water;
	private int _air;
	private int _earth;
	private int _necro;
	private int _poison;
	
	public Prots( Mobile parent ){
	    _parent = parent;
	    _air = 0;
	    _fire = 0;
	    _earth = 0;
	    _water = 0;
	    _necro = 0;
	    _poison = 0;
	}

	public int Fire {
	    get { return _fire; }
	    set { _fire = value; }
	}
	public int Water {
	    get { return _water; }
	    set { _water = value; }
	}
	public int Air {
	    get { return _air; }
	    set { _air = value; }
	}
	public int Earth {
	    get { return _earth; }
	    set { _earth = value; }
	}
	public int Necro {
	    get { return _necro; }
	    set { _necro = value; }
	}
	public int Poison {
	    get { return _poison; }
	    set { _poison = value; }
	}

       	public void UpdateProts(){
	    _air = PiecewiseScale(DamageType.Air);
	    _earth = PiecewiseScale(DamageType.Earth);
	    _fire = PiecewiseScale(DamageType.Fire);
	    _necro = PiecewiseScale(DamageType.Necro);
	    _water = PiecewiseScale(DamageType.Water);
	    _poison = PiecewiseScale(DamageType.Poison);
	}

	//index of the matrix corresponds to a server.layer
       	public static readonly double[] Scalars = {
	    0.00, 0.1, 0.5, 0.05, 0.3, 0.4, 0.2, 0.1,
	    0.3, 0.3, 0.3, 0.0, 0.4, 0.4, 0.3, 0.0,
	    0.0, 0.4, 0.3, 0.2, 0.4, 0.0, 0.4, 0.3,
	    0.3 };

	private int PiecewiseScale( DamageType dt ){
	    double tally = 0;

	    foreach( Layer l in Enum.GetValues( typeof(Server.Layer) ) ){
		if( l < Layer.FirstValid ){
		    continue;
		}
		if( l > Layer.LastUserValid ){
		    break;
		}

		int prot = 0;
		Item theitem = _parent.FindItemOnLayer( l );

		if( theitem != null ){
		    if( theitem is BaseArmor || theitem is BaseJewel ){
			tally += ( Scalars[ (int)l ] * Assess( theitem, dt ) );
		    }
		    else if( theitem is BaseClothing ){
			BaseClothing c = theitem as BaseClothing;
			if( c.Prot.Element == dt ){
			    tally += ( Scalars[ (int)l ] * c.Prot.Level );
			}
		    }
		}
	    }

	    if( tally > 3 ){
		tally = 3; //hard cap at 75% prot
	    }
	    /*if( tally > 0 && tally < 1.0 ){
		tally = 1.0;
	    }*/
	    
	    return (int)tally;
	}

	private int Assess(Item theitem, DamageType dmgtype){
	    CraftResource cr;
	    
	    if( theitem is BaseArmor ){
		cr = ((BaseArmor)theitem).Resource;
	    }
	    else if( theitem is BaseJewel ){
		cr = ((BaseJewel)theitem).Resource;
	    }
	    else {
		return 0;
	    }

	    return GetResist( cr, dmgtype );
	}

	private int GetResist( CraftResource cr, DamageType dmgtype ){
	    switch(dmgtype){
		case DamageType.Air:
		    if( cr == CraftResource.Azurite ){
			return 2;
		    }
		    if( cr == CraftResource.Goddess ){
			return 1;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ) {
			return 3;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 1;
		    }
		    break;
		case DamageType.Earth:
		    if( cr == CraftResource.Destruction ){
			return 1;
		    }
		    if( cr == CraftResource.Crystal ){
			return 1;
		    }
		    if( cr == CraftResource.WyrmLeather ){
			return 1;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 1;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ){
			return 3;
		    }
		    break;
		case DamageType.Fire:
		    if( cr == CraftResource.Lavarock ){
			return 2;
		    }
		    if( cr == CraftResource.LavaLeather ){
			return 2;
		    }
		    if( cr == CraftResource.WyrmLeather ){
			return 2;
		    }
		    if( cr == CraftResource.GoldenDragonLeather ){
			return 3;
		    }
		    if( cr == CraftResource.DarkSableRuby ){
			return 3;
		    }
		    break;
		case DamageType.Necro:
		    if( cr == CraftResource.SilverRock ){
			return 1;
		    }
		    if( cr == CraftResource.LicheLeather ) {
			return 1;
		    }
		    if( cr == CraftResource.NecromancerLeather ){
			return 1;
		    }
		    if( cr == CraftResource.BalronLeather ) {
			return 1;
		    }
		    if( cr == CraftResource.Undead ){
			return 2;
		    }
		    if( cr == CraftResource.Virginity ){
			return 2;
		    }
		    if( cr == CraftResource.RadiantNimbusDiamond ){
			return 3;
		    }
		    break;
		case DamageType.Water:
		    if( cr == CraftResource.IceRock ){
			return 1;
		    }
		    if( cr == CraftResource.Dripstone ){
			return 1;
		    }
		    if( cr == CraftResource.IceCrystalLeather ){
			return 2;
		    }
		    if( cr == CraftResource.EbonTwilightSapphire ){
			return 3;
		    }
		    break;
		default:
		    return 0;
	    }
	    return 0;
	}
		
	private class InternalTarget : Target{
	    public InternalTarget( ) : base( 12, false, TargetFlags.None ){
	    }

	    protected override void OnTarget( Mobile from, object o ){
		if( o is Mobile){
		    Mobile m = o as Mobile;
		    from.SendMessage("Air: {0}, Earth: {1}, Fire: {2}, Necro: {3}, Water: {4}", m.Prots.Air, m.Prots.Earth, m.Prots.Fire, m.Prots.Necro, m.Prots.Water );
		}
		else {
		    from.SendMessage("Can only target Mobiles");
		}
	    }
	}
    }

    public class Prot {
	private int _level;
	private DamageType _element;

	public Prot() : this( DamageType.None, 0 ){}
	public Prot( DamageType element, int level ){
	    _level = level;
	    _element = element;
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public int Level {
	    get{ return _level; }
	    set{ _level = value; }
	}

	[CommandProperty( AccessLevel.GameMaster )]
	public DamageType Element{
	    get{ return _element; }
	    set{ _element = value; }
	}

	public void Serialize( GenericWriter w ){
	    w.Write( (int)0 ); //version

	    w.Write( _level );
	    w.Write( (int)_element );
	}

	public void Deserialize( GenericReader r ){
	    int version = r.ReadInt();

	    _level = r.ReadInt();
	    _element = (DamageType)r.ReadInt();
	}
    }
}
