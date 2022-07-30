using Server.Engines.Craft;

namespace Server.Items
{
    [FlipableAttribute(0x13E3, 0x13E4)]
    public class SmithHammer : BaseEquippableTool
    {
        public override CraftSystem CraftSystem
        {
            get { return DefBlacksmithy.CraftSystem; }
        }


        [Constructible]
        public SmithHammer() : base(0x13E3)
        {
            Weight = 8.0;
            Layer = Layer.OneHanded;
        }


        [Constructible]
        public SmithHammer(int uses) : base(uses, 0x13E3)
        {
            Weight = 8.0;
            Layer = Layer.OneHanded;
        }

        [Constructible]
        public SmithHammer(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}