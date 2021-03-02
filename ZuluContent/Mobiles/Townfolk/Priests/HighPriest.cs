using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Mobiles
{
    public enum PriestDemand
    {
        RemoveCurse,
        DetectCurse,
        Purify,
        CastHeal,
        CastCure,
        CastProtect,
        CastBless
    }

    public class HighPriest : BasePriest
    {
        public override bool CanTeach
        {
            get { return false; }
        }

        private record PriestRequest
        {
            protected PriestDemand PriestDemand { get; init; }
            protected int PriestPrice { get; init; }
            protected Serial TargetedItem { get; init; }
        }

        private Dictionary<Serial, PriestRequest> PriestRequests { get; set; } =
            new Dictionary<Serial, PriestRequest>();

        [Constructible]
        public HighPriest()
        {
            Title = "the High Priest";

            AddItem(new GnarledStaff());

            SetSkill(SkillName.Camping, 80.0, 100.0);
            SetSkill(SkillName.Forensics, 80.0, 100.0);
            SetSkill(SkillName.SpiritSpeak, 80.0, 100.0);
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the High Priest";
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.Alive && from.InRange(this, 3))
                return true;

            return base.HandlesOnSpeech(from);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile.Alive)
            {
                switch (e.Speech.ToLower())
                {
                    case "remove curse":
                    case "remove the curse":
                    case "uncurse":
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Show me the cursed item.");
                        e.Mobile.BeginTarget(12, false, TargetFlags.None, Appraise_OnTarget);
                        e.Handled = true;
                        break;
                    case "purify":
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                            "What item dost thou want me to free from all malefic magical influences?");
                        e.Mobile.BeginTarget(12, false, TargetFlags.None, Purify_OnTarget);
                        e.Handled = true;
                        break;
                }
            }

            base.OnSpeech(e);
        }

        public void Appraise_OnTarget(Mobile from, object obj)
        {
            if (obj is BaseEquippableItem equippableItem)
            {
                BaseEquippableItem item = equippableItem;

                if (item.Cursed == CurseType.None)
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                    return;
                }

                int price = 500; // ComputePriceFor(deed);

                if (price > 0)
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                        "I will remove the curse from that item for at least " + price + " gold pieces.");
                }
                else
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                }
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2,
                    500609); // I can't appraise things I know nothing about...
            }
        }

        public void Purify_OnTarget(Mobile from, object obj)
        {
            if (obj is BaseEquippableItem)
            {
                BaseEquippableItem item = (BaseEquippableItem) obj;

                if (item.Cursed == CurseType.None)
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                    return;
                }

                int price = 500; // ComputePriceFor(deed);

                // Testing POC
                from.SendGump(new WarningGump(1060637, 30720,
                    $"Purify item for {price} gold pieces?",
                    0xFFC000, 420, 280, okay => OnConfirmPurifyCallback(from, okay, item, price)));
            }
            else
            {
                PublicOverheadMessage(MessageType.Regular, 0x3B2, true, "Are you deaf?");
                Timer.DelayCall(TimeSpan.FromSeconds(2), () =>
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                        "I asked you what ITEM you wanted me to free from all malefical magical influences!");
                });
            }
        }

        private void OnConfirmPurifyCallback(Mobile from, bool okay, BaseEquippableItem item, int price)
        {
            if (okay)
                if (Banker.Withdraw(from, price))
                {
                    Direction = GetDirectionTo(from);
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                        "Great Essence of the Virtues, I'm calling upon you to purify this item from all curses");
                    Animate(17, 10, 1, true, false, 0);
                    Timer.DelayCall(TimeSpan.FromSeconds(8), () => {
                        item.Cursed = CurseType.None;
                        from.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                        from.PlaySound(0x1EA);
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                            "I made it!!! That item is now completely free from all curses.");
                    });
                }
                else
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, true,
                        "You donot have enough gold in your bank for that.");
        }

        /* public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is HouseDeed)
            {
                HouseDeed deed = (HouseDeed)dropped;
                int price = ComputePriceFor(deed);

                if (price > 0)
                {
                    if (Banker.Deposit(from, price))
                    {
                        // For the deed I have placed gold in your bankbox :
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, 1008000, AffixType.Append, price.ToString(), "");

                        deed.Delete();
                        return true;
                    }
                    else
                    {
                        PublicOverheadMessage(MessageType.Regular, 0x3B2, 500390); // Your bank box is full.
                        return false;
                    }
                }
                else
                {
                    PublicOverheadMessage(MessageType.Regular, 0x3B2, 500607); // I'm not interested in that.
                    return false;
                }
            }

            return base.OnDragDrop(from, dropped);
        } */

        public override bool ClickTitle
        {
            get { return false; }
        } // Do not display title in OnSingleClick

        public override bool CheckResurrect(Mobile m)
        {
            if (m.Criminal)
            {
                Say(501222); // Thou art a criminal.  I shall not resurrect thee.
                return false;
            }
            else if (m.Kills >= 5)
            {
                Say(501223); // Thou'rt not a decent and good person. I shall not resurrect thee.
                return false;
            }
            else if (m.Karma < 0)
            {
                Say(501224); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.
            }

            return true;
        }

        [Constructible]
        public HighPriest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}