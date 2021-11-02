using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13dc, 0x13d4)]
    public partial class SylvianArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
        
        public override string DefaultName => "Sylvian Studded Sleeves";

        [Constructible]
        public SylvianArms() : base(0x13DC)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 4.0;
        }
    }
}