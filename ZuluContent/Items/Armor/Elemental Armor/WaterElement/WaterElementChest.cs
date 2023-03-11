using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1415, 0x1416)]
    public partial class WaterElementChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 100;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Breastplate of the Water Element";

        [Constructible]
        public WaterElementChest() : base(0x1415)
        {
            Hue = 1167;
            WaterResist = 75;
            Weight = 10.0;
        }
    }
}