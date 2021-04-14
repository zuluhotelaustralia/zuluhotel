using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(0x1451, 0x1456)]
    public class TerrorHelm : BaseArmor, IFortifiable
    {
        public override int InitMinHits => 100;

        public override int InitMaxHits => 100;

        public override int DefaultStrReq => 30;

        public override int DefaultDexBonus => -2;

        public override double DefaultMagicEfficiencyPenalty => 10.0;

        public override int ArmorBase => 50;

        public override ArmorMaterialType MaterialType => ArmorMaterialType.Bone;


        [Constructible]
        public TerrorHelm() : base(0x1451)
        {
            Name = "Bonehelm of Terror";
            Hue = 1181;
            Identified = false;
            Weight = 3.0;
        }

        [Constructible]
        public TerrorHelm(Serial serial) : base(serial)
        {
        }
        
        public override void OnSingleClick(Mobile from)
        {
            if (!string.IsNullOrEmpty(Name))
                LabelTo(from, Name);
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