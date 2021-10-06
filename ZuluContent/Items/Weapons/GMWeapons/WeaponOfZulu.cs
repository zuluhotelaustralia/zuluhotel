using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13B9, 0x13BA)]
    public partial class WeaponOfZulu : BaseSword, IGMItem
    {
        public override int DefaultStrengthReq => 90;

        public override int DefaultMinDamage => 31;

        public override int DefaultMaxDamage => 53;

        public override int DefaultSpeed => 36;
        
        public override int InitMinHits => 200;

        public override int InitMaxHits => 200;
        
        public override bool AllowEquippedCast(Mobile from) => true;

        public override string DefaultName => "Enchanted Sword of Zulu";

        [Constructible]
        public WeaponOfZulu() : base(0x13B9)
        {
            Weight = 6.0;
            Hue = 1165;
            Identified = false;
        }
    }
}