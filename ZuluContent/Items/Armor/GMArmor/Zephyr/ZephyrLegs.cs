using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13be, 0x13c3)]
    public partial class ZephyrLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Chainmail;

        public override string DefaultName => "Zephyr Chainmail Leggings";

        [Constructible]
        public ZephyrLegs() : base(0x13BE)
        {
            Hue = 1328;
            AirResist = 75;
            Identified = false;
            Weight = 7.0;
        }
    }
}