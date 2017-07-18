//all RunZH-native gathering stuff that I write will be "GatherNode"-like in naming schemes, to distinguish
//from RunUO-native stuff following the "HarvestXYZ" naming convention, until we can orphan these things out
// --sith
using System;
using Server.Items;
using Server.Engines.Harvest;

namespace Server.Engines.Gather {
    public class GatherNode {
	private int m_X;
	private int m_Y; //our coordinates on the map plane

	private int m_vX;
	private int m_vY; //2D direction vector for shifting locations

	private HarvestResource m_Res; //what resource are we spawning here?
	private double m_Abundance; //how plentiful is this resource?  a value on the range [0,1]

	private const double m_driftchance = 0.5;
	
	//minskill and maxskill are handled by the resource
	//skill is handled by the derived type

	public int X { get { return m_X; } set { m_X = value; } }
	public int Y { get { return m_Y; } set { m_Y = value; } }
	public int vX { get { return m_vX; } set { m_vX = value; } }
	public int vY { get { return m_vY; } set { m_vY = value; } }
	public HarvestResource Resource { get { return m_Res; } set { m_Res = value; } }
	public double Abundance { get { return m_Abundance; } set { m_Abundance = value; } }

	public GatherNode() {
	    m_X = 0;
	    m_Y = 0;
	    m_vX = 0;
	    m_vY = 0;
	    m_Abundance = 0;

	    //reqskill, minskill, maxskill, obj message, type
	    m_Res = new HarvestResource( 0.00, 0.00, 100.00, 1007072, typeof( IronOre ) );
	}

	public GatherNode( HarvestResource res ) : this() {
	    m_Res = res;
	}

	public GatherNode( int initialX, int initialY, int dirX, int dirY, double a, HarvestResource res ){
	    m_X = initialX;
	    m_Y = initialY;
	    m_vX = dirX;
	    m_vY = dirY;
	    m_Abundance = a;
	    m_Res = res;
	}

	private void ApplyRotation( int x, int y ) {
	    //normally on a 2D Euclidean plane, a 90 degree rotation is
	    //  [ 0 -1 ]
	    //  [ 1  0 ]
	    //
	    //  however on the UO map, y is in the downward/southward direction, so
	    //  [  0 1 ]
	    //  [ -1 0 ]
	    //
	    // therefore,
	    
	    m_vX = y;
	    m_vY = -x;
	}

	private bool WouldNextDriftBeIllegal () {
	    int xprime;
	    int yprime;

	    xprime = m_X + m_vX;
	    yprime = m_Y + m_vY;

	    if ( xprime >= 895 ||
		 xprime <= 0 ||
		 yprime >= 512 ||
		 yprime <= 0 ){
		return true;
	    }
	    else {
		return false;
	    }
	}
	
	public void Drift( ) {
	    
	    if ( m_driftchance >= Utility.RandomDouble() ) {

		if ( WouldNextDriftBeIllegal() ) {
		    ApplyRotation( m_vX, m_vY );
		}
		
		m_X += m_vX;
		m_Y += m_vY;
	    }
	}
    }
}
