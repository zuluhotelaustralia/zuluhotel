using System;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Items
{
    public class PhandelsFineIntellectPotion : BasePhandelsPotion
    {
        public override string DefaultName { get; } = "Phandel's Fine Intellect potion";
        public override uint PotionStrength { get; set; } = 3;
        public PhandelsFineIntellectPotion() : base(PotionEffect.PhandelsFineIntellect) { }
        public PhandelsFineIntellectPotion(Serial serial) : base(serial) { }
        
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
    
    public class PhandelsFabulousIntellectPotion : BasePhandelsPotion
    {
        public override uint PotionStrength { get; set; } = 5;

        public override string DefaultName { get; } = "Phandel's Fabulous Intellect potion";
        public PhandelsFabulousIntellectPotion() : base(PotionEffect.PhandelsFabulousIntellect) { }
        public PhandelsFabulousIntellectPotion(Serial serial) : base(serial) { }
        
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
    
    public class PhandelsFantasticIntellectPotion : BasePhandelsPotion
    {
        public override string DefaultName { get; } = "Phandel's Fantastic Intellect potion";
        public override uint PotionStrength { get; set; } = 7;
        public PhandelsFantasticIntellectPotion() : base(PotionEffect.PhandelsFantasticIntellect) { }
        public PhandelsFantasticIntellectPotion(Serial serial) : base(serial) { }
        
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
    
    public abstract class BasePhandelsPotion : BasePotion
    {
        public BasePhandelsPotion(PotionEffect effect) : base(0xE29, effect)
        {
        }

        public BasePhandelsPotion(Serial serial) : base(serial)
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
            if (from.CanBuff(from, icons: BuffIcon.Cunning))
            {
                from.TryAddBuff(new StatBuff(StatType.Int)
                {
                    Title = DefaultName,
                    Details = new []{ $"Potion Strength: {PotionStrength}"},
                    Value = Utility.Dice(PotionStrength, 10, 0),
                    Duration = TimeSpan.FromSeconds(PotionStrength * 20),
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
                from.PlaySound(0x1EB);
                PlayDrinkEffect(from);
                Consume();
            }
        }
    }
}