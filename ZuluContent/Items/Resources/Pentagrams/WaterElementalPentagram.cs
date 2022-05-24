using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public abstract partial class WaterElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Water Pentagram Piece {piece + 1}";
            Hue = 1167;
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram1 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram1() : base(0)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram2 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram2() : base(1)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram3 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram3() : base(2)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram4 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram4() : base(3)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram5 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram5() : base(4)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram6 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram6() : base(5)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram7 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram7() : base(6)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram8 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram8() : base(7)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class WaterElementalPentagram9 : WaterElementalPentagram
    {
        [Constructible]
        public WaterElementalPentagram9() : base(8)
        {
        }
    }
}