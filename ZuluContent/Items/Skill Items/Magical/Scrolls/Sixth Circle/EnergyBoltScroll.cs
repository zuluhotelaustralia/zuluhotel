namespace Server.Items
{
    public class EnergyBoltScroll : SpellScroll
    {
        [Constructible]
        public EnergyBoltScroll() : this(1)
        {
        }


        [Constructible]
        public EnergyBoltScroll(int amount) : base(Spells.SpellEntry.EnergyBolt, 0x1F56, amount)
        {
        }

        [Constructible]
        public EnergyBoltScroll(Serial serial) : base(serial)
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