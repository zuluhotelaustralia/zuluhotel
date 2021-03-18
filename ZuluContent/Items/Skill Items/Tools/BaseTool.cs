using System;
using Server.Network;
using Server.Engines.Craft;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public abstract class BaseTool : Item, IUsesRemaining, IResource, ICraftable
    {
        private CraftResource m_Resource;
        private MarkQuality m_Mark;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
            set
            {
                if (m_Resource != value)
                {
                    m_Resource = value;

                    if (CraftItem.RetainsColor(GetType()))
                    {
                        Hue = CraftResources.GetHue(m_Resource);
                    }
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => m_Mark;
            set
            {
                UnscaleUses();
                m_Mark = value;
                ScaleUses();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        public BaseTool(int itemID) : this(Utility.RandomMinMax(25, 75), itemID)
        {
        }

        public BaseTool(int uses, int itemID) : base(itemID)
        {
            UsesRemaining = uses;
            Mark = MarkQuality.Regular;
        }

        public BaseTool(Serial serial) : base(serial)
        {
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
            return Mark == MarkQuality.Exceptional ? 200 : 100;
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

            HandleSingleClick(this, from);
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

            ICraftable.Serialize(writer, this);

            writer.WriteEncodedInt((int) m_Resource);

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
                    ICraftable.Deserialize(reader, this);

                    m_Resource = (CraftResource) reader.ReadEncodedInt();

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

        public virtual int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (MarkQuality) mark;

            if (makersMark)
                Crafter = from;

            var resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            PlayerConstructed = true;

            var context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        #endregion
    }
}