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

	private Type m_Res; //what resource are we spawning here?
	private double m_Abundance; //how plentiful is this resource?  a value on the range [0,1]

	private const double m_driftchance = 0.5;
	private const double m_mutatechance = 0.1;

	private const int m_xbound = 7168;
	private const int m_ybound = 4096;

	//difficulty represents the difficulty to actually find it
	// graphically, this is the fall-off rate per unit distance (higher is slower fall-off)
	//set iron to 2500.0, set nimbus and other rare stuff to 10, set intermediate resources in the 100 range
	private double m_Difficulty;
	public double Difficulty { get { return m_Difficulty; } }
	
	private double m_MinSkill;
	private double m_MaxSkill;

	public int X { get { return m_X; } set { m_X = value; } }
	public int Y { get { return m_Y; } set { m_Y = value; } }
	public int vX { get { return m_vX; } set { m_vX = value; } }
	public int vY { get { return m_vY; } set { m_vY = value; } }
	public double MinSkill { get { return m_MinSkill; } set { m_MinSkill = value; } }
	public double MaxSkill { get { return m_MaxSkill; } set { m_MaxSkill = value; } }
	public Type Resource { get { return m_Res; } set { m_Res = value; } }
	public double Abundance { get { return m_Abundance; } set { m_Abundance = value; } }

	public override string ToString() {
	    return "GatherNode: " + m_Res.ToString() + " " + X + " " + Y + " " + Abundance + " " + Difficulty;
	}
	
	public GatherNode() {
	    m_X = 0;
	    m_Y = 0;
	    m_vX = 0;
	    m_vY = 0;
	    m_Difficulty = 100.0;
	    m_MinSkill = 0.0;
	    m_MaxSkill = 150.0;
	    m_Abundance = 0;

	    m_Res = typeof(IronOre);
	}

	public GatherNode( Type res ) : this() {
	    m_Res = res;
	}

	public GatherNode( int initialX, int initialY, int dirX, int dirY, double a, double d, double minskill, double maxskill, Type res ){
	    m_X = initialX;
	    m_Y = initialY;
	    m_vX = dirX;
	    m_vY = dirY;
	    m_Abundance = a;
	    m_Difficulty = d;
	    m_Res = res;
	    m_MinSkill = minskill;
	    m_MaxSkill = maxskill;
	}

	public void Mutate() {
	    if( m_mutatechance <= Utility.RandomDouble() ) {
		m_vX = Utility.RandomMinMax(-10, 10);
		m_vY = Utility.RandomMinMax(-10, 10);
	    }
	}

	public void Drift() {
	    
	    if ( m_driftchance >= Utility.RandomDouble() ) {

		//don't ask just let it happen
		if( m_X < 0 ){
		    m_X *= -1;
		}
		if( m_Y < 0 ){
		    m_Y *= -1;
		}
		
		m_X = ( m_X + m_vX ) % m_xbound;
		m_Y = ( m_Y + m_vY ) % m_ybound;
		
		Mutate(); //keep it gangsta
	    }
	}
    }
}
