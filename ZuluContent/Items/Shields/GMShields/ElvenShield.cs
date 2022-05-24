using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class ElvenShield : BaseShield, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 70;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Elven Shield";

        [Constructible]
        public ElvenShield() : base(0x1B72)
        {
            Hue = 1165;
            Identified = false;
            Weight = 6.0;
        }
    }
}