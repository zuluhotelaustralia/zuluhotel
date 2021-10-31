using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1410, 0x1417)]
    public partial class ElvenArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Elven Platemail Arms";

        [Constructible]
        public ElvenArms() : base(0x1410)
        {
            Hue = 1165;
            Identified = false;
            Weight = 5.0;
        }
    }
}