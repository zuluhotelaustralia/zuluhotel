using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x144e, 0x1453)]
    public partial class TerrorArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;
        
        public override string DefaultName => "Bone Arms of Terror";

        [Constructible]
        public TerrorArms() : base(0x144E)
        {
            Hue = 1181;
            Identified = false;
            Weight = 2.0;
        }
    }
}