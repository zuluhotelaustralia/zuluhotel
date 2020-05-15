using System;
using Server;

namespace Server.Items
{
    public abstract class BaseManaPotion : BasePotion
    {
        public abstract double Mana { get; }

        public BaseManaPotion(PotionEffect effect) : base(0xF0B, effect)
        {
        }

        public BaseManaPotion(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Drink(Mobile from)
        {
            if (from.Mana < from.ManaMax)
            {
                from.Mana += Scale(from, (int)(Mana * from.ManaMax));

                BasePotion.PlayDrinkEffect(from);

                if (!Engines.ConPVP.DuelContext.IsFreeConsume(from))
                    this.Consume();
            }
            else
            {
                from.SendMessage("You decide against drinking this potion, as you are already at full mana.");
            }
        }
    }
}
