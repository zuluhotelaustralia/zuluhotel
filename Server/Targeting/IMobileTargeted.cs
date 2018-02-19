using Server;

namespace Server.Targeting {
    public interface IMobileTargeted {
        void OnTarget( Mobile from, Mobile target );
        void OnTargetFinished( Mobile from );
    }
}
