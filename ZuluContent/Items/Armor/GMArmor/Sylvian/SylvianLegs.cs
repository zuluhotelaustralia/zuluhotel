using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13da, 0x13e1)]
    public partial class SylvianLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override string DefaultName => "Sylvian Studded Leggings";

        [Constructible]
        public SylvianLegs() : base(0x13DA)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 5.0;
        }
    }
}