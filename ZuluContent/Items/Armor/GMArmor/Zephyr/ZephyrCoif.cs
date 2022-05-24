using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13BB, 0x13C0)]
    public partial class ZephyrCoif : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;

        public override string DefaultName => "Zephyr Chainmail Coif";


        [Constructible]
        public ZephyrCoif() : base(0x13BB)
        {
            Hue = 1328;
            AirResist = 75;
            Identified = false;
            Weight = 1.0;
        }
    }
}