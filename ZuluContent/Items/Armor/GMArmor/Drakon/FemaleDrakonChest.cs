using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1c04, 0x1c05)]
    public partial class FemaleDrakonChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -6;

        public override double DefaultMagicEfficiencyPenalty => 17.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Female Plate of Drakon";

        [Constructible]
        public FemaleDrakonChest() : base(0x1C04)
        {
            Hue = 1162;
            Identified = false;
            Weight = 4.0;
        }
    }
}