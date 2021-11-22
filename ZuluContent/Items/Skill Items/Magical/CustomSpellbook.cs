using System;
using System.Collections.Generic;
using Scripts.Zulu.Utilities;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Spells;

namespace Server.Items
{
    public abstract class CustomSpellbook : Item, ISecurable, ISpellbook
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get; set; }
        [CommandProperty(AccessLevel.GameMaster)]
        public List<Mobile> Openers { get; set; } = new();

        [CommandProperty(AccessLevel.GameMaster)]
        public ulong Content { get; set; }
        public abstract Type SpellType { get; }
        public virtual int BookOffset { get; } = 0;
        public virtual int BookCount { get; } = 16;
        public override bool DisplayLootType { get; } = false;

        public virtual Dictionary<SpellEntry, int> SpellIcons { get; }

        [Constructible]
        public CustomSpellbook(int itemId) : base(itemId)
        {
            Weight = 3.0;
            LootType = LootType.Blessed;

            Layer = Layer.OneHanded;

            Content = 0;

            Level = SecureLevel.CoOwners;
        }

        [Constructible]
        public CustomSpellbook(Serial serial) : base(serial)
        {
        }

        public bool HasSpell(SpellEntry spellId)
        {
            spellId -= BookOffset;

            return spellId >= 0 && (int) spellId < BookCount && (Content & ((ulong) 1 << (int) spellId)) != 0;
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            return true;
        }

        public virtual bool CanAddEntry(Mobile from, CustomSpellScroll scroll)
        {
            if (!SpellRegistry.GetInfo(scroll.SpellEntry).Type.IsAssignableTo(SpellType))
            {
                from.SendFailureMessage(501624); // Can't inscribe that item.
                return false;
            }
            
            if (scroll.Amount == 1)
            {
                if (HasSpell(scroll.SpellEntry))
                {
                    from.SendFailureMessage(500179); // That spell is already present in that spellbook.
                    return false;
                }

                var val = (int) scroll.SpellEntry - BookOffset;

                if (val >= 0 && val < BookCount)
                {
                    Content |= (ulong) 1 << val;

                    scroll.Delete();
                    return true;
                }

                return false;
            }

            return false;
        }

        public void AddEntry(CustomSpellScroll scroll)
        {
            var val = (int) scroll.SpellEntry - BookOffset;

            if (val >= 0 && val < BookCount)
            {
                Content |= (ulong) 1 << val;

                scroll.Consume();
            }
        }

        public bool IsOpen(Mobile toCheck)
        {
            var ns = toCheck.NetState;

            if (ns != null)
            {
                foreach (var gump in ns.Gumps)
                {
                    if (gump is CustomSpellbookGump bookGump && bookGump.Book == this)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public override bool OnDragLift(Mobile from)
        {
            if (from.HasGump<CustomSpellbookGump>())
            {
                from.SendLocalizedMessage(500169); // You cannot pick that up.
                return false;
            }

            foreach (var m in Openers)
                if (IsOpen(m))
                    m.CloseGump<CustomSpellbookGump>();

            Openers.Clear();
            return true;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(GetWorldLocation(), 1) && CheckAccess(from))
            {
                if (RootParent is BaseCreature)
                {
                    from.SendLocalizedMessage(502402); // That is inaccessible.
                    return;
                }

                from.CloseGump<CustomSpellbookGump>();

                OnOpenSpellbook(from);

                Openers.Add(from);
            }
        }

        public virtual void OnOpenSpellbook(Mobile from)
        {
        }

        public override void OnAfterDuped(Item newItem)
        {
            if (!(newItem is CustomSpellbook book))
                return;

            book.Content = Content;
        }

        public bool CheckAccess(Mobile m)
        {
            if (!IsLockedDown || m.AccessLevel >= AccessLevel.GameMaster)
                return true;

            var house = BaseHouse.FindHouseAt(this);
            return house != null && house.HasSecureAccess(m, Level);
        }
        
        
        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
            writer.Write((int) Level);
            writer.Write(Content);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            LootType = LootType.Blessed;
            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    Level = (SecureLevel) reader.ReadInt();
                    Content = reader.ReadULong();
                    break;
                }
            }
        }
    }
}