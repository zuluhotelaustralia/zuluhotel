namespace Servers.Engines.Harvest.GatherSystem {
    public class Lumberjacking : Server.Engines.Harvest.GatherSystem {
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
