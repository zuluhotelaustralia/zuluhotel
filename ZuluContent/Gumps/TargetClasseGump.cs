using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Gumps
{
    public class TargetClasseGump : Gump
    {
        private void AddBackground()
        {
            AddPage(0);
            
            AddBackground(0, 0, 460, 540, 2620);
        }

        public TargetClasseGump() : base(250, 200)
        {
            AddBackground();
            
            AddLabel(100, 50, 590, "Select the classe you wish to specialize in:");
            
            AddItem(130, 90, 0x13FE);
            AddLabel(180, 92, 590, "Warrior");
            AddRadio(290, 92, 210, 211, false, (int) ZuluClassType.Warrior);
            
            AddItem(130, 130, 0x13B1);
            AddLabel(180, 132, 590, "Ranger");
            AddRadio(290, 132, 210, 211, false, (int) ZuluClassType.Ranger);

            AddItem(130, 180, 0x13C6);
            AddLabel(180, 182, 590, "Thief");
            AddRadio(290, 182, 210, 211, false, (int) ZuluClassType.Thief);
            
            AddItem(130, 230, 0x0E3B);
            AddLabel(180, 232, 590, "Mage");
            AddRadio(290, 232, 210, 211, false, (int) ZuluClassType.Mage);
            
            AddItem(130, 283, 0x13E3);
            AddLabel(180, 282, 590, "Crafter");
            AddRadio(290, 282, 210, 211, false, (int) ZuluClassType.Crafter);
            
            AddItem(125, 330, 0x0EB2);
            AddLabel(180, 332, 590, "Bard");
            AddRadio(290, 332, 210, 211, false, (int) ZuluClassType.Bard);
            
            AddItem(135, 380, 0x1BC5);
            AddLabel(180, 382, 590, "Power Player");
            AddRadio(290, 382, 210, 211, false, (int) ZuluClassType.PowerPlayer);
            
            // Cancel
            AddButton(140, 460, 242, 241, 0, GumpButtonType.Reply, 2);
            // Okay
            AddButton(260, 460, 249, 248, 1, GumpButtonType.Reply, 1);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1 && info.Switches.Length > 0 && state.Mobile is PlayerMobile playerMobile)
            {
                var targetClass = (ZuluClassType)info.Switches[0];
                playerMobile.TargetZuluClass = targetClass;
                playerMobile.SendSuccessMessage($"You have set your target classe to {targetClass.FriendlyName()}.");
            }
        }
    }
}