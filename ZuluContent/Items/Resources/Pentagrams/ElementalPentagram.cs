namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class ElementalPentagram : BaseShrine
    {
        [Constructible]
        public ElementalPentagram(ushort piece) : base(piece, 4070 + piece)
        {
            Weight = 1;
        }
    }
}