namespace Server.Engines.Gather {
    public class Lumberjacking : Server.Engines.Gather.GatherSystem {
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
