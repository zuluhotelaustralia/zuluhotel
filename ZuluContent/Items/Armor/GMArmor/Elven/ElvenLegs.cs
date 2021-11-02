using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class ElvenLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 2.0;

        public override int ArmorBase => 65;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Elven Platemail Legs";

        [Constructible]
        public ElvenLegs() : base(0x1411)
        {
            Hue = 1165;
            Identified = false;
            Weight = 7.0;
        }
    }
}