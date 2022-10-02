using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cc, 0x13d3)]
    public partial class WaterElementLeatherChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Tunic of the Water Element";

        [Constructible]
        public WaterElementLeatherChest() : base(0x13CC)
        {
            Hue = 1167;
            WaterResist = 50;
            Weight = 6.0;
        }
    }
}