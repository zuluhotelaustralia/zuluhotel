using System;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
    public enum ToolQuality
    {
        Low,
        Regular,
        Exceptional
    }

    public abstract class BaseTool : Item, IUsesRemaining, ICraftable
    {
        private ToolQuality m_Mark;

        public BaseTool(int itemID) : this(Utility.RandomMinMax(25, 75), itemID)
        {
        }

        public BaseTool(int uses, int itemID) : base(itemID)
        {
            UsesRemaining = uses;
            m_Mark = ToolQuality.Regular;
        }

        public BaseTool(Serial serial) : base(serial)
        {
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public ToolQuality Mark
        {
            get { return m_Mark; }
            set
            {
                UnscaleUses();
                m_Mark = value;
                ScaleUses();
            }
        }

        public virtual bool BreakOnDepletion => true;

        public abstract CraftSystem CraftSystem { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining { get; set; }

        public bool ShowUsesRemaining
        {
            get { return true; }
            set { }
        }

        public void ScaleUses()
        {
            UsesRemaining = UsesRemaining * GetUsesScalar() / 100;
        }

        public void UnscaleUses()
        {
            UsesRemaining = UsesRemaining * 100 / GetUsesScalar();
        }

        public int GetUsesScalar()
        {
            return m_Mark == ToolQuality.Exceptional ? 200 : 100;
        }

        public virtual void DisplayDurabilityTo(Mobile m)
        {
            LabelToAffix(m, 1017323, AffixType.Append, ": " + UsesRemaining.ToString()); // Durability
        }

        public static bool CheckAccessible(Item tool, Mobile m)
        {
            return tool.IsChildOf(m) || tool.Parent == m;
        }

        public override void OnSingleClick(Mobile from)
        {
            DisplayDurabilityTo(from);

            base.OnSingleClick(from);
        }

        public virtual void OnBeginCraft(Mobile from, CraftItem item, CraftSystem system)
        {
        }

        public virtual void OnEndCraft(Mobile from, CraftItem craftItem, CraftSystem craftSystem)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
            {
                CraftSystem system = CraftSystem;

                int num = system.CanCraft(from, this, null);

                if (num > 0)
                {
                    from.SendLocalizedMessage(num);
                }
                else if (!from.CanBeginAction(typeof(CraftSystem)))
                {
                    from.SendLocalizedMessage(500119); // You must wait to perform another action
                }
                else
                {
                    from.SendGump(new CraftGump(from, system, this, null));
                }
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write((Mobile) Crafter);
            writer.Write((int) m_Mark);

            writer.Write((int) UsesRemaining);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                {
                    Crafter = reader.ReadEntity<Mobile>();
                    m_Mark = (ToolQuality) reader.ReadInt();
                    goto case 0;
                }
                case 0:
                {
                    UsesRemaining = reader.ReadInt();
                    break;
                }
            }
        }

        #region ICraftable Members

        public bool PlayerConstructed { get; set; }

        public virtual int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (ToolQuality) mark;

            if (makersMark)
                Crafter = from;

            return mark;
        }

        #endregion
    }
}