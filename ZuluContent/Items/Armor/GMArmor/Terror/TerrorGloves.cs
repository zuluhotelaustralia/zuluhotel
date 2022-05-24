using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x1450, 0x1455)]
    public partial class TerrorGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

        public override string DefaultName => "Bone Gloves of Terror";

        [Constructible]
        public TerrorGloves() : base(0x1450)
        {
            Hue = 1181;
            Identified = false;
            Weight = 2.0;
        }
    }
}