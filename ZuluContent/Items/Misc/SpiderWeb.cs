using System;
using Scripts.Zulu.Utilities;

namespace Server.Items
{
    public class SpiderWeb : Item
    {
        private readonly InternalTimer m_Timer;

        public override bool HandlesOnMovement => true;

        public override bool Decays => true;

        public override string DefaultName => "Sticky Web";

        public override bool OnMoveOver(Mobile mobile)
        {
            Stick(mobile);

            return true;
        }

        public static void Stick(Mobile mobile)
        {
            mobile.Paralyze(TimeSpan.FromSeconds(10));
            mobile.SendFailureMessage("You are trapped in a spider web!");
        }

        [Constructible]
        public SpiderWeb(Point3D loc, Map map, TimeSpan duration) : base(0xEE4)
        {
            Movable = false;
            MoveToWorld(loc, map);

            m_Timer = new InternalTimer(this, duration, TimeSpan.Zero, TimeSpan.Zero);
            m_Timer.Start();
        }

        [Constructible]
        public SpiderWeb(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        private class InternalTimer : Timer
        {
            private readonly SpiderWeb m_Item;
            private readonly long m_End;

            public InternalTimer(SpiderWeb item, TimeSpan duration, TimeSpan interval, TimeSpan initialDelay)
                : base(initialDelay, interval)
            {
                m_Item = item;
                m_End = Core.TickCount + (int) duration.TotalMilliseconds;
                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (Core.TickCount > m_End)
                {
                    m_Item.Delete();
                }
            }
        }
    }
}