using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13db, 0x13e2)]
    public partial class SylvianChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 10;

        public override int ArmorBase => 45;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Studded;
        
        public override string DefaultName => "Sylvian Studded Tunic";

        [Constructible]
        public SylvianChest() : base(0x13DB)
        {
            Hue = 1285;
            Identified = false;
            EarthResist = 75;
            Weight = 8.0;
        }
    }
}