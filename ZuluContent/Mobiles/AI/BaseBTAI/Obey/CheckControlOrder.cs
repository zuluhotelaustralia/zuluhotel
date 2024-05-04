namespace Server.Mobiles;

public class CheckControlOrder<T> : BTNode<T> where T : IAIState
{
    private readonly OrderType m_OrderToCheck;
    
    public CheckControlOrder(OrderType orderToCheck)
    {
        m_OrderToCheck = orderToCheck;
    }

    public override NodeState Evaluate(T treeState)
    {
        var controlOrder = treeState.Creature.ControlOrder;

        if (controlOrder == m_OrderToCheck)
        {
            return NodeState.SUCCESS;
        }
        
        return NodeState.FAILURE;
    }
}