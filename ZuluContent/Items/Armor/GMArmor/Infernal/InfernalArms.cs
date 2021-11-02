using ZuluContent.Zulu.Engines.Magic.Enchantments;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [Serializable(0, false)]
    [FlipableAttribute(0x13ee, 0x13ef)]
    public partial class InfernalArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 20;

        public override int DefaultDexBonus => -1;

        public override double DefaultMagicEfficiencyPenalty => 3.0;

        public override int ArmorBase => 55;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Ringmail;

        public override string DefaultName => "Infernal Ringmail Sleeves";

        [Constructible]
        public InfernalArms() : base(0x13EE)
        {
            Hue = 1141;
            FireResist = 75;
            Identified = false;
            Weight = 7.0;
        }
    }
}