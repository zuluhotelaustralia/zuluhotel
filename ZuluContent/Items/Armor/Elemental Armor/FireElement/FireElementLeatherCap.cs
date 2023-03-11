using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class FireElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 40;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Mask of the Fire Element";

        [Constructible]
        public FireElementLeatherCap() : base(0x1547)
        {
            Hue = 1172;
            FireResist = 75;
            Weight = 2.0;
        }
    }
}