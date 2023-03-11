using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class EarthElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 50;

        public override int DefaultStrReq => 60;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Mask of the Earth Element";

        [Constructible]
        public EarthElementLeatherCap() : base(0x1547)
        {
            Hue = 1134;
            EarthResist = 75;
            Weight = 2.0;
        }
    }
}