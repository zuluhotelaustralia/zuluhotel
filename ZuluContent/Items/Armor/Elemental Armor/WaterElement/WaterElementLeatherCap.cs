using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1db9, 0x1dba)]
    public partial class WaterElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override int DefaultStrReq => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Cap of the Water Element";

        [Constructible]
        public WaterElementLeatherCap() : base(0x1DB9)
        {
            Hue = 1167;
            WaterResist = 75;
            Weight = 2.0;
        }
    }
}