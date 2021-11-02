using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class ElvenGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 0;

        public override double DefaultMagicEfficiencyPenalty => 1.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Elven Platemail Gorget";

        [Constructible]
        public ElvenGorget() : base(0x1413)
        {
            Hue = 1165;
            Identified = false;
            Weight = 2.0;
        }
    }
}