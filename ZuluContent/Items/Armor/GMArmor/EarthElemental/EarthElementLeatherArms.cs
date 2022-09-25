using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cd, 0x13c5)]
    public partial class EarthElementLeatherArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

		public override int DefaultStrReq => 60;

		public override int DefaultDexBonus => -2;

		public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
        
        public override string DefaultName => "Leather Sleeves of the Earth Element";

        [Constructible]
        public EarthElementLeatherArms() : base(0x13CD)
        {
            Hue = 1134;
			EarthResist = 50;
            Weight = 2.0;
        }
    }
}