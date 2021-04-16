using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [FlipableAttribute(0x1450, 0x1455)]
    public class TerrorGloves : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 5.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public TerrorGloves() : base(0x1450)
        {
            Name = "Bone Gloves of Terror";
            Hue = 1181;
            Identified = false;
            Weight = 2.0;
        }

        [Constructible]
        public TerrorGloves(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);

            Weight = 2.0;
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}