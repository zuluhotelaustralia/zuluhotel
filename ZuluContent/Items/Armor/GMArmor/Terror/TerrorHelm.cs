using ModernUO.Serialization;
using Server.Engines.Craft;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1451, 0x1456)]
    public partial class TerrorHelm : BaseArmor, IFortifiable, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

        public override string DefaultName => "Bone Helm of Terror";

        [Constructible]
        public TerrorHelm() : base(0x1451)
        {
            Hue = 1181;
            Identified = false;
            Weight = 3.0;
        }
    }
}