namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class FireElementalPentagram : ElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram(ushort piece) : base(piece)
        {
            Name = $"Fire Pentagram Piece {piece + 1}";
            Hue = 139;
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram1 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram1() : base(0)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram2 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram2() : base(1)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram3 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram3() : base(2)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram4 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram4() : base(3)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram5 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram5() : base(4)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram6 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram6() : base(5)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram7 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram7() : base(6)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram8 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram8() : base(7)
        {
        }
    }
    
    [Serializable(0, false)]
    public partial class FireElementalPentagram9 : FireElementalPentagram
    {
        [Constructible]
        public FireElementalPentagram9() : base(8)
        {
        }
    }
}