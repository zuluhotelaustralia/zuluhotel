using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [Flipable]
    public partial class AirElementLeatherGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 35;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Gloves of the Air Element";

        [Constructible]
        public AirElementLeatherGloves() : base(0x13C6)
        {
            Hue = 1161;
            AirResist = 75;
            Weight = 1.0;
            DexBonus = 1;
        }
    }
}