namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class ElementalPentagram : BaseShrine
    {
        [Constructible]
        public ElementalPentagram(int piece) : base(4070 + piece)
        {
            Piece = piece;
            Weight = 1;
        }
    }
}