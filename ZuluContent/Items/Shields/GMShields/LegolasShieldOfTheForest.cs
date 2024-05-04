using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class LegolasShieldOfTheForest : BaseShield, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 110;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Legola's Shield of the Bray";

        [Constructible]
        public LegolasShieldOfTheForest() : base(0x1B7A)
        {
            Hue = 1175;
            Identified = false;
            Weight = 6.0;
        }
    }
}