using Server;

namespace Server.Targeting {
    public class MobileTarget : Target {
        IMobileTargeted m_Receiver;

        public MobileTarget( IMobileTargeted receiver, int range, TargetFlags flags ) : base( range, false, flags ) {
            m_Receiver = receiver;
        }

        protected override void OnTarget( Mobile from, object o ) {
            if ( ! o is Mobile ) {
                Console.Error.WriteLine("MobileTarget received non-mobile as a target.  Sounds like a bug or buggy client.");
            }

            m_Receiver.OnTarget( from, (Mobile) o );
        }

        protected override OnTargetFinished( Mobile from ) {
            m_Receiver.OnTargetFinished( from );
        }
    }
}
