using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1415, 0x1416)]
    public partial class AirElementChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Breastplate of the Air Element";

        [Constructible]
        public AirElementChest() : base(0x1415)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 10.0;
            DexBonus = 3;
        }
    }
}