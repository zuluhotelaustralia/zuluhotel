using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1452, 0x1457)]
    public partial class TerrorLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

        public override string DefaultName => "Bone Legs of Terror";

        [Constructible]
        public TerrorLegs() : base(0x1452)
        {
            Hue = 1181;
            Identified = false;
            Weight = 3.0;
        }
    }
}