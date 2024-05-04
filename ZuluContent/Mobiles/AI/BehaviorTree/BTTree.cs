namespace Server.Mobiles;

public interface IBTTree<T> where T : IAIState
{
    public BTNode<T> Root { get; }

    public NodeState Update(T treeState)
    {
        return Root.Evaluate(treeState);
    }

    private BTNode<T> SetupTree()
    {
        throw new System.NotImplementedException();
    }
}