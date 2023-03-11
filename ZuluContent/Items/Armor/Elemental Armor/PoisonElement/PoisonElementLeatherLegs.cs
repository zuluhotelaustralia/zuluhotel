using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cb, 0x13d2)]
    public partial class PoisonElementLeatherLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 80;

        public override int InitMaxHits => 80;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Legs of the Poison Element";

        [Constructible]
        public PoisonElementLeatherLegs() : base(0x13CB)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 4.0;
        }
    }
}