using System;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Regions;

namespace Server.Mobiles
{
    public enum VendorShoeType
    {
        None,
        Shoes,
        Boots,
        Sandals,
        ThighBoots
    }

    public abstract class BaseVendor : BaseCreature, IVendor
    {
        private const int MaxSell = 500;

        protected abstract List<SBInfo> SBInfos { get; }

        private ArrayList m_ArmorBuyInfo = new ArrayList();
        private ArrayList m_ArmorSellInfo = new ArrayList();

        private DateTime m_LastRestock;

        //All vendors will buy anything from the player while this is true.
        public static bool zuluStyleSell = true;

        public override bool CanTeach
        {
            get { return true; }
        }

        public override bool BardImmune
        {
            get { return true; }
        }

        public override bool PlayerRangeSensitive
        {
            get { return true; }
        }

        public virtual bool IsActiveVendor
        {
            get { return true; }
        }

        public virtual bool IsActiveBuyer
        {
            get { return IsActiveVendor; }
        } // response to vendor SELL

        public virtual bool IsActiveSeller
        {
            get { return IsActiveVendor; }
        } // repsonse to vendor BUY

        public virtual NpcGuild NpcGuild
        {
            get { return NpcGuild.None; }
        }

        public override bool ShowFameTitle
        {
            get { return false; }
        }

        public virtual void OnSuccessfulBulkOrderReceive(Mobile from)
        {
        }

        public BaseVendor(string title)
            : base(AIType.AI_Vendor, FightMode.None, 2, 1, 0.5, 2)
        {
            LoadSBInfo();

            this.Title = title;
            InitBody();
            InitOutfit();

            Container pack;
            //these packs MUST exist, or the client will crash when the packets are sent
            pack = new Backpack();
            pack.Layer = Layer.ShopBuy;
            pack.Movable = false;
            pack.Visible = false;
            AddItem(pack);

            pack = new Backpack();
            pack.Layer = Layer.ShopResale;
            pack.Movable = false;
            pack.Visible = false;
            AddItem(pack);

            m_LastRestock = DateTime.Now;
        }

        public BaseVendor(Serial serial)
            : base(serial)
        {
        }

        public DateTime LastRestock
        {
            get { return m_LastRestock; }
            set { m_LastRestock = value; }
        }

        public virtual TimeSpan RestockDelay
        {
            get { return TimeSpan.FromHours(1); }
        }

        public Container BuyPack
        {
            get
            {
                Container pack = FindItemOnLayer(Layer.ShopBuy) as Container;

                if (pack == null)
                {
                    pack = new Backpack();
                    pack.Layer = Layer.ShopBuy;
                    pack.Visible = false;
                    AddItem(pack);
                }

                return pack;
            }
        }

        public abstract void InitSBInfo();

        protected void LoadSBInfo()
        {
            m_LastRestock = DateTime.Now;

            for (int i = 0; i < m_ArmorBuyInfo.Count; ++i)
            {
                GenericBuyInfo buy = m_ArmorBuyInfo[i] as GenericBuyInfo;

                if (buy != null)
                    buy.DeleteDisplayEntity();
            }

            SBInfos.Clear();

            InitSBInfo();

            m_ArmorBuyInfo.Clear();
            m_ArmorSellInfo.Clear();

            for (int i = 0; i < SBInfos.Count; i++)
            {
                SBInfo sbInfo = (SBInfo) SBInfos[i];
                m_ArmorBuyInfo.AddRange(sbInfo.BuyInfo);
                m_ArmorSellInfo.Add(sbInfo.SellInfo);
            }
        }

        public virtual bool GetGender()
        {
            return Utility.RandomBool();
        }

        public virtual void InitBody()
        {
            InitStats(100, 100, 25);

            SpeechHue = Utility.RandomDyedHue();
            Hue = Race.RandomSkinHue();

            if (Female = GetGender())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
            }
        }

        public virtual int GetRandomHue()
        {
            switch (Utility.Random(5))
            {
                default:
                case 0: return Utility.RandomBlueHue();
                case 1: return Utility.RandomGreenHue();
                case 2: return Utility.RandomRedHue();
                case 3: return Utility.RandomYellowHue();
                case 4: return Utility.RandomNeutralHue();
            }
        }

        public virtual int GetShoeHue()
        {
            if (0.1 > Utility.RandomDouble())
                return 0;

            return Utility.RandomNeutralHue();
        }

        public virtual VendorShoeType ShoeType
        {
            get { return VendorShoeType.Shoes; }
        }

        protected override void OnMapChange(Map oldMap)
        {
            base.OnMapChange(oldMap);

            LoadSBInfo();
        }

        public virtual void CapitalizeTitle()
        {
            string title = this.Title;

            if (title == null)
                return;

            string[] split = title.Split(' ');

            for (int i = 0; i < split.Length; ++i)
            {
                if (InsensitiveStringHelpers.Equals(split[i], "the"))
                    continue;

                if (split[i].Length > 1)
                    split[i] = Char.ToUpper(split[i][0]) + split[i].Substring(1);
                else if (split[i].Length > 0)
                    split[i] = Char.ToUpper(split[i][0]).ToString();
            }

            this.Title = String.Join(" ", split);
        }

        public virtual int GetHairHue()
        {
            return Race.RandomHairHue();
        }

        public virtual void InitOutfit()
        {
            switch (Utility.Random(3))
            {
                case 0:
                    AddItem(new FancyShirt(GetRandomHue()));
                    break;
                case 1:
                    AddItem(new Doublet(GetRandomHue()));
                    break;
                case 2:
                    AddItem(new Shirt(GetRandomHue()));
                    break;
            }

            switch (ShoeType)
            {
                case VendorShoeType.Shoes:
                    AddItem(new Shoes());
                    break;
                case VendorShoeType.Boots:
                    AddItem(new Boots());
                    break;
                case VendorShoeType.Sandals:
                    AddItem(new Sandals());
                    break;
                case VendorShoeType.ThighBoots:
                    AddItem(new ThighBoots());
                    break;
            }

            int hairHue = GetHairHue();

            Utility.AssignRandomHair(this, hairHue);
            Utility.AssignRandomFacialHair(this, hairHue);

            if (Female)
            {
                switch (Utility.Random(6))
                {
                    case 0:
                        AddItem(new ShortPants(GetRandomHue()));
                        break;
                    case 1:
                    case 2:
                        AddItem(new Kilt(GetRandomHue()));
                        break;
                    case 3:
                    case 4:
                    case 5:
                        AddItem(new Skirt(GetRandomHue()));
                        break;
                }
            }
            else
            {
                switch (Utility.Random(2))
                {
                    case 0:
                        AddItem(new LongPants(GetRandomHue()));
                        break;
                    case 1:
                        AddItem(new ShortPants(GetRandomHue()));
                        break;
                }
            }

            PackGold(100, 200);
        }

        public virtual void Restock()
        {
            m_LastRestock = DateTime.Now;

            IBuyItemInfo[] buyInfo = this.GetBuyInfo();

            foreach (IBuyItemInfo bii in buyInfo)
                bii.OnRestock();
        }

        private static TimeSpan InventoryDecayTime = TimeSpan.FromHours(1.0);

        public virtual void VendorBuy(Mobile from)
        {
            if (!IsActiveSeller)
                return;

            if (!from.CheckAlive())
                return;

            if (!CheckVendorAccess(from))
            {
                Say(501522); // I shall not treat with scum like thee!
                return;
            }

            if (DateTime.Now - m_LastRestock > RestockDelay)
                Restock();
            
            List<BuyItemState> list;
            IBuyItemInfo[] buyInfo = this.GetBuyInfo();
            IShopSellInfo[] sellInfo = this.GetSellInfo();

            list = new List<BuyItemState>(buyInfo.Length);
            Container cont = this.BuyPack;
            
            var opls = new List<ObjectPropertyList>();

            for (int idx = 0; idx < buyInfo.Length; idx++)
            {
                IBuyItemInfo buyItem = (IBuyItemInfo) buyInfo[idx];

                if (buyItem.Amount <= 0 || list.Count >= 250)
                    continue;

                // NOTE: Only GBI supported; if you use another implementation of IBuyItemInfo, this will crash
                GenericBuyInfo gbi = (GenericBuyInfo) buyItem;
                IEntity disp = gbi.GetDisplayEntity();

                list.Add(new BuyItemState(buyItem.Name, cont.Serial, disp == null ? (Serial) 0x7FC0FFEE : disp.Serial,
                    buyItem.Price, buyItem.Amount, buyItem.ItemID, buyItem.Hue));
                
                if (disp is IPropertyListObject obj)
                {
                    opls.Add(obj.PropertyList);
                }
            }

            List<Item> playerItems = cont.Items;

            for (int i = playerItems.Count - 1; i >= 0; --i)
            {
                if (i >= playerItems.Count)
                    continue;

                Item item = playerItems[i];

                if (item.LastMoved + InventoryDecayTime <= DateTime.Now)
                    item.Delete();
            }

            for (int i = 0; i < playerItems.Count; ++i)
            {
                Item item = playerItems[i];

                int price = 0;
                string name = null;

                foreach (IShopSellInfo ssi in sellInfo)
                {
                    if (ssi.IsSellable(item))
                    {
                        price = ssi.GetBuyPriceFor(item);
                        name = ssi.GetNameFor(item);
                        break;
                    }
                }

                if (name != null && list.Count < 250)
                {
                    list.Add(
                        new BuyItemState(name, cont.Serial, item.Serial, price, item.Amount, item.ItemID, item.Hue));
                    opls.Add(item.PropertyList);
                }
            }

            //one (not all) of the packets uses a byte to describe number of items in the list.  Osi = dumb.
            //if ( list.Count > 255 )
            //	Console.WriteLine( "Vendor Warning: Vendor {0} has more than 255 buy items, may cause client errors!", this );

            if (list.Count > 0)
            {
                list.Sort(new BuyItemStateComparer());

                SendPacksTo(from);

                NetState ns = from.NetState;

                if (ns == null)
                    return;

                from.NetState.SendVendorBuyContent(list);
                from.NetState.SendVendorBuyList(this, list);
                from.NetState.SendDisplayBuyList(Serial);
                from.NetState.SendMobileStatus(from); // make sure their gold amount is sent
                
                for (var i = 0; i < opls.Count; ++i)
                {
                    from.NetState?.Send(opls[i].Buffer);
                }

                SayTo(from, 500186); // Greetings.  Have a look around.
            }
        }

        public virtual void SendPacksTo(Mobile from)
        {
            var pack = FindItemOnLayer(Layer.ShopBuy);

            if (pack == null)
            {
                pack = new Backpack {Layer = Layer.ShopBuy, Movable = false, Visible = false};
                AddItem(pack);
            }

            from.NetState.SendEquipUpdate(pack);

            pack = FindItemOnLayer(Layer.ShopSell);

            if (pack != null)
            {
                from.NetState.SendEquipUpdate(pack);
            }

            pack = FindItemOnLayer(Layer.ShopResale);

            if (pack == null)
            {
                pack = new Backpack {Layer = Layer.ShopResale, Movable = false, Visible = false};
                AddItem(pack);
            }

            from.NetState.SendEquipUpdate(pack);
        }

        public virtual void VendorSell(Mobile from)
        {
            if (!IsActiveBuyer)
                return;

            if (!from.CheckAlive())
                return;

            if (!CheckVendorAccess(from))
            {
                Say(501522); // I shall not treat with scum like thee!
                return;
            }

            Container pack = from.Backpack;

            if (pack != null)
            {
                IShopSellInfo[] info = GetSellInfo();

                List<SellItemState> table = new List<SellItemState>();

                foreach (IShopSellInfo ssi in info)
                {
                    Item[] items = pack.FindItemsByType(ssi.Types);

                    foreach (Item item in items)
                    {
                        if (item is Container && ((Container) item).Items.Count != 0)
                            continue;

                        if (item.IsStandardLoot() && item.Movable && ssi.IsSellable(item))
                            table.Add(new SellItemState(item, ssi.GetSellPriceFor(item), ssi.GetNameFor(item)));
                    }
                }

                if (table.Count > 0)
                {
                    SendPacksTo(from);

                    from.NetState.SendVendorSellList(Serial, table);
                }
                else
                {
                    Say(true, "You have nothing I would be interested in.");
                }
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            /* TODO: Thou art giving me? and fame/karma for gold gifts */

            return base.OnDragDrop(from, dropped);
        }

        private GenericBuyInfo LookupDisplayObject(object obj)
        {
            IBuyItemInfo[] buyInfo = this.GetBuyInfo();

            for (int i = 0; i < buyInfo.Length; ++i)
            {
                GenericBuyInfo gbi = (GenericBuyInfo) buyInfo[i];

                if (gbi.GetDisplayEntity() == obj)
                    return gbi;
            }

            return null;
        }

        private void ProcessSinglePurchase(BuyItemResponse buy, IBuyItemInfo bii, List<BuyItemResponse> validBuy,
            ref bool fullPurchase, ref int totalCost)
        {
            int amount = buy.Amount;

            if (amount > bii.Amount)
                amount = bii.Amount;

            if (amount <= 0)
                return;

            totalCost += bii.Price * amount;
            validBuy.Add(buy);
        }

        private void ProcessValidPurchase(int amount, IBuyItemInfo bii, Mobile buyer, Container cont)
        {
            if (amount > bii.Amount)
                amount = bii.Amount;

            if (amount < 1)
                return;

            bii.Amount -= amount;

            IEntity o = bii.GetEntity();

            if (o is Item)
            {
                Item item = (Item) o;

                if (item.Stackable)
                {
                    item.Amount = amount;

                    if (cont == null || !cont.TryDropItem(buyer, item, false))
                        item.MoveToWorld(buyer.Location, buyer.Map);
                }
                else
                {
                    item.Amount = 1;

                    if (cont == null || !cont.TryDropItem(buyer, item, false))
                        item.MoveToWorld(buyer.Location, buyer.Map);

                    for (int i = 1; i < amount; i++)
                    {
                        item = bii.GetEntity() as Item;

                        if (item != null)
                        {
                            item.Amount = 1;

                            if (cont == null || !cont.TryDropItem(buyer, item, false))
                                item.MoveToWorld(buyer.Location, buyer.Map);
                        }
                    }
                }
            }
            else if (o is Mobile)
            {
                Mobile m = (Mobile) o;

                m.Direction = (Direction) Utility.Random(8);
                m.MoveToWorld(buyer.Location, buyer.Map);
                m.PlaySound(m.GetIdleSound());

                if (m is BaseCreature)
                    ((BaseCreature) m).SetControlMaster(buyer);

                for (int i = 1; i < amount; ++i)
                {
                    m = bii.GetEntity() as Mobile;

                    if (m != null)
                    {
                        m.Direction = (Direction) Utility.Random(8);
                        m.MoveToWorld(buyer.Location, buyer.Map);

                        if (m is BaseCreature)
                            ((BaseCreature) m).SetControlMaster(buyer);
                    }
                }
            }
        }

        public virtual bool OnBuyItems(Mobile buyer, List<BuyItemResponse> list)
        {
            if (!IsActiveSeller)
                return false;

            if (!buyer.CheckAlive())
                return false;

            if (!CheckVendorAccess(buyer))
            {
                Say(501522); // I shall not treat with scum like thee!
                return false;
            }

            IBuyItemInfo[] buyInfo = this.GetBuyInfo();
            IShopSellInfo[] info = GetSellInfo();
            int totalCost = 0;
            List<BuyItemResponse> validBuy = new List<BuyItemResponse>(list.Count);
            Container cont;
            bool bought = false;
            bool fromBank = false;
            bool fullPurchase = true;

            foreach (BuyItemResponse buy in list)
            {
                Serial ser = buy.Serial;
                int amount = buy.Amount;

                if (ser.IsItem)
                {
                    Item item = World.FindItem(ser);

                    if (item == null)
                        continue;

                    GenericBuyInfo gbi = LookupDisplayObject(item);

                    if (gbi != null)
                    {
                        ProcessSinglePurchase(buy, gbi, validBuy, ref fullPurchase, ref totalCost);
                    }
                    else if (item != this.BuyPack && item.IsChildOf(this.BuyPack))
                    {
                        if (amount > item.Amount)
                            amount = item.Amount;

                        if (amount <= 0)
                            continue;

                        foreach (IShopSellInfo ssi in info)
                        {
                            if (ssi.IsSellable(item))
                            {
                                if (ssi.IsResellable(item))
                                {
                                    totalCost += ssi.GetBuyPriceFor(item) * amount;
                                    validBuy.Add(buy);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (ser.IsMobile)
                {
                    Mobile mob = World.FindMobile(ser);

                    if (mob == null)
                        continue;

                    GenericBuyInfo gbi = LookupDisplayObject(mob);

                    if (gbi != null)
                        ProcessSinglePurchase(buy, gbi, validBuy, ref fullPurchase, ref totalCost);
                }
            } //foreach

            if (fullPurchase && validBuy.Count == 0)
                SayTo(buyer, 500190); // Thou hast bought nothing!
            else if (validBuy.Count == 0)
                SayTo(buyer, 500187); // Your order cannot be fulfilled, please try again.

            if (validBuy.Count == 0)
                return false;

            bought = buyer.AccessLevel >= AccessLevel.GameMaster;

            cont = buyer.Backpack;
            if (!bought && cont != null)
            {
                if (cont.ConsumeTotal(typeof(Gold), totalCost))
                    bought = true;
                else if (totalCost < 2000)
                    SayTo(buyer, 500192); // Begging thy pardon, but thou canst not afford that.
            }

            if (!bought && totalCost >= 2000)
            {
                cont = buyer.FindBankNoCreate();
                if (cont != null && cont.ConsumeTotal(typeof(Gold), totalCost))
                {
                    bought = true;
                    fromBank = true;
                }
                else
                {
                    SayTo(buyer, 500191); //Begging thy pardon, but thy bank account lacks these funds.
                }
            }

            if (!bought)
                return false;
            else
                buyer.PlaySound(0x32);

            cont = buyer.Backpack;
            if (cont == null)
                cont = buyer.BankBox;

            foreach (BuyItemResponse buy in validBuy)
            {
                Serial ser = buy.Serial;
                int amount = buy.Amount;

                if (amount < 1)
                    continue;

                if (ser.IsItem)
                {
                    Item item = World.FindItem(ser);

                    if (item == null)
                        continue;

                    GenericBuyInfo gbi = LookupDisplayObject(item);

                    if (gbi != null)
                    {
                        ProcessValidPurchase(amount, gbi, buyer, cont);
                    }
                    else
                    {
                        if (amount > item.Amount)
                            amount = item.Amount;

                        foreach (IShopSellInfo ssi in info)
                        {
                            if (ssi.IsSellable(item))
                            {
                                if (ssi.IsResellable(item))
                                {
                                    Item buyItem;
                                    if (amount >= item.Amount)
                                    {
                                        buyItem = item;
                                    }
                                    else
                                    {
                                        buyItem = Mobile.LiftItemDupe(item, item.Amount - amount);

                                        if (buyItem == null)
                                            buyItem = item;
                                    }

                                    if (cont == null || !cont.TryDropItem(buyer, buyItem, false))
                                        buyItem.MoveToWorld(buyer.Location, buyer.Map);

                                    break;
                                }
                            }
                        }
                    }
                }
                else if (ser.IsMobile)
                {
                    Mobile mob = World.FindMobile(ser);

                    if (mob == null)
                        continue;

                    GenericBuyInfo gbi = LookupDisplayObject(mob);

                    if (gbi != null)
                        ProcessValidPurchase(amount, gbi, buyer, cont);
                }
            } //foreach

            if (fullPurchase)
            {
                if (buyer.AccessLevel >= AccessLevel.GameMaster)
                    SayTo(buyer, true,
                        "I would not presume to charge thee anything.  Here are the goods you requested.");
                else if (fromBank)
                    SayTo(buyer, true,
                        "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.",
                        totalCost);
                else
                    SayTo(buyer, true, "The total of thy purchase is {0} gold.  My thanks for the patronage.",
                        totalCost);
            }
            else
            {
                if (buyer.AccessLevel >= AccessLevel.GameMaster)
                    SayTo(buyer, true,
                        "I would not presume to charge thee anything.  Unfortunately, I could not sell you all the goods you requested.");
                else if (fromBank)
                    SayTo(buyer, true,
                        "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.",
                        totalCost);
                else
                    SayTo(buyer, true,
                        "The total of thy purchase is {0} gold.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.",
                        totalCost);
            }

            return true;
        }

        public virtual bool CheckVendorAccess(Mobile from)
        {
            GuardedRegion reg = (GuardedRegion) this.Region.GetRegion(typeof(GuardedRegion));

            if (reg != null && !reg.CheckVendorAccess(this, from))
                return false;

            if (this.Region != from.Region)
            {
                reg = (GuardedRegion) from.Region.GetRegion(typeof(GuardedRegion));

                if (reg != null && !reg.CheckVendorAccess(this, from))
                    return false;
            }

            return true;
        }

        public virtual bool OnSellItems(Mobile seller, List<SellItemResponse> list)
        {
            if (!IsActiveBuyer)
                return false;

            if (!seller.CheckAlive())
                return false;

            if (!CheckVendorAccess(seller))
            {
                Say(501522); // I shall not treat with scum like thee!
                return false;
            }

            seller.PlaySound(0x32);

            IShopSellInfo[] info = GetSellInfo();
            IBuyItemInfo[] buyInfo = this.GetBuyInfo();
            int GiveGold = 0;
            int Sold = 0;
            Container cont;

            foreach (SellItemResponse resp in list)
            {
                if (resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() ||
                    !resp.Item.Movable || resp.Item is Container && ((Container) resp.Item).Items.Count != 0)
                    continue;

                foreach (IShopSellInfo ssi in info)
                {
                    if (ssi.IsSellable(resp.Item))
                    {
                        Sold++;
                        break;
                    }
                }
            }

            if (Sold > MaxSell)
            {
                SayTo(seller, true, "You may only sell {0} items at a time!", MaxSell);
                return false;
            }
            else if (Sold == 0)
            {
                return true;
            }

            foreach (SellItemResponse resp in list)
            {
                if (resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() ||
                    !resp.Item.Movable || resp.Item is Container && ((Container) resp.Item).Items.Count != 0)
                    continue;

                foreach (IShopSellInfo ssi in info)
                {
                    if (ssi.IsSellable(resp.Item))
                    {
                        int amount = resp.Amount;

                        if (amount > resp.Item.Amount)
                            amount = resp.Item.Amount;

                        if (ssi.IsResellable(resp.Item))
                        {
                            bool found = false;

                            foreach (IBuyItemInfo bii in buyInfo)
                            {
                                if (bii.Restock(resp.Item, amount))
                                {
                                    resp.Item.Consume(amount);
                                    found = true;

                                    break;
                                }
                            }

                            if (!found)
                            {
                                cont = this.BuyPack;

                                if (amount < resp.Item.Amount)
                                {
                                    Item item = Mobile.LiftItemDupe(resp.Item, resp.Item.Amount - amount);

                                    if (item != null)
                                    {
                                        item.SetLastMoved();
                                        cont.DropItem(item);
                                    }
                                    else
                                    {
                                        resp.Item.SetLastMoved();
                                        cont.DropItem(resp.Item);
                                    }
                                }
                                else
                                {
                                    resp.Item.SetLastMoved();
                                    cont.DropItem(resp.Item);
                                }
                            }
                        }
                        else
                        {
                            if (amount < resp.Item.Amount)
                                resp.Item.Amount -= amount;
                            else
                                resp.Item.Delete();
                        }

                        GiveGold += ssi.GetSellPriceFor(resp.Item) * amount;
                        break;
                    }
                }
            }
            int BeforeGiveGold = GiveGold;
            if (GiveGold > 0)
            {
                while (GiveGold > 60000)
                {
                    seller.AddToBackpack(new Gold(60000));
                    GiveGold -= 60000;
                }

                seller.AddToBackpack(new Gold(GiveGold));

                seller.PlaySound(0x0037); //Gold dropping sound
            }
            SayTo( seller, true, "Thank you! I bought {0} item{1}. Here is your {2}gp.", Sold, (Sold > 1 ? "s" : ""), BeforeGiveGold );

            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            List<SBInfo> sbInfos = this.SBInfos;

            for (int i = 0; sbInfos != null && i < sbInfos.Count; ++i)
            {
                SBInfo sbInfo = sbInfos[i];
                List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

                for (int j = 0; buyInfo != null && j < buyInfo.Count; ++j)
                {
                    GenericBuyInfo gbi = (GenericBuyInfo) buyInfo[j];

                    int maxAmount = gbi.MaxAmount;
                    int doubled = 0;

                    switch (maxAmount)
                    {
                        case 40:
                            doubled = 1;
                            break;
                        case 80:
                            doubled = 2;
                            break;
                        case 160:
                            doubled = 3;
                            break;
                        case 320:
                            doubled = 4;
                            break;
                        case 640:
                            doubled = 5;
                            break;
                        case 999:
                            doubled = 6;
                            break;
                    }

                    if (doubled > 0)
                    {
                        writer.WriteEncodedInt(1 + j * sbInfos.Count + i);
                        writer.WriteEncodedInt(doubled);
                    }
                }
            }

            writer.WriteEncodedInt(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            LoadSBInfo();

            List<SBInfo> sbInfos = this.SBInfos;

            switch (version)
            {
                case 1:
                {
                    int index;

                    while ((index = reader.ReadEncodedInt()) > 0)
                    {
                        int doubled = reader.ReadEncodedInt();

                        if (sbInfos != null)
                        {
                            index -= 1;
                            int sbInfoIndex = index % sbInfos.Count;
                            int buyInfoIndex = index / sbInfos.Count;

                            if (sbInfoIndex >= 0 && sbInfoIndex < sbInfos.Count)
                            {
                                SBInfo sbInfo = sbInfos[sbInfoIndex];
                                List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

                                if (buyInfo != null && buyInfoIndex >= 0 && buyInfoIndex < buyInfo.Count)
                                {
                                    GenericBuyInfo gbi = (GenericBuyInfo) buyInfo[buyInfoIndex];

                                    int amount = 20;

                                    switch (doubled)
                                    {
                                        case 1:
                                            amount = 40;
                                            break;
                                        case 2:
                                            amount = 80;
                                            break;
                                        case 3:
                                            amount = 160;
                                            break;
                                        case 4:
                                            amount = 320;
                                            break;
                                        case 5:
                                            amount = 640;
                                            break;
                                        case 6:
                                            amount = 999;
                                            break;
                                    }

                                    gbi.Amount = gbi.MaxAmount = amount;
                                }
                            }
                        }
                    }

                    break;
                }
            }
        }
        
        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive && IsActiveVendor)
            {
                if (IsActiveSeller)
                {
                    list.Add(new VendorBuyEntry(from, this));
                }

                if (IsActiveBuyer)
                {
                    list.Add(new VendorSellEntry(from, this));
                }
            }

            base.AddCustomContextEntries(from, list);
        }

        public virtual IShopSellInfo[] GetSellInfo()
        {
            return (IShopSellInfo[]) m_ArmorSellInfo.ToArray(typeof(IShopSellInfo));
        }

        public virtual IBuyItemInfo[] GetBuyInfo()
        {
            return (IBuyItemInfo[]) m_ArmorBuyInfo.ToArray(typeof(IBuyItemInfo));
        }
    }
}

namespace Server.ContextMenus
{
    public class VendorBuyEntry : ContextMenuEntry
    {
        private readonly BaseVendor m_Vendor;

        public VendorBuyEntry(Mobile from, BaseVendor vendor)
            : base(6103, 8)
        {
            m_Vendor = vendor;
            Enabled = vendor.CheckVendorAccess(from);
        }

        public override void OnClick()
        {
            m_Vendor.VendorBuy(Owner.From);
        }
    }

    public class VendorSellEntry : ContextMenuEntry
    {
        private readonly BaseVendor m_Vendor;

        public VendorSellEntry(Mobile from, BaseVendor vendor)
            : base(6104, 8)
        {
            m_Vendor = vendor;
            Enabled = vendor.CheckVendorAccess(from);
        }

        public override void OnClick()
        {
            m_Vendor.VendorSell(Owner.From);
        }
    }
}

namespace Server
{
    public interface IShopSellInfo
    {
        //get display name for an item
        string GetNameFor(Item item);

        //get price for an item which the player is selling
        int GetSellPriceFor(Item item);

        //get price for an item which the player is buying
        int GetBuyPriceFor(Item item);

        //can we sell this item to this vendor?
        bool IsSellable(Item item);

        //What do we sell?
        Type[] Types { get; }

        //does the vendor resell this item?
        bool IsResellable(Item item);
    }

    public interface IBuyItemInfo
    {
        //get a new instance of an object (we just bought it)
        IEntity GetEntity();

        int ControlSlots { get; }

        int PriceScalar { get; set; }

        //display price of the item
        int Price { get; }

        //display name of the item
        string Name { get; }

        //display hue
        int Hue { get; }

        //display id
        int ItemID { get; }

        //amount in stock
        int Amount { get; set; }

        //max amount in stock
        int MaxAmount { get; }

        //Attempt to restock with item, (return true if restock sucessful)
        bool Restock(Item item, int amount);

        //called when its time for the whole shop to restock
        void OnRestock();
    }
}