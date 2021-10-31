using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class WaterElementLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 100;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Legs of the Water Element";

        [Constructible]
        public WaterElementLegs() : base(0x1411)
        {
            Hue = 1167;
            Weight = 7.0;
        }
    }
}