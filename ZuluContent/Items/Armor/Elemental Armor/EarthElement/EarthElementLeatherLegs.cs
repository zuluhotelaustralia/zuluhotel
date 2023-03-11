using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cb, 0x13d2)]
    public partial class EarthElementLeatherLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 50;

        public override int DefaultStrReq => 60;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Legs of the Earth Element";

        [Constructible]
        public EarthElementLeatherLegs() : base(0x13CB)
        {
            Hue = 1134;
            EarthResist = 75;
            Weight = 4.0;
        }
    }
}