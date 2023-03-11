using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class AirElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 35;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Mask of the Air Element";

        [Constructible]
        public AirElementLeatherCap() : base(0x1547)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 2.0;
            DexBonus = 2;
        }
    }
}