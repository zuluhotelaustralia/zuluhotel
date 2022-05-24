using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13f0, 0x13f1)]
    public partial class InfernalLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;

        public override string DefaultName => "Infernal Ringmail Leggings";

        [Constructible]
        public InfernalLegs() : base(0x13F0)
        {
            Hue = 1141;
            FireResist = 75;
            Identified = false;
            Weight = 7.0;
        }
    }
}