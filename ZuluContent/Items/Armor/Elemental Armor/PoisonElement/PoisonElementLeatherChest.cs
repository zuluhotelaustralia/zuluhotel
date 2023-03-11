using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cc, 0x13d3)]
    public partial class PoisonElementLeatherChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 90;

        public override int InitMaxHits => 90;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Leather Tunic of the Poison Element";

        [Constructible]
        public PoisonElementLeatherChest() : base(0x13CC)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 6.0;
        }
    }
}