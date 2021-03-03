using System;
using System.Runtime.CompilerServices;
using Server.Network;

namespace Server.Targeting
{
    public record TargetOptions
    {
        public int Range { get; init; }
        public bool AllowGround { get; init; }
        public TargetFlags Flags { get; init; }
    }

    public enum TargetResponseType
    {
        TargetingFinished,
        Success,
        NotAccessible,
        InSecureTrade,
        NonLocal,
        CantSee,
        OutOfLos,
        OutOfRange,
        Deleted,
        Untargetable,
        Cancelled
    }

    public record TargetResponse<T>
    {
        public TargetResponseType Type { get; set; } = TargetResponseType.TargetingFinished;
        public T Target { get; set; }
        public object InvalidTarget { get; set; }
        public TargetCancelType? CancelType { get; set; }
    }
    
    public class AsyncTarget<T> : Target, INotifyCompletion
    {
        private readonly Mobile m_Mobile;
        private Action m_Continuation;
        public bool IsCompleted { get; private set; }

        private readonly TargetResponse<T> m_Response = new();

        public AsyncTarget(Mobile mobile, TargetOptions opts) : base(opts.Range, opts.AllowGround, opts.Flags)
        {
            m_Mobile = mobile;
        }

        public AsyncTarget<T> GetAwaiter() => this;
        public TargetResponse<T> GetResult() => m_Response;

        public override void SendTargetTo(NetState ns)
        {
            base.SendTargetTo(ns);
            BeginTimeout(m_Mobile, TimeSpan.FromSeconds(30.0));
        }

        public void OnCompleted(Action continuation)
        {
            if (!IsCompleted && TimeoutTime < DateTime.Now)
            {
                m_Continuation = continuation;
            }
            else
            {
                continuation();
            }
        }

        protected override void OnTargetFinish(Mobile from)
        {
            base.OnTargetFinish(from);
            IsCompleted = true;
            m_Continuation?.Invoke();
        }
        
        protected override void OnTarget(Mobile from, object o)
        {
            base.OnTarget(from, o);
            m_Response.Target = o is T value ? value : default;
            m_Response.Type = TargetResponseType.Success;
        }
        
        protected override void OnTargetNotAccessible(Mobile from, object targeted)
        {
            base.OnTargetNotAccessible(from, targeted);
            m_Response.Type = TargetResponseType.NotAccessible;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetInSecureTrade(Mobile from, object targeted)
        {
            base.OnTargetInSecureTrade(from, targeted);
            m_Response.Type = TargetResponseType.InSecureTrade;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnNonlocalTarget(Mobile from, object targeted)
        {
            base.OnNonlocalTarget(from, targeted);
            m_Response.Type = TargetResponseType.NonLocal;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnCantSeeTarget(Mobile from, object targeted)
        {
            base.OnCantSeeTarget(from, targeted);
            m_Response.Type = TargetResponseType.CantSee;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetOutOfLOS(Mobile from, object targeted)
        {
            base.OnTargetOutOfLOS(from, targeted);
            m_Response.Type = TargetResponseType.OutOfLos;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetOutOfRange(Mobile from, object targeted)
        {
            base.OnTargetOutOfRange(from, targeted);
            m_Response.Type = TargetResponseType.OutOfRange;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetDeleted(Mobile from, object targeted)
        {
            base.OnTargetDeleted(from, targeted);
            m_Response.Type = TargetResponseType.Deleted;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetUntargetable(Mobile from, object targeted)
        {
            base.OnTargetUntargetable(from, targeted);
            m_Response.Type = TargetResponseType.Untargetable;
            m_Response.InvalidTarget = targeted;
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            base.OnTargetCancel(from, cancelType);
            m_Response.CancelType = cancelType;
            m_Response.Type = TargetResponseType.Cancelled;
        }
    }
}