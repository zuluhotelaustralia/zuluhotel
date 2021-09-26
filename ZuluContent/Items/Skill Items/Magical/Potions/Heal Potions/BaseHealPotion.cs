using System;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public abstract class BaseHealPotion : BasePotion
    {
        public abstract int MinHeal { get; }
        public abstract int MaxHeal { get; }
        public abstract double Delay { get; }

        public BaseHealPotion(PotionEffect effect) : base(0xF0C, effect)
        {
        }

        public BaseHealPotion(Serial serial) : base(serial)
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

        public void DoHeal(Mobile from)
        {
            var heal = (double)Utility.Dice(12, PotionStrength + 1, 0);

            from.FireHook(h => h.OnHeal(from, from, this, ref heal));
            from.Heal((int)heal);
        }

        public override void Drink(Mobile from)
        {
            if (from.Hits < from.HitsMax)
            {
                if (from.Poisoned)
                {
                    // You can not heal yourself in your current state.
                    from.LocalOverheadMessage(MessageType.Regular, 0x22, 1005000); 
                }
                else
                {
                    if (from.BeginAction(typeof(BaseHealPotion)))
                    {
                        DoHeal(from);

                        PlayDrinkEffect(from);

                        Consume();

                        Timer.StartTimer(TimeSpan.FromSeconds(Delay), () => ReleaseHealLock(from));
                    }
                    else
                    {
                        from.LocalOverheadMessage(MessageType.Regular, 0x22,
                            500235); // You must wait 10 seconds before using another healing potion.
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(
                    1049547); // You decide against drinking this potion, as you are already at full health.
            }
        }

        private static void ReleaseHealLock(object state)
        {
            ((Mobile) state).EndAction(typeof(BaseHealPotion));
        }
    }
}