using System;
using Server.Engines.Craft;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    [Engines.Craft.Anvil]
    public class AnvilComponent : AddonComponent
    {
        [Constructible]
        public AnvilComponent(int itemID) : base(itemID)
        {
        }

        [Constructible]
        public AnvilComponent(Serial serial) : base(serial)
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

    [Engines.Craft.Forge]
    public class ForgeComponent : AddonComponent
    {
        public ForgeComponent(int itemID) : base(itemID)
        {
        }

        public ForgeComponent(Serial serial) : base(serial)
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

    public class LocalizedAddonComponent : AddonComponent
    {
        private int m_LabelNumber;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Number
        {
            get => m_LabelNumber;
            set => m_LabelNumber = value;
        }

        public override int LabelNumber => m_LabelNumber;


        public LocalizedAddonComponent(int itemID, int labelNumber) : base(itemID)
        {
            m_LabelNumber = labelNumber;
        }

        public LocalizedAddonComponent(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write((int) m_LabelNumber);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    m_LabelNumber = reader.ReadInt();
                    break;
                }
            }
        }
    }

    public class AddonComponent : Item, IChopable, ICraftable, IResource
    {
        private CraftResource m_Resource;

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get => m_Resource;
            set
            {
                if (m_Resource != value)
                {
                    m_Resource = value;
                    Hue = CraftResources.GetHue(m_Resource);
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Crafter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerConstructed { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public BaseAddon Addon { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D Offset { get; set; }

        public virtual bool NeedsWall => false;

        public virtual Point3D WallPosition => Point3D.Zero;

        public AddonComponent(int itemID) : base(itemID)
        {
            Mark = MarkQuality.Regular;
            Movable = false;
            ApplyLightTo(this);
        }

        public AddonComponent(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Addon != null)
                Addon.OnComponentUsed(this, from);
        }

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
            Type typeRes, BaseTool tool,
            CraftItem craftItem, int resHue)
        {
            return mark;
        }

        public void OnChop(Mobile from)
        {
            if (Addon != null && from.InRange(GetWorldLocation(), 3))
                Addon.OnChop(from);
            else
                from.SendLocalizedMessage(500446); // That is too far away.
        }

        public override void OnLocationChange(Point3D old)
        {
            if (Addon != null)
                Addon.Location = new Point3D(X - Offset.X, Y - Offset.Y, Z - Offset.Z);
        }

        public override void OnMapChange()
        {
            if (Addon != null)
                Addon.Map = Map;
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (Addon != null)
                Addon.Delete();
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 2); // version

            ICraftable.Serialize(writer, this);

            writer.WriteEncodedInt((int) m_Resource);

            writer.Write(Addon);
            writer.Write(Offset);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    ICraftable.Deserialize(reader, this);

                    m_Resource = (CraftResource) reader.ReadEncodedInt();

                    goto case 1;
                case 1:
                case 0:
                {
                    Addon = reader.ReadEntity<BaseAddon>();
                    Offset = reader.ReadPoint3D();

                    Addon?.OnComponentLoaded(this);

                    ApplyLightTo(this);

                    break;
                }
            }

            if (version < 1 && Weight == 0)
                Weight = -1;
        }

        public static void ApplyLightTo(Item item)
        {
            if ((item.ItemData.Flags & TileFlag.LightSource) == 0)
                return; // not a light source

            int itemID = item.ItemID;

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                LightEntry entry = m_Entries[i];
                int[] toMatch = entry.m_ItemIDs;
                bool contains = false;

                for (int j = 0; !contains && j < toMatch.Length; ++j)
                    contains = itemID == toMatch[j];

                if (contains)
                {
                    item.Light = entry.m_Light;
                    return;
                }
            }
        }

        private static LightEntry[] m_Entries = new[]
        {
            new LightEntry(LightType.WestSmall, 1122, 1123, 1124, 1141, 1142, 1143, 1144, 1145, 1146, 2347, 2359, 2360,
                2361, 2362, 2363, 2364, 2387, 2388, 2389, 2390, 2391, 2392),
            new LightEntry(LightType.NorthSmall, 1131, 1133, 1134, 1147, 1148, 1149, 1150, 1151, 1152, 2352, 2373, 2374,
                2375, 2376, 2377, 2378, 2401, 2402, 2403, 2404, 2405, 2406),
            new LightEntry(LightType.Circle300, 6526, 6538, 6571),
            new LightEntry(LightType.Circle150, 5703, 6587)
        };

        private class LightEntry
        {
            public LightType m_Light;
            public int[] m_ItemIDs;

            public LightEntry(LightType light, params int[] itemIDs)
            {
                m_Light = light;
                m_ItemIDs = itemIDs;
            }
        }
    }
}