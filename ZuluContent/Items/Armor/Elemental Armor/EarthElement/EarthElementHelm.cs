using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class EarthElementHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 65;

        public override int DefaultDexBonus => -4;

        public override double DefaultMagicEfficiencyPenalty => 9.0;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of the Earth Element";

        [Constructible]
        public EarthElementHelm() : base(0x1412)
        {
            Hue = 1134;
            EarthResist = 75;
            Weight = 5.0;
        }
    }
}