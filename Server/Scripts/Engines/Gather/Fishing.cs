using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Engines.Gather {
    public class Fishing : Server.Engines.Gather.GatherSystem {

	public enum Fish {
	    Fish,
	    BigFish
	}

	private Fishing( Serial serial ) : this() {
	}

	private Fishing() {
	    m_Nodes = new List<GatherNode>();
	    GatherNode node = new GatherNode (0, 0, Utility.RandomMinMax(0,10), Utility.RandomMinMax(0,10), Utility.RandomDouble(), 250.0, 100.0, 150.0, typeof( Fish ) );
	    m_Nodes.Add(node);
	}
	
	private static GatherSystemController m_Controller;
	public static GatherSystemController Controller {
	    get { return m_Controller; }
	}
	
	public static void Setup( GatherSystemController stone ) {
	    m_Controller = stone;
	    m_Controller.System = System; //see Mining.cs
	    m_System.SkillName = SkillName.Fishing;
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
