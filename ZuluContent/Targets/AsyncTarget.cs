using System;
using System.Runtime.CompilerServices;
using Server.Network;

namespace Server.Targeting
{
    public record TargetOptions
    {
        public int Range { get; init; }
        public bool AllowGround { get; init; }
        public bool AllowMultis { get; init; } = true;
        public bool AllowNonLocal { get; init; }
        public bool CheckLos { get; init; }
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

    public interface ITargetResponse<out T>
    {
        T Target { get; }
        TargetResponseType Type { get; }
        object InvalidTarget { get; }
        TargetCancelType? CancelType { get; }
        bool HasValue => Target is not null;
    }

    public record TargetResponse<T> : ITargetResponse<T>
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

        public TargetResponse<TCast> Cast<TCast>() => new()
        {
            Target = Target is TCast value ? value : default,
            Type = Type,
            InvalidTarget = Target is not TCast ? Target : InvalidTarget,
            CancelType = CancelType
        };
    }

    public class AsyncTarget<T> : AsyncTarget
    {
        public AsyncTarget(Mobile mobile, TargetOptions opts) : base(mobile, opts) { }
        public override AsyncTarget<T> GetAwaiter() => this;
        public new TargetResponse<T> GetResult() => Response.Cast<T>();
    }

    public class AsyncTarget : Target, INotifyCompletion
    {
        public static long TargetTimeout = (int) TimeSpan.FromMinutes(1.0).TotalMilliseconds;

        protected readonly Mobile Mobile;
        protected Action Continuation;
        public bool IsCompleted { get; private set; }

        protected TargetResponse<object> Response = new()
        {
            Type = TargetResponseType.TargetingFinished
        };

        public AsyncTarget(Mobile mobile, TargetOptions opts) : base(opts.Range, opts.AllowGround, opts.Flags)
        {
            DisallowMultis = !opts.AllowMultis;
            CheckLOS = opts.CheckLos;
            AllowNonlocal = opts.AllowNonLocal;
            Mobile = mobile;
        }

        public virtual AsyncTarget GetAwaiter() => this;
        public TargetResponse<object> GetResult() => Response;

        public void OnCompleted(Action continuation)
        {
            if (!IsCompleted && TimeoutTime < Core.TickCount)
            {
                Continuation = continuation;
                BeginTimeout(Mobile, TargetTimeout);
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
            Continuation?.Invoke();
        }

        protected override void OnTarget(Mobile from, object o)
        {
            base.OnTarget(from, o);
            Response = Response with { Target = o, Type = TargetResponseType.Success };
        }

        protected override void OnTargetNotAccessible(Mobile from, object targeted)
        {
            base.OnTargetNotAccessible(from, targeted);
            Response = Response with { Type = TargetResponseType.NotAccessible, InvalidTarget = targeted };
        }

        protected override void OnTargetInSecureTrade(Mobile from, object targeted)
        {
            base.OnTargetInSecureTrade(from, targeted);
            Response = Response with { Type = TargetResponseType.InSecureTrade, InvalidTarget = targeted };
        }

        protected override void OnNonlocalTarget(Mobile from, object targeted)
        {
            base.OnNonlocalTarget(from, targeted);
            Response = Response with { Type = TargetResponseType.NonLocal, InvalidTarget = targeted };
        }

        protected override void OnCantSeeTarget(Mobile from, object targeted)
        {
            base.OnCantSeeTarget(from, targeted);
            Response = Response with { Type = TargetResponseType.CantSee, InvalidTarget = targeted };
        }

        protected override void OnTargetOutOfLOS(Mobile from, object targeted)
        {
            base.OnTargetOutOfLOS(from, targeted);
            Response = Response with { Type = TargetResponseType.OutOfLos, InvalidTarget = targeted };
        }

        protected override void OnTargetOutOfRange(Mobile from, object targeted)
        {
            base.OnTargetOutOfRange(from, targeted);
            Response = Response with { Type = TargetResponseType.OutOfRange, InvalidTarget = targeted };
        }

        protected override void OnTargetDeleted(Mobile from, object targeted)
        {
            base.OnTargetDeleted(from, targeted);
            Response = Response with { Type = TargetResponseType.Deleted, InvalidTarget = targeted };
        }

        protected override void OnTargetUntargetable(Mobile from, object targeted)
        {
            base.OnTargetUntargetable(from, targeted);
            Response = Response with { Type = TargetResponseType.Untargetable, InvalidTarget = targeted };
        }

        protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
        {
            base.OnTargetCancel(from, cancelType);
            Response = Response with { CancelType = cancelType, Type = TargetResponseType.Cancelled };
        }
    }
}