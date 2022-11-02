using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1410, 0x1417)]
    public partial class EarthElementArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int DefaultDexBonus => -4;

        public override double DefaultMagicEfficiencyPenalty => 9.0;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Arms of the Earth Element";

        [Constructible]
        public EarthElementArms() : base(0x1410)
        {
            Hue = 1134;
            EarthResist = 50;
            Weight = 5.0;
        }
    }
}