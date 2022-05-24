using ModernUO.Serialization;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public abstract partial class ShadowElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Shadow Pentagram Piece {piece + 1}";
            Hue = 1157;
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram1 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram1() : base(0)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram2 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram2() : base(1)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram3 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram3() : base(2)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram4 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram4() : base(3)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram5 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram5() : base(4)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram6 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram6() : base(5)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram7 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram7() : base(6)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram8 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram8() : base(7)
        {
        }
    }

    [SerializationGenerator(0, false)]
    public partial class ShadowElementalPentagram9 : ShadowElementalPentagram
    {
        [Constructible]
        public ShadowElementalPentagram9() : base(8)
        {
        }
    }
}