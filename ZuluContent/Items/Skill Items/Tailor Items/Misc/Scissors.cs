using Scripts.Zulu.Utilities;
using Server.Targeting;

namespace Server.Items
{
    public interface IScissorable
    {
        bool Scissor(Mobile from, Scissors scissors);
    }

    [FlipableAttribute(0xf9f, 0xf9e)]
    public class Scissors : Item
    {
        [Constructible]
        public Scissors() : base(0xF9F)
        {
            Weight = 1.0;
        }

        [Constructible]
        public Scissors(Serial serial) : base(serial)
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

        public override void OnDoubleClick(Mobile from)
        {
            from.SendSuccessMessage(502434); // What should I use these scissors on?

            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private readonly Scissors m_Item;

            public InternalTarget(Scissors item) : base(2, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted)
                    return;
                
                if (targeted is Item item && !item.IsStandardLoot() && item is not DeathRobe)
                {
                    from.SendFailureMessage(502440); // Scissors can not be used on that to produce anything.
                }
                else if (targeted is IScissorable scissorable)
                {
                    if (scissorable.Scissor(from, m_Item))
                        from.PlaySound(0x248);
                }
                else
                {
                    from.SendFailureMessage(502440); // Scissors can not be used on that to produce anything.
                }
            }
        }
    }
}