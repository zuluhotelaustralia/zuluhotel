using System.Collections.Generic;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Targets;

public class BTAIControlMobileTarget<T> : Target where T : IAIState
{
    private readonly List<BaseBTAI<T>> m_List;

    public OrderType Order { get; }

    public BTAIControlMobileTarget(BaseBTAI<T> ai, OrderType order) : base(-1, false,
        order == OrderType.Attack ? TargetFlags.Harmful : TargetFlags.None)
    {
        m_List = new List<BaseBTAI<T>>();
        Order = order;

        AddAI(ai);
    }

    public void AddAI(BaseBTAI<T> ai)
    {
        if (!m_List.Contains(ai))
            m_List.Add(ai);
    }

    protected override void OnTarget(Mobile from, object o)
    {
        if (o is Mobile)
        {
            var m = (Mobile)o;
            for (var i = 0; i < m_List.Count; ++i)
                m_List[i].EndPickTarget(from, m, Order);
        }
    }
}