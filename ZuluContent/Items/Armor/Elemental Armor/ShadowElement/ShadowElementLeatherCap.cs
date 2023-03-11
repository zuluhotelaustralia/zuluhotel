using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class ShadowElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Mask of the Shadow Element";

        [Constructible]
        public ShadowElementLeatherCap() : base(0x1547)
        {
            Hue = 1157;
            NecroResist = 75;
            Weight = 2.0;
        }
    }
}