using System;
using Scripts.Zulu.Packets;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

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
            if (from.CanBuff(from, icons: BuffIcon.Agility))
            {
                from.TryAddBuff(new StatBuff(StatType.Dex)
                {
                    Title = LabelNumber > 0 ? ClilocList.Translate(LabelNumber, string.Empty, true) : null,
                    Details = new []{ $"Potion Strength: {PotionStrength}"},
                    Value = Utility.Dice(PotionStrength, 2, 5),
                    Duration = TimeSpan.FromSeconds(PotionStrength * 120),
                });
                return true;
            }
            
            return false;
        }

        public override void Drink(Mobile from)
        {
            if (DoAgility(from))
            {
                from.FixedEffect(0x375A, 10, 15);
                from.PlaySound(0x1E7);
                PlayDrinkEffect(from);
                Consume();
            }
        }
    }
}