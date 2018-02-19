using Server;

namespace Server.Targeting {
    public interface IMobileTargeted {
        public void OnTarget( Mobile from, Mobile target );
        public void OnTargetFinished( Mobile from )
    }
}
