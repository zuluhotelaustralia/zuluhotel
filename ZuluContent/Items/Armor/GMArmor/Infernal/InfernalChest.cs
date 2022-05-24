using ModernUO.Serialization;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [SerializationGenerator(0, false)]
    [FlipableAttribute(0x13ec, 0x13ed)]
    public partial class InfernalChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 6.0;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;

        public override string DefaultName => "Infernal Ringmail Tunic";

        [Constructible]
        public InfernalChest() : base(0x13EC)
        {
            Hue = 1141;
            FireResist = 75;
            Identified = false;
            Weight = 10.0;
        }
    }
}