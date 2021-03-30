using System;
using System.Collections.Generic;
using Server;
using Server.Spells;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs
{
    public class WraithForm : Polymorph
    {
        private DateTime m_Start;
        private TimeSpan m_Duration;
        public override TimeSpan Duration => m_Duration;
        public override DateTime Start => m_Start;
        public override string Description => $"{m_Stacks.Count} stack{(m_Stacks.Count > 1 ? "s" : "")}";

        private readonly List<WraithFormStack> m_Stacks = new();
        private WraithFormTimer m_Timer;
        private Mobile m_Caster;

        public override void OnBuffAdded(Mobile parent)
        {
            m_Start = DateTime.UtcNow;
            m_Caster = parent;
            m_Timer?.Stop();
            m_Timer = new WraithFormTimer(this);
            m_Timer.Start();
            base.OnBuffAdded(parent);
        }

        public override void OnBuffRemoved(Mobile parent)
        {
            parent.FixedParticles(0x370A, 10, 16, 5013, EffectLayer.LeftFoot);
            parent.PlaySound(0x20F);
            m_Timer?.Stop();
            base.OnBuffRemoved(parent);
        }

        public void AddStack(TimeSpan duration, TimeSpan interval, Action onTick)
        {
            m_Stacks.Add(new WraithFormStack
            {
                NextTick = Core.TickCount + (int)interval.TotalMilliseconds,
                ExpireAt = Core.TickCount + (int)duration.TotalMilliseconds,
                Interval = interval,
                OnTick = onTick
            });
            
            if(duration >= m_Duration)
                m_Duration = duration;
            
            m_Start = DateTime.UtcNow;
            (m_Caster as IBuffable)?.BuffManager.ResendBuff(this);
        }

        private void ExpireStacks()
        {
            var removed = m_Stacks.RemoveAll(s => s.ExpireAt <= Core.TickCount);

            if (removed > 0) 
                (m_Caster as IBuffable)?.BuffManager.ResendBuff(this);
        }
        
        private record WraithFormStack
        {
            public long NextTick { get; set; }
            public long ExpireAt { get; init; }
            public TimeSpan Interval { get; init; }
            public Action OnTick { get; init; }
        }
        
        public class WraithFormTimer : Timer
        {
            private static readonly TimeSpan TickInterval = TimeSpan.FromMilliseconds(250);
            
            public WraithForm Buff { get; }

            public WraithFormTimer(WraithForm buff) : base(TimeSpan.Zero, TickInterval)
            {
                Buff = buff;
            }

            protected override void OnTick()
            {
                Buff.ExpireStacks();
                
                foreach (var stack in Buff.m_Stacks)
                {
                    if (Core.TickCount > stack.NextTick)
                    {
                        stack.OnTick?.Invoke();
                        stack.NextTick = Core.TickCount + (int)stack.Interval.TotalMilliseconds;
                    }
                }
            }
        } 
    }
}