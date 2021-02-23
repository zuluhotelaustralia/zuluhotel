using Server.Configurations;


// TODO: Temporary polyfill to prevent conflicts in other PRs
namespace Server.Configurations
{
    public static class MessageHueConfiguration
    {
        public static int MessageSuccessHue => ZhConfig.Messaging.SuccessHue;
        public static int MessageFailureHue => ZhConfig.Messaging.FailureHue;
    }

    public partial class ResourceConfiguration
    {
        public static OreSettings OreConfiguration => ZhConfig.Resources.Ores;
        public static LogSettings LogConfiguration => ZhConfig.Resources.Logs;
    }
}