
using Server.Network;
using ZuluContent.Zulu.Items.SingleClick;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server.Configurations
{
    public class MessagingConfiguration : BaseSingleton<MessagingConfiguration>
    {
        public readonly int SuccessHue;
        public readonly int FailureHue;
        
        private record MessagingSettings
        {
            public int SuccessHue { get; init; }
            public int FailureHue { get; init; }
            public bool VisibleDamage { get; init; }
            public bool ObjectPropertyList { get; init; }
            public bool GuildClickMessage { get; init; }
            public bool AsciiClickMessage { get; init; }
            public bool SingleClickProps { get; init; }
            public bool StaffRevealMagicItems { get; init; }
        }

        protected MessagingConfiguration()
        {
            var config = ZHConfig.DeserializeJsonConfig<MessagingSettings>("Data/messaging.json");

            SuccessHue = config.SuccessHue;
            FailureHue = config.FailureHue;
            
            ObjectPropertyList.Enabled = config.ObjectPropertyList;
            Mobile.VisibleDamageType = config.VisibleDamage ? VisibleDamageType.Related : VisibleDamageType.None;
            Mobile.GuildClickMessage = config.GuildClickMessage;
            Mobile.AsciiClickMessage = config.AsciiClickMessage;
            IncomingEntityPackets.SingleClickProps = config.SingleClickProps;
            SingleClickHandler.StaffRevealedMagicItems = config.StaffRevealMagicItems;
        }
        
    }
}