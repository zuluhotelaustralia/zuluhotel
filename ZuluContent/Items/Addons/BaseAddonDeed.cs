using System;
using Scripts.Zulu.Utilities;
using Server.Engines.Craft;
using Server.Multis;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;
using static ZuluContent.Zulu.Items.SingleClick.SingleClickHandler;

namespace Server.Items
{
    [Flipable(0x14F0, 0x14EF)]
    public abstract class BaseAddonDeed : Item, ICraftable, IResource
    {
        public abstract BaseAddon Addon { get; }

        private CraftResource m_Resource;
        private EnchantmentDictionary m_Enchantments;

        public EnchantmentDictionary Enchantments
        {
            get => m_Enchantments ??= new EnchantmentDictionary();
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MarkQuality Mark
        {
            get => Enchantments.Get((ItemMark e) => (MarkQuality) e.Value);
            set { Enchantments.Set((ItemMark e) => e.Value = (int) value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public CraftResource Resource
        {
            get { return m_Resource; }
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

        public BaseAddonDeed() : base(0x14F0)
        {
            Weight = 1.0;

            Mark = MarkQuality.Regular;

            LootType = LootType.Newbied;
        }

        public BaseAddonDeed(Serial serial) : base(serial)
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

            int version = reader.ReadInt();

            if (version == 1)
            {
                ICraftable.Deserialize(reader, this);

                m_Enchantments = EnchantmentDictionary.Deserialize(reader);

                m_Resource = (CraftResource) reader.ReadEncodedInt();
            }

            if (Weight == 0.0)
                Weight = 1.0;
        }

        public int OnCraft(int mark, double quality, bool makersMark, Mobile from, CraftSystem craftSystem,
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

            return mark;
        }

        public override void OnSingleClick(Mobile from)
        {
            HandleSingleClick(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
                from.Target = new InternalTarget(this);
            else
                from.SendFailureMessage(1042001); // That must be in your pack for you to use it.
        }

        private class InternalTarget : Target
        {
            private BaseAddonDeed m_Deed;

            public InternalTarget(BaseAddonDeed deed) : base(-1, true, TargetFlags.None)
            {
                m_Deed = deed;

                CheckLOS = false;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                IPoint3D p = targeted as IPoint3D;
                Map map = from.Map;

                if (p == null || map == null || m_Deed.Deleted)
                    return;

                if (m_Deed.IsChildOf(from.Backpack))
                {
                    BaseAddon addon = m_Deed.Addon;

                    addon.SetComponentProps(m_Deed.Mark, m_Deed.Resource, m_Deed.Crafter, m_Deed.PlayerConstructed);

                    Spells.SpellHelper.GetSurfaceTop(ref p);

                    BaseHouse house = null;

                    AddonFitResult res = addon.CouldFit(p, map, from, ref house);

                    if (res == AddonFitResult.Valid)
                        addon.MoveToWorld(new Point3D(p), map);
                    else if (res == AddonFitResult.Blocked)
                        from.SendFailureMessage(500269); // You cannot build that there.
                    else if (res == AddonFitResult.NotInHouse)
                        from.SendFailureMessage(500274); // You can only place this in a house that you own!
                    else if (res == AddonFitResult.DoorTooClose)
                        from.SendFailureMessage(500271); // You cannot build near the door.
                    else if (res == AddonFitResult.NoWall)
                        from.SendFailureMessage(500268); // This object needs to be mounted on something.

                    if (res == AddonFitResult.Valid)
                    {
                        m_Deed.Delete();
                        house.Addons.Add(addon);
                    }
                    else
                    {
                        addon.Delete();
                    }
                }
                else
                {
                    from.SendFailureMessage(1042001); // That must be in your pack for you to use it.
                }
            }
        }
    }
}