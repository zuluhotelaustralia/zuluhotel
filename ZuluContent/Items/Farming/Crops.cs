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
        protected abstract BaseHarvestedCrop ToHarvest { get; }
        
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
    public partial class GarlicCrop : BaseCrop
    {
        protected override BaseHarvestedCrop ToHarvest => new RawGarlic();

        protected override int MaxHarvestAmount => 2;
        
        public override string DefaultName => "Garlic Crop";

        [Constructible]
        public GarlicCrop() : base(0x18E3)
        {
        }
    }
}