using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x144f, 0x1454)]
    public partial class TerrorChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 25.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

        public override string DefaultName => "Bone Tunic of Terror";

        [Constructible]
        public TerrorChest() : base(0x144F)
        {
            Hue = 1181;
            Identified = false;
            Weight = 6.0;
        }
    }
}