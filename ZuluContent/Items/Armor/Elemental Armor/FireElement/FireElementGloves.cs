using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1414, 0x1418)]
    public partial class FireElementGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Gloves of the Fire Element";


        [Constructible]
        public FireElementGloves() : base(0x1414)
        {
            Hue = 1172;
            FireResist = 75;
            Weight = 2.0;
        }
    }
}