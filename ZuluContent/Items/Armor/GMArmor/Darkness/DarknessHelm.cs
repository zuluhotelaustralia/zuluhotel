using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class DarknessHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -2;
        
        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 70;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of Darkness";

        [Constructible]
        public DarknessHelm() : base(0x1412)
        {
            Hue = 1157;
            Identified = false;
            Weight = 5.0;
        }
    }
}