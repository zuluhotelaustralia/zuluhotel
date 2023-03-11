using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1410, 0x1417)]
    public partial class PoisonElementArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;

        public override string DefaultName => "Plate Arms of the Poison Element";

        [Constructible]
        public PoisonElementArms() : base(0x1410)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 5.0;
        }
    }
}