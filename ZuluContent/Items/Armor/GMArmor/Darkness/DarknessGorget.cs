using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class DarknessGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override int DefaultStrReq => 0;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 70;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Gorget of Darkness";

        [Constructible]
        public DarknessGorget() : base(0x1413)
        {
            Hue = 1157;
            Identified = false;
            Weight = 2.0;
        }
    }
}