namespace Server.Mobiles;

public class DoWander<T> : BTNode<T> where T : IAIState
{
    private readonly int m_ChanceToNotMove;
    private readonly int m_ChanceToDir;
    private readonly int m_Steps;
    
    public DoWander(int chanceToNotMove, int chanceToDir, int steps)
    {
        m_ChanceToNotMove = chanceToNotMove;
        m_ChanceToDir = chanceToDir;
        m_Steps = steps;
    }

    private bool _DoMove(Direction d, T treeState)
    {
        var res = treeState.Creature.DoMoveImpl(d);

        return res is MoveResult.Success or MoveResult.SuccessAutoTurn;
    }
    
    private void _WalkRandom(int chanceToNotMove, int chanceToDir, int steps, T treeState)
    {
        if (treeState.Creature.Deleted || treeState.Creature.DisallowAllMoves)
            return;

        for (int i = 0; i < steps; i++)
        {
            if (Utility.Random(8 * chanceToNotMove) <= 8)
            {
                int iRndMove = Utility.Random(0, 8 + 9 * chanceToDir);

                switch (iRndMove)
                {
                    case 0:
                        _DoMove(Direction.Up, treeState);
                        break;
                    case 1:
                        _DoMove(Direction.North, treeState);
                        break;
                    case 2:
                        _DoMove(Direction.Left, treeState);
                        break;
                    case 3:
                        _DoMove(Direction.West, treeState);
                        break;
                    case 5:
                        _DoMove(Direction.Down, treeState);
                        break;
                    case 6:
                        _DoMove(Direction.South, treeState);
                        break;
                    case 7:
                        _DoMove(Direction.Right, treeState);
                        break;
                    case 8:
                        _DoMove(Direction.East, treeState);
                        break;
                    default:
                        _DoMove(treeState.Creature.Direction, treeState);
                        break;
                }
            }
        }
    }

    public override NodeState Evaluate(T treeState)
    {
        if (treeState.Creature.Deleted || treeState.Creature.DisallowAllMoves || treeState.Creature.Hidden)
        {
            return NodeState.FAILURE;
        }
        
        treeState.Creature.DebugSay("I am wandering");
        
        treeState.Creature.Warmode = false;
        treeState.Creature.Combatant = null;
        treeState.Creature.FocusMob = null;
        treeState.Creature.CurrentSpeed = treeState.Creature.PassiveSpeed;
            
        if (treeState.Creature.Home == Point3D.Zero)
        {
            if (treeState.Creature.Spawner != null)
            {
                var region = treeState.Creature.Spawner.Region;

                if (treeState.Creature.Region.AcceptsSpawnsFrom(region))
                {
                    treeState.Creature.WalkRegion = region;
                    _WalkRandom(m_ChanceToNotMove, m_ChanceToDir, m_Steps, treeState);
                    treeState.Creature.WalkRegion = null;
                }
                else
                {
                    if (region.GoLocation != Point3D.Zero && Utility.Random(10) > 5)
                    {
                        _DoMove(treeState.Creature.GetDirectionTo(region.GoLocation), treeState);
                    }
                    else
                    {
                        _WalkRandom(m_ChanceToNotMove, m_ChanceToDir, 1, treeState);
                    }
                }
            }
            else
            {
                _WalkRandom(m_ChanceToNotMove, m_ChanceToDir, m_Steps, treeState);
            }
        }
        else
        {
            for (int i = 0; i < m_Steps; i++)
            {
                if (treeState.Creature.RangeHome != 0)
                {
                    int iCurrDist = (int) treeState.Creature.GetDistanceToSqrt(treeState.Creature.Home);

                    if (iCurrDist < treeState.Creature.RangeHome * 2 / 3)
                    {
                        _WalkRandom(m_ChanceToNotMove, m_ChanceToDir, 1, treeState);
                    }
                    else if (iCurrDist > treeState.Creature.RangeHome)
                    {
                        _DoMove(treeState.Creature.GetDirectionTo(treeState.Creature.Home), treeState);
                    }
                    else
                    {
                        if (Utility.Random(10) > 5)
                        {
                            _DoMove(treeState.Creature.GetDirectionTo(treeState.Creature.Home), treeState);
                        }
                        else
                        {
                            _WalkRandom(m_ChanceToNotMove, m_ChanceToDir, 1, treeState);
                        }
                    }
                }
                else
                {
                    if (treeState.Creature.Location != treeState.Creature.Home)
                    {
                        _DoMove(treeState.Creature.GetDirectionTo(treeState.Creature.Home), treeState);
                    }
                }
            }
        }
        
        return NodeState.RUNNING;
    }
}