using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class PoisonElementGorget : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int DefaultStrReq => 110;

        public override int ArmorBase => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Plate;
        
        public override string DefaultName => "Plate Gorget of the Poison Element";

        [Constructible]
        public PoisonElementGorget() : base(0x1413)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 2.0;
        }
    }
}