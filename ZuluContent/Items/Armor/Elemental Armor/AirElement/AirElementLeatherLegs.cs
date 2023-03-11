using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cb, 0x13d2)]
    public partial class AirElementLeatherLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 35;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Legs of the Air Element";

        [Constructible]
        public AirElementLeatherLegs() : base(0x13CB)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 4.0;
            DexBonus = 3;
        }
    }
}