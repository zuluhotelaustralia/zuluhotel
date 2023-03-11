using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class AirElementGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Gorget of the Air Element";

        [Constructible]
        public AirElementGorget() : base(0x1413)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 2.0;
            DexBonus = 1;
        }
    }
}