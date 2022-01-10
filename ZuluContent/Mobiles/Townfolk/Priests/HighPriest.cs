using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;
using ZuluContent.Zulu.Items.SingleClick;

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

    public class PriestRequest
    {
        public PriestDemand PriestDemand { get; }
        
        public int PriestPrice { get; }
        
        public BaseEquippableItem TargetedItem { get; }
        
        public PriestRequest(PriestDemand priestDemand, int priestPrice, BaseEquippableItem targetedItem)
        {
            PriestDemand = priestDemand;
            PriestPrice = priestPrice;
            TargetedItem = targetedItem;
        }

        public static PriestRequest Deserialize(IGenericReader reader)
        {
            var version = reader.ReadInt();

            return new PriestRequest(
                (PriestDemand) reader.ReadInt(),
                reader.ReadInt(),
                reader.ReadEntity<BaseEquippableItem>());
        }

        public void Serialize(IGenericWriter writer)
        {
            writer.Write(0);
            
            writer.Write((int) PriestDemand);
            writer.Write(PriestPrice);
            writer.Write(TargetedItem);
        }
    }

    public class HighPriest : BasePriest
    {
        public override bool CanTeach => false;

        [CommandProperty(AccessLevel.GameMaster)]
        private Dictionary<uint, PriestRequest> PriestRequests { get; set; } = new();

        [CommandProperty(AccessLevel.GameMaster)]
        private Dictionary<uint, bool> PriestUpsetters { get; set; } = new();

        [Constructible]
        public HighPriest()
        {
            Title = "the High Priest";

            AddItem(new GnarledStaff());

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
                Direction = GetDirectionTo(e.Mobile);
                    
                PriestUpsetters.TryGetValue(e.Mobile.Serial.Value, out var upset);

                if (upset)
                {
                    Say("No, I won't listen to thy request.");
                    Say("Thine past behavior made thee unworthy in the eyes of the Virtues!");
                    Say("Perhaps for a worthy donation I wilst forgive thee.");
                }
                else
                {
                    switch (e.Speech.ToLower())
                    {
                        case "greetings":
                        case "hail":
                        case "hi":
                        case "hello":
                            Say("Greetings, I hope thee is having a good day!");
                            break;
                        case "help":
                            Say("How can I help thee?");
                            Say("Say uncurse, detect curse, or purify and I will attempt to remove the curse from an item.");
                            break;
                        case "detect curse":
                            Say("Which item do you suspect to be cursed?");
                            e.Mobile.BeginTarget(12, false, TargetFlags.None, Detect_OnTarget);
                            e.Handled = true;
                            break;
                        case "remove curse":
                        case "remove the curse":
                        case "uncurse":
                            Say("Show me the cursed item.");
                            e.Mobile.BeginTarget(12, false, TargetFlags.None, Uncurse_OnTarget);
                            e.Handled = true;
                            break;
                        case "purify":
                            Say("What item dost thou want me to free from all malefic magical influences?");
                            e.Mobile.BeginTarget(12, false, TargetFlags.None, Purify_OnTarget);
                            e.Handled = true;
                            break;
                    }
                }
            }

            base.OnSpeech(e);
        }
        
        private async void Detect_OnTarget(Mobile from, object obj)
        {
            if (obj is BaseEquippableItem equippableItem)
            {
                if (equippableItem.Cursed is CurseType.Revealed or CurseType.RevealedCantUnEquip)
                {
                    Say("That item isn't only cursed, but the curse is also already revealed.");
                }
                else
                {
                    var price = from.Str;

                    PriestRequests[from.Serial.Value] =
                        new PriestRequest(PriestDemand.DetectCurse, price, equippableItem);

                    Say($"I'll tell thee if that item is cursed if thee can spare at least {price} gold pieces.");
                }
            }
            else if (obj is PlayerMobile)
            {
                Say("I really don't think it's cursed, nor that it will ever be.");
            }
            else
            {
                Say("I can't detect a curse from something I know nothing about.");
            }
        }

        private async void Uncurse_OnTarget(Mobile from, object obj)
        {
            if (obj is BaseEquippableItem equippableItem)
            {
                if (equippableItem.Cursed == CurseType.RevealedCantUnEquip)
                {
                    var price = from.Str * 10;

                    PriestRequests[from.Serial.Value] =
                        new PriestRequest(PriestDemand.RemoveCurse, price, equippableItem);

                    Say($"I will remove the curse from that item for at least {price} gold pieces.");
                }
                else
                {
                    Say("I can't remove a curse from an item that isn't cursed or don't seem to be.");
                }
            }
            else if (obj is PlayerMobile)
            {
                Say("The Virtues of Compassion teach us to always respect the people around us.");
                await Timer.Pause(5000);
                Say("That also means to not insult them by telling them they are a curse.");
            }
            else
            {
                Say("I can't remove a curse from something I know nothing about.");
            }
        }

        public async void Purify_OnTarget(Mobile from, object obj)
        {
            if (obj is BaseEquippableItem equippableItem)
            {
                if (equippableItem.Cursed != CurseType.None)
                {
                    var price = from.Str * 250;

                    PriestRequests[from.Serial.Value] =
                        new PriestRequest(PriestDemand.Purify, price, equippableItem);

                    Say($"That's a hard feat and I'll only do it if thee agree to make a donation of at least {price} gold pieces.");
                }
                else
                {
                    Say("I can't purify an item that isn't cursed or don't seem to be.");
                }
            }
            else if (obj is PlayerMobile)
            {
                Say("Are you deaf?");
                Timer.Pause(2000);
                Say("I asked you what ITEM you wanted me to free from all malefical magical influences!");
            }
            else
            {
                Say("I can't purify something I know nothing about.");
            }
        }
        
        private async void DetectCurse(Mobile from, BaseEquippableItem item, int donation, int price)
        {
            var chance = 90;

            if (donation > price)
            {
                chance += (donation - price) / 2;
            }

            Direction = GetDirectionTo(from);
            StallMovement(TimeSpan.FromSeconds(10));
            
            Say("Powers of the Virtues, I'm calling upon you to show us if that item is under malefic influences.");
            Animate(203, 7, 1, true, false, 0);
            await Timer.Pause(5000);

            if (Utility.RandomMinMax(1, 100) <= chance)
            {
                if (item.Cursed != CurseType.None)
                {
                    item.Cursed = CurseType.Revealed;
                    Say($"Ah ah! That item is cursed and it revealed itself to be a {SingleClickHandler.GetMagicItemName(item)}.");
                    from.PlaySound(0x1FD);
                }
                else
                {
                    Say("I don't sense presence of any curses in that item.");
                }
            }
            else
            {
                Say("I failed to see if that item is cursed!");
                FixedEffect(0x3735, 6, 30);
                from.PlaySound(0x05A);
                await Timer.Pause(2000);
                Say($"Maybe if thee had give me more than {donation} gold pieces, I would have succeeded.");
            }
        }

        private async void RemoveCurse(Mobile from, BaseEquippableItem item, int donation, int price)
        {
            var chance = 75;

            if (donation > price)
            {
                chance += (donation - price) / 10;
            }

            Direction = GetDirectionTo(from);
            StallMovement(TimeSpan.FromSeconds(10));
            Say("Powers of the Virtues, I'm calling upon you to remove the curse from that item.");
            Animate(203, 7, 1, true, false, 0);
            await Timer.Pause(6000);

            if (Utility.RandomMinMax(1, 100) <= chance)
            {
                item.Cursed = CurseType.Revealed;
                Say($"From now on thee can unequip this {SingleClickHandler.GetMagicItemName(item)} as any normal item.");
                from.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                from.PlaySound(0x1EA);
            }
            else
            {
                Say("Oh no! I failed in removing that curse!");
                from.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                from.PlaySound(0x1E1);
                await Timer.Pause(6000);
                Say($"Maybe if thee had give me more than {donation} gold pieces, I would have succeeded.");
            }
        }
        
        private async void Purify(Mobile from, BaseEquippableItem item, int donation, int price)
        {
            var chance = 60;

            if (donation > price)
            {
                chance += (donation - price) / 100;
            }

            Direction = GetDirectionTo(from);
            StallMovement(TimeSpan.FromSeconds(10));
            Say("Great Essence of the Virtues, I'm calling upon you to purify this item from all curses.");
            Animate(203, 7, 1, true, false, 0);
            await Timer.Pause(8000);

            if (Utility.RandomMinMax(1, 100) <= chance)
            {
                item.Cursed = CurseType.None;
                Say("I made it!!! That item is now completely free from all curses.");
                from.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                from.PlaySound(0x1EA);
            }
            else
            {
                Say("That curse is too strong, I failed to obliterate it!");
                from.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                from.PlaySound(0x1E1);
                await Timer.Pause(2000);
                Say($"Maybe if thee had give me more than {donation} gold pieces, I would have succeeded.");
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is not Gold gold)
            {
                Say(500607); // I'm not interested in that.
                return false;
            }

            PriestRequests.TryGetValue(from.Serial.Value, out var demand);
            if (demand == null)
            {
                PriestUpsetters.TryGetValue(from.Serial.Value, out var upset);
                if (upset)
                {
                    if (gold.Amount >= from.Str * 2)
                    {
                        Say("Thee be blessed for this generous donation.");
                        PriestUpsetters.Remove(from.Serial.Value);
                    }
                    else
                    {
                        Say("Dost thou think that they can be forgiven for such a pitiful amount of gold?");
                    }
                }
                else
                {
                    Say("Thee be blessed for this generous donation.");
                    gold.Delete();
                }

                return true;
            }
            
            PriestRequests.Remove(from.Serial.Value);

            var price = demand.PriestPrice;
            if (gold.Amount < price)
            {
                Say($"I was very generous by asking thee such a low price as {price} gold pieces for that service.");
                Say("So I had expected thee to have the politeness to give me what I asked for.");
                PriestUpsetters[from.Serial.Value] = true;
                return false;
            }

            switch (demand.PriestDemand)
            {
                case PriestDemand.DetectCurse:
                {
                    DetectCurse(from, demand.TargetedItem, gold.Amount, demand.PriestPrice);
                    break;
                }
                case PriestDemand.RemoveCurse:
                {
                    RemoveCurse(from, demand.TargetedItem, gold.Amount, demand.PriestPrice);
                    break;
                }
                case PriestDemand.Purify:
                {
                    Purify(from, demand.TargetedItem, gold.Amount, demand.PriestPrice);
                    break;
                }
            }

            gold.Delete();
            return true;
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick

        public override bool CheckResurrect(Mobile m)
        {
            if (m.Criminal)
            {
                Say(501222); // Thou art a criminal.  I shall not resurrect thee.
                return false;
            }

            if (m.Kills >= 5)
            {
                Say(501223); // Thou'rt not a decent and good person. I shall not resurrect thee.
                return false;
            }

            if (m.Karma < 0)
                Say(501224); // Thou hast strayed from the path of virtue, but thou still deservest a second chance.

            return true;
        }
        
        [Constructible]
        public HighPriest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
            
            PriestRequests.Tidy();
            
            var priestRequestsCount = PriestRequests.Count;
            writer.WriteEncodedInt(priestRequestsCount);
            
            if (priestRequestsCount > 0)
            {
                foreach (var (priestRequestsKey, priestRequestsValue) in PriestRequests)
                {
                    writer.Write(priestRequestsKey);
                    priestRequestsValue.Serialize(writer);
                }
            }
            
            PriestUpsetters.Tidy();
            
            var priestUpsettersCount = PriestUpsetters.Count;
            writer.WriteEncodedInt(priestUpsettersCount);
            
            if (priestUpsettersCount > 0)
            {
                foreach (var (priestUpsettersKey, priestUpsettersValue) in PriestUpsetters)
                {
                    writer.Write(priestUpsettersKey);
                    writer.Write(priestUpsettersValue);
                }
            }
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            
            var priestRequestsCount = reader.ReadEncodedInt();
            PriestRequests = new Dictionary<uint, PriestRequest> (priestRequestsCount);
            for (var priestRequestsIndex = 0; priestRequestsIndex < priestRequestsCount; priestRequestsIndex++)
            {
                var key = reader.ReadUInt();
                var val = PriestRequest.Deserialize(reader);
                PriestRequests.Add(key, val);
            }
            
            var priestUpsettersCount = reader.ReadEncodedInt();
            PriestUpsetters = new Dictionary<uint, bool> (priestUpsettersCount);
            for (var priestUpsettersIndex = 0; priestUpsettersIndex < priestRequestsCount; priestUpsettersIndex++)
            {
                var key = reader.ReadUInt();
                var val = reader.ReadBool();
                PriestUpsetters.Add(key, val);
            }
        }
    }
}