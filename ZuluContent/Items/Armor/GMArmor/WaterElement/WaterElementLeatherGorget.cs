using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class WaterElementLeatherGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Gorget of the Water Element";

        [Constructible]
        public WaterElementLeatherGorget() : base(0x13C7)
        {
            Hue = 1167;
            Weight = 1.0;
        }
    }
}