using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13C6, 0x13CE)]
    public partial class EarthElementLeatherGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 60;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Gloves of the Earth Element";

        [Constructible]
        public EarthElementLeatherGloves() : base(0x13C6)
        {
            Hue = 1134;
            EarthResist = 50;
            Weight = 1.0;
        }
    }
}