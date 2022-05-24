using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1415, 0x1416)]
    public partial class DrakonChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 11.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Platemail of Drakon";

        [Constructible]
        public DrakonChest() : base(0x1415)
        {
            Hue = 1162;
            Identified = false;
            Weight = 10.0;
        }
    }
}