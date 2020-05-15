using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Auction
{
    public class AuctionGump : Gump
    {
        public enum Buttons
        {
            Exit,
            NextPageButton,
            PrevPageButton,
            BidButton,
            ReplyButton
        }

        private Mobile m_Viewer;
        private AuctionController m_Stone;
        private const int m_ItemsPerPage = 8;

        private const int _col1X = 20;
        private const int _col2X = 190;
        private const int _col3X = 360;
        private const int _col4X = 530;
        private const int _row1Y = 50;
        private const int _row2Y = 220;
        private const int _footerY = 400;

        private const int _boxsize = 150;
        private const int _boxspacing = 20;

        private const int _windowX = 700;
        private const int _windowY = 500;

        private Dictionary<int, AuctionItem> _dict;

        public AuctionGump(Mobile from, AuctionController stone) : base(100, 100)
        {
            m_Viewer = from;
            m_Stone = stone;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            _dict = new Dictionary<int, AuctionItem>();

            int pages = NumPages();

            //public void AddImageTiled( int x, int y, int width, int height, int gumpID )
            //AddButton( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); 

            AddPage(0);
            AddBackground(0, 0, _windowX, _windowY, 9270);//9300 );
            AddHtml(20, 10, 460, 100, "<h2><basefont color='#FFFFFF'>Auction Items for Sale</basefont></h2>", false, false);

            AddImageTiled(_col1X, _footerY, 150, 22, 9354);
            AddTextEntry(_col1X + 10, _footerY, 140, 50, 49, 0, "");

            AddButton(_col2X, _footerY, 247, 248, (int)Buttons.BidButton, GumpButtonType.Reply, 2);
            AddImageTiled(_col2X, _footerY, 68, 22, 2624); // Submit Button BG
            AddLabel(_col2X + 10, _footerY + 2, 49, @"Bid");


            if (m_Stone.SaleItems.Count == 0)
            {
                AddHtml(_col1X, _row1Y, 460, 100, "<basefont color='#FFFFFF'>Nothing for sale at this time.</basefont>", false, false);
                return;
            }

            int idx = 0; // this is just the position on the page!
            int dictindex = 0;
            int currentpage = 1;
            AddPage(currentpage);
            foreach (AuctionItem item in m_Stone.SaleItems)
            {
                _dict.Add(dictindex, item);
                //MakeBox( _col1X + (170 * idx), _row1Y + (170 * (idx/4)), dictindex, item );
                MakeBox(idx, dictindex, item);
                idx++;
                dictindex++;

                if (currentpage > 1)
                {
                    // add "prev page" button"
                    AddButton(_col3X, _footerY, 247, 248, (int)Buttons.PrevPageButton, GumpButtonType.Page, currentpage - 1);
                    AddImageTiled(_col3X, _footerY, 68, 22, 2624); //paging button BG
                    AddLabel(_col3X + 10, _footerY + 2, 49, @"Prev");
                }

                if (idx > 7)
                {

                    // and we're about to move to the next page so add "next page" button
                    AddButton(_col4X, _footerY, 247, 248, (int)Buttons.NextPageButton, GumpButtonType.Page, currentpage + 1);
                    AddImageTiled(_col4X, _footerY, 68, 22, 2624); //paging button BG
                    AddLabel(_col4X + 20, _footerY + 2, 49, @"Next");

                    idx = 0;
                    currentpage++;
                    AddPage(currentpage);
                }
            }
        }

        private void MakeBox(int idx, int dictindex, AuctionItem ai)
        {
            int x = _col1X;
            int y = _row1Y;

            //row1 0, 1, 2, 3
            if (idx < 4)
            {
                x += (170 * idx);
            }
            else
            {
                // 4, 5, 6, 7
                x += (170 * (idx - 4));
                y += 170;
            }

            AddBackground(x, y, _boxsize, _boxsize, 9350);
            AddRadio(x, y, 208, 209, false, dictindex);
            AddItem(x + 20, y, ai.SaleItem.ItemID, ai.SaleItem.Hue);
            AddHtml(x + 5, y + 20, _boxsize, 20, ai.SaleItem.Name == null ? ai.SaleItem.GetType().Name : ai.SaleItem.Name, false, false);
            AddHtml(x + 5, y + 40, _boxsize, 20, "Amount: " + ai.SaleItem.Amount, false, false);
            AddHtml(x + 5, y + 60, _boxsize, 20, "Bid: " + (ai.LeadingBid == 0 ? ai.ListPrice.ToString() : ai.LeadingBid.ToString()), false, false);
            AddHtml(x + 5, y + 80, _boxsize, 20, "Closes: " + ai.SellByDate.ToString(), false, false);
            AddHtml(x + 5, y + 100, _boxsize, 20, "Closing Time: " + ai.SellByDate.Hour.ToString() + ":" + ai.SellByDate.Minute.ToString(), false, false);
        }

        public int NumPages()
        {
            if (m_Stone.SaleItems == null)
            {
                return 0;
            }

            int totalItems = m_Stone.SaleItems.Count;
            int pages = totalItems / m_ItemsPerPage; //intentional integer division
            int remainder = totalItems - (pages * m_ItemsPerPage);

            if (remainder != 0)
            {
                pages++;
            }

            return pages;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.ReplyButton:
                    {
                        Console.WriteLine(" reply button {0} (should be bid)", info.ButtonID);
                        break;
                    }
                case (int)Buttons.BidButton:
                    {
                        int whichradio = -1; //which radio button did they tick?

                        if (info.Switches.Length > 0)
                        {
                            whichradio = info.Switches[0]; // there should only be one because they're radios.... right?
                        }

                        if (whichradio == -1)
                        {
                            from.SendMessage("You must select an item to bid on!");
                            return;
                        }

                        int response;
                        string text = info.GetTextEntry(0).Text;
                        bool success = Int32.TryParse(text, out response);
                        if (!success)
                        {
                            response = 0;
                        }


                        AuctionController.AuctionStone.RegisterBid(from, response, _dict[whichradio]);
                        break;
                    }
            }
        }
    }

    public class AuctionSellGump : Gump
    {
        public enum Buttons
        {
            Exit,
            NextPageButton,
            PrevPageButton,
            BidButton,
            ReplyButton
        }

        private Mobile m_Viewer;
        private AuctionController m_Stone;

        private int _windowX = 320;
        private int _windowY = 240;

        public AuctionSellGump(Mobile from, AuctionController stone) : base(100, 100)
        {
            m_Viewer = from;
            m_Stone = stone;

            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            //int pages = NumPages();

            //public void AddImageTiled( int x, int y, int width, int height, int gumpID )
            //AddButton( X, Y, UnCheckedGumpID, CheckedGumpID, StartChecked?, SwitchID ); 

            AddPage(0);
            AddBackground(0, 0, _windowX, _windowY, 9300);
            AddHtml(20, 10, 460, 100, "<h2>List an item for auction.</h2>", false, false);
        }
    }
}
