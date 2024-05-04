using System;

namespace Server.Mobiles;

public class DoEndCombatTimer<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        treeState.CombatStartTime = 0;
        
        return NodeState.SUCCESS;
    }
}