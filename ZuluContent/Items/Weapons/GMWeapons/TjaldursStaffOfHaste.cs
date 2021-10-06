using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0xDF1, 0xDF0)]
    public partial class TjaldursStaffOfHaste : BaseStaff, IGMItem
    {
        public override int DefaultStrengthReq => 70;

        public override int DefaultMinDamage => 22;

        public override int DefaultMaxDamage => 42;

        public override int DefaultSpeed => 90;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Tjaldur's Staff of Haste";

        [Constructible]
        public TjaldursStaffOfHaste() : base(0xDF0)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
            Hue = 1167;
            Identified = false;
        }
    }
}