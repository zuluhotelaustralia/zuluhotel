using Server.Spells;

namespace Server.Items
{
    public class CallLightningScroll : EarthSpellScroll
    {
        [Constructible]
        public CallLightningScroll() : this(1)
        {
        }


        [Constructible]
        public CallLightningScroll(int amount) : base(SpellEntry.CallLightning, 0x1F32, amount)
        {
        }

        [Constructible]
        public CallLightningScroll(Serial serial) : base(serial)
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