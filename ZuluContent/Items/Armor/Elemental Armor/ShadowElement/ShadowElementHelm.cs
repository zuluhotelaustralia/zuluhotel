using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class ShadowElementHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of the Shadow Element";

        [Constructible]
        public ShadowElementHelm() : base(0x1412)
        {
            Hue = 1157;
            NecroResist = 75;
            Weight = 5.0;
        }
    }
}