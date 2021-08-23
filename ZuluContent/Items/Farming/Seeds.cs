using System;
using Scripts.Zulu.Utilities;
using Server.Network;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class SeedDirt : Item
    {
        [SerializableField(0)] private BaseCrop _crop;

        [Constructible]
        public SeedDirt(BaseCrop crop) : base(0x0914)
        {
            Movable = false;

            Crop = crop;

            new GrowTimer(this).Start();
        }

        [AfterDeserialization]
        private void OnAfterDeserialization()
        {
            Crop.MoveToWorld(Location, Map);
            Delete();
        }

        private class GrowTimer : Timer
        {
            private int m_Count;
            private SeedDirt m_Dirt;

            public GrowTimer(SeedDirt dirt) : base(TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0))
            {
                m_Dirt = dirt;
            }

            protected override void OnTick()
            {
                m_Count++;

                if (m_Count % 10 > 0)
                {
                    m_Dirt.PublicOverheadMessage(MessageType.Emote, 0, false,
                        $"*{m_Dirt.Crop.Name} matures {m_Count * 10}%*");
                }
                else
                {
                    Stop();

                    m_Dirt.Crop.MoveToWorld(m_Dirt.Location, m_Dirt.Map);
                    m_Dirt.Delete();
                }
            }
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
            
            Consume();
            
            from.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false,"*plants seed*");
            from.Animate(32, 5, 1, true, false, 0);
            from.PlaySound(0x383);
            
            var dirt = new SeedDirt(Crop);
            dirt.MoveToWorld(from.Location, from.Map);
            
            from.SendSuccessMessage("You plant the seed.");
        }
    }
    
    [Serializable(0, false)]
    public partial class CabbageSeed : BaseSeed
    {
        protected override BaseCrop Crop => new CabbageCrop();
        
        public override string DefaultName => "cabbage seed";

        [Constructible]
        public CabbageSeed() : this(1)
        {
        }
        
        [Constructible]
        public CabbageSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class CarrotSeed : BaseSeed
    {
        protected override BaseCrop Crop => new CarrotCrop();
        
        public override string DefaultName => "carrot seed";

        [Constructible]
        public CarrotSeed() : this(1)
        {
        }
        
        [Constructible]
        public CarrotSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class CottonSeed : BaseSeed
    {
        protected override BaseCrop Crop => new CottonCrop();
        
        public override string DefaultName => "cotton seed";

        [Constructible]
        public CottonSeed() : this(1)
        {
        }
        
        [Constructible]
        public CottonSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FlaxSeed : BaseSeed
    {
        protected override BaseCrop Crop => new FlaxCrop();
        
        public override string DefaultName => "flax seed";

        [Constructible]
        public FlaxSeed() : this(1)
        {
        }
        
        [Constructible]
        public FlaxSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class LettuceSeed : BaseSeed
    {
        protected override BaseCrop Crop => new LettuceCrop();
        
        public override string DefaultName => "lettuce seed";

        [Constructible]
        public LettuceSeed() : this(1)
        {
        }
        
        [Constructible]
        public LettuceSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class OnionSeed : BaseSeed
    {
        protected override BaseCrop Crop => new OnionCrop();
        
        public override string DefaultName => "onion seed";

        [Constructible]
        public OnionSeed() : this(1)
        {
        }
        
        [Constructible]
        public OnionSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PumpkinSeed : BaseSeed
    {
        protected override BaseCrop Crop => new PumpkinCrop();
        
        public override string DefaultName => "pumpkin seed";

        [Constructible]
        public PumpkinSeed() : this(1)
        {
        }
        
        [Constructible]
        public PumpkinSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class TurnipSeed : BaseSeed
    {
        protected override BaseCrop Crop => new TurnipCrop();
        
        public override string DefaultName => "turnip seed";

        [Constructible]
        public TurnipSeed() : this(1)
        {
        }
        
        [Constructible]
        public TurnipSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class WheatSeed : BaseSeed
    {
        protected override BaseCrop Crop => new WheatCrop();
        
        public override string DefaultName => "wheat seed";

        [Constructible]
        public WheatSeed() : this(1)
        {
        }
        
        [Constructible]
        public WheatSeed(int amount) : base(amount)
        {
        }
    }

    [Serializable(0, false)]
    public partial class GarlicSeed : BaseSeed
    {
        protected override BaseCrop Crop => new GarlicCrop();
        
        public override string DefaultName => "garlic seed";

        [Constructible]
        public GarlicSeed() : this(1)
        {
        }
        
        [Constructible]
        public GarlicSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class MandrakeSeed : BaseSeed
    {
        protected override BaseCrop Crop => new MandrakeCrop();
        
        public override string DefaultName => "mandrake seed";

        [Constructible]
        public MandrakeSeed() : this(1)
        {
        }
        
        [Constructible]
        public MandrakeSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class NightshadeSeed : BaseSeed
    {
        protected override BaseCrop Crop => new NightshadeCrop();
        
        public override string DefaultName => "nightshade seed";

        [Constructible]
        public NightshadeSeed() : this(1)
        {
        }
        
        [Constructible]
        public NightshadeSeed(int amount) : base(amount)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class GinsengSeed : BaseSeed
    {
        protected override BaseCrop Crop => new GinsengCrop();
        
        public override string DefaultName => "ginseng seed";

        [Constructible]
        public GinsengSeed() : this(1)
        {
        }
        
        [Constructible]
        public GinsengSeed(int amount) : base(amount)
        {
        }
    }
}