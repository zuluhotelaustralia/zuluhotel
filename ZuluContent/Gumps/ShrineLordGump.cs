using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Utilities;

namespace Server.Gumps
{
    public class ShrineLordGump : Gump
    {
        private BaseShrineLord m_ShrineLord;
        
        private void AddBackground()
        {
            AddPage(0);

            AddBackground(0, 0, 460, 300, 2620);
        }

        public ShrineLordGump(BaseShrineLord shrineLord) : base(250, 200)
        {
            m_ShrineLord = shrineLord;
            
            AddBackground();

            AddLabel(120, 50, 590, "Pick the gear you would like:");

            AddItem(130, 90, 0x1415);
            AddLabel(180, 92, 590, "Platemail");
            AddRadio(290, 92, 210, 211, false, 0);

            AddItem(130, 130, 0x13CC);
            AddLabel(180, 132, 590, "Leather");
            AddRadio(290, 132, 210, 211, false, 1);

            AddItem(130, 180, 0x1404);
            AddLabel(180, 182, 590, "War Fork");
            AddRadio(290, 182, 210, 211, false, 2);

            // Okay
            AddButton(200, 240, 249, 248, 0, GumpButtonType.Reply, 1);
        }

        public override async void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 && info.Switches.Length > 0 && state.Mobile is PlayerMobile playerMobile)
            {
                m_ShrineLord.Say($"{playerMobile.Name}, I will create your items.");
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                m_ShrineLord.Say("Vas An Water Do Air Vas Water Mani Corp Water An Vas Water");
                playerMobile.PlaySound(0x011);
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                playerMobile.PlaySound(0x013);
                playerMobile.SendSuccessMessage("The ground begins to rumble...");
                playerMobile.PlaySound(0x028);
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                
                var chosen = info.Switches[0];

                switch (chosen)
                {
                    case 0:
                    {
                        var platemail = new[]
                        {
                            typeof(WaterElementArms), typeof(WaterElementChest), typeof(WaterElementGloves),
                            typeof(WaterElementGorget), typeof(WaterElementHelm), typeof(WaterElementLegs)
                        };

                        foreach (var type in platemail)
                        {
                            playerMobile.AddToBackpack(type.CreateInstance<Item>());
                            playerMobile.PlaySound(0x013);
                            await Timer.Pause(1000);
                        }
                        
                        break;
                    }
                    case 1:
                    {
                        var leather = new[]
                        {
                            typeof(WaterElementLeatherArms), typeof(WaterElementLeatherChest), typeof(WaterElementLeatherGloves),
                            typeof(WaterElementLeatherGorget), typeof(WaterElementLeatherCap), typeof(WaterElementLeatherLegs)
                        };
                    
                        foreach (var type in leather)
                        {
                            playerMobile.AddToBackpack(type.CreateInstance<Item>());
                            playerMobile.PlaySound(0x013);
                            await Timer.Pause(1000);
                        }
                        
                        break;
                    }
                    case 2:
                    {
                        playerMobile.AddToBackpack(new WaterElementFork());
                        
                        break;
                    }
                }

                m_ShrineLord.ShrineCollections.Remove(playerMobile.Serial.Value);
            }
        }
    }
}