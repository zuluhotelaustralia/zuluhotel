using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1410, 0x1417)]
    public partial class DrakonArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 125;

        public override int InitMaxHits => 125;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -3;

        public override double DefaultMagicEfficiencyPenalty => 7.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Platemail Arms of Drakon";

        [Constructible]
        public DrakonArms() : base(0x1410)
        {
            Hue = 1162;
            Identified = false;
            Weight = 5.0;
        }
    }
}