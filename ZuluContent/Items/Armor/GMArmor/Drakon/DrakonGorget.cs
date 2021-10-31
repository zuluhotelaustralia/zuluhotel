using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class DrakonGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 0;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Gorget of Drakon";

        [Constructible]
        public DrakonGorget() : base(0x1413)
        {
            Hue = 1162;
            Identified = false;
            Weight = 2.0;
        }
    }
}