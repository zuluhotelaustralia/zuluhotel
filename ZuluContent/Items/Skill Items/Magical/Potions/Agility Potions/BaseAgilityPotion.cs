using System;
using Server.Spells;

namespace Server.Items
{
    public abstract class BaseAgilityPotion : BasePotion
    {
        public abstract int DexOffset { get; }
        public abstract TimeSpan Duration { get; }

        public BaseAgilityPotion(PotionEffect effect) : base(0xF08, effect)
        {
        }

        public BaseAgilityPotion(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public bool DoAgility(Mobile from)
        {
            var mod = Utility.Dice(PotionStrength, 2, 5);
            var duration = TimeSpan.FromSeconds(PotionStrength * 120);

            if (SpellHelper.AddStatBonus(from, from, StatType.Dex, mod, duration))
            {
                from.FixedEffect(0x375A, 10, 15);
                from.PlaySound(0x1E7);
                return true;
            }

            from.SendLocalizedMessage(502173); // You are already under a similar effect.
            return false;
        }

        public override void Drink(Mobile from)
        {
            if (DoAgility(from))
            {
                PlayDrinkEffect(from);
                Consume();
            }
        }
    }
}