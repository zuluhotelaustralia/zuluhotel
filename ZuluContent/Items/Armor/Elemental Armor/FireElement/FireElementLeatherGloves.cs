using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [Flipable]
    public partial class FireElementLeatherGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Gloves of the Fire Element";

        [Constructible]
        public FireElementLeatherGloves() : base(0x13C6)
        {
            Hue = 1172;
            FireResist = 75;
            Weight = 1.0;
        }
    }
}