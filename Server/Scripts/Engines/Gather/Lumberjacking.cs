using Server.Items;

namespace Server.Engines.Gather {
    public class Lumberjacking : Server.Engines.Gather.GatherSystem {

	public enum Logs{
	    Log,
	    PinetreeLog,
	    CherryLog,
	    OakLog,
	    PurplePassionLog,
	    GoldenReflectionLog,
	    HardrangerLog,
	    JadewoodLog,
	    DarkwoodLog,
	    StonewoodLog,
	    SunwoodLog,
	    GauntletLog,
	    SwampwoodLog,
	    StardustLog,
	    SilverleafLog,
	    StormtealLog,
	    EmeraldLog,
	    BloodwoodLog,
	    CrystalLog,
	    BloodhorseLog,
	    DoomwoodLog,
	    ZuluLog,
	    DarknessLog,
	    ElvenLog
	}

	private static GatherSystemController m_Controller;
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	}
	
        private static Lumberjacking m_System;
        public static Lumberjacking System {
            get {
                if ( m_System == null ) m_System = new Lumberjacking();
                return m_System;
            }
            set {
                m_System = value;
            }
        }
    }
}
