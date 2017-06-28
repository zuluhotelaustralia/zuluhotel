namespace Servers.Engines.Harvest.GatherSystem {
    public class Fishing : Server.Engines.Harvest.GatherSystem {
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
