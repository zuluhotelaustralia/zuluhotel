using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1c02, 0x1c03)]
    public partial class FemaleSylvianChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;

        public override string DefaultName => "Sylvian Female Studded";

        [Constructible]
        public FemaleSylvianChest() : base(0x1C02)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 6.0;
        }
    }
}