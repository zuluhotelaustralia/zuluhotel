using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
	[FlipableAttribute(0x1412, 0x1419)]
    public partial class WaterElementHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 100;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of the Water Element";

        [Constructible]
        public WaterElementHelm() : base(0x1412)
        {
            Hue = 1167;
            Weight = 5.0;
        }
    }
}