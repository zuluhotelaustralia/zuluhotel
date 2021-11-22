namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class PoisonElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Poison Pentagram Piece {piece + 1}";
            Hue = 1162;
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram1 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram1() : base(0)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram2 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram2() : base(1)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram3 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram3() : base(2)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram4 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram4() : base(3)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram5 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram5() : base(4)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram6 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram6() : base(5)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram7 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram7() : base(6)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram8 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram8() : base(7)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class PoisonElementalPentagram9 : PoisonElementalPentagram
    {
        [Constructible]
        public PoisonElementalPentagram9() : base(8)
        {
        }
    }
}