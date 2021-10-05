using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Scripts.Zulu.Packets;
using Server;
using Server.Logging;
using Server.Network;
using Server.Scripts.Engines.Loot;
// ReSharper disable UnusedMember.Global

namespace ZuluContent.Configuration.Types
{
    public record MessagingConfiguration
    {
        private static readonly ILogger Logger = LogFactory.GetLogger(typeof(MessagingConfiguration));

        [JsonIgnore]
        public IReadOnlyDictionary<int, string> Cliloc { get; private set; }
        public int SuccessHue { get; init; }
        public int FailureHue { get; init; }
        public bool VisibleDamage { get; init; }
        public bool ObjectPropertyList { get; init; }
        public bool GuildClickMessage { get; init; }
        public bool AsciiClickMessage { get; init; }
        public bool SingleClickProps { get; init; }
        public bool StaffRevealMagicItems { get; init; }
        
        public bool RewriteMessagesToAscii { get; init; }

        public static void Configure()
        {
            var path = Core.FindDataFile("Cliloc.enu");
            Logger.Information($"Loading {path} ... ");
            ZhConfig.Messaging.Cliloc = ClilocList.Load(path);
            Logger.Information("Done.");
            
            Server.ObjectPropertyList.Enabled = ZhConfig.Messaging.ObjectPropertyList;
            Mobile.VisibleDamageType = ZhConfig.Messaging.VisibleDamage ? VisibleDamageType.Related : VisibleDamageType.None;
            Mobile.GuildClickMessage = ZhConfig.Messaging.GuildClickMessage;
            Mobile.AsciiClickMessage = ZhConfig.Messaging.AsciiClickMessage;
            IncomingEntityPackets.SingleClickProps = ZhConfig.Messaging.SingleClickProps;
            LootItem.StaffRevealMagicItems = ZhConfig.Messaging.StaffRevealMagicItems;
        }
    }
}