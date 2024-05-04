using System.Collections.Generic;

namespace Server.Mobiles;

public class TamedBT<T> where T : IAIState
{
    public static BTNode<T> DoLastOrder()
    {
        return new BTInverter<T>(new BTSucceeder<T>(new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckLastOrderFollowing<T>(),
            new DoChangeLastOrder<T>()
        })));
    }
    
    public static BTNode<T> DoFollow()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Follow),
            new CheckMoveTo<T>(MoveToTargetType.ControlTarget),
            new DoBeginFollow<T>(),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckTargetTooFarAway<T>(MoveToTargetType.ControlTarget),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckTargetTooClose<T>(MoveToTargetType.ControlTarget, 1),
                    new DoClearPath<T>(),
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckPathExists<T>(),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
                new DoMoveToTarget<T>(MoveToTargetType.ControlTarget, true),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new DoCreatePath<T>(MoveToTargetType.ControlTarget),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
            }),
        });
    }
    
    public static BTNode<T> DoCome()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Come),
            new CheckMoveTo<T>(MoveToTargetType.ControlMaster),
            new BTInverter<T>(new CheckTargetTooFarAway<T>(MoveToTargetType.ControlMaster)),
            new DoBeginCome<T>(),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckTargetTooClose<T>(MoveToTargetType.ControlMaster, 1),
                    new DoClearPath<T>(),
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckPathExists<T>(),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
                new DoMoveToTarget<T>(MoveToTargetType.ControlMaster, true),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new DoCreatePath<T>(MoveToTargetType.ControlMaster),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
            }),
        });
    }
    
    public static BTNode<T> DoStop()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Stop),
            new BTInverter<T>(new BTSucceeder<T>(new DoBeginStop<T>()))
        });
    }
    
    public static BTNode<T> DoStay()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Stay),
            new DoBeginStay<T>()
        });
    }
    
    public static BTNode<T> DoGuardOwner()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Guard),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckControlMasterAggressors<T>(),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckMoveTo<T>(MoveToTargetType.ControlMaster),
                    new BTInverter<T>(new CheckTargetTooFarAway<T>(MoveToTargetType.ControlMaster)),
                    new DoBeginCome<T>(),
                    new BTSelector<T>(new List<BTNode<T>>
                    {
                        new BTSequence<T>(new List<BTNode<T>>
                        {
                            new CheckTargetTooClose<T>(MoveToTargetType.ControlMaster, 1),
                            new DoClearPath<T>(),
                        }),
                        new BTSequence<T>(new List<BTNode<T>>
                        {
                            new CheckPathExists<T>(),
                            new DoFollowPath<T>(),
                            new DoClearPath<T>()
                        }),
                        new DoMoveToTarget<T>(MoveToTargetType.ControlMaster, true),
                        new BTSequence<T>(new List<BTNode<T>>
                        {
                            new DoCreatePath<T>(MoveToTargetType.ControlMaster),
                            new DoFollowPath<T>(),
                            new DoClearPath<T>()
                        }),
                    }),
                })
            }),
        });
    }
    
    public static BTNode<T> DoTransfer()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Transfer),
            new DoBeginTransfer<T>()
        });
    }
    
    public static BTNode<T> DoRelease()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Release),
            new DoBeginRelease<T>()
        });
    }
    
    public static BTNode<T> DoFriend()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Friend),
            new DoBeginFriend<T>()
        });
    }
    
    public static BTNode<T> DoUnFriend()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Unfriend),
            new DoBeginUnFriend<T>()
        });
    }
    
    public static BTNode<T> DoDrop()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Drop),
            new DoBeginDrop<T>()
        });
    }
}