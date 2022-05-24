using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public abstract partial class AirElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Air Pentagram Piece {piece + 1}";
            Hue = 1161;
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram1 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram1() : base(0)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram2 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram2() : base(1)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram3 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram3() : base(2)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram4 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram4() : base(3)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram5 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram5() : base(4)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram6 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram6() : base(5)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram7 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram7() : base(6)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram8 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram8() : base(7)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class AirElementalPentagram9 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram9() : base(8)
        {
        }
    }
}