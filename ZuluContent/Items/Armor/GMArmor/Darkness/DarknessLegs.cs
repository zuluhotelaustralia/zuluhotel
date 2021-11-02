using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class DarknessLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 110;

        public override int InitMaxHits => 110;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 70;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Legs of Darkness";

        [Constructible]
        public DarknessLegs() : base(0x1411)
        {
            Hue = 1157;
            Identified = false;
            Weight = 7.0;
        }
    }
}