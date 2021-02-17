using System;
using Server.Spells;

namespace Server.Items
{
    public class HomericMightPotion : BaseHomericMightPotion
    {
        public override int Hue { get; set; } = 133;

        public override string DefaultName { get; } = "Homeric Might potion";
        public override uint PotionStrength { get; set; } = 4;
        public HomericMightPotion() : base(PotionEffect.HomericMight) { }
        public HomericMightPotion(Serial serial) : base(serial) { }
        
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
    }
    
    public class GreaterHomericMightPotion : BaseHomericMightPotion
    {
        public override int Hue { get; set; } = 0;

        public override string DefaultName { get; } = "a greater Homeric Might potion";
        public override uint PotionStrength { get; set; } = 9;
        public GreaterHomericMightPotion() : base(PotionEffect.GreaterHomericMight) { }
        public GreaterHomericMightPotion(Serial serial) : base(serial) { }


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
    }
    public abstract class BaseHomericMightPotion : BasePotion
    {
        public BaseHomericMightPotion(PotionEffect effect) : base(0xEFB, effect)
        {
        }

        public BaseHomericMightPotion(Serial serial) : base(serial)
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

        public bool Buff(Mobile from)
        {
            var mod = Utility.Dice(PotionStrength, 3, 0);
            var duration = TimeSpan.FromSeconds(PotionStrength * 15);

            if (SpellHelper.AddStatBonus(from, from, StatType.Str, mod, duration))
            {
                SpellHelper.AddStatBonus(from, from, StatType.Int, mod, duration);
                SpellHelper.AddStatBonus(from, from, StatType.Dex, mod, duration);
                return true;
            }

            from.SendLocalizedMessage(502173); // You are already under a similar effect.
            return false;
        }

        public override void Drink(Mobile from)
        {
            if (Buff(from))
            {
                from.FixedEffect(0x3739, 10, 15);
                from.PlaySound(0x1EA);
                PlayDrinkEffect(from);
                Consume();
            }
        }
    }
}