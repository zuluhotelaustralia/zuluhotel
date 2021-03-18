using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.Craft;
using Server.Multis;
using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace Server.Items
{
    public enum AddonFitResult
    {
        Valid,
        Blocked,
        NotInHouse,
        DoorTooClose,
        NoWall,
        DoorsNotClosed
    }

    public interface IAddon
    {
        Item Deed { get; }

        bool CouldFit(IPoint3D p, Map map);
    }

    public abstract class BaseAddon : Item, IChopable, IAddon
    {
        private List<AddonComponent> m_Components;

        public void AddComponent(AddonComponent c, int x, int y, int z)
        {
            if (Deleted)
                return;

            m_Components.Add(c);

            c.Addon = this;
            c.Offset = new Point3D(x, y, z);
            c.MoveToWorld(new Point3D(X + x, Y + y, Z + z), Map);
        }

        public void SetComponentProps(MarkQuality mark, CraftResource resource, Mobile crafter, bool playerConstructed)
        {
            if (!Deleted && m_Components != null)
            {
                foreach (var component in m_Components)
                {
                    component.Mark = mark;
                    component.Resource = resource;
                    component.Crafter = crafter;
                    component.PlayerConstructed = playerConstructed;
                }
            }
        }

        public BaseAddon() : base(1)
        {
            Movable = false;
            Visible = false;

            m_Components = new List<AddonComponent>();
        }

        public virtual void OnChop(Mobile from)
        {
            BaseHouse house = BaseHouse.FindHouseAt(this);

            if (house != null && house.IsOwner(from) && house.Addons.Contains(this))
            {
                Effects.PlaySound(GetWorldLocation(), Map, 0x3B3);
                from.SendLocalizedMessage(500461); // You destroy the item.

                Delete();

                house.Addons.Remove(this);

                var deed = Deed;

                if (deed != null)
                {
                    deed.Mark = m_Components[0].Mark;
                    deed.Resource = m_Components[0].Resource;
                    deed.PlayerConstructed = m_Components[0].PlayerConstructed;
                    deed.Crafter = m_Components[0].Crafter;

                    from.AddToBackpack(deed);
                }
            }
        }

        public virtual BaseAddonDeed Deed => null;

        Item IAddon.Deed => Deed;

        public List<AddonComponent> Components => m_Components;

        public BaseAddon(Serial serial) : base(serial)
        {
        }

        public bool CouldFit(IPoint3D p, Map map)
        {
            BaseHouse h = null;
            return CouldFit(p, map, null, ref h) == AddonFitResult.Valid;
        }

        public virtual AddonFitResult CouldFit(IPoint3D p, Map map, Mobile from, ref BaseHouse house)
        {
            if (Deleted)
                return AddonFitResult.Blocked;

            foreach (AddonComponent c in m_Components)
            {
                Point3D p3D = new Point3D(p.X + c.Offset.X, p.Y + c.Offset.Y, p.Z + c.Offset.Z);

                if (!map.CanFit(p3D.X, p3D.Y, p3D.Z, c.ItemData.Height, false, true, c.Z == 0))
                    return AddonFitResult.Blocked;
                else if (!CheckHouse(from, p3D, map, c.ItemData.Height, ref house))
                    return AddonFitResult.NotInHouse;

                if (c.NeedsWall)
                {
                    Point3D wall = c.WallPosition;

                    if (!IsWall(p3D.X + wall.X, p3D.Y + wall.Y, p3D.Z + wall.Z, map))
                        return AddonFitResult.NoWall;
                }
            }

            List<BaseDoor> doors = house.Doors;

            for (int i = 0; i < doors.Count; ++i)
            {
                BaseDoor door = doors[i];

                Point3D doorLoc = door.GetWorldLocation();
                int doorHeight = door.ItemData.CalcHeight;

                foreach (AddonComponent c in m_Components)
                {
                    Point3D addonLoc = new Point3D(p.X + c.Offset.X, p.Y + c.Offset.Y, p.Z + c.Offset.Z);
                    int addonHeight = c.ItemData.CalcHeight;

                    if (Utility.InRange(doorLoc, addonLoc, 1) && (addonLoc.Z == doorLoc.Z ||
                                                                  addonLoc.Z + addonHeight > doorLoc.Z &&
                                                                  doorLoc.Z + doorHeight > addonLoc.Z))
                        return AddonFitResult.DoorTooClose;
                }
            }

            return AddonFitResult.Valid;
        }

        public static bool CheckHouse(Mobile from, Point3D p, Map map, int height, ref BaseHouse house)
        {
            house = BaseHouse.FindHouseAt(p, map, height);

            if (house == null || @from != null && !house.IsOwner(@from))
                return false;

            return true;
        }

        public static bool IsWall(int x, int y, int z, Map map)
        {
            if (map == null)
                return false;

            StaticTile[] tiles = map.Tiles.GetStaticTiles(x, y, true);

            for (int i = 0; i < tiles.Length; ++i)
            {
                StaticTile t = tiles[i];
                ItemData id = TileData.ItemTable[t.ID & TileData.MaxItemValue];

                if ((id.Flags & TileFlag.Wall) != 0 && z + 16 > t.Z && t.Z + t.Height > z)
                    return true;
            }

            return false;
        }

        public virtual void OnComponentLoaded(AddonComponent c)
        {
        }

        public virtual void OnComponentUsed(AddonComponent c, Mobile from)
        {
        }

        public override void OnLocationChange(Point3D oldLoc)
        {
            if (Deleted)
                return;

            foreach (AddonComponent c in m_Components)
                c.Location = new Point3D(X + c.Offset.X, Y + c.Offset.Y, Z + c.Offset.Z);
        }

        public override void OnMapChange()
        {
            if (Deleted)
                return;

            foreach (AddonComponent c in m_Components)
                c.Map = Map;
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            foreach (AddonComponent c in m_Components)
                c.Delete();
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write(m_Components);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                case 0:
                {
                    m_Components = reader.ReadEntityList<AddonComponent>();
                    break;
                }
            }

            if (version < 1 && Weight == 0)
                Weight = -1;
        }
    }
}