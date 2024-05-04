namespace Server.Mobiles;

public class DoProcessFleeing<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var fleeing = treeState.IsFleeing;
        var nextStopFlee = treeState.NextStopFlee;

        if (fleeing && Core.TickCount < nextStopFlee)
        {
            return NodeState.RUNNING;
        }
        
        return NodeState.SUCCESS;
    }
}