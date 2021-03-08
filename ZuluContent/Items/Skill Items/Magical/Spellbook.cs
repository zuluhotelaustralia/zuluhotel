using System;
using System.Collections.Generic;
using Server.Commands;
using Server.Engines.Craft;
using Server.Network;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
    public enum SpellbookType
    {
        Invalid = -1,
        Regular
    }

    public class Spellbook : Item, ISlayer
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public string EngravedText { get; set; }

        public static void Initialize()
        {
            EventSink.OpenSpellbookRequest += EventSink_OpenSpellbookRequest;
            EventSink.CastSpellRequest += EventSink_CastSpellRequest;

            CommandSystem.Register("AllSpells", AccessLevel.GameMaster, AllSpells_OnCommand);
        }

        [Usage("AllSpells")]
        [Description("Completely fills a targeted spellbook with scrolls.")]
        private static void AllSpells_OnCommand(CommandEventArgs e)
        {
            e.Mobile.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
            e.Mobile.SendMessage("Target the spellbook to fill.");
        }

        private static void AllSpells_OnTarget(Mobile from, object obj)
        {
            if (obj is Spellbook book)
            {
                if (book.BookCount == 64)
                    book.Content = ulong.MaxValue;
                else
                    book.Content = (1ul << book.BookCount) - 1;

                from.SendMessage("The spellbook has been filled.");

                CommandLogging.WriteLine(from, "{0} {1} filling spellbook {2}", from.AccessLevel,
                    CommandLogging.Format(from), CommandLogging.Format(book));
            }
            else
            {
                from.BeginTarget(-1, false, TargetFlags.None, AllSpells_OnTarget);
                from.SendMessage("That is not a spellbook. Try again.");
            }
        }

        private static void EventSink_OpenSpellbookRequest(Mobile from, int i)
        {
            Find(from, SpellEntry.None, SpellbookType.Regular).DisplayTo(from);
        }

        private static void EventSink_CastSpellRequest(Mobile from, int id, Item item)
        {
            var spellId = (SpellEntry) id;

            if (!(item is Spellbook book) || !book.HasSpell(spellId))
                book = Find(from, spellId);

            if (book != null && book.HasSpell(spellId))
            {
                var spell = SpellRegistry.Create(spellId, from, null);

                if (spell != null)
                    spell.Cast();
                else
                    from.SendLocalizedMessage(502345); // This spell has been temporarily disabled.
            }
            else
            {
                from.SendLocalizedMessage(500015); // You do not have that spell!
            }
        }

        private static readonly Dictionary<Mobile, List<Spellbook>> Table = new Dictionary<Mobile, List<Spellbook>>();

        public static SpellbookType GetTypeForSpell(SpellEntry spellId)
        {
            if ((int) spellId >= 0 && (int) spellId < 64)
                return SpellbookType.Regular;

            return SpellbookType.Invalid;
        }

        public static Spellbook Find(Mobile from, SpellEntry spellId)
        {
            return Find(from, spellId, GetTypeForSpell(spellId));
        }

        public static Spellbook Find(Mobile from, SpellEntry spellId, SpellbookType type)
        {
            if (from == null)
                return null;

            if (from.Deleted)
            {
                Table.Remove(from);
                return null;
            }

            List<Spellbook> list = null;

            Table.TryGetValue(from, out list);

            bool searchAgain = false;

            if (list == null)
                Table[from] = list = FindAllSpellbooks(from);
            else
                searchAgain = true;

            Spellbook book = FindSpellbookInList(list, from, spellId, type);

            if (book == null && searchAgain)
            {
                Table[from] = list = FindAllSpellbooks(from);

                book = FindSpellbookInList(list, from, spellId, type);
            }

            return book;
        }

        public static Spellbook FindSpellbookInList(List<Spellbook> list, Mobile from, SpellEntry spellId,
            SpellbookType type)
        {
            Container pack = from.Backpack;

            for (int i = list.Count - 1; i >= 0; --i)
            {
                if (i >= list.Count)
                    continue;

                Spellbook book = list[i];

                if (!book.Deleted && (book.Parent == from || pack != null && book.Parent == pack) &&
                    ValidateSpellbook(book, spellId, type))
                    return book;

                list.RemoveAt(i);
            }

            return null;
        }

        public static List<Spellbook> FindAllSpellbooks(Mobile from)
        {
            List<Spellbook> list = new List<Spellbook>();

            Item item = from.FindItemOnLayer(Layer.OneHanded);

            if (item is Spellbook)
                list.Add((Spellbook) item);

            Container pack = from.Backpack;

            if (pack == null)
                return list;

            for (int i = 0; i < pack.Items.Count; ++i)
            {
                item = pack.Items[i];

                if (item is Spellbook)
                    list.Add((Spellbook) item);
            }

            return list;
        }

        public static Spellbook FindEquippedSpellbook(Mobile from)
        {
            return @from.FindItemOnLayer(Layer.OneHanded) as Spellbook;
        }

        public static bool ValidateSpellbook(Spellbook book, SpellEntry spellId, SpellbookType type)
        {
            return book.SpellbookType == type && (spellId == SpellEntry.None || book.HasSpell(spellId));
        }

        public override bool DisplayWeight
        {
            get { return false; }
        }

        public virtual SpellbookType SpellbookType
        {
            get { return SpellbookType.Regular; }
        }

        public virtual int BookOffset
        {
            get { return 0; }
        }

        public virtual int BookCount
        {
            get { return 64; }
        }

        private ulong m_Content;

        public override bool CanEquip(Mobile from)
        {
            if (!from.CanBeginAction(typeof(BaseWeapon)))
            {
                return false;
            }

            return base.CanEquip(from);
        }

        public override bool AllowEquippedCast(Mobile from)
        {
            return true;
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is SpellScroll scroll && scroll.Amount == 1)
            {
                SpellbookType type = GetTypeForSpell(scroll.SpellEntry);

                if (type != SpellbookType)
                {
                    return false;
                }
                else if (HasSpell(scroll.SpellEntry))
                {
                    from.SendLocalizedMessage(500179); // That spell is already present in that spellbook.
                    return false;
                }
                else
                {
                    int val = (int) scroll.SpellEntry - BookOffset;

                    if (val >= 0 && val < BookCount)
                    {
                        m_Content |= (ulong) 1 << val;
                        ++SpellCount;

                        scroll.Delete();

                        from.SendSound(0x249, GetWorldLocation());
                        return true;
                    }

                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ulong Content
        {
            get { return m_Content; }
            set
            {
                if (m_Content != value)
                {
                    m_Content = value;

                    SpellCount = 0;

                    while (value > 0)
                    {
                        SpellCount += (int) (value & 0x1);
                        value >>= 1;
                    }
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpellCount { get; private set; }


        [Constructible]
        public Spellbook() : this((ulong) 0)
        {
        }


        [Constructible]
        public Spellbook(ulong content) : this(content, 0xEFA)
        {
        }

        [Constructible]
        public Spellbook(ulong content, int itemId) : base(itemId)
        {
            Weight = 3.0;
            Layer = Layer.OneHanded;
            LootType = LootType.Blessed;

            Content = content;
        }

        public bool HasSpell(SpellEntry spellId)
        {
            spellId -= BookOffset;

            return spellId >= 0 && (int) spellId < BookCount && (m_Content & ((ulong) 1 << (int) spellId)) != 0;
        }

        [Constructible]
        public Spellbook(Serial serial) : base(serial)
        {
        }

        public void DisplayTo(Mobile to)
        {
            // The client must know about the spellbook or it will crash!
            var ns = to.NetState;

            if (ns == null)
            {
                return;
            }

            if (Parent == null)
            {
                SendWorldPacketTo(to.NetState);
            }
            else if (Parent is Item)
            {
                to.NetState.SendContainerContentUpdate(this);
            }
            else if (Parent is Mobile)
            {
                to.NetState.SendEquipUpdate(this);
            }

            to.NetState.SendDisplaySpellbook(Serial);
            to.NetState.SendSpellbookContent(Serial, ItemID, BookOffset + 1, m_Content);
        }

        public override bool DisplayLootType
        {
            get { return false; }
        }

        public override void OnSingleClick(Mobile from)
        {
            base.OnSingleClick(from);

            LabelTo(from, 1042886, SpellCount.ToString());
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;

            if (Parent == from || pack != null && Parent == pack)
                DisplayTo(from);
            else
                from.SendLocalizedMessage(
                    500207); // The spellbook must be in your backpack (and not in a container within) to open.
        }


        //Currently though there are no dual slayer spellbooks, OSI has a habit of putting dual slayer stuff in later

        [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public SlayerName OldSlayer2 { get; set; }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version

            writer.Write((string) EngravedText);

            writer.Write((int) OldSlayer);
            writer.Write((int) OldSlayer2);

            writer.Write(m_Content);
            writer.Write(SpellCount);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                {
                    EngravedText = reader.ReadString();
                    OldSlayer = (SlayerName) reader.ReadInt();
                    OldSlayer2 = (SlayerName) reader.ReadInt();
                    goto case 1;
                }
                case 1:
                case 0:
                {
                    m_Content = reader.ReadULong();
                    SpellCount = reader.ReadInt();

                    break;
                }
            }

            if (Parent is Mobile)
                ((Mobile) Parent).CheckStatTimers();
        }
    }
}