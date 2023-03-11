using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cc, 0x13d3)]
    public partial class AirElementLeatherChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int ArmorBase => 35;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Tunic of the Air Element";

        [Constructible]
        public AirElementLeatherChest() : base(0x13CC)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 6.0;
            DexBonus = 4;
        }
    }
}