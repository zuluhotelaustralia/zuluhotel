using System.Collections.Generic;
using System.Linq;
using System.Net;
using Server.Network;

namespace Server.Misc
{
  public class IPLimiter
  {
    public static bool Enabled { get; private set; }
    public static bool SocketBlock { get; private set; }
    public static int MaxAddresses { get; private set; }

    public static void Configure()
    {
      Enabled = ServerConfiguration.GetOrUpdateSetting("ipLimiter.enable", true);
      SocketBlock = ServerConfiguration.GetOrUpdateSetting("ipLimiter.blockAtConnection", true);
      MaxAddresses = ServerConfiguration.GetOrUpdateSetting("ipLimiter.maxConnectionsPerIP", 10);
    }

    public static IPAddress[] Exemptions =
    {
      // IPAddress.Parse( "127.0.0.1" ),
    };

    public static bool IsExempt(IPAddress ip) => Exemptions.Contains(ip);

    public static bool Verify(IPAddress ourAddress)
    {
      if (!Enabled || IsExempt(ourAddress))
        return true;
      
      var count = TcpServer.Instances.Count(ns => ourAddress.Equals(ns.Address));
      
      return count < MaxAddresses;
    }
  }
}
