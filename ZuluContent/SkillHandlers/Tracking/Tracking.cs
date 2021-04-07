using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using ZuluContent.Zulu.Engines.Magic;
using System.Linq;
using Scripts.Zulu.Utilities;

namespace Server.SkillHandlers
{
    public static class Tracking
    {
        public static void Configure()
        {
            IncomingExtendedCommandPackets.RegisterExtended(0x07, true, QuestArrow);
            SkillInfo.Table[(int) SkillName.Tracking].Callback = OnUse;
        }

        public static void QuestArrow(NetState state, CircularBufferReader reader, ref int packetLength)
        {
            if (state.Mobile is PlayerMobile from)
            {
                var rightClick = reader.ReadBoolean();

                from.QuestArrow?.OnClick(rightClick);
            }
        }

        public static TimeSpan OnUse(Mobile m)
        {
            if (m is PlayerMobile from)
            {
                if (from.CheckSkill(SkillName.Tracking, -1, ZhConfig.Skills.Entries[SkillName.Anatomy].DefaultPoints) == false)
                {
                    from.SendFailureMessage("You fail to find any creatures nearby");
                }
                else
                {
                    from.SendLocalizedMessage(1011350); // What do you wish to track?
                    from.CloseGump<TrackWhatGump>();
                    from.CloseGump<TrackWhoGump>();
                    from.SendGump(new TrackWhatGump(from));
                }
            }


            return ZhConfig.Skills.Entries[SkillName.Tracking].Delay;
        }

        public class TrackingInfo
        {
            public Mobile m_Tracker;
            public Mobile m_Target;
            public Point2D m_Location;
            public Map m_Map;

            public TrackingInfo(Mobile tracker, Mobile target)
            {
                m_Tracker = tracker;
                m_Target = target;
                m_Location = new Point2D(target.X, target.Y);
                m_Map = target.Map;
            }
        }

        private static Dictionary<Mobile, TrackingInfo> m_Table = new Dictionary<Mobile, TrackingInfo>();

        public static void AddInfo(Mobile tracker, Mobile target)
        {
            TrackingInfo info = new TrackingInfo(tracker, target);
            m_Table[tracker] = info;
        }


        public static void ClearTrackingInfo(Mobile tracker)
        {
            m_Table.Remove(tracker);
        }
    }

    public class TrackWhatGump : Gump
    {
        private PlayerMobile m_From;

        private int m_Range;

        private List<Mobile> m_MobileList;

        private static TrackTypeDelegate[] m_Delegates = new TrackTypeDelegate[]
        {
            IsAnimal,
            IsMonster,
            IsHumanNPC,
            IsPlayer
        };

        private static bool IsAnimal(Mobile m)
        {
            return !m.Player && m.Body.IsAnimal;
        }

        private static bool IsMonster(Mobile m)
        {
            return !m.Player && m.Body.IsMonster;
        }

        private static bool IsHumanNPC(Mobile m)
        {
            return !m.Player && m.Body.IsHuman;
        }

        private static bool IsPlayer(Mobile m)
        {
            return m.Player;
        }

        public TrackWhatGump(PlayerMobile from) : base(20, 30)
        {
            Map map = from.Map;

            if (map == null)
                return;

            m_From = from;

            m_Range = (int) (from.Skills[SkillName.Tracking].Value / 3);
            from.FireHook(h => h.OnTracking(from, ref m_Range));

            m_MobileList = new List<Mobile>();

            var animalsNearby = false;
            var monstersNearby = false;
            var humansNearby = false;
            var playersNearby = false;

            foreach (Mobile m in from.GetMobilesInRange(m_Range))
            {
                // Ghosts can no longer be tracked
                if (m != from &&
                    (!m.Hidden || m.AccessLevel == AccessLevel.Player || from.AccessLevel > m.AccessLevel))
                {
                    m_MobileList.Add(m);

                    if (IsAnimal(m))
                        animalsNearby = true;
                    else if (IsMonster(m))
                        monstersNearby = true;
                    else if (IsHumanNPC(m))
                        humansNearby = true;
                    else if (IsPlayer(m))
                        playersNearby = true;
                }
            }

            AddPage(0);

            AddBackground(0, 0, 440, 135, 5054);

            AddBackground(10, 10, 420, 75, 2620);
            AddBackground(10, 85, 420, 25, 3000);

            var xOffset = 0;

            if (animalsNearby)
            {
                AddItem(20, 20, 9682);
                AddButton(20, 110, 4005, 4007, 1, GumpButtonType.Reply, 0);
                AddHtmlLocalized(20, 90, 100, 20, 1018087, false, false); // Animals
                xOffset += 100;
            }

            if (monstersNearby)
            {
                AddItem(xOffset + 20, 20, 9607);
                AddButton(xOffset + 20, 110, 4005, 4007, 2, GumpButtonType.Reply, 0);
                AddHtmlLocalized(xOffset + 20, 90, 100, 20, 1018088, false, false); // Monsters
                xOffset += 100;
            }

            if (humansNearby)
            {
                AddItem(xOffset + 20, 20, 8454);
                AddButton(xOffset + 20, 110, 4005, 4007, 3, GumpButtonType.Reply, 0);
                AddHtmlLocalized(xOffset + 20, 90, 100, 20, 1018089, false, false); // Human NPCs
                xOffset += 100;
            }

            if (playersNearby)
            {
                AddItem(xOffset + 20, 20, 8455);
                AddButton(xOffset + 20, 110, 4005, 4007, 4, GumpButtonType.Reply, 0);
                AddHtmlLocalized(xOffset + 20, 90, 100, 20, 1018090, false, false); // Players
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID >= 1 && info.ButtonID <= 4)
            {
                var type = info.ButtonID - 1;
                var check = m_Delegates[type];
                TrackWhoGump.DisplayTo(m_From, m_Range, m_MobileList.Where(m => check(m)).ToList());
            }
        }
    }

    public delegate bool TrackTypeDelegate(Mobile m);

    public class TrackWhoGump : Gump
    {
        private class InternalSorter : IComparer<Mobile>
        {
            private Mobile m_From;

            public InternalSorter(Mobile from)
            {
                m_From = from;
            }

            public int Compare(Mobile x, Mobile y)
            {
                if (x == null && y == null)
                    return 0;
                else if (x == null)
                    return -1;
                else if (y == null)
                    return 1;

                return m_From.GetDistanceToSqrt(x).CompareTo(m_From.GetDistanceToSqrt(y));
            }
        }

        public static void DisplayTo(PlayerMobile from, int range, List<Mobile> mobileList)
        {
            mobileList.Sort(new InternalSorter(from));

            from.SendGump(new TrackWhoGump(from, mobileList, range));
            from.SendLocalizedMessage(1018093); // Select the one you would like to track.
        }

        private PlayerMobile m_From;

        private List<Mobile> m_List;

        private int m_Range;

        private TrackWhoGump(PlayerMobile from, List<Mobile> list, int range) : base(20, 30)
        {
            m_From = from;
            m_List = list;
            m_Range = range;

            AddPage(0);

            AddBackground(0, 0, 440, 155, 5054);

            AddBackground(10, 10, 420, 75, 2620);
            AddBackground(10, 85, 420, 45, 3000);

            if (list.Count > 4)
            {
                AddBackground(0, 155, 440, 155, 5054);

                AddBackground(10, 165, 420, 75, 2620);
                AddBackground(10, 240, 420, 45, 3000);

                if (list.Count > 8)
                {
                    AddBackground(0, 310, 440, 155, 5054);

                    AddBackground(10, 320, 420, 75, 2620);
                    AddBackground(10, 395, 420, 45, 3000);
                }
            }

            for (int i = 0; i < list.Count && i < 12; ++i)
            {
                Mobile m = list[i];

                AddItem(20 + i % 4 * 100, 20 + i / 4 * 155, ShrinkTable.Lookup(m));
                AddButton(20 + i % 4 * 100, 130 + i / 4 * 155, 4005, 4007, i + 1, GumpButtonType.Reply, 0);

                if (m.Name != null)
                    AddHtml(20 + i % 4 * 100, 90 + i / 4 * 155, 90, 40, m.Name, false, false);
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            int index = info.ButtonID - 1;

            if (index >= 0 && index < m_List.Count && index < 12)
            {
                Mobile m = m_List[index];

                m_From.QuestArrow = new TrackArrow(m_From, m, m_Range * 2);
            }
        }
    }

    public class TrackArrow : QuestArrow
    {
        private Mobile m_From;
        private Timer m_Timer;

        public TrackArrow(PlayerMobile from, Mobile target, int range) : base(from, target)
        {
            m_From = from;
            m_Timer = new TrackTimer(from, target, range, this);
            m_Timer.Start();
        }

        public override void OnClick(bool rightClick)
        {
            if (rightClick)
            {
                Tracking.ClearTrackingInfo(m_From);

                m_From = null;

                Stop();
            }
        }

        public override void OnStop()
        {
            m_Timer.Stop();

            if (m_From != null)
            {
                Tracking.ClearTrackingInfo(m_From);

                m_From.SendLocalizedMessage(503177); // You have lost your quarry.
            }
        }
    }

    public class TrackTimer : Timer
    {
        private Mobile m_From, m_Target;
        private int m_Range;
        private int m_LastX, m_LastY;
        private QuestArrow m_Arrow;

        public TrackTimer(Mobile from, Mobile target, int range, QuestArrow arrow) : base(TimeSpan.FromSeconds(0.25),
            TimeSpan.FromSeconds(2.5))
        {
            m_From = from;
            m_Target = target;
            m_Range = range;

            m_Arrow = arrow;
        }

        protected override void OnTick()
        {
            if (!m_Arrow.Running)
            {
                Stop();
                return;
            }
            else if (m_From.NetState == null || m_From.Deleted || m_Target.Deleted || m_From.Map != m_Target.Map ||
                     !m_From.InRange(m_Target, m_Range) || m_Target.Hidden && m_Target.AccessLevel > m_From.AccessLevel)
            {
                m_Arrow.Stop();
                Stop();
                return;
            }

            if (m_LastX != m_Target.X || m_LastY != m_Target.Y)
            {
                m_LastX = m_Target.X;
                m_LastY = m_Target.Y;

                m_Arrow.Update();
            }
        }
    }
}