using Server.Engines.Craft;

namespace Server.Items
{
    public class MortarPestle : BaseTool
    {
        public override CraftSystem CraftSystem => DefAlchemy.NormalCraftSystem;

        [Constructible]
        public MortarPestle() : base(0xE9B)
        {
            Weight = 1.0;
        }


        [Constructible]
        public MortarPestle(int uses) : base(uses, 0xE9B)
        {
            Weight = 1.0;
        }

        [Constructible]
        public MortarPestle(Serial serial) : base(serial)
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