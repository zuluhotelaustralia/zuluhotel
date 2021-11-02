using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class DrakonHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -3;
        
        public override double DefaultMagicEfficiencyPenalty => 7.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Helm of Drakon";

        [Constructible]
        public DrakonHelm() : base(0x1412)
        {
            Hue = 1162;
            Identified = false;
            Weight = 5.0;
        }
    }
}