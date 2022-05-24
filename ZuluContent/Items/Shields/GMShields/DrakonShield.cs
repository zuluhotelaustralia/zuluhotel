using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    public partial class DrakonShield : BaseShield, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 70;

        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Shield of Drakon";

        [Constructible]
        public DrakonShield() : base(0x1B76)
        {
            Hue = 1162;
            Identified = false;
            Weight = 8.0;
        }
    }
}