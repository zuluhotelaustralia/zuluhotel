using System.Linq;
using Server;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        public static void HandleSingleClick(BaseJewel item, Mobile m)
        {
            DefaultHandleSingleClick(item, m);
        }
    }
}