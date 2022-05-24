using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class SylvianGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override string DefaultName => "Sylvian Studded Gorget";

        [Constructible]
        public SylvianGorget() : base(0x13D6)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 1.0;
        }
    }
}