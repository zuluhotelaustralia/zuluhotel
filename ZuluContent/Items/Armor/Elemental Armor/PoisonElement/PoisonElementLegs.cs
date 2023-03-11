using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1411, 0x141a)]
    public partial class PoisonElementLegs : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Legs of the Poison Element";

        [Constructible]
        public PoisonElementLegs() : base(0x1411)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 7.0;
        }
    }
}