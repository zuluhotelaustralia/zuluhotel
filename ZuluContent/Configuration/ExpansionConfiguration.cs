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

        AccountGold.Enabled = ServerConfiguration.GetSetting("accountGold.enable", false);
        AccountGold.ConvertOnBank = ServerConfiguration.GetSetting("accountGold.convertOnBank", false);
        AccountGold.ConvertOnTrade = ServerConfiguration.GetSetting("accountGold.convertOnTrade", false);
        VirtualCheck.UseEditGump = ServerConfiguration.GetSetting("virtualChecks.useEditGump", true);

        Mobile.InsuranceEnabled = ServerConfiguration.GetSetting("insurance.enable", false);
      
        Mobile.ActionDelay = ServerConfiguration.GetSetting("actionDelay", 500);
    }
  }
}
