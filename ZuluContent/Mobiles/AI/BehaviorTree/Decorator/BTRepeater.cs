using System.Collections.Generic;

namespace Server.Mobiles;

public class BTRepeater<T> : BTNode<T> where T : IAIState
{
    private int m_NumRepeats;
    
    public BTRepeater()
    {
    }

    public BTRepeater(BTNode<T> child, int numRepeats = -1) : base(new List<BTNode<T>> { child })
    {
        m_NumRepeats = numRepeats;
    }

    public override NodeState Evaluate(T treeState)
    {
        var nodeState = NodeState.RUNNING;
        
        while (m_NumRepeats == -1 || m_NumRepeats-- > 0)
        {
            nodeState = Children[0].Evaluate(treeState);
        }

        return nodeState;
    }
}