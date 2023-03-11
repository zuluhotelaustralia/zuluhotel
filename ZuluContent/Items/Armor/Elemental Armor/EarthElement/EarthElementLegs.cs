using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class EarthElementLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 65;

        public override int DefaultDexBonus => -4;

        public override double DefaultMagicEfficiencyPenalty => 9.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Legs of the Earth Element";

        [Constructible]
        public EarthElementLegs() : base(0x1411)
        {
            Hue = 1134;
            EarthResist = 75;
            Weight = 7.0;
        }
    }
}