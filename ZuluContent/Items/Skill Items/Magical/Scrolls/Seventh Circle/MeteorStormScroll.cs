namespace Server.Items
{
    public class MeteorSwarmScroll : SpellScroll
    {
        [Constructible]
        public MeteorSwarmScroll() : this(1)
        {
        }


        [Constructible]
        public MeteorSwarmScroll(int amount) : base(Spells.SpellEntry.MeteorSwarm, 0x1F63, amount)
        {
        }

        [Constructible]
        public MeteorSwarmScroll(Serial serial) : base(serial)
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