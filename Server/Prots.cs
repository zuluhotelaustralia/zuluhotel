using System;

using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server {
    private class InternalTarget : Target{
	public InternalTarget( ) : base( 12, false, TargetFlags.None ){
	}

	protected override void OnTarget( Mobile from, object o ){
	    if( o is Mobile){
		
	}
    }
    
    public class Prots {

	public static void Initialize(){
	    CommandSystem.Register("GetProts", AccessLevel.GameMaster, new CommandEventHandler( GetProts_OnCommand ) );
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
	
	public Prots( Mobile parent ){
	    _parent = parent;
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

       	public void UpdateProts(){
	    
	}

	private int Assess(Item theitem){
	    if( !(theitem is BaseArmor) ){
		return 0;
	    }
	    Item ar = theitem as BaseArmor;
	    CraftResource cr = ar.Resource;
	    switch(cr){
		case CraftResource.Lavarock:
		    return 50;
		    break;
		case CraftResource.SilverRock:
		    
	private int NeckProt(){
	}

	private int HandsProt(){}

	private int ArmsProt(){}
	private int HeadProt(){}
	private int LegsProt(){}
	private int ChestProt(){}
	
    }
}
