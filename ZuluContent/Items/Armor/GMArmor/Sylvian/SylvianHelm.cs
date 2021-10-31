using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x1db9, 0x1dba)]
    public partial class SylvianHelm : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Leather;
        
        public override string DefaultName => "Sylvian Leather Cap";

        [Constructible]
        public SylvianHelm() : base(0x1DB9)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 2.0;
        }
    }
}