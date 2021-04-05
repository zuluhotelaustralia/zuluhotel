using System;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Items
{
    public class NightSightPotion : BasePotion
    {
        public override uint PotionStrength { get; set; } = 1;

        [Constructible]
        public NightSightPotion() : base(0xF06, PotionEffect.Nightsight)
        {
        }

        [Constructible]
        public NightSightPotion(Serial serial) : base(serial)
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

        public override void Drink(Mobile from)
        {
            if (!from.CanBuff(from, true, BuffIcon.NightSight, BuffIcon.Shadow))
                return;

            from.TryAddBuff(new NightSight
            {
                Value = LightCycle.DayLevel,
                Duration = TimeSpan.FromSeconds(from.Skills.Magery.Value * 60),
            });
            
            from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            from.PlaySound(0x1E3);

            PlayDrinkEffect(from);

            Consume();
        }
    }
}