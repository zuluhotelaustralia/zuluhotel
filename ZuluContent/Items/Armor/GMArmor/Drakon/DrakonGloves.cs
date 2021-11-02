using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1414, 0x1418)]
    public partial class DrakonGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 70;
        
        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Gloves of Drakon";


        [Constructible]
        public DrakonGloves() : base(0x1414)
        {
            Hue = 1162;
            Identified = false;
            Weight = 2.0;
        }
    }
}