using System.Collections.Generic;
using Server.ContextMenus;
using Server.Items;

namespace Server.Mobiles
{
    public class Banker : BaseVendor
    {
        private readonly List<SBInfo> m_SBInfos = new();
        protected override List<SBInfo> SBInfos => m_SBInfos;

        public override NpcGuild NpcGuild => NpcGuild.MerchantsGuild;


        [Constructible]
        public Banker() : base("the Banker")
        {
        }

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBBanker());
        }

        public static int GetBalance(Mobile from)
        {
            Item[] gold;

            return GetBalance(from, out gold);
        }

        public static int GetBalance(Mobile from, out Item[] gold)
        {
            var balance = 0;

            Container bank = from.FindBankNoCreate();

            if (bank != null)
            {
                gold = bank.FindItemsByType(typeof(Gold));

                for (var i = 0; i < gold.Length; ++i)
                    balance += gold[i].Amount;
            }
            else
            {
                gold = new Item[0];
            }

            return balance;
        }

        public static bool Withdraw(Mobile from, int amount)
        {
            Item[] gold;
            var balance = GetBalance(from, out gold);

            if (balance < amount)
                return false;

            for (var i = 0; amount > 0 && i < gold.Length; ++i)
                if (gold[i].Amount <= amount)
                {
                    amount -= gold[i].Amount;
                    gold[i].Delete();
                }
                else
                {
                    gold[i].Amount -= amount;
                    amount = 0;
                }

            return true;
        }

        public static bool Deposit(Mobile from, int amount)
        {
            var box = from.FindBankNoCreate();
            if (box == null)
                return false;

            var items = new List<Item>();

            while (amount > 0)
            {
                Item item;
                if (amount <= 60000)
                {
                    item = new Gold(amount);
                    amount = 0;
                }
                else
                {
                    item = new Gold(60000);
                    amount -= 60000;
                }

                if (box.TryDropItem(from, item, false))
                {
                    items.Add(item);
                }
                else
                {
                    item.Delete();
                    foreach (var curItem in items) curItem.Delete();

                    return false;
                }
            }

            return true;
        }

        public static int DepositUpTo(Mobile from, int amount)
        {
            var box = from.FindBankNoCreate();
            if (box == null)
                return 0;

            var amountLeft = amount;
            while (amountLeft > 0)
            {
                Item item;
                int amountGiven;

                if (amountLeft <= 60000)
                {
                    item = new Gold(amountLeft);
                    amountGiven = amountLeft;
                }
                else
                {
                    item = new Gold(60000);
                    amountGiven = 60000;
                }

                if (box.TryDropItem(from, item, false))
                {
                    amountLeft -= amountGiven;
                }
                else
                {
                    item.Delete();
                    break;
                }
            }

            return amount - amountLeft;
        }

        public static void Deposit(Container cont, int amount)
        {
            while (amount > 0)
            {
                Item item;

                if (amount <= 60000)
                {
                    item = new Gold(amount);
                    amount = 0;
                }
                else
                {
                    item = new Gold(60000);
                    amount -= 60000;
                }

                cont.DropItem(item);
            }
        }

        [Constructible]
        public Banker(Serial serial) : base(serial)
        {
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(Location, 12))
                return true;

            return base.HandlesOnSpeech(from);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile.InRange(Location, 12))
                for (var i = 0; i < e.Keywords.Length; ++i)
                {
                    var keyword = e.Keywords[i];

                    switch (keyword)
                    {
                        case 0x0000: // *withdraw*
                        {
                            e.Handled = true;

                            if (e.Mobile.Criminal)
                            {
                                Say(500389); // I will not do business with a criminal!
                                break;
                            }

                            var split = e.Speech.Split(' ');

                            if (split.Length >= 2)
                            {
                                int amount;

                                var pack = e.Mobile.Backpack;

                                if (!int.TryParse(split[1], out amount))
                                    break;

                                if (amount > 5000)
                                {
                                    Say(500381); // Thou canst not withdraw so much at one time!
                                }
                                else if (pack == null || pack.Deleted || !(pack.TotalWeight < pack.MaxWeight) ||
                                         !(pack.TotalItems < pack.MaxItems))
                                {
                                    Say(1048147); // Your backpack can't hold anything else.
                                }
                                else if (amount > 0)
                                {
                                    var box = e.Mobile.FindBankNoCreate();

                                    if (box == null || !box.ConsumeTotal(typeof(Gold), amount))
                                    {
                                        Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold!
                                    }
                                    else
                                    {
                                        pack.DropItem(new Gold(amount));

                                        Say(1010005); // Thou hast withdrawn gold from thy account.
                                    }
                                }
                            }

                            break;
                        }
                        case 0x0001: // *balance*
                        {
                            e.Handled = true;

                            if (e.Mobile.Criminal)
                            {
                                Say(500389); // I will not do business with a criminal!
                                break;
                            }

                            var box = e.Mobile.FindBankNoCreate();

                            if (box != null)
                                Say(1042759, box.TotalGold.ToString()); // Thy current bank balance is ~1_AMOUNT~ gold.
                            else
                                Say(1042759, "0"); // Thy current bank balance is ~1_AMOUNT~ gold.

                            break;
                        }
                        case 0x0002: // *bank*
                        {
                            e.Handled = true;

                            if (e.Mobile.Criminal)
                            {
                                Say(500378); // Thou art a criminal and cannot access thy bank box.
                                break;
                            }

                            e.Mobile.BankBox.Open();

                            break;
                        }
                    }
                }

            base.OnSpeech(e);
        }
        
        public override void AddCustomContextEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive)
            {
                list.Add(new OpenBankEntry(this));
            }

            base.AddCustomContextEntries(from, list);
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}