using Server;
// ReSharper disable UnusedMember.Global

namespace ZuluContent.Configuration.Types
{
    public record CoreConfiguration
    {
        public Expansion Expansion { get; set; }
        public bool InsuranceEnabled { get; set; }
        public int ActionDelay { get; set; }
        
        public static void Configure()
        {
            Core.Expansion = ZhConfig.Core.Expansion;
            Mobile.InsuranceEnabled = ZhConfig.Core.InsuranceEnabled;
            Mobile.ActionDelay = ZhConfig.Core.ActionDelay;
        }
    }
}