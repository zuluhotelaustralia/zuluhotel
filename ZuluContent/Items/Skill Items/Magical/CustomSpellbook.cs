using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Multis;
using Server.Spells;
using static Server.Spells.SpellEntries;

namespace Server.Items
{
    public class CustomSpellbook : Item, ISecurable
    {
        private List<string> m_Entries;
        private int m_DefaultIndex;
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

            m_Entries = new List<string>(new string[16]);

            m_DefaultIndex = -1;

            m_Level = SecureLevel.CoOwners;
        }


        public List<string> Entries
        {
            get { return m_Entries; }
        }

        public string Default
        {
            get
            {
                if (m_DefaultIndex >= 0 && m_DefaultIndex < m_Entries.Count)
                    return m_Entries[m_DefaultIndex];

                return null;
            }
            set
            {
                if (value == null)
                    m_DefaultIndex = -1;
                else
                    m_DefaultIndex = m_Entries.IndexOf(value);
            }
        }

        [Constructible]
        public CustomSpellbook(Serial serial) : base(serial)
        {
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

            writer.Write(m_Entries.Count);

            for (int i = 0; i < m_Entries.Count; ++i)
                writer.Write(m_Entries[i]);

            writer.Write(m_DefaultIndex);
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
                    int count = reader.ReadInt();

                    m_Entries = new List<string>(new string[count]);

                    for (int i = 0; i < count; ++i)
                        m_Entries[i] = reader.ReadString();

                    m_DefaultIndex = reader.ReadInt();

                    break;
                }
            }
        }

        public void AddEntry(SpellEntry entry)
        {
            var entryIndex = GetSpellEntryIndex(entry);
            m_Entries[entryIndex] = GetSpellEntryName(entry);
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

            book.m_Entries = new List<string>(16);

            for (int i = 0; i < m_Entries.Count; i++)
            {
                string entry = m_Entries[i];

                book.m_Entries[i] = entry;
            }
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