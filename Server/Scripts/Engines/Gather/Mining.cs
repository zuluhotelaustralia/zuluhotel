using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Harvest;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Gather
{
    public class Mining : GatherSystem
    {
	public enum Ores{
	    AnraOre,
	    AzuriteOre,
	    BlackDwarfOre,
	    BronzeOre,
	    CopperOre,
	    CrystalOre,
	    DarkPaganOre,
	    DarkSableRubyOre,
	    DestructionOre,
	    DoomOre,
	    DripstoneOre,
	    DullCopperOre,
	    EbonTwilightSapphireOre,
	    ExecutorOre,
	    FruityOre,
	    GoddessOre,
	    GoldOre,
	    IceRockOre,
	    IronOre,
	    LavarockOre,
	    MalachiteOre,
	    MysticOre,
	    NewZuluOre,
	    OldBritainOre,
	    OnyxOre,
	    PeachblueOre,
	    PlatinumOre,
	    PyriteOre,
	    RadiantNimbusDiamondOre,
	    RedElvenOre,
	    SilverRockOre,
	    SpectralOre,
	    SpikeOre,
	    UndeadOre,
	    VirginityOre
	}
	
	private static int[] m_OreTiles = new int[]
	    {
		220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
		230, 231, 236, 237, 238, 239, 240, 241, 242, 243,
		244, 245, 246, 247, 252, 253, 254, 255, 256, 257,
		258, 259, 260, 261, 262, 263, 268, 269, 270, 271,
		272, 273, 274, 275, 276, 277, 278, 279, 286, 287,
		288, 289, 290, 291, 292, 293, 294, 296, 296, 297,
		321, 322, 323, 324, 467, 468, 469, 470, 471, 472,
		473, 474, 476, 477, 478, 479, 480, 481, 482, 483,
		484, 485, 486, 487, 492, 493, 494, 495, 543, 544,
		545, 546, 547, 548, 549, 550, 551, 552, 553, 554,
		555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
		565, 566, 567, 568, 569, 570, 571, 572, 573, 574,
		575, 576, 577, 578, 579, 581, 582, 583, 584, 585,
		586, 587, 588, 589, 590, 591, 592, 593, 594, 595,
		596, 597, 598, 599, 600, 601, 610, 611, 612, 613,

		1010, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1749,
		1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1771, 1772,
		1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782,
		1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1801, 1802,
		1803, 1804, 1805, 1806, 1807, 1808, 1809, 1811, 1812, 1813,
		1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823,
		1824, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839,
		1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849,
		1850, 1851, 1852, 1853, 1854, 1861, 1862, 1863, 1864, 1865,
		1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875,
		1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1981,
		1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991,
		1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
		2002, 2003, 2004, 2028, 2029, 2030, 2031, 2032, 2033, 2100,
		2101, 2102, 2103, 2104, 2105,

		0x453B, 0x453C, 0x453D, 0x453E, 0x453F, 0x4540, 0x4541,
		0x4542, 0x4543, 0x4544,	0x4545, 0x4546, 0x4547, 0x4548,
		0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E,	0x454F
	    };

	private static int[] m_SandTiles = new int[]
	    {
		22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
		32, 33, 34, 35, 36, 37, 38, 39, 40, 41,
		42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
		52, 53, 54, 55, 56, 57, 58, 59, 60, 61,
		62, 68, 69, 70, 71, 72, 73, 74, 75,

		286, 287, 288, 289, 290, 291, 292, 293, 294, 295,
		296, 297, 298, 299, 300, 301, 402, 424, 425, 426,
		427, 441, 442, 443, 444, 445, 446, 447, 448, 449,
		450, 451, 452, 453, 454, 455, 456, 457, 458, 459,
		460, 461, 462, 463, 464, 465, 642, 643, 644, 645,
		650, 651, 652, 653, 654, 655, 656, 657, 821, 822,
		823, 824, 825, 826, 827, 828, 833, 834, 835, 836,
		845, 846, 847, 848, 849, 850, 851, 852, 857, 858,
		859, 860, 951, 952, 953, 954, 955, 956, 957, 958,
		967, 968, 969, 970,

		1447, 1448, 1449, 1450, 1451, 1452, 1453, 1454, 1455,
		1456, 1457, 1458, 1611, 1612, 1613, 1614, 1615, 1616,
		1617, 1618, 1623, 1624, 1625, 1626, 1635, 1636, 1637,
		1638, 1639, 1640, 1641, 1642, 1647, 1648, 1649, 1650
	    };

	private GatherFXHolder m_EffectsHolder;
	
	private static GatherSystemController m_Controller;
	public static GatherSystemController Controller {
	    get { return m_Controller; }
	}
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	    m_Controller.System = System; //if you use m_System here, it's not initialized and will fuck you up.
	    m_System.SkillName = SkillName.Mining;
	    m_Controller.Name = "Mining System Control Stone";
	    m_Controller.Hue = 0x279;
	    m_Controller.Movable = false;

	    m_System.m_Nodes.Clear();

	    // LB's throne
	    int x = 1323;
	    int y = 1624;
	    
	    Console.WriteLine("Gather Engine: Setting up mining nodes...");	    
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 250.0, 0.0, 70.0, typeof(Server.Items.IronOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 250.0, 10.0, 80.0, typeof(Server.Items.GoldOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 240.0, 10.0, 80.0, typeof(Server.Items.SpikeOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 230.0, 10.0, 80.0, typeof(Server.Items.FruityOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 220.0, 10.0, 80.0, typeof(Server.Items.BronzeOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 210.0, 20.0, 90.0, typeof(Server.Items.IceRockOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 200.0, 20.0, 90.0, typeof(Server.Items.BlackDwarfOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 200.0, 20.0, 90.0, typeof(Server.Items.DullCopperOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.9,
					 190.0, 20.0, 90.0, typeof(Server.Items.PlatinumOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.8,
					 180.0, 30.0, 100.0, typeof(Server.Items.SilverRockOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.8,
					 170.0, 30.0, 100.0, typeof(Server.Items.DarkPaganOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.8,
					 160.0, 30.0, 100.0, typeof(Server.Items.CopperOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.8,
					 150.0, 30.0, 100.0, typeof(Server.Items.MysticOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.8,
					 140.0, 30.0, 100.0, typeof(Server.Items.SpectralOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.7,
					 130.0, 30.0, 100.0, typeof(Server.Items.OldBritainOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.7,
					 120.0, 40.0, 100.0, typeof(Server.Items.OnyxOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.7,
					 110.0, 40.0, 100.0, typeof(Server.Items.RedElvenOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.6,
					 100.0, 50.0, 100.0, typeof(Server.Items.UndeadOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.6,
					 90.0, 50.0, 100.0, typeof(Server.Items.PyriteOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.6,
					 80.0, 50.0, 100.0, typeof(Server.Items.VirginityOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.5,
					 70.0, 50.0, 110.0, typeof(Server.Items.MalachiteOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.5,
					 60.0, 60.0, 120.0, typeof(Server.Items.LavarockOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.5,
					 50.0, 60.0, 130.0, typeof(Server.Items.AzuriteOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.5,
					 50.0, 70.0, 150.0, typeof(Server.Items.DripstoneOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.4,
					 50.0, 70.0, 150.0, typeof(Server.Items.ExecutorOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.4,
					 50.0, 80.0, 150.0, typeof(Server.Items.PeachblueOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.3,
					 50.0, 80.0, 150.0, typeof(Server.Items.DestructionOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.3,
					 50.0, 90.0, 150.0, typeof(Server.Items.AnraOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.2,
					 40.0, 90.0, 150.0, typeof(Server.Items.CrystalOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.2,
					 30.0, 100.0, 150.0, typeof(Server.Items.DoomOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.2,
					 20.0, 100.0, 150.0, typeof(Server.Items.GoddessOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.1,
					 20.0, 100.0, 150.0, typeof(Server.Items.NewZuluOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.1,
					 10.0, 130.0, 150.0, typeof(Server.Items.EbonTwilightSapphireOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.1,
					 10.0, 140.0, 150.0, typeof(Server.Items.DarkSableRubyOre)));
	    m_System.m_Nodes.Add( new GatherNode( x + Utility.RandomMinMax(-100, 100), y + Utility.RandomMinMax(-100, 100), Utility.RandomMinMax(-10, 10), Utility.RandomMinMax(-10, 10), 0.1,
					 10.0, 145.0, 150.0, typeof(Server.Items.RadiantNimbusDiamondOre)));
	    Console.WriteLine("Complete.");
	}
	
	private static Mining m_System;
	public static Mining System
	{    get
	    {
		if ( m_System == null ) {
		    m_System = new Mining();
		}
		
		return m_System;
	    }
	}

	public bool Validate( int tileID ) {
	    return ValidateRock( tileID );
	}
	
	public bool ValidateRock( int tileID )
	{
	    // is this fast enough?  Should it be in its own thread?
	    int dist = -1;
	    for ( int i = 0; dist < 0 && i < m_OreTiles.Length; ++i ){
		dist = ( m_OreTiles[i] - tileID );
		if( dist == 0){
		    return true;
		}
	    }

            return false;
	}

	public bool ValidateSand( int tileID )
	{
	    int dist = -1;
	    
	    for ( int i = 0; dist < 0 && i < m_SandTiles.Length; ++i ){
		dist = ( m_SandTiles[i] - tileID );
		if( dist == 0 ) {
		    return true;
		}
	    }

	    return false;
	}

	public override bool BeginGathering( Mobile from, Item tool )
	{
	    if ( !base.BeginGathering( from, tool ) )
		return false;

	    from.SendLocalizedMessage( 503033 ); // Where do you wish to dig?
	    return true;
	}

	public override void StartGathering( Mobile from, Item tool, object targeted) {
	    int tileID;
	    Point3D loc;

	    if( targeted is Static && !((Static)targeted).Movable ){
		Static obj = (Static)targeted;
		loc = new Point3D(obj.Location);
		tileID = (obj.ItemID & 0x3FFF) | 0x4000; //what the actual fuck does this do?
	    }
	    else if( targeted is StaticTarget ){
		StaticTarget obj = (StaticTarget)targeted;
		loc = new Point3D(obj.Location);
		tileID = (obj.ItemID & 0x3FFF) | 0x4000;
	    }
	    else if( targeted is LandTarget ){
		LandTarget obj = (LandTarget)targeted;
		loc = new Point3D(obj.Location);
		tileID = obj.TileID;
	    }
	    else {
		loc = new Point3D(from.Location);
		tileID = 0;
	    }

	    if( ValidateRock( tileID ) ) {
		m_EffectsHolder.PlayEffects( from, loc );
		base.StartGathering( from, tool, targeted );
	    }
	    if( ValidateSand( tileID ) ) {
		m_EffectsHolder.PlayEffects( from, loc ); //TODO: change these to be appropriate SFX/VFX and cliloc msgs
		base.StartGathering( from, tool, targeted, true );
	    }
	}

	
	public void OnBadGatherTarget( Mobile from, Item tool, object toHarvest )
	{
	    if ( toHarvest is LandTarget )
		from.SendLocalizedMessage( 501862 ); // You can't mine there.
	    else
		from.SendLocalizedMessage( 501863 ); // You can't mine that.
	}

	public bool CheckHarvest( Mobile from, Item tool, object toHarvest )
	{
            // TODO: No base implementation yet, do we need one?
	    // if ( !base.CheckHarvest( from, tool, toHarvest ) )
	    //     return false;

            /*	    if ( def == m_Sand && !(from is PlayerMobile && from.Skills[SkillName.Mining].Base >= 100.0 && ((PlayerMobile)from).SandMining) )
	    {
		OnBadGatherTarget( from, tool, toHarvest );
		return false;
	    }
	    else */if ( from.Mounted )
	    {
		from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
		return false;
	    }
	    else if ( from.IsBodyMod && !from.Body.IsHuman )
	    {
		from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
		return false;
	    }

	    return true;
	}

	public override void StartGatherTimer( Mobile from, Item tool, GatherSystem system, GatherNode node, object targeted, object locked ) {
	    TimeSpan delay = m_EffectsHolder.EffectDelay;
	    int which = TimeSpan.Compare(m_EffectsHolder.EffectDelay, m_EffectsHolder.EffectSoundDelay);
	    // if which == -1, argument 1 shorter than arg2
	    // if which == 0, they're equal
	    // if which == 1, arg1 longer than arg2
	    // therefore
	    
	    if ( which < 1 ) {
		delay = m_EffectsHolder.EffectSoundDelay;
	    }
	    
	    new GatherTimer( from, tool, system, node, targeted, locked, delay ).Start();
	}

	
	
	public override void SendFailMessage( Mobile m ) {
	    m.SendLocalizedMessage( 503043 ); // You loosen some rocks but...
	}

	public override void SendNoResourcesMessage( Mobile m) {
	    m.SendLocalizedMessage( 503040 ); //there's no metal here to mine
	}

	public override void SendSuccessMessage( Mobile m ) {
	    m.SendLocalizedMessage( 1044606 ); //you extract some bla bla bla
	}

	public override void SendSandFailMessage( Mobile m ) {
	    m.SendLocalizedMessage(1044630);
	}
	public override void SendSandNoResourcesMessage( Mobile m ) {
	    m.SendLocalizedMessage(1044629);
	}
	public override void SendSandSuccessMessage( Mobile m ) {
	    m.SendLocalizedMessage(1044631);
	}
	
	private Mining()
	{
	    m_Nodes = new List<GatherNode>();
	    m_SandNodes = new List<GatherNode>();
	    m_EffectsHolder = new GatherFXHolder();

	    int x = 1323;
	    int y = 1624;

	    GatherNode node = new GatherNode( x, y, 10, 10, 0.9, 250.0, 100.0, 150.0, typeof(IronOre));
	    m_Nodes.Add(node);

	    m_SandNodes.Add( new GatherNode( x, y, 0, 0, 0.9, 250.0, 0.0, 150.0, typeof(Server.Items.Sand) ) );
	    
	    m_EffectsHolder.EffectActions = new int[]{ 11 };
	    m_EffectsHolder.EffectSounds = new int[]{ 0x125, 0x126 };
	    m_EffectsHolder.EffectCounts = new int[]{ 1 };
	    m_EffectsHolder.EffectDelay = TimeSpan.FromSeconds( 0.9 );
	    m_EffectsHolder.EffectSoundDelay = TimeSpan.FromSeconds( 1.6 );

	    // oreAndStone.NoResourcesMessage = 503040; // There is no metal here to mine.
	    // oreAndStone.DoubleHarvestMessage = 503042; // Someone has gotten to the metal before you.
	    // oreAndStone.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
	    // oreAndStone.OutOfRangeMessage = 500446; // That is too far away.
	    // oreAndStone.FailMessage = 503043; // You loosen some rocks but fail to find any useable ore.
	    // oreAndStone.PackFullMessage = 1010481; // Your backpack is full, so the ore you mined is lost.
	    // oreAndStone.ToolBrokeMessage = 1044038; // You have worn out your tool!

	    // sand.NoResourcesMessage = 1044629; // There is no sand here to mine.
	    // sand.DoubleHarvestMessage = 1044629; // There is no sand here to mine.
	    // sand.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
	    // sand.OutOfRangeMessage = 500446; // That is too far away.
	    // sand.FailMessage = 1044630; // You dig for a while but fail to find any of sufficient quality for glassblowing.
	    // sand.PackFullMessage = 1044632; // Your backpack can't hold the sand, and it is lost!
	    // sand.ToolBrokeMessage = 1044038; // You have worn out your tool!

	}

    }
}
