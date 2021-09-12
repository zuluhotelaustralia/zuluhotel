namespace Server.Items
{
    [Serializable(0, false)]
    public partial class IceArrow : Item
    {
        public override double DefaultWeight => 0.1;

        public override string DefaultName => "ice arrow";

        [Constructible]
        public IceArrow() : this(1)
        {
        }


        [Constructible]
        public IceArrow(int amount) : base(0x0F3F)
        {
            Hue = 0x0492;
            Stackable = true;
            Amount = amount;
        }
    }
}