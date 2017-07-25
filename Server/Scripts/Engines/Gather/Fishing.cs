using Server.Items;

namespace Server.Engines.Gather {
    public class Fishing : Server.Engines.Gather.GatherSystem {

	public enum Fish {
	    Fish,
	    BigFish
	}

	private static GatherSystemController m_Controller;
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	}
	
        private static Fishing m_System;
        public static Fishing System {
            get {
                if ( m_System == null ) m_System = new Fishing();
                return m_System;
            }
            set {
                m_System = value;
            }
        }
    }
}
