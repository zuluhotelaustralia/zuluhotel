using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class AirElementLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Legs of the Air Element";

        [Constructible]
        public AirElementLegs() : base(0x1411)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 7.0;
            DexBonus = 2;
        }
    }
}