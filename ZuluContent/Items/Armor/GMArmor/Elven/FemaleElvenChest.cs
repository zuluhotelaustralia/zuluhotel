using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1c04, 0x1c05)]
    public partial class FemaleElvenChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Elven Female Plate";

        [Constructible]
        public FemaleElvenChest() : base(0x1C04)
        {
            Hue = 1165;
            Identified = false;
            Weight = 4.0;
        }
    }
}