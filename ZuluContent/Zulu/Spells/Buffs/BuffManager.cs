using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using Server.Network;

namespace Server.Spells
{
    // <body><basefont SIZE=5 FACE=UO_Gargish_20pt>Elemental Protections:</basefont>
    // <basefont SIZE=5 FACE=UO_Gargish_16pt COLOR=#6A6A6A>+1 Air Protection</basefont>
    // <basefont SIZE=5 FACE=UO_Gargish_16pt COLOR=#834118>+2 Earth Protection</basefont>
    // <basefont SIZE=5 FACE=UO_Gargish_16pt COLOR=#DE4f45>+3 Fire Protection</basefont>
    // <basefont SIZE=5 FACE=UO_Gargish_16pt COLOR=#2083CD>+4 Necro Protection</basefont>
    // <basefont SIZE=5 FACE=UO_Gargish_16pt COLOR=#395A52>+5 Poison Protection</basefont></body>

    public class BuffManager
    {
        private BuffTimer m_Timer;
        private readonly Mobile m_Parent;
        private readonly Dictionary<BuffIcon, IBuff> m_BuffTable = new();

        public BuffManager(Mobile parent) => m_Parent = parent;

        public static void Initialize()
        {
            EventSink.ClientVersionReceived += (ns, _) =>
            {
                if (ns.Mobile is IBuffable buffable)
                    Timer.DelayCall(TimeSpan.Zero, buffable.BuffManager.ResendAllBuffs);
            };

            EventSink.PlayerDeath += mobile =>
            {
                if (mobile is IBuffable buffable)
                    Timer.DelayCall(TimeSpan.Zero, buffable.BuffManager.RemoveBuffsOnDeath);
            };
        }

        public IEnumerable<IBuff> Values => m_BuffTable.Values;

        public bool TryAddBuff(IBuff b)
        {
            m_Timer ??= new BuffTimer(this);
            m_Timer.Start();

            if (m_BuffTable.TryAdd(b.Icon, b))
            {
                b.OnBuffAdded(m_Parent);
                b.SendAddBuffPacket(m_Parent);
                return true;
            }

            return false;
        }

        public bool RemoveBuff(BuffIcon icon) => m_BuffTable.Remove(icon, out _);

        public bool RemoveBuff(IBuff b)
        {
            if (RemoveBuff(b.Icon))
            {
                b.OnBuffRemoved(m_Parent);

                if (m_BuffTable.Count == 0)
                    m_Timer.Stop();

                b.SendRemoveBuffPacket(m_Parent);
            }

            return false;
        }

        public bool RemoveBuff<T>() where T : IBuff
        {
            if (m_BuffTable.Values.FirstOrDefault(b => b is T) is { } buff)
                return RemoveBuff(buff);

            return false;
        }

        public void DispelBuffs()
        {
            m_BuffTable.Values
                .Where(b => b.Dispellable)
                .ToList()
                .ForEach(b => RemoveBuff(b));
        }

        private void RemoveBuffsOnDeath()
        {
            m_BuffTable.Values
                .Where(b => b.ExpireOnDeath)
                .ToList()
                .ForEach(b => RemoveBuff(b));
        }

        public bool HasBuff(BuffIcon icon)
        {
            return m_BuffTable.ContainsKey(icon);
        }

        public bool HasBuff<T>() where T : IBuff
        {
            return m_BuffTable.Values.Any(b => b is T);
        }
        
        public void ResendAllBuffs()
        {
            if (m_BuffTable.Count == 0)
                return;

            foreach (var buff in m_BuffTable.Values)
                ResendBuff(buff);
        }

        private void ExpireBuffs()
        {
            m_BuffTable.Values
                .Where(b => b.Start != DateTime.MinValue && b.Start + b.Duration < DateTime.UtcNow)
                .ToList()
                .ForEach(b => RemoveBuff(b));
        }

        public void ResendBuff(IBuff buff)
        {
            buff.SendRemoveBuffPacket(m_Parent);
            buff.SendAddBuffPacket(m_Parent);
        }

        #region Internal Timer
        private class BuffTimer : Timer
        {
            private readonly BuffManager m_Manager;

            public BuffTimer(BuffManager m) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0)) =>
                (m_Manager, Priority) = (m, TimerPriority.OneSecond);

            protected override void OnTick() => m_Manager.ExpireBuffs();
        }
        #endregion
    }
}