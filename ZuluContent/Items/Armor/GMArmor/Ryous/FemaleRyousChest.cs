using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1c04, 0x1c05)]
    public partial class FemaleRyousChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 8.0;

        public override int ArmorBase => 75;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Female Plate of Ryous";

        [Constructible]
        public FemaleRyousChest() : base(0x1C04)
        {
            Hue = 1413;
            Identified = false;
            Weight = 4.0;
        }
    }
}