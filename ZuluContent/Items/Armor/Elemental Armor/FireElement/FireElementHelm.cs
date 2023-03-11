using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class FireElementHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of the Fire Element";

        [Constructible]
        public FireElementHelm() : base(0x1412)
        {
            Hue = 1172;
            FireResist = 75;
            Weight = 5.0;
        }
    }
}