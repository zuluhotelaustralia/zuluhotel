using System;
using System.Collections.Generic;
using System.Linq;
using Server.Items;
using Server.Spells.Fourth;
using Server.Spells.Second;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Mobiles
{
    public class FamiliarAI : BaseAI
    {
        private static readonly Dictionary<string, Action<BaseCreature, PlayerMobile>> FamiliarActions = new()
        {
            ["heal"] = (familiar, _) => { new GreaterHealSpell(familiar, null).Cast(); },
            ["cure"] = (familiar, _) => { new CureSpell(familiar, null).Cast(); },
            ["protect"] = (familiar, _) => { new ArchProtectionSpell(familiar, null).Cast(); },
            ["bless"] = (familiar, _) => { new BlessSpell(familiar, null).Cast(); },
            ["loot"] = (familiar, _) => { familiar.ControlOrder = OrderType.Loot; },
            ["speak"] = (familiar, _) => { familiar.PlaySound(0x253); },
            ["drop"] = (familiar, _) => { familiar.ControlOrder = OrderType.Drop; }
        };

        private readonly IList<Item> m_ItemsToLoot = new List<Item>();
        
        private const int LootDelay = 1_000;
        private long m_NextLootTime = 0;

        private const int HideCooldown = 30_000;
        private long m_NextHideTime = 0;
        
        private readonly double m_DefaultActiveSpeed;
        private const double FollowSpeed = 0.06;

        public FamiliarAI(BaseCreature m) : base(m)
        {
            m_DefaultActiveSpeed = m.ActiveSpeed;
        }

        public override bool Obey()
        {
            if (m_Mobile.Deleted)
                return false;

            ProcessTarget();

            if (m_Mobile.ControlOrder == OrderType.Loot)
                return DoOrderLoot();


            return base.Obey();
        }

        protected virtual void ProcessTarget()
        {
            if (m_Mobile.Target != null)
            {
                if (m_Mobile.InRange(m_Mobile.ControlMaster, m_Mobile.Target.Range))
                {
                    switch (m_Mobile.Target)
                    {
                        case CureSpell.InternalTarget:
                        case GreaterHealSpell.InternalTarget:
                        case BlessSpell.InternalTarget:
                        case ArchProtectionSpell.InternalTarget:
                            m_Mobile.Target.Invoke(m_Mobile, m_Mobile.ControlMaster);
                            break;
                        default:
                            m_Mobile.Target.Cancel(m_Mobile, TargetCancelType.Canceled);
                            break;
                    }
                }
                else
                {
                    m_Mobile.Target.Cancel(m_Mobile, TargetCancelType.Canceled);
                }
            }
        }

        public override void OnCurrentOrderChanged()
        {
            switch (m_Mobile.ControlOrder)
            {
                case OrderType.Loot:
                {
                    m_Mobile.ActiveSpeed = FollowSpeed;
                    var map = m_Mobile.Map;
                    IPooledEnumerable eable = map.GetItemsInRange(m_Mobile.Location, m_Mobile.RangePerception);


                    foreach (Item item in eable)
                    {
                        if (
                            (item.Movable && item.Decays || item is Corpse)
                            && map.LineOfSight(m_Mobile, item)
                            && item.IsAccessibleTo(m_Mobile)
                        )
                        {
                            m_ItemsToLoot.Add(item);
                        }
                    }

                    eable.Free();
                    break;
                }
                case OrderType.Follow:
                    m_Mobile.ActiveSpeed = FollowSpeed;
                    base.OnCurrentOrderChanged();
                    break;
                default:
                    m_Mobile.ActiveSpeed = m_DefaultActiveSpeed;
                    base.OnCurrentOrderChanged();
                    break;
            }
        }

        protected virtual bool DoOrderLoot()
        {
            if (m_NextLootTime > Core.TickCount)
                return true;

            static void LootItem(FamiliarAI ai, Item item)
            {
                if (item == null || !item.Movable)
                    return;

                ai.m_Mobile.DebugSay($"I am looting {item}.");
                ai.m_Mobile.Backpack?.DropItem(item);
                ai.m_Mobile.Say("* Yoink *");

                ai.m_NextLootTime = Core.TickCount + LootDelay;
            }

            if (m_ItemsToLoot.Count > 0)
            {
                var item = m_ItemsToLoot[0];

                if (!m_Mobile.Map.LineOfSight(m_Mobile, item) || !item.IsAccessibleTo(m_Mobile))
                {
                    m_Mobile.DebugSay($"I tried to loot {item} but it's not in my line or sight, or accessible.");
                    m_ItemsToLoot.Remove(item);
                    return true;
                }

                if (
                    (item.X != m_Mobile.Location.X || item.Y != m_Mobile.Location.Y)
                    && item.Map == m_Mobile.Map
                    && item.Parent == null
                    && !item.Deleted
                )
                {
                    m_Mobile.DebugSay($"I will move towards looting {item.Name}.");
                    DoMove(m_Mobile.GetDirectionTo(item.Location) | Direction.Running);
                }
                else if (item is Corpse corpse)
                {
                    m_Mobile.DebugSay($"I will loot corpse {corpse.Name}.");

                    if (corpse.Items.Count == 0)
                    {
                        m_ItemsToLoot.Remove(corpse);
                        m_Mobile.DebugSay($"Corpse is empty {corpse.Name}, moving on.");
                    }
                    else
                    {
                        LootItem(this, corpse.Items.FirstOrDefault());
                    }
                }
                else
                {
                    LootItem(this, item);
                    m_ItemsToLoot.Remove(item);
                }
            }
            else
            {
                m_Mobile.ControlOrder = OrderType.Follow;
            }

            return true;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (e.Mobile == m_Mobile.ControlMaster && m_Mobile.ControlMaster is PlayerMobile player)
            {
                var entry = FamiliarActions.FirstOrDefault(kv => e.Speech.InsensitiveContains(kv.Key));

                if (entry.Key != null)
                    entry.Value(m_Mobile, player);
            }
            
            base.OnSpeech(e);
        }

        public override bool DoOrderFollow(bool alwaysRun = false)
        {
            if (m_Mobile.ControlTarget?.Hidden == true)
            {
                m_Mobile.DebugSay("I can't follow, they're hidden! I will stay.");

                m_Mobile.ControlOrder = OrderType.Stay;
                return true;
            }
            
            if (
                m_Mobile.ControlTarget == m_Mobile.ControlMaster 
                && m_Mobile.ControlMaster != null 
                && m_Mobile.GetDistanceToSqrt(m_Mobile.ControlTarget) > m_Mobile.RangePerception * 2
            )
            {
                // They probably recalled/gated
                m_Mobile.DebugSay("I can't follow, they're suddenly very far, I will teleport.");
                DoTeleport(m_Mobile, m_Mobile.ControlMaster);
                return true;
            }
            
            return base.DoOrderFollow(true);
        }

        public override bool DoOrderAttack()
        {
            if (m_Mobile.CanFlee)
            {
                m_Mobile.DebugSay("I am fleeing!");

                if (Action != ActionType.Flee)
                {
                    Action = ActionType.Flee;
                    m_Mobile.FocusMob = m_Mobile.ControlTarget;
                }

                DoActionFlee();

                if (Action == ActionType.Guard)
                {
                    m_Mobile.DebugSay("I am safe!");

                    if (Core.TickCount > m_NextHideTime)
                    {
                        m_Mobile.DebugSay("I will hide now!");
                        m_NextHideTime = Core.TickCount + HideCooldown;
                        m_Mobile.Hidden = true;
                    }
                    else
                    {
                        m_Mobile.DebugSay("I can't hide, I'll just stay here!");
                    }
                    m_Mobile.ControlOrder = OrderType.Stay;
                }

                return true;
            }

            return true;
        }

        public override void BeginPickTarget(Mobile from, OrderType order)
        {
            switch (order)
            {
                case OrderType.Attack: return;
                case OrderType.Friend: return;
                case OrderType.Unfriend: return;
                case OrderType.Guard: return;
                case OrderType.Release: return;
                case OrderType.Transfer: return;
            }

            base.BeginPickTarget(from, order);
        }
    }
}