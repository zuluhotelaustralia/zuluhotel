using Server.Accounting;
using Server.Items;
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global

namespace Server
{
  public static class ExpansionConfiguration
  {
    public static void Configure()
    {
        Core.Expansion = ServerConfiguration.GetOrUpdateSetting("currentExpansion", Expansion.T2A);

        Mobile.InsuranceEnabled = ServerConfiguration.GetSetting("insurance.enable", false);
      
        Mobile.ActionDelay = ServerConfiguration.GetSetting("actionDelay", 500);
    }
  }
}
