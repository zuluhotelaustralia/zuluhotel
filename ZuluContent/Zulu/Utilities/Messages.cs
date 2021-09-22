using Server;
using Server.Misc;
using Server.Network;
using static Server.Configurations.MessagingConfiguration;

namespace Scripts.Zulu.Utilities
{
    public static class Messages
    {
        private static void SendAscii(Mobile mobile, bool success, TextDefinition def, string args = "")
        {
            if (def.IsString)
                mobile.SendAsciiMessage(
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    def.String
                );
            else if (def.IsNumber)
                mobile.SendLocalizedMessage(
                    def.Number,
                    args,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue
                );
        }

        private static void SendLocalOverhead(Mobile mobile, bool success, TextDefinition def, string args = "")
        {
            if (def.IsString)
                mobile.LocalOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    true,
                    def.String
                );
            else if (def.IsNumber)
                mobile.LocalOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    def.Number,
                    args
                );
        }
        
        private static void SendPublicOverhead(Mobile mobile, bool success, TextDefinition def, string args = "")
        {
            if (def.IsString)
                mobile.PublicOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    true,
                    def.String
                );
            else if (def.IsNumber)
                mobile.PublicOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    def.Number,
                    args
                );
        }
        
        private static void SendPrivateOverhead(Mobile mobile, Mobile above, bool success, TextDefinition def, string args = "")
        {
            if (def.IsString)
                above.PrivateOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    true,
                    def.String,
                    mobile.NetState
                );
            else if (def.IsNumber)
                above.PrivateOverheadMessage(
                    MessageType.Regular,
                    success ? ZhConfig.Messaging.SuccessHue : ZhConfig.Messaging.FailureHue,
                    def.Number,
                    args,
                    mobile.NetState
                );
        }

        public static void SendSuccessPrivateOverHeadMessage(this Mobile mobile, Mobile above, TextDefinition text, string args = "") =>
            SendPrivateOverhead(mobile, above, true, text, args);

        public static void SendSuccessPublicOverHeadMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendPublicOverhead(mobile, true, text, args);
        
        public static void SendSuccessLocalOverHeadMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendLocalOverhead(mobile, true, text, args);

        public static void SendSuccessMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendAscii(mobile, true, text, args);
        
        
        public static void SendFailurePrivateOverHeadMessage(this Mobile mobile, Mobile above, TextDefinition text, string args = "") =>
            SendPrivateOverhead(mobile, above, false, text, args);

        public static void SendFailurePublicOverHeadMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendPublicOverhead(mobile, false, text, args);
        
        public static void SendFailureLocalOverHeadMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendLocalOverhead(mobile, false, text, args);
        
        public static void SendFailureMessage(this Mobile mobile, TextDefinition text, string args = "") =>
            SendAscii(mobile, false, text, args);
    }
}