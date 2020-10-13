using System.Collections.Generic;
using Server.Spells;

namespace Server.Items
{
    public class SpellScroll : Item
    {
        public int SpellId { get; private set; }

        [Constructible]
        public SpellScroll(Serial serial) : base(serial)
        {
        }


        [Constructible]
        public SpellScroll(int spellId, int itemId) : this(spellId, itemId, 1)
        {
        }


        [Constructible]
        public SpellScroll(int spellId, int itemId, int amount) : base(itemId)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;

            SpellId = spellId;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version

            writer.Write((int) SpellId);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                {
                    SpellId = reader.ReadInt();

                    break;
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            var spell = SpellRegistry.Create(SpellId, from, this);

            if (spell != null)
                spell.Cast();
            else
                from.SendLocalizedMessage(502345); // This spell has been temporarily disabled.
        }
    }
}