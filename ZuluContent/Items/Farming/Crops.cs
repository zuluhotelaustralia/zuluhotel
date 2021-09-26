using System;
using System.Collections.Generic;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Network;

namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class BaseCrop : Item
    {
        protected abstract Item ToHarvest { get; }
        
        protected abstract int MaxHarvestAmount { get; }

        [SerializableField(0)]
        private int _harvestAmount;

        public BaseCrop(int itemID) : base(itemID)
        {
            Movable = false;
        }

        public override async void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(Location, 2) || !from.InLOS(this))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }
            
            if (from.BeginAction(typeof(BaseCrop)))
            {
                from.SendSuccessMessage("You start harvesting...");

                var oldLocation = from.Location;

                while (from.Location == oldLocation && HarvestAmount < MaxHarvestAmount)
                {
                    from.PublicOverheadMessage(MessageType.Emote, from.EmoteHue, false,"*harvests*");
                    from.Animate(32, 5, 1, true, false, 0);
                    from.PlaySound(0x57);

                    await Timer.Pause(TimeSpan.FromSeconds(2.0));

                    HarvestAmount++;

                    var difficulty = Utility.RandomMinMax(1, 100);

                    if (from.ShilCheckSkill(SkillName.Lumberjacking, difficulty, 200))
                    {
                        if (Utility.Random(100) == 0)
                        {
                            var dirt = new FertileDirt();
                            from.Backpack.TryDropItem(from, dirt, false);
                            from.SendSuccessMessage("You have found some fertile dirt!");
                        }
                        else
                        {
                            from.SendSuccessMessage("You put the crops in your pack.");
                            from.Backpack.TryDropItem(from, ToHarvest, false);
                        }
                    }
                    else
                    {
                        from.SendFailureMessage("You fail to harvest.");
                    }
                }
                
                from.SendSuccessMessage("You stop harvesting...");
                from.EndAction(typeof(BaseCrop));

                if (HarvestAmount == MaxHarvestAmount)
                {
                    from.SendFailureMessage("You harvested all you can.");
                    Delete();
                }
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x22,
                    1042144); // This is currently in use.
            }
        }
    }
    
    [Serializable(0, false)]
    public partial class CabbageCrop : BaseCrop
    {
        protected override Item ToHarvest => new Cabbage();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "cabbage crop";

        [Constructible]
        public CabbageCrop() : base(0x0C7B)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class CarrotCrop : BaseCrop
    {
        protected override Item ToHarvest => new Carrot();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "carrot crop";

        [Constructible]
        public CarrotCrop() : base(0x0C76)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class CottonCrop : BaseCrop
    {
        protected override Item ToHarvest => new Cotton();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "cotton crop";

        [Constructible]
        public CottonCrop() : base(0x0C4F)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FlaxCrop : BaseCrop
    {
        protected override Item ToHarvest => new Flax();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "flax crop";

        [Constructible]
        public FlaxCrop() : base(0x1A9B)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class LettuceCrop : BaseCrop
    {
        protected override Item ToHarvest => new Lettuce();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "lettuce crop";

        [Constructible]
        public LettuceCrop() : base(0x0C70)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class OnionCrop : BaseCrop
    {
        protected override Item ToHarvest => new Onion();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "onion crop";

        [Constructible]
        public OnionCrop() : base(0x0C6F)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PumpkinCrop : BaseCrop
    {
        protected override Item ToHarvest => new Pumpkin();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "pumpkin crop";

        [Constructible]
        public PumpkinCrop() : base(0x0C60)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class TurnipCrop : BaseCrop
    {
        protected override Item ToHarvest => new Turnip();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "turnip crop";

        [Constructible]
        public TurnipCrop() : base(0x0C61)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class WheatCrop : BaseCrop
    {
        protected override Item ToHarvest => new Wheat();

        protected override int MaxHarvestAmount => 10;
        
        public override string DefaultName => "wheat crop";

        [Constructible]
        public WheatCrop() : base(0x0C55)
        {
        }
    }

    [Serializable(0, false)]
    public partial class GarlicCrop : BaseCrop
    {
        protected override BaseHarvestedCrop ToHarvest => new RawGarlic();

        protected override int MaxHarvestAmount => 2;
        
        public override string DefaultName => "garlic crop";

        [Constructible]
        public GarlicCrop() : base(0x18E1)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class MandrakeCrop : BaseCrop
    {
        protected override BaseHarvestedCrop ToHarvest => new RawMandrake();

        protected override int MaxHarvestAmount => 2;
        
        public override string DefaultName => "mandrake crop";

        [Constructible]
        public MandrakeCrop() : base(0x18DF)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class NightshadeCrop : BaseCrop
    {
        protected override BaseHarvestedCrop ToHarvest => new RawNightshade();

        protected override int MaxHarvestAmount => 2;
        
        public override string DefaultName => "nightshade crop";

        [Constructible]
        public NightshadeCrop() : base(0x18E5)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class GinsengCrop : BaseCrop
    {
        protected override BaseHarvestedCrop ToHarvest => new RawGinseng();

        protected override int MaxHarvestAmount => 2;
        
        public override string DefaultName => "ginseng crop";

        [Constructible]
        public GinsengCrop() : base(0x18E9)
        {
        }
    }
}