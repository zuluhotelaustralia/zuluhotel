namespace Servers.Engines.Harvest.GatherSystem {
    public class Mining : Server.Engines.Harvest.GatherSystem {
        private static Mining m_System;
        public static Mining System {
            get {
                if ( m_System == null ) m_System = new Mining();
                return m_System;
            }
            set {
                m_System = value;
            }
        }
    }
}
