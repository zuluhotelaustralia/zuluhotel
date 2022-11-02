using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cd, 0x13c5)]
    public partial class WaterElementLeatherArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Sleeves of the Water Element";

        [Constructible]
        public WaterElementLeatherArms() : base(0x13CD)
        {
            Hue = 1167;
            WaterResist = 50;
            Weight = 2.0;
        }
    }
}