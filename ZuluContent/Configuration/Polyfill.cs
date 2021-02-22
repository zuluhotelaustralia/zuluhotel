using Server.Configurations;


// TODO: Temporary polyfill to prevent conflicts in other PRs
namespace Server.Configurations
{
    public static class MessageHueConfiguration
    {
        public static int MessageSuccessHue => ZHConfig.Messaging.SuccessHue;
        public static int MessageFailureHue => ZHConfig.Messaging.FailureHue;
    }

    public partial class ResourceConfiguration
    {
        public static OreSettings OreConfiguration => ZHConfig.Resources.Ores;
        public static LogSettings LogConfiguration => ZHConfig.Resources.Logs;
        public static HideSettings HideConfiguration => ZHConfig.Resources.Hides;
    }
}