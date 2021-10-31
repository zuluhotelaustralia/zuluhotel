using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    public partial class RyousShield : BaseShield, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int ArmorBase => 45;

        public override int DefaultStrReq => 70;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Shield of Ryous";

        [Constructible]
        public RyousShield() : base(0x1B74)
        {
            Hue = 1413;
            Identified = false;
            Weight = 7.0;
        }
    }
}