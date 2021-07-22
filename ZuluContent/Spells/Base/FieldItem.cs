using System;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Spells
{
    [DispellableField]
    public class FieldItem : Item, IDispellable
    {
        private readonly Action<FieldItem> m_OnTick;
        private readonly Func<Mobile, bool?> m_OnMoveOver;
        private readonly InternalTimer m_Timer;

        public bool Dispellable { get; set; } = true;
        
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Caster { get; }

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan Interval => m_Timer.Interval;
        
        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan TimeLeft => m_Timer.TimeLeft;
        
        private FieldItem(
            int itemId, 
            Point3D loc, 
            Mobile caster, 
            TimeSpan duration,
            TimeSpan interval,
            TimeSpan initialDelay,
            Action<FieldItem> onTick,
            Func<Mobile, bool?> onMoveOver
        ) : base(itemId)
        {
            m_OnTick = onTick;
            m_OnMoveOver = onMoveOver;

            Caster = caster;
            Visible = false;
            Movable = false;
            Light = LightType.Circle300;

            if (Deleted)
                return;

            MoveToWorld(loc, Caster.Map);

            m_Timer = new InternalTimer(this, duration, interval, initialDelay);
            m_Timer.Start();
        }

        public static IList<FieldItem> CreateField(
            (int ewItemId, int nsItemId) itemIds,
            Point3D point,
            Mobile caster,
            TimeSpan duration,
            TimeSpan interval,
            Action<FieldItem> onCreate = null,
            Action<FieldItem> onTick = null,
            Func<Mobile, bool?> onMoveOver = null,
            int length = 2
        )
        {
            var dx = caster.Location.X - point.X;
            var dy = caster.Location.Y - point.Y;
            var rx = (dx - dy) * 44;
            var ry = (dx + dy) * 44;

            var eastToWest = rx switch
            {
                >= 0 when ry >= 0 => false,
                >= 0 => true,
                _ => ry >= 0
            };

            var items = new List<FieldItem>();

            for (var i = length * -1; i <= length; ++i)
            {
                var loc = new Point3D(eastToWest ? point.X + i : point.X, eastToWest ? point.Y : point.Y + i, point.Z);
                var canFit = SpellHelper.AdjustField(ref loc, caster.Map, 12, false);

                if (!canFit || !caster.InLOS(loc))
                    continue;

                var item = new FieldItem(
                    eastToWest ? itemIds.ewItemId : itemIds.nsItemId,
                    loc, 
                    caster, 
                    duration,
                    interval,
                    TimeSpan.FromSeconds(Math.Abs(i) * 0.2),
                    onTick,
                    onMoveOver
                );
                items.Add(item);
                onCreate?.Invoke(item);
            }

            return items;
        }

        public FieldItem(Serial serial) : base(serial)
        {
            Timer.DelayCall(Delete);
        }

        public override bool BlocksFit => true;

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }

        public override bool OnMoveOver(Mobile mobile)
        {
            return m_OnMoveOver?.Invoke(mobile) ?? base.OnMoveOver(mobile);
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();
            m_Timer?.Stop();
        }

        private class InternalTimer : Timer
        {
            private readonly FieldItem m_Item;
            private readonly long m_End;

            public TimeSpan TimeLeft => TimeSpan.FromMilliseconds(m_End - Core.TickCount);

            public InternalTimer(FieldItem item, TimeSpan duration, TimeSpan interval, TimeSpan initialDelay)
                : base(initialDelay, interval)
            {
                m_Item = item;
                m_End = Core.TickCount + (int)duration.TotalMilliseconds;
            }

            protected override void OnTick()
            {
                if (Core.TickCount > m_End)
                {
                    m_Item.Delete();
                    return;
                }
                
                if (m_Item.Deleted)
                    return;

                if (!m_Item.Visible)
                {
                    m_Item.Visible = true;
                    m_Item.ProcessDelta();
                    return;
                }
                
                m_Item.m_OnTick?.Invoke(m_Item);
            }
        }

    }
}