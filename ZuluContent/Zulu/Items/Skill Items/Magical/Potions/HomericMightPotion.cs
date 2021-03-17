using System;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

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

        public override string DefaultName { get; } = "Greater Homeric Might potion";
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
            if (from.CanBuff(from, BuffIcon.Bless))
            {
                from.TryAddBuff(new StatBuff(StatType.All)
                {
                    Title = DefaultName,
                    Details = new []{ $"Potion Strength: {PotionStrength}"},
                    Value = Utility.Dice(PotionStrength, 3, 0),
                    Duration = TimeSpan.FromSeconds(PotionStrength * 15),
                });
                return true;
            }
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