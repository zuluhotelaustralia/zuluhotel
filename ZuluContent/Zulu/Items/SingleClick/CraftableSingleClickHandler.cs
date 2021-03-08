using Server;
using Server.Engines.Craft;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        public static void HandleSingleClick<T>(T item, Mobile m) where T : Item, ICraftable
        {
            CraftableHandleSingleClick(item, m);
        }
    }
}