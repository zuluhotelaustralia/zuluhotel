using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class FireArrow : Item
    {
        public override double DefaultWeight => 0.1;

        public override string DefaultName => "fire arrow";

        [Constructible]
        public FireArrow() : this(1)
        {
        }


        [Constructible]
        public FireArrow(int amount) : base(0x0F3F)
        {
            Hue = 0x0494;
            Stackable = true;
            Amount = amount;
        }
    }
}