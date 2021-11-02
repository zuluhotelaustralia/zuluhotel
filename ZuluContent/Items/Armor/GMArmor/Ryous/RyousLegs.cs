using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class RyousLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 70;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 4.0;

        public override int ArmorBase => 75;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Platemail Legs of Ryous";

        [Constructible]
        public RyousLegs() : base(0x1411)
        {
            Hue = 1413;
            Identified = false;
            Weight = 7.0;
        }
    }
}