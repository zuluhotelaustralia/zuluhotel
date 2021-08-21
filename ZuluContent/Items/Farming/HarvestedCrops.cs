using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Network;

namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class BaseHarvestedCrop : Item
    {
        protected abstract Item Product { get; }

        protected BaseHarvestedCrop(int itemID) : this(itemID, 1)
        {
        }
        
        protected BaseHarvestedCrop(int itemID, int amount) : base(itemID)
        {
            Amount = amount;
            Stackable = true;
            Weight = 2;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendFailureMessage("That must be in your backpack!");
                return;
            }

            if (from.ShilCheckSkill(SkillName.Alchemy))
            {
                from.Backpack.TryDropItem(from, Product, false);
                from.SendSuccessMessage("You successfully cull the reagent.");
            }
            else
                from.SendFailureMessage("You pick the plant down to nothing.");

            Consume();
        }
        
    }

    [Serializable(0, false)]
    public partial class RawGarlic : BaseHarvestedCrop
    {
        protected override Item Product => new Garlic(10);

        public override string DefaultName => "Raw Garlic";
        
        [Constructible]
        public RawGarlic() : this(1)
        {
        }

        [Constructible]
        public RawGarlic(int amount) : base(0x18E3, amount)
        {
        }
    }
}