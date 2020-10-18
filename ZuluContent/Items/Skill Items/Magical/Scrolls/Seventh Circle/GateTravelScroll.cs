namespace Server.Items
{
    public class GateTravelScroll : SpellScroll
    {
        [Constructible]
        public GateTravelScroll() : this(1)
        {
        }


        [Constructible]
        public GateTravelScroll(int amount) : base(Spells.SpellEntry.GateTravel, 0x1F60, amount)
        {
        }

        [Constructible]
        public GateTravelScroll(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();
        }
    }
}