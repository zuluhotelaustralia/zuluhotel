using System.Linq;
using Scripts.Engines.Magic;
using Server;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic;

namespace ZuluContent.Zulu.Items.SingleClick
{
    public static partial class SingleClickHandler
    {
        public static void HandleSingleClick(BaseWeapon item, Mobile m)
        {
            HandleSingleClick((IMagicItem)item, m);
        }
    }
}