using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.Engines.Craft;
using System;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    public abstract class BaseContainer : Container, ICraftable, IMagicItem
    {
        private CraftResource m_Resource;
        private EnchantmentDictionary m_Enchantments;

        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get => Enchantments.Identified;
            set
            {
                if (!Enchantments.Identified && value)
                    Enchantments.OnIdentified(this);

                Enchantments.Identified = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ContainerQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (ContainerQuality) e.Value);
            set { Enchantments.Set((ItemMark e) => e.Value = (int) value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public virtual bool PlayerConstructed { get; set; }

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
        public virtual Mobile Crafter { get; set; }

        public override int DefaultMaxWeight
        {
            get
            {
                if (IsSecure)
                    return 0;

                return base.DefaultMaxWeight;
            }
        }

        public BaseContainer(int itemID) : base(itemID)
        {
        }

        public override bool IsAccessibleTo(Mobile m)
        {
            if (!BaseHouse.CheckAccessible(m, this))
                return false;

            return base.IsAccessibleTo(m);
        }

        public override bool CheckItemUse(Mobile from, Item item)
        {
            if (IsDecoContainer && item is BaseBook)
                return true;

            return base.CheckItemUse(from, item);
        }

        public override bool TryDropItem(Mobile from, Item dropped, bool sendFullMessage)
        {
            if (!CheckHold(from, dropped, sendFullMessage, true))
                return false;

            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null && house.IsLockedDown(this))
            {
                if (!house.LockDown(from, dropped, false))
                    return false;
            }

            List<Item> list = Items;

            for (int i = 0; i < list.Count; ++i)
            {
                Item item = list[i];

                if (!(item is Container) && item.StackWith(from, dropped, false))
                    return true;
            }

            DropItem(dropped);

            return true;
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            if (!CheckHold(from, item, true, true))
                return false;

            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null && house.IsLockedDown(this))
            {
                if (!house.LockDown(from, item, false))
                    return false;
            }

            item.Location = new Point3D(p.X, p.Y, 0);
            AddItem(item);

            from.SendSound(GetDroppedSound(item), GetWorldLocation());

            return true;
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player || from.InRange(GetWorldLocation(), 2) ||
                RootParent is PlayerVendor)
                Open(from);
            else
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
        }

        public virtual void Open(Mobile from)
        {
            DisplayTo(from);
        }

        public virtual int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes,
            BaseTool tool, CraftItem craftItem, int resHue)
        {
            Mark = (ContainerQuality) mark;

            if (makersMark)
                Crafter = from;

            Type resourceType = typeRes;

            if (resourceType == null)
                resourceType = craftItem.Resources[0].ItemType;

            Resource = CraftResources.GetFromType(resourceType);

            PlayerConstructed = true;

            CraftContext context = craftSystem.GetContext(from);

            if (context != null && context.DoNotColor)
                Hue = 0;

            return mark;
        }

        public BaseContainer(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(1); // version

            ICraftable.Serialize(writer, this);

            Enchantments.Serialize(writer);

            writer.WriteEncodedInt((int) m_Resource);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            if (version == 1)
            {
                ICraftable.Deserialize(reader, this);

                m_Enchantments = EnchantmentDictionary.Deserialize(reader);

                m_Resource = (CraftResource) reader.ReadEncodedInt();
            }
        }
    }

    public class CreatureBackpack : Backpack //Used on BaseCreature
    {
        [Constructible]
        public CreatureBackpack(string name)
        {
            Name = name;
            Layer = Layer.Backpack;
            Hue = 5;
            Weight = 3.0;
        }

        public override void OnItemRemoved(Item item)
        {
            if (Items.Count == 0)
                Delete();

            base.OnItemRemoved(item);
        }

        public override bool OnDragLift(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player)
                return true;

            from.SendLocalizedMessage(500169); // You cannot pick that up.
            return false;
        }

        public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
        {
            return false;
        }

        public override bool TryDropItem(Mobile from, Item dropped, bool sendFullMessage)
        {
            return false;
        }

        [Constructible]
        public CreatureBackpack(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
                Weight = 13.0;
        }
    }

    public class StrongBackpack : Backpack //Used on Pack animals
    {
        public StrongBackpack()
        {
            Layer = Layer.Backpack;
            Weight = 13.0;
        }

        public override bool CheckHold(Mobile m, Item item, bool message, bool checkItems, int plusItems,
            int plusWeight)
        {
            return base.CheckHold(m, item, false, checkItems, plusItems, plusWeight);
        }

        public override int DefaultMaxWeight
        {
            get { return 1600; }
        }

        public override bool CheckContentDisplay(Mobile from)
        {
            object root = RootParent;

            if (root is BaseCreature && ((BaseCreature) root).Controlled && ((BaseCreature) root).ControlMaster == from)
                return true;

            return base.CheckContentDisplay(from);
        }

        public StrongBackpack(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
                Weight = 13.0;
        }
    }

    public class Backpack : BaseContainer, IDyable
    {
        [Constructible]
        public Backpack() : base(0xE75)
        {
            Layer = Layer.Backpack;
            Weight = 3.0;
        }

        public Backpack(Serial serial) : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted) return false;

            Hue = sender.DyedHue;

            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && ItemID == 0x9B2)
                ItemID = 0xE75;
        }
    }

    public class Pouch : TrapableContainer
    {
        [Constructible]
        public Pouch() : base(0xE79)
        {
            Weight = 1.0;
        }

        public Pouch(Serial serial) : base(serial)
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

    public abstract class BaseBagBall : BaseContainer, IDyable
    {
        [Constructible]
        public BaseBagBall(int itemID) : base(itemID)
        {
            Weight = 1.0;
        }

        public BaseBagBall(Serial serial) : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
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

    public class SmallBagBall : BaseBagBall
    {
        [Constructible]
        public SmallBagBall() : base(0x2256)
        {
        }

        public SmallBagBall(Serial serial) : base(serial)
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

    public class LargeBagBall : BaseBagBall
    {
        [Constructible]
        public LargeBagBall() : base(0x2257)
        {
        }

        public LargeBagBall(Serial serial) : base(serial)
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

    public class Bag : BaseContainer, IDyable
    {
        [Constructible]
        public Bag() : base(0xE76)
        {
            Weight = 2.0;
        }

        public Bag(Serial serial) : base(serial)
        {
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted) return false;

            Hue = sender.DyedHue;

            return true;
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

    public class Barrel : BaseContainer
    {
        [Constructible]
        public Barrel() : base(0xE77)
        {
            Weight = 25.0;
        }

        public Barrel(Serial serial) : base(serial)
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

            if (Weight == 0.0)
                Weight = 25.0;
        }
    }

    public class Keg : BaseContainer
    {
        [Constructible]
        public Keg() : base(0xE7F)
        {
            Weight = 15.0;
        }

        public Keg(Serial serial) : base(serial)
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

    public class PicnicBasket : BaseContainer
    {
        [Constructible]
        public PicnicBasket() : base(0xE7A)
        {
            Weight = 2.0; // Stratics doesn't know weight
        }

        public PicnicBasket(Serial serial) : base(serial)
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

    public class Basket : BaseContainer
    {
        [Constructible]
        public Basket() : base(0x990)
        {
            Weight = 1.0; // Stratics doesn't know weight
        }

        public Basket(Serial serial) : base(serial)
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

    [Furniture]
    [Flipable(0x9AA, 0xE7D)]
    public class WoodenBox : LockableContainer
    {
        [Constructible]
        public WoodenBox() : base(0x9AA)
        {
            Weight = 4.0;
        }

        public WoodenBox(Serial serial) : base(serial)
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

    [Furniture]
    [Flipable(0x9A9, 0xE7E)]
    public class SmallCrate : LockableContainer
    {
        [Constructible]
        public SmallCrate() : base(0x9A9)
        {
            Weight = 2.0;
        }

        public SmallCrate(Serial serial) : base(serial)
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

            if (Weight == 4.0)
                Weight = 2.0;
        }
    }

    [Furniture]
    [Flipable(0xE3F, 0xE3E)]
    public class MediumCrate : LockableContainer
    {
        [Constructible]
        public MediumCrate() : base(0xE3F)
        {
            Weight = 2.0;
        }

        public MediumCrate(Serial serial) : base(serial)
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

            if (Weight == 6.0)
                Weight = 2.0;
        }
    }

    [Furniture]
    [Flipable(0xE3D, 0xE3C)]
    public class LargeCrate : LockableContainer
    {
        [Constructible]
        public LargeCrate() : base(0xE3D)
        {
            Weight = 1.0;
        }

        public LargeCrate(Serial serial) : base(serial)
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

            if (Weight == 8.0)
                Weight = 1.0;
        }
    }

    [DynamicFliping]
    [Flipable(0x9A8, 0xE80)]
    public class MetalBox : LockableContainer
    {
        [Constructible]
        public MetalBox() : base(0x9A8)
        {
        }

        public MetalBox(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 3)
                Weight = -1;
        }
    }

    [DynamicFliping]
    [Flipable(0x9AB, 0xE7C)]
    public class MetalChest : LockableContainer
    {
        [Constructible]
        public MetalChest() : base(0x9AB)
        {
        }

        public MetalChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 25)
                Weight = -1;
        }
    }

    [DynamicFliping]
    [Flipable(0xE41, 0xE40)]
    public class MetalGoldenChest : LockableContainer
    {
        [Constructible]
        public MetalGoldenChest() : base(0xE41)
        {
        }

        public MetalGoldenChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 25)
                Weight = -1;
        }
    }

    [Furniture]
    [Flipable(0xe43, 0xe42)]
    public class WoodenChest : LockableContainer
    {
        [Constructible]
        public WoodenChest() : base(0xe43)
        {
            Weight = 2.0;
        }

        public WoodenChest(Serial serial) : base(serial)
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

            if (Weight == 15.0)
                Weight = 2.0;
        }
    }

    [Furniture]
    [Flipable(0x280B, 0x280C)]
    public class PlainWoodenChest : LockableContainer
    {
        [Constructible]
        public PlainWoodenChest() : base(0x280B)
        {
        }

        public PlainWoodenChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 15)
                Weight = -1;
        }
    }

    [Furniture]
    [Flipable(0x280D, 0x280E)]
    public class OrnateWoodenChest : LockableContainer
    {
        [Constructible]
        public OrnateWoodenChest() : base(0x280D)
        {
        }

        public OrnateWoodenChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 15)
                Weight = -1;
        }
    }

    [Furniture]
    [Flipable(0x280F, 0x2810)]
    public class GildedWoodenChest : LockableContainer
    {
        [Constructible]
        public GildedWoodenChest() : base(0x280F)
        {
        }

        public GildedWoodenChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 15)
                Weight = -1;
        }
    }

    [Furniture]
    [Flipable(0x2811, 0x2812)]
    public class WoodenFootLocker : LockableContainer
    {
        [Constructible]
        public WoodenFootLocker() : base(0x2811)
        {
            GumpID = 0x10B;
        }

        public WoodenFootLocker(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 15)
                Weight = -1;

            if (version < 2)
                GumpID = 0x10B;
        }
    }

    [Furniture]
    [Flipable(0x2813, 0x2814)]
    public class FinishedWoodenChest : LockableContainer
    {
        [Constructible]
        public FinishedWoodenChest() : base(0x2813)
        {
        }

        public FinishedWoodenChest(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0 && Weight == 15)
                Weight = -1;
        }
    }
}