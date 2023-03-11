using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cd, 0x13c5)]
    public partial class AirElementLeatherArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 35;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
        
        public override string DefaultName => "Leather Sleeves of the Air Element";

        [Constructible]
        public AirElementLeatherArms() : base(0x13CD)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 2.0;
            DexBonus = 3;
        }
    }
}