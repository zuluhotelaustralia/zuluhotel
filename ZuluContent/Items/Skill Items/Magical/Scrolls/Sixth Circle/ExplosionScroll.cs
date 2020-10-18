namespace Server.Items
{
    public class ExplosionScroll : SpellScroll
    {
        [Constructible]
        public ExplosionScroll() : this(1)
        {
        }


        [Constructible]
        public ExplosionScroll(int amount) : base(Spells.SpellEntry.Explosion, 0x1F57, amount)
        {
        }

        [Constructible]
        public ExplosionScroll(Serial serial) : base(serial)
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