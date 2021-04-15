using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [FlipableAttribute(0x144e, 0x1453)]
    public class TerrorArms : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;

        [Constructible]
        public TerrorArms() : base(0x144E)
        {
            Name = "Bone Arms of Terror";
            Hue = 1181;
            Identified = false;
            Weight = 2.0;
        }

        [Constructible]
        public TerrorArms(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}