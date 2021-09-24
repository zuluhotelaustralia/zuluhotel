namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class AirElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram(int piece) : base(piece)
        {
            Name = $"Air Pentagram Piece {piece + 1}";
            Hue = 1161;
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram1 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram1() : base(0)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram2 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram2() : base(1)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram3 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram3() : base(2)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram4 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram4() : base(3)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram5 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram5() : base(4)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram6 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram6() : base(5)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram7 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram7() : base(6)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram8 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram8() : base(7)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class AirElementalPentagram9 : AirElementalPentagram
    {
        [Constructible]
        public AirElementalPentagram9() : base(8)
        {
        }
    }
}