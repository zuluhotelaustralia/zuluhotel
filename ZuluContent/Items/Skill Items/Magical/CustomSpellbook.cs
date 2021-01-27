using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Multis;
using Server.Spells;

namespace Server.Items
{
    public class CustomSpellbook : Item, ISecurable
    {
        private ulong m_Entries;
        private SecureLevel m_Level;

        private List<Mobile> m_Openers = new List<Mobile>();

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        public List<Mobile> Openers
        {
            get { return m_Openers; }
            set { m_Openers = value; }
        }


        [Constructible]
        public CustomSpellbook(int itemID) : base(itemID)
        {
            Weight = 3.0;
            LootType = LootType.Blessed;

            Layer = Layer.OneHanded;

            m_Entries = (ulong) 0;

            m_Level = SecureLevel.CoOwners;
        }


        [CommandProperty(AccessLevel.GameMaster)]
        public ulong Entries
        {
            get => m_Entries;
            set
            {
                if (m_Entries != value)
                {
                    m_Entries = value;
                }
            }
        }

        [Constructible]
        public CustomSpellbook(Serial serial) : base(serial)
        {
        }

        public bool HasSpell(SpellEntry spellId)
        {
            spellId -= BookOffset;

            return spellId >= 0 && (int) spellId < BookCount && (m_Entries & ((ulong) 1 << (int) spellId)) != 0;
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0);

            writer.Write((int) m_Level);

            writer.Write(m_Entries);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            LootType = LootType.Blessed;

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_Level = (SecureLevel) reader.ReadInt();

                    m_Entries = reader.ReadULong();

                    break;
                }
            }
        }

        public virtual int BookOffset
        {
            get { return 0; }
        }

        public virtual int BookCount
        {
            get { return 16; }
        }

        public virtual SpellCircle SpellbookCircle
        {
            get => 0;
        }

        public static SpellCircle GetCircleForSpell(SpellEntry spellEntry)
        {
            return SpellRegistry.GetInfo(spellEntry).Circle;
        }

        public bool CanAddEntry(Mobile from, CustomSpellScroll scroll)
        {
            if (scroll.Amount == 1)
            {
                SpellCircle circle = GetCircleForSpell(scroll.SpellEntry);

                if (circle != SpellbookCircle)
                    return false;

                if (HasSpell(scroll.SpellEntry))
                {
                    from.SendLocalizedMessage(500179); // That spell is already present in that spellbook.
                    return false;
                }

                int val = (int) scroll.SpellEntry - BookOffset;

                if (val >= 0 && val < BookCount)
                {
                    m_Entries |= (ulong) 1 << val;

                    scroll.Delete();
                    return true;
                }

                return false;
            }

            return false;
        }

        public void AddEntry(CustomSpellScroll scroll)
        {
            if (scroll.Amount == 1)
            {
                int val = (int) scroll.SpellEntry - BookOffset;

                if (val >= 0 && val < BookCount)
                {
                    m_Entries |= (ulong) 1 << val;

                    scroll.Delete();
                }
            }
        }

        public bool IsOpen(Mobile toCheck)
        {
            NetState ns = toCheck.NetState;

            if (ns != null)
            {
                foreach (var gump in ns.Gumps)
                {
                    CustomSpellbookGump bookGump = gump as CustomSpellbookGump;

                    if (bookGump != null && bookGump.Book == this)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override bool DisplayLootType
        {
            get { return false; }
        }

        public override bool OnDragLift(Mobile from)
        {
            if (from.HasGump<CustomSpellbookGump>())
            {
                from.SendLocalizedMessage(500169); // You cannot pick that up.
                return false;
            }

            foreach (Mobile m in m_Openers)
                if (IsOpen(m))
                    m.CloseGump<CustomSpellbookGump>();
            ;

            m_Openers.Clear();

            return true;
        }

        public override void OnSingleClick(Mobile from)
        {
            if (!string.IsNullOrEmpty(Name))
                LabelTo(from, Name);
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

                m_Openers.Add(from);
            }
        }

        public virtual void OnOpenSpellbook(Mobile from)
        {
        }

        public override void OnAfterDuped(Item newItem)
        {
            CustomSpellbook book = newItem as CustomSpellbook;

            if (book == null)
                return;

            book.m_Entries = m_Entries;
        }

        public bool CheckAccess(Mobile m)
        {
            if (!IsLockedDown || m.AccessLevel >= AccessLevel.GameMaster)
                return true;

            BaseHouse house = BaseHouse.FindHouseAt(this);

            return house != null && house.HasSecureAccess(m, m_Level);
        }
    }
}