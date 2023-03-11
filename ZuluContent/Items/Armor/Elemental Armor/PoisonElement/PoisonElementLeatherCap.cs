using ModernUO.Serialization;
using ZuluContent.Zulu.Items;
using Server.Engines.Magic;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class PoisonElementLeatherCap : BaseArmor, IGMItem
    {
        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 60;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;

        public override string DefaultName => "Mask of the Poison Element";

        [Constructible]
        public PoisonElementLeatherCap() : base(0x1547)
        {
            Hue = 264;
            PoisonImmunity = PoisonLevel.Greater;
            Weight = 2.0;
        }
    }
}