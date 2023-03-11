using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13cd, 0x13c5)]
    public partial class PoisonElementLeatherArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
        
        public override string DefaultName => "Leather Sleeves of the Poison Element";

        [Constructible]
        public PoisonElementLeatherArms() : base(0x13CD)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 2.0;
        }
    }
}