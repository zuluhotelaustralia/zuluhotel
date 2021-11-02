using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1414, 0x1418)]
    public partial class DarknessGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 70;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Gloves of Darkness";


        [Constructible]
        public DarknessGloves() : base(0x1414)
        {
            Hue = 1157;
            Identified = false;
            Weight = 2.0;
        }
    }
}