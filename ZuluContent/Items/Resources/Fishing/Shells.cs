namespace Server.Items
{
    [Serializable(0, false)]
    public abstract partial class BaseShrine : Item
    {
        public override double DefaultWeight => 1.0;

        [SerializableField(0)]
        private ushort _piece;

        public BaseShrine(ushort piece, int itemID) : base(itemID)
        {
            _piece = piece;
            
        }
    }

    [Serializable(0, false)]
    public partial class Shell : BaseShrine
    {
        private static readonly string[] Names = { "Aquaria Shell 1", "Capricornia Shell 2", "Sea Nymph 3", "Neptune's Nautilus 4", "Sea Shore Sand Dollar 5", "Divinia Shell 6", "Mermaid Shell 7", "Ocean Odyssey 8", "Talimari 9" };

        private static readonly int[] ItemIDs = { 0xFC4, 0xFC5, 0xFC6, 0xFC7, 0xFC8, 0xFC9, 0xFCA, 0xFCB, 0xFCC  };

        private static readonly int[] Hues = { 0x504, 0x519, 0x606, 0x505, 0x501, 0x499, 0x480, 0x489, 0x496 };

        [Constructible]
        public Shell(ushort piece) : base(piece, ItemIDs[piece])
        {
            Name = Names[piece];
            Hue = Hues[piece];
        }
    }
}