using System.Collections.Generic;

namespace Server.Mobiles;

public class RangedBT<T> : BaseSingleton<RangedBT<T>>, IBTTree<T> where T : IAIState
{
    public BTNode<T> Root { get; }

    private RangedBT()
    {
        Root = SetupTree();
    }
    
    public static BTNode<T> DoCombat()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckCombatantExists<T>(),
                new CheckCombat<T>(),
            }),
            new DoEndCombat<T>(),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckCombatantExists<T>(),
                new BTInverter<T>(new BTSucceeder<T>(new DoBeginGuarding<T>()))
            }),
            /*new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckCombatantInRange<T>(),
                new DoAcquireNewCombatantInRange<T>(),
                new CheckCombatantInRange<T>(3),
                new BTInverter<T>(new BTSucceeder<T>(new DoRemoveCombatant<T>()))
            }),*/
            new DoBeginCombat<T>(),
            new CheckMoveTo<T>(),
            new CheckLastMoveTime<T>(),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckCombatantInAttackRange<T>(),
                    new DoClearPath<T>()
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckPathExists<T>(),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
                new DoMoveToTarget<T>(MoveToTargetType.Combatant, true),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new DoCreatePath<T>(),
                    new DoFollowPath<T>(),
                    new DoClearPath<T>()
                }),
                new DoAcquireNewCombatantInRange<T>(),
                new BTInverter<T>(new CheckTargetTooFarAway<T>()),
                new BTInverter<T>(new BTSucceeder<T>(new DoBeginGuarding<T>()))
            }),
        });
    }
    
    public static BTNode<T> DoGuard()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new BTSelector<T>(new List<BTNode<T>>
            {
                new CheckGuarding<T>(),
                new BTInverter<T>(new BTSucceeder<T>(new DoEndGuarding<T>()))
            }),
            new BTSucceeder<T>(new DoAcquireNewCombatantInRange<T>()),
            new DoProcessGuarding<T>(),
            new BTInverter<T>(new BTSucceeder<T>(new DoEndGuarding<T>()))
        });
    }
    
    public static BTNode<T> DoAttack()
    {
        return new BTSequence<T>(new List<BTNode<T>>
        {
            new CheckControlOrder<T>(OrderType.Attack),
            new DoBeginAttack<T>(),
            new BTSelector<T>(new List<BTNode<T>>
            {
                DoCombat(),
                TamedBT<T>.DoLastOrder()
            }),
        });
    }

    public static BTNode<T> DoObey()
    {
        return new BTSelector<T>(new List<BTNode<T>>
        {
            DoCombat(),
            DoAttack(),
            TamedBT<T>.DoFollow(),
            TamedBT<T>.DoCome(),
            TamedBT<T>.DoStop(),
            TamedBT<T>.DoStay(),
            TamedBT<T>.DoGuardOwner(),
            TamedBT<T>.DoTransfer(),
            TamedBT<T>.DoRelease(),
            TamedBT<T>.DoFriend(),
            TamedBT<T>.DoUnFriend(),
            TamedBT<T>.DoDrop(),
            new DoWander<T>(3, 2, 1)
        });
    }

    public static BTNode<T> DoThink()
    {
        return new BTSelector<T>(new List<BTNode<T>>
        {
            DoCombat(),
            DoGuard(),
            new DoAcquireNewCombatantInRange<T>(),
            new BTInverter<T>(new CheckIdle<T>()),
            new DoWander<T>(2, 2, 1)
        });
    }

    private BTNode<T> SetupTree()
    {
        var root = new BTSequence<T>(new List<BTNode<T>>
        {
            new BTInverter<T>(new CheckIsDeleted<T>()),
            new BTInverter<T>(new CheckMapIsNullOrInternal<T>()),
            new BTInverter<T>(new CheckMapSectorInactive<T>()),
            new BTSelector<T>(new List<BTNode<T>>
            {
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckBardPacified<T>(),
                    new DoBardPacified<T>()
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckBardProvoked<T>(),
                    new BTSucceeder<T>(DoCombat()),
                    new DoBardProvoked<T>()
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new CheckControlled<T>(),
                    DoObey()
                }),
                new BTSequence<T>(new List<BTNode<T>>
                {
                    new BTInverter<T>(new CheckControlled<T>()),
                    DoThink()
                })
            })
        });

        return root;
    }
}