using Server.Spells;

namespace Server.Items
{
    public abstract class CustomSpellScroll : SpellScroll
    {
        [Constructible]
        public CustomSpellScroll(SpellEntry spellEntry, int itemId, int amount, int hue) : base(spellEntry, itemId,
            amount)
        {
            Hue = hue;
            Name = SpellRegistry.GetInfo(spellEntry).Name;
        }

        [Constructible]
        public CustomSpellScroll(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (!string.IsNullOrEmpty(Name))
                LabelTo(from, Name);
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