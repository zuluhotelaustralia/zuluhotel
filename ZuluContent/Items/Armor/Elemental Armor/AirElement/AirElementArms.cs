using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1410, 0x1417)]
    public partial class AirElementArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Arms of the Air Element";

        [Constructible]
        public AirElementArms() : base(0x1410)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 5.0;
            DexBonus = 2;
        }
    }
}