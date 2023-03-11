using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Utilities;
using System;

namespace Server.Gumps
{
    public class ShrineLordGump : Gump
    {
        private BaseShrineLord m_ShrineLord;
        
        private void AddBackground()
        {
            AddPage(0);

            AddBackground(0, 0, 640, 480, 3600);
        }

        public ShrineLordGump(BaseShrineLord shrineLord) : base(250, 200)
        {
            m_ShrineLord = shrineLord;

            AddBackground();

            if (m_ShrineLord.Title == "the Air Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Air Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0x13B2);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Air Element Bow is the finest Weapon");
                AddLabel(170, 115, 600, "made with the help of the Water Elemental Lord");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was created to improve the aim");
                AddLabel(170, 230, 600, "of anyone who wears it");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "The Platemail was created when knights");
                AddLabel(170, 355, 600, "noticed the great ability of the Leather Magic Armor");
                AddLabel(170, 370, 600, "but its lack of protection didnt please them");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
            else if (m_ShrineLord.Title == "the Earth Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Earth Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0x1439);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Earth Element War Hammer is an ancient Weapon");
                AddLabel(170, 115, 600, "used by only the strongest Worriors chosen by their Kings");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was create by an ancient alchemist");
                AddLabel(170, 230, 600, "trying to overcome his Lord");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "The Platemail was forged by an ancient Earth Lord to be wear");
                AddLabel(170, 355, 600, "by the knights protector");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
            else if (m_ShrineLord.Title == "the Fire Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Fire Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0x13FE);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Fire Element Katana was enchanted by the Great Fire Lord");
                AddLabel(170, 115, 600, "to inflict even more damage to his foes");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was made so humans could fight");
                AddLabel(170, 230, 600, "in the hazardous fire Lands");
                AddLabel(170, 245, 600, "many years ago");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "Even though the leather armor protect the soldiers from the fire");
                AddLabel(170, 355, 600, "Strong Knights also needed protection against the enemys strikes");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
            else if (m_ShrineLord.Title == "the Poison Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Poison Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0x1401);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Poison Element Kryss is a Weapon");
                AddLabel(170, 115, 600, "created to be the best assassins weapon ever");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was created to protect");
                AddLabel(170, 230, 600, "the Lords from the assassins with poison strikes");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "The Platemail was created when the Warriors found out");
                AddLabel(170, 355, 600, "the power of the Leather, and wanted the protection");
                AddLabel(170, 370, 600, "without loosing their physical protection");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
            else if (m_ShrineLord.Title == "the Shadow Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Sadow Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0xE87);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Shadow Element Pitchfork is a Weapon");
                AddLabel(170, 115, 600, "created by the Evil Shadow Lord, to corrupt the souls");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was created by the Evil shadow Lord");
                AddLabel(170, 230, 600, "to protect his army from the Humans");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "The Platemail was created to protect the bests and strong");
                AddLabel(170, 355, 600, "warriors from Shadow Lord Army");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
            else if (m_ShrineLord.Title == "the Water Element Shrine Lord")
            {
                AddLabel(80, 15, 550, "Welcome to the Water Element Prize Choosing");

                AddLabel(20, 70, 600, "Weapon");
                AddItem(20, 100, 0x1404);
                AddRadio(150, 70, 210, 211, false, 2);
                AddLabel(170, 100, 600, "The Water Element War Fork is an ancient Weapon");
                AddLabel(170, 115, 600, "used by only the Kings finest Warriors");

                AddLabel(20, 190, 600, "Magic Leather");
                AddItem(20, 230, 0x13CC);
                AddRadio(150, 190, 210, 211, false, 1);
                AddLabel(170, 215, 600, "The Magic Leather Armor was created in an alchemic disaster");
                AddLabel(170, 230, 600, "the armor was peeled of the corps of an alchemist thousands");
                AddLabel(170, 245, 600, "of years ago");

                AddLabel(20, 300, 600, "PlateMail");
                AddItem(20, 340, 0x1415);
                AddRadio(150, 300, 210, 211, false, 0);
                AddLabel(170, 340, 600, "The Platemail was created when the Water Lord attacked");
                AddLabel(170, 355, 600, "a royal guard the plate was like no other, it seemed ");
                AddLabel(170, 370, 600, "to have been blessed by this vicious attack");

                AddButton(300, 430, 249, 248, 0, GumpButtonType.Reply, 1);
            }
        }

        public override async void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 0 && info.Switches.Length > 0 && state.Mobile is PlayerMobile playerMobile)
            {
                m_ShrineLord.Say($"{playerMobile.Name}, I will create your items.");
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                if (m_ShrineLord.Title == "the Air Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Air Do Air Vas Air Mani Corp Air An Vas Air");
                else if (m_ShrineLord.Title == "the Earth Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Earth Do Air Vas Earth Mani Corp Earth An Vas Earth");
                else if (m_ShrineLord.Title == "the Fire Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Fire Do Air Vas Fire Mani Corp Fire An Vas Fire");
                else if (m_ShrineLord.Title == "the Poison Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Poison Do Air Vas Poison Mani Corp Poison An Vas Poison");
                else if (m_ShrineLord.Title == "the Shadow Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Shadow Do Air Vas Shadow Mani Corp Shadow An Vas Shadow");
                else if (m_ShrineLord.Title == "the Water Element Shrine Lord")
                    m_ShrineLord.Say("Vas An Water Do Air Vas Water Mani Corp Water An Vas Water");
                playerMobile.PlaySound(0x011);
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                playerMobile.PlaySound(0x013);
                playerMobile.SendSuccessMessage("The ground begins to rumble...");
                playerMobile.PlaySound(0x028);
                m_ShrineLord.Animate(17, 5, 1, true, false, 0);
                
                var chosen = info.Switches[0];

                var platemail = Array.Empty<Type>();
                var leather = Array.Empty<Type>();

                switch (chosen)
                {
                    case 0:
                        {
                            if (m_ShrineLord.Title == "the Air Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(AirElementArms), typeof(AirElementChest), typeof(AirElementGloves),
                                typeof(AirElementGorget), typeof(AirElementHelm), typeof(AirElementLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Earth Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(EarthElementArms), typeof(EarthElementChest), typeof(EarthElementGloves),
                                typeof(EarthElementGorget), typeof(EarthElementHelm), typeof(EarthElementLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Fire Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(FireElementArms), typeof(FireElementChest), typeof(FireElementGloves),
                                typeof(FireElementGorget), typeof(FireElementHelm), typeof(FireElementLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Poison Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(PoisonElementArms), typeof(PoisonElementChest), typeof(PoisonElementGloves),
                                typeof(PoisonElementGorget), typeof(PoisonElementHelm), typeof(PoisonElementLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Shadow Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(ShadowElementArms), typeof(ShadowElementChest), typeof(ShadowElementGloves),
                                typeof(ShadowElementGorget), typeof(ShadowElementHelm), typeof(ShadowElementLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Water Element Shrine Lord")
                            {
                                platemail = new[]
                                {
                                typeof(WaterElementArms), typeof(WaterElementChest), typeof(WaterElementGloves),
                                typeof(WaterElementGorget), typeof(WaterElementHelm), typeof(WaterElementLegs)
                                };
                            }

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
                            if (m_ShrineLord.Title == "the Air Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(AirElementLeatherArms), typeof(AirElementLeatherChest), typeof(AirElementLeatherGloves),
                                    typeof(AirElementLeatherGorget), typeof(AirElementLeatherCap), typeof(AirElementLeatherLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Earth Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(EarthElementLeatherArms), typeof(EarthElementLeatherChest), typeof(EarthElementLeatherGloves),
                                    typeof(EarthElementLeatherGorget), typeof(EarthElementLeatherCap), typeof(EarthElementLeatherLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Fire Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(FireElementLeatherArms), typeof(FireElementLeatherChest), typeof(FireElementLeatherGloves),
                                    typeof(FireElementLeatherGorget), typeof(FireElementLeatherCap), typeof(FireElementLeatherLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Poison Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(PoisonElementLeatherArms), typeof(PoisonElementLeatherChest), typeof(PoisonElementLeatherGloves),
                                    typeof(PoisonElementLeatherGorget), typeof(PoisonElementLeatherCap), typeof(PoisonElementLeatherLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Shadow Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(ShadowElementLeatherArms), typeof(ShadowElementLeatherChest), typeof(ShadowElementLeatherGloves),
                                    typeof(ShadowElementLeatherGorget), typeof(ShadowElementLeatherCap), typeof(ShadowElementLeatherLegs)
                                };
                            }
                            else if (m_ShrineLord.Title == "the Water Element Shrine Lord")
                            {
                                leather = new[]
                                {
                                    typeof(WaterElementLeatherArms), typeof(WaterElementLeatherChest), typeof(WaterElementLeatherGloves),
                                    typeof(WaterElementLeatherGorget), typeof(WaterElementLeatherCap), typeof(WaterElementLeatherLegs)
                                };
                            }

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
                            if (m_ShrineLord.Title == "the Air Element Shrine Lord")
                                playerMobile.AddToBackpack(new AirElementBow());
                            else if (m_ShrineLord.Title == "the Earth Element Shrine Lord")
                                playerMobile.AddToBackpack(new EarthElementWarHammer());
                            else if (m_ShrineLord.Title == "the Fire Element Shrine Lord")
                                playerMobile.AddToBackpack(new FireElementKatana());
                            else if (m_ShrineLord.Title == "the Poison Element Shrine Lord")
                                playerMobile.AddToBackpack(new PoisonElementKryss());
                            else if (m_ShrineLord.Title == "the Shadow Element Shrine Lord")
                                playerMobile.AddToBackpack(new ShadowElementPitchfork());
                            else if (m_ShrineLord.Title == "the Water Element Shrine Lord")
                                playerMobile.AddToBackpack(new WaterElementFork());

                            break;
                    }
                }

                m_ShrineLord.ShrineCollections.Remove(playerMobile.Serial.Value);
            }
        }
    }
}