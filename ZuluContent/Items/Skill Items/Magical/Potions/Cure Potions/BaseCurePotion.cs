using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace Server.Items
{
    public record CureLevelInfo(Poison Poison, double Chance);

    public abstract class BaseCurePotion : BasePotion
    {
        public abstract CureLevelInfo[] LevelInfo { get; }

        public BaseCurePotion(PotionEffect effect) : base(0xF07, effect)
        {
        }

        public BaseCurePotion(Serial serial) : base(serial)
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

        public void DoCure(Mobile from)
        {
            
            var power = PotionStrength;
            
            if ((from as IEnchanted)?.Enchantments.Get((HealingBonus e) => e.Value) > 0) 
                power += 1;

            if (power > from.Poison.Level && from.CurePoison(from))
            {
                from.SendLocalizedMessage(500231); // You feel cured of poison!

                from.FixedEffect(0x373A, 10, 15);
                from.PlaySound(0x1E0);
            }
            else
            {
                from.SendLocalizedMessage(500232); // That potion was not strong enough to cure your ailment!
            }
        }

        public override void Drink(Mobile from)
        {
            if (from.Poisoned)
            {
                DoCure(from);

                PlayDrinkEffect(from);

                from.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
                from.PlaySound(0x1E0);

                Consume();
            }
            else
            {
                from.SendLocalizedMessage(1042000); // You are not poisoned.
            }
        }
    }
}