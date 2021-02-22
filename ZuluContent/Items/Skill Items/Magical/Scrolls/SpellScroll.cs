using Server.Spells;

namespace Server.Items
{
    public class SpellScroll : Item
    {
        [Constructible]
        public SpellScroll(Serial serial) : base(serial)
        {
        }


        [Constructible]
        public SpellScroll(SpellEntry spellEntry, int itemId) : this(spellEntry, itemId, 1)
        {
        }


        [Constructible]
        public SpellScroll(SpellEntry spellEntry, int itemId, int amount) : base(itemId)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;

            SpellEntry = spellEntry;
        }

        public SpellEntry SpellEntry { get; private set; }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version

            writer.Write((int) SpellEntry);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            var version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    SpellEntry = (SpellEntry) reader.ReadInt();

                    break;
                }
            }
        }
        
        public override void OnSingleClick(Mobile from)
        {
            LabelTo(from, $"{SpellRegistry.GetInfo(SpellEntry).Name} scroll");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            var spell = SpellRegistry.Create(SpellEntry, from, this);

            if (spell != null)
                spell.Cast();
            else
                from.SendLocalizedMessage(502345); // This spell has been temporarily disabled.
        }
    }
}