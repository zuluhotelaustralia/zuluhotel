namespace Server.Items
{
    public class TeleportScroll : SpellScroll
    {
        [Constructible]
        public TeleportScroll() : this(1)
        {
        }


        [Constructible]
        public TeleportScroll(int amount) : base(Spells.SpellEntry.Teleport, 0x1F42, amount)
        {
        }

        [Constructible]
        public TeleportScroll(Serial serial) : base(serial)
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