using System;
using Scripts.Zulu.Utilities;
using Server.Network;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class SeedDirt : Item
    {
        [Constructible]
        public SeedDirt() : base(0x0914)
        {
            Movable = false;

            Timer.DelayCall(TimeSpan.FromMinutes(1.0), Delete);
        }
    }

    [Serializable(0, false)]
    public abstract partial class BaseSeed : Item
    {
        protected abstract BaseCrop Crop { get; }

        protected BaseSeed() : this(1)
        {
        }

        protected BaseSeed(int amount) : base(0xF7F)
        {
            Amount = amount;
            Stackable = true;
            Weight = 1;
        }

        private static bool IsPlantable(Mobile from)
        {
            var location = from.Location;
            var map = from.Map;
            var landTile = map.Tiles.GetLandTile(location.X, location.Y);
            var tileId = landTile.ID;

            if (tileId is >= 0x9 and <= 0x15 or >= 0x14F and <= 0x15c)
            {
                var itemInWay = false;

                IPooledEnumerable eable = map.GetItemsInRange(location, 0);

                foreach (Item item in eable)
                    if (map.LineOfSight(from, item))
                    {
                        itemInWay = true;
                        break;
                    }

                eable.Free();

                return !itemInWay;
            }

            return false;
        }
        
        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendFailureMessage("That must be in your backpack!");
                return;
            }

            if (!IsPlantable(from))
            {
                from.SendFailureMessage("You cannot plant here!");
                return;
            }

            var dirt = new SeedDirt();
            dirt.MoveToWorld(from.Location, from.Map);
            
            Consume();
            
            from.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false,"*plants seed*");
            from.Animate(32, 5, 1, true, false, 0);
            from.PlaySound(0x383);
            from.SendSuccessMessage("You plant the seed.");
            
            Timer.DelayCall(TimeSpan.FromMinutes(1.0), GrowSeed_Callback, Crop, from.Location, from.Map);
        }

        public static void GrowSeed_Callback(Item crop, Point3D location, Map map)
        {
            crop.MoveToWorld(location, map);
        }
    }

    [Serializable(0, false)]
    public partial class GarlicSeed : BaseSeed
    {
        protected override BaseCrop Crop => new GarlicCrop();
        
        public override string DefaultName => "Garlic Seed";

        [Constructible]
        public GarlicSeed() : this(1)
        {
        }
        
        [Constructible]
        public GarlicSeed(int amount) : base(amount)
        {
        }
    }
}