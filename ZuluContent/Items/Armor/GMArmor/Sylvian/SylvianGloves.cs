using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13d5, 0x13dd)]
    public partial class SylvianGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
        
        public override string DefaultName => "Sylvian Studded Gloves";

        [Constructible]
        public SylvianGloves() : base(0x13D5)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 1.0;
        }
    }
}