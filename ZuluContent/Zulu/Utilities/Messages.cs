using Server;
using Server.Network;
using static Server.Configurations.MessageHueConfiguration;

namespace Scripts.Zulu.Utilities
{
    public static class Messages
    {
        public static void SendSuccessMessage(this Mobile mobile, string text) => 
            mobile.SendAsciiMessage(MessageSuccessHue, text);
        
        public static void SendSuccessMessage(this Mobile mobile, int number, string args = "") =>
            mobile.SendLocalizedMessage(number, args, MessageSuccessHue);
        
        
        
        
        public static void SendFailureMessage(this Mobile mobile, string text) => 
            mobile.SendAsciiMessage(MessageFailureHue, text);
        
        public static void SendFailureMessage(this Mobile mobile, int number, string args = "") =>
            mobile.SendLocalizedMessage(number, args, MessageFailureHue);
        
    }
}