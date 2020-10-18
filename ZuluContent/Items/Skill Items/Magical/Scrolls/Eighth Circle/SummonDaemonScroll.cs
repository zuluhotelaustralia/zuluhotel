namespace Server.Items
{
    public class SummonDaemonScroll : SpellScroll
    {
        [Constructible]
        public SummonDaemonScroll() : this(1)
        {
        }


        [Constructible]
        public SummonDaemonScroll(int amount) : base(Spells.SpellEntry.SummonDaemon, 0x1F69, amount)
        {
        }

        [Constructible]
        public SummonDaemonScroll(Serial serial) : base(serial)
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