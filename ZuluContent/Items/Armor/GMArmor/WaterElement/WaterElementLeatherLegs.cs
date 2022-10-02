using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cb, 0x13d2)]
    public partial class WaterElementLeatherLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Legs of the Water Element";

        [Constructible]
        public WaterElementLeatherLegs() : base(0x13CB)
        {
            Hue = 1167;
            WaterResist = 50;
            Weight = 4.0;
        }
    }
}