using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1415, 0x1416)]
    public partial class RyousChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 11.0;

        public override int ArmorBase => 75;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail of Ryous";

        [Constructible]
        public RyousChest() : base(0x1415)
        {
            Hue = 1413;
            Identified = false;
            Weight = 10.0;
        }
    }
}