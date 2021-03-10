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

        public bool AddBuff(IBuff b)
        {
            m_Timer ??= new BuffTimer(this);
            m_Timer.Start();

            if (m_BuffTable.TryAdd(b.Icon, b))
            {
                b.OnBuffAdded(m_Parent);
                SendAddBuffPacket(m_Parent.NetState, m_Parent.Serial, b);
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

                SendRemoveBuffPacket(m_Parent.NetState, m_Parent.Serial, b);
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
                .Where(b => !b.RetainThroughDeath)
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

        public bool CanBuffWithNotifyOnFail<T>(Mobile caster) where T : IBuff
        {
            if (HasBuff<T>())
            {
                caster.SendLocalizedMessage(caster == m_Parent 
                        ? 502173 // You are already under a similar effect.
                        : 1156094 // Your target is already under the effect of this ability.
                );
                return false;
            }

            return true;
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
            foreach (var buff in m_BuffTable.Values.Where(b => b.Start + b.Duration < DateTime.UtcNow))
                RemoveBuff(buff);
        }

        public void ResendBuff(IBuff buff)
        {
            SendRemoveBuffPacket(m_Parent.NetState, m_Parent.Serial, buff);
            SendAddBuffPacket(m_Parent.NetState, m_Parent.Serial, buff);
        }

        #region Packets
        private static void SendAddBuffPacket(NetState ns, Serial m, IBuff buff) => SendAddBuffPacket(
            ns,
            m,
            buff.Icon,
            buff.TitleCliloc,
            buff.SecondaryCliloc,
            buff.Args,
            buff.Start != DateTime.MinValue ? buff.Start + buff.Duration - DateTime.UtcNow : TimeSpan.Zero
        );

        private static void SendAddBuffPacket(
            NetState ns,
            Serial mob,
            BuffIcon iconId,
            int titleCliloc,
            int secondaryCliloc,
            TextDefinition args,
            TimeSpan ts
        )
        {
            if (ns == null)
                return;

            var hasArgs = args != null;
            var length = hasArgs ? args.ToString().Length * 2 + 52 : 46;
            var writer = new SpanWriter(stackalloc byte[length]);
            writer.Write((byte) 0xDF); // Packet ID
            writer.Write((ushort) length);
            writer.Write(mob);
            writer.Write((short) iconId);
            writer.Write((short) 0x1); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);

            writer.Write((short) iconId);
            writer.Write((short) 0x1); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);
            writer.Write((short) (ts <= TimeSpan.Zero ? 0 : ts.TotalSeconds));
            writer.Clear(3);
            writer.Write(titleCliloc);
            writer.Write(secondaryCliloc);

            if (hasArgs)
            {
                writer.Write(0);
                writer.Write((short) 0x1);
                writer.Write((ushort) 0);
                writer.WriteLE('\t');
                writer.WriteLittleUniNull(args);
                writer.Write((short) 0x1);
                writer.Write((ushort) 0);
            }
            else
            {
                writer.Clear(10);
            }

            ns.Send(writer.Span);
        }

        public static void SendRemoveBuffPacket(NetState ns, Serial mob, IBuff buff) =>
            SendRemoveBuffPacket(ns, mob, buff.Icon);

        private static void SendRemoveBuffPacket(NetState ns, Serial mob, BuffIcon iconId)
        {
            if (ns == null)
                return;

            var writer = new SpanWriter(stackalloc byte[15]);
            writer.Write((byte) 0xDF); // Packet ID
            writer.Write((ushort) 15);
            writer.Write(mob);
            writer.Write((short) iconId);
            writer.Write((short) 0x0); // command (0 = remove, 1 = add, 2 = data)
            writer.Write(0);

            ns.Send(writer.Span);
        }
        #endregion

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