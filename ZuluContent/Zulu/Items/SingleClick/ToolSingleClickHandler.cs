using Server;
using Server.Items;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        public static void HandleSingleClick(BaseEquippableTool item, Mobile m)
        {
            DefaultHandleSingleClick(item, m);
        }
    }
}