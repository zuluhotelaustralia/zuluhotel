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
        public T Target { get; init; }
        public TargetResponseType Type { get; init; }
        public object InvalidTarget { get; init; }
        public TargetCancelType? CancelType { get; init; }

        public void Deconstruct(out T target) =>
            target = Target;

        public void Deconstruct(out T target, out TargetResponseType type) =>
            (target, type) = (Target, Type);

        public void Deconstruct(out T target, out TargetResponseType type, out object invalidTarget) =>
            (target, type, invalidTarget) = (Target, Type, InvalidTarget);

        public void Deconstruct(
            out T target,
            out TargetResponseType type,
            out object invalidTarget,
            out TargetCancelType? cancelType
        ) =>
            (target, type, invalidTarget, cancelType) = (Target, Type, InvalidTarget, CancelType);
    }


    public class AsyncTarget<T> : Target, INotifyCompletion
    {
        private readonly Mobile m_Mobile;
        private Action m_Continuation;
        public bool IsCompleted { get; private set; }

        private TargetResponse<T> m_Response = new()
        {
            Type = TargetResponseType.TargetingFinished
        };

        public AsyncTarget(Mobile mobile, TargetOptions opts) : base(opts.Range, opts.AllowGround, opts.Flags)
        {
            m_Mobile = mobile;
        }

        public AsyncTarget<T> GetAwaiter() => this;
        public TargetResponse<T> GetResult() => m_Response;

        public void OnCompleted(Action continuation)
        {
            if (!IsCompleted && TimeoutTime < DateTime.Now)
            {
                m_Continuation = continuation;
                BeginTimeout(m_Mobile, TimeSpan.FromSeconds(30.0));
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
            m_Response = m_Response with { Target = o is T value ? value : default, Type = TargetResponseType.Success };
        }

        protected override void OnTargetNotAccessible(Mobile from, object targeted)
        {
            base.OnTargetNotAccessible(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.NotAccessible, InvalidTarget = targeted };
        }

        protected override void OnTargetInSecureTrade(Mobile from, object targeted)
        {
            base.OnTargetInSecureTrade(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.InSecureTrade, InvalidTarget = targeted };
        }

        protected override void OnNonlocalTarget(Mobile from, object targeted)
        {
            base.OnNonlocalTarget(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.NonLocal, InvalidTarget = targeted };
        }

        protected override void OnCantSeeTarget(Mobile from, object targeted)
        {
            base.OnCantSeeTarget(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.CantSee, InvalidTarget = targeted };
        }

        protected override void OnTargetOutOfLOS(Mobile from, object targeted)
        {
            base.OnTargetOutOfLOS(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.OutOfLos, InvalidTarget = targeted };
        }

        protected override void OnTargetOutOfRange(Mobile from, object targeted)
        {
            base.OnTargetOutOfRange(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.OutOfRange, InvalidTarget = targeted };
        }

        protected override void OnTargetDeleted(Mobile from, object targeted)
        {
            base.OnTargetDeleted(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.Deleted, InvalidTarget = targeted };
        }

        protected override void OnTargetUntargetable(Mobile from, object targeted)
        {
            base.OnTargetUntargetable(from, targeted);
            m_Response = m_Response with { Type = TargetResponseType.Untargetable, InvalidTarget = targeted };
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            base.OnTargetCancel(from, cancelType);
            m_Response = m_Response with { CancelType = cancelType, Type = TargetResponseType.Cancelled };
        }
    }
}