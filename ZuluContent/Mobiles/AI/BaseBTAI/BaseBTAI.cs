using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targets;

namespace Server.Mobiles;

public interface IAIState
{
    public BaseCreature Creature { get; }
    public bool InCombat { get; set; }
    public long CombatStartTime { get; set; }
    public bool IsGuarding { get; set; }
    public bool IsFleeing { get; set; }
    public long NextStopGuard { get; set; }
    public long NextStopFlee { get; set; }
    public PathFollower Path { get; set; }
    public OrderType LastControlOrder { get; set; }
}

public class AIState : IAIState
{
    public BaseCreature Creature { get; init; }
    public bool InCombat { get; set; }
    public long CombatStartTime { get; set; }
    public bool IsGuarding { get; set; }
    public bool IsFleeing { get; set; }
    public long NextStopGuard { get; set; }
    public long NextStopFlee { get; set; }
    public PathFollower Path { get; set; }
    public OrderType LastControlOrder { get; set; }
}

public class BaseBTAI<T> where T : IAIState
{
    private readonly Timer m_Timer;
    private IBTTree<T> BehaviorTree { get; }

    private T State { get; }

    public BaseBTAI(T state, IBTTree<T> behaviorTree)
    {
        State = state;
        BehaviorTree = behaviorTree;
        m_Timer = new AITimer<T>(this);

        bool activate;

        if (!State.Creature.PlayerRangeSensitive)
            activate = true;
        else if (World.Loading)
            activate = false;
        else if (State.Creature.Map == null || State.Creature.Map == Map.Internal ||
                 !State.Creature.Map.GetSector(State.Creature.Location).Active)
            activate = false;
        else
            activate = true;

        if (activate) Activate();
    }

    public void Deactivate()
    {
        if (State.Creature.PlayerRangeSensitive)
        {
            m_Timer.Stop();

            var se = State.Creature.Spawner;

            if (se != null && se.ReturnOnDeactivate && !State.Creature.Controlled)
            {
                if (se.HomeLocation == Point3D.Zero)
                {
                    if (!State.Creature.Region.AcceptsSpawnsFrom(se.Region))
                        Timer.DelayCall(TimeSpan.Zero, ReturnToHome);
                }
                else if (!State.Creature.InRange(se.HomeLocation, se.HomeRange))
                {
                    Timer.DelayCall(TimeSpan.Zero, ReturnToHome);
                }
            }
        }
        else if (State.Creature.Deleted)
        {
            m_Timer.Stop();
        }
    }

    public void Activate()
    {
        if (!m_Timer.Running)
        {
            m_Timer.Delay = TimeSpan.Zero;
            m_Timer.Start();
        }
    }

    public void StopTimer()
    {
        m_Timer.Stop();
    }

    private void ReturnToHome()
    {
        if (State.Creature.Spawner != null)
        {
            var loc = State.Creature.Spawner.GetSpawnPosition(State.Creature, State.Creature.Spawner.Map);

            if (loc != Point3D.Zero) State.Creature.MoveToWorld(loc, State.Creature.Spawner.Map);
        }
    }

    public virtual void EndPickTarget(Mobile from, Mobile target, OrderType order)
    {
        if (State.Creature.Deleted || !State.Creature.Controlled || !from.InRange(State.Creature, 14) ||
            from.Map != State.Creature.Map ||
            !from.CheckAlive())
            return;

        var isOwner = from == State.Creature.ControlMaster;
        var isFriend = !isOwner && State.Creature.IsPetFriend(from);

        if (!isOwner && !isFriend)
            return;

        if (isFriend && order != OrderType.Follow && order != OrderType.Stay && order != OrderType.Stop)
            return;

        if (order == OrderType.Attack)
            if (target is BaseCreature { IsScaryToPets: true } && State.Creature.IsScaredOfScaryThings)
            {
                State.Creature.SayTo(from, "Your pet refuses to attack this creature!");
                return;
            }

        if (State.Creature.CheckControlChance(from))
        {
            State.Creature.ControlTarget = target;
            State.Creature.ControlOrder = order;
        }
    }

    public bool HandlesOnSpeech(Mobile from)
    {
        if (from.AccessLevel >= AccessLevel.GameMaster)
            return true;

        if (from.Alive && State.Creature.Controlled && State.Creature.Commandable &&
            (from == State.Creature.ControlMaster || State.Creature.IsPetFriend(from)))
            return true;

        return from.Alive && from.InRange(State.Creature.Location, 3) && State.Creature.IsHumanInTown();
    }

    private static readonly SkillName[] m_KeywordTable =
    {
        SkillName.Parry,
        SkillName.Healing,
        SkillName.Hiding,
        SkillName.Stealing,
        SkillName.Alchemy,
        SkillName.AnimalLore,
        SkillName.ItemID,
        SkillName.ArmsLore,
        SkillName.Begging,
        SkillName.Blacksmith,
        SkillName.Fletching,
        SkillName.Peacemaking,
        SkillName.Camping,
        SkillName.Carpentry,
        SkillName.Cartography,
        SkillName.Cooking,
        SkillName.DetectHidden,
        SkillName.Discordance, //??
        SkillName.EvalInt,
        SkillName.Fishing,
        SkillName.Provocation,
        SkillName.Lockpicking,
        SkillName.Magery,
        SkillName.MagicResist,
        SkillName.Tactics,
        SkillName.Snooping,
        SkillName.RemoveTrap,
        SkillName.Musicianship,
        SkillName.Poisoning,
        SkillName.Archery,
        SkillName.SpiritSpeak,
        SkillName.Tailoring,
        SkillName.AnimalTaming,
        SkillName.TasteID,
        SkillName.Tinkering,
        SkillName.Veterinary,
        SkillName.Forensics,
        SkillName.Herding,
        SkillName.Tracking,
        SkillName.Stealth,
        SkillName.Inscribe,
        SkillName.Swords,
        SkillName.Macing,
        SkillName.Fencing,
        SkillName.Wrestling,
        SkillName.Lumberjacking,
        SkillName.Mining,
        SkillName.Meditation
    };

    public virtual void BeginPickTarget(Mobile from, OrderType order)
    {
        if (State.Creature.Deleted || !State.Creature.Controlled || !from.InRange(State.Creature, 14) ||
            from.Map != State.Creature.Map)
            return;

        var isOwner = from == State.Creature.ControlMaster;
        var isFriend = !isOwner && State.Creature.IsPetFriend(from);

        if (!isOwner && !isFriend)
            return;

        if (isFriend && order != OrderType.Follow && order != OrderType.Stay && order != OrderType.Stop)
            return;

        if (from.Target == null)
        {
            if (order == OrderType.Transfer)
                from.SendLocalizedMessage(502038); // Click on the person to transfer ownership to.
            else if (order == OrderType.Friend)
                from.SendLocalizedMessage(502020); // Click on the player whom you wish to make a co-owner.
            else if (order == OrderType.Unfriend)
                from.SendLocalizedMessage(1070948); // Click on the player whom you wish to remove as a co-owner.

            from.Target = new BTAIControlMobileTarget<T>(this, order);
        }
        else if (from.Target is BTAIControlMobileTarget<T>)
        {
            var t = (BTAIControlMobileTarget<T>)from.Target;

            if (t.Order == order)
                t.AddAI(this);
        }
    }
    
    public virtual void OnBeforeCurrentOrderChanged()
    {
        if (State.Creature.Deleted || State.Creature.ControlMaster == null || State.Creature.ControlMaster.Deleted)
            return;
        
        State.LastControlOrder = State.Creature.ControlOrder;
    }

    public void OnSpeech(SpeechEventArgs e)
    {
        if (e.Mobile.Alive && e.Mobile.InRange(State.Creature.Location, 3) && State.Creature.IsHumanInTown())
        {
            if (e.HasKeyword(0x9D) && WasNamed(e.Speech)) // *move*
            {
                if (State.Creature.Combatant != null)
                    // I am too busy fighting to deal with thee!
                    State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 501482);
                else
                    // Excuse me?
                    State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 501516);
            }
            else if (e.HasKeyword(0x9E) && WasNamed(e.Speech)) // *time*
            {
                if (State.Creature.Combatant != null)
                {
                    // I am too busy fighting to deal with thee!
                    State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 501482);
                }
                else
                {
                    int generalNumber;
                    string exactTime;

                    Clock.GetTime(State.Creature, out generalNumber, out exactTime);

                    State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, generalNumber);
                }
            }
            else if (e.HasKeyword(0x6C)) // *train
            {
                if (State.Creature.Combatant != null)
                {
                    // I am too busy fighting to deal with thee!
                    State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 501482);
                }
                else
                {
                    var foundSomething = false;

                    var ourSkills = State.Creature.Skills;
                    var theirSkills = e.Mobile.Skills;

                    for (var i = 0; i < ourSkills.Length && i < theirSkills.Length; ++i)
                    {
                        var skill = ourSkills[i];
                        var theirSkill = theirSkills[i];

                        if (skill != null && theirSkill != null && skill.Base >= 60.0 &&
                            State.Creature.CheckTeach(skill.SkillName, e.Mobile))
                        {
                            var toTeach = skill.Base / 3.0;

                            if (toTeach > 42.0)
                                toTeach = 42.0;

                            if (toTeach > theirSkill.Base)
                            {
                                var number = 1043059 + i;

                                if (number > 1043107)
                                    continue;

                                if (!foundSomething)
                                    State.Creature.Say(1043058); // I can train the following:

                                State.Creature.Say(number);

                                foundSomething = true;
                            }
                        }
                    }

                    if (!foundSomething)
                        State.Creature.Say(501505); // Alas, I cannot teach thee anything.
                }
            }
            else
            {
                var toTrain = (SkillName)(-1);

                for (var i = 0; toTrain == (SkillName)(-1) && i < e.Keywords.Length; ++i)
                {
                    var keyword = e.Keywords[i];

                    if (keyword == 0x154)
                    {
                        toTrain = SkillName.Anatomy;
                    }
                    else if (keyword >= 0x6D && keyword <= 0x9C)
                    {
                        var index = keyword - 0x6D;

                        if (index >= 0 && index < m_KeywordTable.Length)
                            toTrain = m_KeywordTable[index];
                    }
                }

                if (toTrain != (SkillName)(-1))
                {
                    if (State.Creature.Combatant != null)
                    {
                        // I am too busy fighting to deal with thee!
                        State.Creature.PublicOverheadMessage(MessageType.Regular, 0x3B2, 501482);
                    }
                    else
                    {
                        var skills = State.Creature.Skills;
                        var skill = skills[toTrain];

                        if (skill == null || skill.Base < 60.0 || !State.Creature.CheckTeach(toTrain, e.Mobile))
                            State.Creature.Say(501507); // 'Tis not something I can teach thee of.
                        else
                            State.Creature.Teach(toTrain, e.Mobile, 0, false);
                    }
                }
            }
        }

        if (State.Creature.Controlled && State.Creature.Commandable)
        {
            State.Creature.DebugSay("Listening...");

            var isOwner = e.Mobile == State.Creature.ControlMaster;
            var isFriend = !isOwner && State.Creature.IsPetFriend(e.Mobile);

            if (e.Mobile.Alive && (isOwner || isFriend))
            {
                State.Creature.DebugSay("It's from my master");

                var keywords = e.Keywords;
                var speech = e.Speech;

                // First, check the all*
                for (var i = 0; i < keywords.Length; ++i)
                {
                    var keyword = keywords[i];

                    switch (keyword)
                    {
                        case 0x164: // all come
                        {
                            if (!isOwner)
                                break;

                            if (State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Come;
                            }

                            return;
                        }
                        case 0x165: // all follow
                        {
                            BeginPickTarget(e.Mobile, OrderType.Follow);
                            return;
                        }
                        case 0x166: // all guard
                        case 0x16B: // all guard me
                        {
                            if (!isOwner)
                                break;

                            if (State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Guard;
                            }

                            return;
                        }
                        case 0x167: // all stop
                        {
                            if (State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Stop;
                            }

                            return;
                        }
                        case 0x168: // all kill
                        case 0x169: // all attack
                        {
                            if (!isOwner)
                                break;

                            BeginPickTarget(e.Mobile, OrderType.Attack);
                            return;
                        }
                        case 0x16C: // all follow me
                        {
                            if (State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = e.Mobile;
                                State.Creature.ControlOrder = OrderType.Follow;
                            }

                            return;
                        }
                        case 0x170: // all stay
                        {
                            if (State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Stay;
                            }

                            return;
                        }
                    }
                }

                // No all*, so check *command
                for (var i = 0; i < keywords.Length; ++i)
                {
                    var keyword = keywords[i];

                    switch (keyword)
                    {
                        case 0x155: // *come
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Come;
                            }

                            return;
                        }
                        case 0x156: // *drop
                        {
                            if (!isOwner)
                                break;

                            if (!State.Creature.Summoned && WasNamed(speech) &&
                                State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Drop;
                            }

                            return;
                        }
                        case 0x15A: // *follow
                        {
                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                                BeginPickTarget(e.Mobile, OrderType.Follow);

                            return;
                        }
                        case 0x15B: // *friend
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                if (State.Creature.Summoned || State.Creature.SpellBound)
                                    e.Mobile.SendLocalizedMessage(
                                        1005481); // Summoned creatures are loyal only to their summoners.
                                else if (e.Mobile.HasTrade)
                                    e.Mobile.SendLocalizedMessage(
                                        1070947); // You cannot friend a pet with a trade pending
                                else
                                    BeginPickTarget(e.Mobile, OrderType.Friend);
                            }

                            return;
                        }
                        case 0x15C: // *guard
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Guard;
                            }

                            return;
                        }
                        case 0x15D: // *kill
                        case 0x15E: // *attack
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                                BeginPickTarget(e.Mobile, OrderType.Attack);

                            return;
                        }
                        case 0x15F: // *patrol
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Patrol;
                            }

                            return;
                        }
                        case 0x161: // *stop
                        {
                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Stop;
                            }

                            return;
                        }
                        case 0x163: // *follow me
                        {
                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = e.Mobile;
                                State.Creature.ControlOrder = OrderType.Follow;
                            }

                            return;
                        }
                        case 0x16D: // *release
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                if (!State.Creature.Summoned)
                                {
                                    e.Mobile.SendGump(new ConfirmReleaseGump(e.Mobile, State.Creature));
                                }
                                else
                                {
                                    State.Creature.ControlTarget = null;
                                    State.Creature.ControlOrder = OrderType.Release;
                                }
                            }

                            return;
                        }
                        case 0x16E: // *transfer
                        {
                            if (!isOwner)
                                break;

                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                if (State.Creature.Summoned || State.Creature.SpellBound)
                                    e.Mobile.SendLocalizedMessage(
                                        1005487); // You cannot transfer ownership of a summoned creature.
                                else if (e.Mobile.HasTrade)
                                    e.Mobile.SendLocalizedMessage(
                                        1010507); // You cannot transfer a pet with a trade pending
                                else
                                    BeginPickTarget(e.Mobile, OrderType.Transfer);
                            }

                            return;
                        }
                        case 0x16F: // *stay
                        {
                            if (WasNamed(speech) && State.Creature.CheckControlChance(e.Mobile))
                            {
                                State.Creature.ControlTarget = null;
                                State.Creature.ControlOrder = OrderType.Stay;
                            }

                            return;
                        }
                    }
                }
            }
        }
        else
        {
            if (e.Mobile.AccessLevel >= AccessLevel.GameMaster)
            {
                State.Creature.DebugSay("It's from a GM");

                if (State.Creature.FindMyName(e.Speech, true))
                {
                    var str = e.Speech.Split(' ');
                    int i;

                    for (i = 0; i < str.Length; i++)
                    {
                        var word = str[i];

                        if (Equals(word, "obey"))
                        {
                            State.Creature.SetControlMaster(e.Mobile);

                            if (State.Creature.Summoned)
                                State.Creature.SummonMaster = e.Mobile;

                            return;
                        }
                    }
                }
            }
        }
    }
    
    public void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (from.Alive && State.Creature.Controlled && from.InRange(State.Creature, 14))
            {
                if (from == State.Creature.ControlMaster)
                {
                    list.Add(new InternalEntry(from, 6107, 14, State.Creature, this, OrderType.Guard));  // Command: Guard
                    list.Add(new InternalEntry(from, 6108, 14, State.Creature, this, OrderType.Follow)); // Command: Follow

                    if (State.Creature.CanDrop)
                    {
                        list.Add(new InternalEntry(from, 6109, 14, State.Creature, this, OrderType.Drop)); // Command: Drop
                    }

                    list.Add(new InternalEntry(from, 6111, 14, State.Creature, this, OrderType.Attack)); // Command: Kill

                    list.Add(new InternalEntry(from, 6112, 14, State.Creature, this, OrderType.Stop)); // Command: Stop
                    list.Add(new InternalEntry(from, 6114, 14, State.Creature, this, OrderType.Stay)); // Command: Stay

                    if (!State.Creature.Summoned)
                    {
                        list.Add(new InternalEntry(from, 6110, 14, State.Creature, this, OrderType.Friend));   // Add Friend
                        list.Add(new InternalEntry(from, 6099, 14, State.Creature, this, OrderType.Unfriend)); // Remove Friend
                        list.Add(new InternalEntry(from, 6113, 14, State.Creature, this, OrderType.Transfer)); // Transfer
                    }

                    list.Add(new InternalEntry(from, 6118, 14, State.Creature, this, OrderType.Release)); // Release
                }
                else if (State.Creature.IsPetFriend(from))
                {
                    list.Add(new InternalEntry(from, 6108, 14, State.Creature, this, OrderType.Follow)); // Command: Follow
                    list.Add(new InternalEntry(from, 6112, 14, State.Creature, this, OrderType.Stop));   // Command: Stop
                    list.Add(new InternalEntry(from, 6114, 14, State.Creature, this, OrderType.Stay));   // Command: Stay
                }
            }
        }

    public bool WasNamed(string speech)
    {
        var name = State.Creature.Name;

        return name != null && speech.InsensitiveStartsWith(name);
    }
    
    private class InternalEntry : ContextMenuEntry
    {
        private readonly BaseBTAI<T> m_AI;
        private readonly Mobile m_From;
        private readonly BaseCreature m_Mobile;
        private readonly OrderType m_Order;

        public InternalEntry(Mobile from, int number, int range, BaseCreature mobile, BaseBTAI<T> ai, OrderType order)
            : base(number, range)
        {
            m_From = from;
            m_Mobile = mobile;
            m_AI = ai;
            m_Order = order;

            if (!mobile.Alive && (order == OrderType.Guard || order == OrderType.Attack ||
                                     order == OrderType.Transfer || order == OrderType.Drop))
            {
                Enabled = false;
            }
        }

        public override void OnClick()
        {
            if (!m_Mobile.Deleted && m_Mobile.Controlled && m_From.CheckAlive())
            {
                if (!m_Mobile.Alive && (m_Order == OrderType.Guard || m_Order == OrderType.Attack ||
                                           m_Order == OrderType.Transfer || m_Order == OrderType.Drop))
                {
                    return;
                }

                var isOwner = m_From == m_Mobile.ControlMaster;
                var isFriend = !isOwner && m_Mobile.IsPetFriend(m_From);

                if (!isOwner && !isFriend)
                {
                    return;
                }

                if (isFriend && m_Order != OrderType.Follow && m_Order != OrderType.Stay && m_Order != OrderType.Stop)
                {
                    return;
                }

                switch (m_Order)
                {
                    case OrderType.Follow:
                    case OrderType.Attack:
                    case OrderType.Transfer:
                    case OrderType.Friend:
                    case OrderType.Unfriend:
                        {
                            if (m_Order == OrderType.Transfer && m_From.HasTrade)
                            {
                                m_From.SendLocalizedMessage(1010507); // You cannot transfer a pet with a trade pending
                            }
                            else if (m_Order == OrderType.Friend && m_From.HasTrade)
                            {
                                m_From.SendLocalizedMessage(1070947); // You cannot friend a pet with a trade pending
                            }
                            else
                            {
                                m_AI.BeginPickTarget(m_From, m_Order);
                            }

                            break;
                        }
                    case OrderType.Release:
                        {
                            if (m_Mobile.Summoned)
                            {
                                goto default;
                            }

                            m_From.SendGump(new ConfirmReleaseGump(m_From, m_Mobile));

                            break;
                        }
                    default:
                        {
                            if (m_Mobile.CheckControlChance(m_From))
                            {
                                m_Mobile.ControlOrder = m_Order;
                            }

                            break;
                        }
                }
            }
        }
    }

    private class AITimer<T> : Timer where T : IAIState
    {
        private readonly BaseBTAI<T> m_AI;

        public AITimer(BaseBTAI<T> ai)
            : base(TimeSpan.FromSeconds(Utility.RandomDouble()),
                TimeSpan.FromSeconds(Math.Max(0.0, ai.State.Creature.CurrentSpeed)))
        {
            m_AI = ai;
        }

        protected override void OnTick()
        {
            var state = m_AI.BehaviorTree.Update(m_AI.State);

            if (state == NodeState.FAILURE)
                m_AI.Deactivate();
        }
    }
}