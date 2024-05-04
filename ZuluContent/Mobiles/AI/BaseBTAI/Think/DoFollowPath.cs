namespace Server.Mobiles;

public class DoFollowPath<T> : BTNode<T> where T : IAIState
{
    public override NodeState Evaluate(T treeState)
    {
        var path = treeState.Path;

        if (path?.Follow(true, 1) == true)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}