namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class EarthElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Earth Pentagram Piece {piece + 1}";
            Hue = 1538;
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram1 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram1() : base(0)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram2 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram2() : base(1)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram3 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram3() : base(2)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram4 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram4() : base(3)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram5 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram5() : base(4)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram6 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram6() : base(5)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram7 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram7() : base(6)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram8 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram8() : base(7)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class EarthElementalPentagram9 : EarthElementalPentagram
    {
        [Constructible]
        public EarthElementalPentagram9() : base(8)
        {
        }
    }
}