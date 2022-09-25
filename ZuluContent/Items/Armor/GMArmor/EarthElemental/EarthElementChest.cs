using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1415, 0x1416)]
    public partial class EarthElementChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

		public override int DefaultDexBonus => -13;

		public override double DefaultMagicEfficiencyPenalty => 28.0;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Breastplate of the Earth Element";

        [Constructible]
        public EarthElementChest() : base(0x1415)
        {
            Hue = 1134;
			EarthResist = 50;
            Weight = 10.0;
        }
    }
}