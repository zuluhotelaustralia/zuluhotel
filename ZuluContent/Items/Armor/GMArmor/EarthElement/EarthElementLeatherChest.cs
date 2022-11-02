using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cc, 0x13d3)]
    public partial class EarthElementLeatherChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 60;

        public override int DefaultDexBonus => -5;

        public override double DefaultMagicEfficiencyPenalty => 13.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Tunic of the Earth Element";

        [Constructible]
        public EarthElementLeatherChest() : base(0x13CC)
        {
            Hue = 1134;
            EarthResist = 50;
            Weight = 6.0;
        }
    }
}