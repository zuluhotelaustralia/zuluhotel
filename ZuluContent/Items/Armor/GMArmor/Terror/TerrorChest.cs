using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [FlipableAttribute(0x144f, 0x1454)]
    public class TerrorChest : BaseArmor, IGMItem
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 25.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public TerrorChest() : base(0x144F)
        {
            Name = "Bone Tunic of Terror";
            Hue = 1181;
            Identified = false;
            Weight = 6.0;
        }

        [Constructible]
        public TerrorChest(Serial serial) : base(serial)
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