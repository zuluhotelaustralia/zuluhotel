using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1414, 0x1418)]
    public partial class ElvenGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 70;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Elven Platemail Gloves";


        [Constructible]
        public ElvenGloves() : base(0x1414)
        {
            Hue = 1165;
            Identified = false;
            Weight = 2.0;
        }
    }
}