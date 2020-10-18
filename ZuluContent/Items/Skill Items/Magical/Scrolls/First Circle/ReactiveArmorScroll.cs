namespace Server.Items
{
    public class ReactiveArmorScroll : SpellScroll
    {
        [Constructible]
        public ReactiveArmorScroll() : this(1)
        {
        }


        [Constructible]
        public ReactiveArmorScroll(int amount) : base(Spells.SpellEntry.ReactiveArmor, 0x1F2D, amount)
        {
        }

        [Constructible]
        public ReactiveArmorScroll(Serial ser) : base(ser)
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