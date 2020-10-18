using System;
using Server.Spells;

namespace Server.Items
{
    public class GiantSpikeTrap : BaseTrap
    {
        [Constructible]
        public GiantSpikeTrap() : base(1)
        {
        }

        public override bool PassivelyTriggered
        {
            get { return true; }
        }

        public override TimeSpan PassiveTriggerDelay
        {
            get { return TimeSpan.Zero; }
        }

        public override int PassiveTriggerRange
        {
            get { return 3; }
        }

        public override TimeSpan ResetDelay
        {
            get { return TimeSpan.FromSeconds(0.0); }
        }

        public override void OnTrigger(Mobile from)
        {
            if (from.AccessLevel > AccessLevel.Player)
                return;

            Effects.SendLocationEffect(Location, Map, 0x1D99, 48, 2, GetEffectHue(), 0);

            if (from.Alive && CheckRange(from.Location, 0))
            {
                SpellHelper.Damage(Utility.Dice(10, 7, 0), from, from, null, TimeSpan.FromTicks(1));
            }
        }

        [Constructible]
        public GiantSpikeTrap(Serial serial) : base(serial)
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
    }
}