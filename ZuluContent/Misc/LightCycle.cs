using System;
using System.Linq;
using Scripts.Zulu.Spells.Earth;
using Server.Network;
using Server.Commands;

namespace Server
{
    public class LightCycle
    {
        public const int DayLevel = 26;
        public const int NightLevel = 12;
        public const int DungeonLevel = 0;
        public const int JailLevel = 9;

        private static int m_LevelOverride = int.MinValue;

        public static int LevelOverride
        {
            get { return m_LevelOverride; }
            set
            {
                m_LevelOverride = value;

                foreach (var ns in TcpServer.Instances)
                {
                    ns.Mobile?.CheckLightLevels(false);
                }
            }
        }

        public static void Initialize()
        {
            new LightCycleTimer().Start();
            EventSink.Login += OnLogin;

            CommandSystem.Register("GlobalLight", AccessLevel.GameMaster, Light_OnCommand);
        }

        [Usage("GlobalLight <value>")]
        [Description("Sets the current global light level.")]
        private static void Light_OnCommand(CommandEventArgs e)
        {
            if (e.Length >= 1)
            {
                LevelOverride = e.GetInt32(0);
                e.Mobile.SendMessage("Global light level override has been changed to {0}.", m_LevelOverride);
            }
            else
            {
                LevelOverride = int.MinValue;
                e.Mobile.SendMessage("Global light level override has been cleared.");
            }
        }

        public static void OnLogin(Mobile m)
        {
            m.CheckLightLevels(true);
        }

        public static int ComputeLevelFor(Mobile from)
        {
            if (m_LevelOverride > int.MinValue)
                return m_LevelOverride;

            int hours, minutes;

            Items.Clock.GetTime(from.Map, from.X, from.Y, out hours, out minutes);

            /* OSI times:
             *
             * Midnight ->  3:59 AM : Night
             *  4:00 AM -> 11:59 PM : Day
             *
             * RunUO times:
             *
             * 10:00 PM -> 11:59 PM : Scale to night
             * Midnight ->  3:59 AM : Night
             *  4:00 AM ->  5:59 AM : Scale to day
             *  6:00 AM ->  9:59 PM : Day
             */

            if (hours < 4)
                return NightLevel;

            if (hours < 6)
                return NightLevel + ((hours - 4) * 60 + minutes) * (DayLevel - NightLevel) / 120;

            if (hours < 22)
                return DayLevel;

            if (hours < 24)
                return DayLevel + ((hours - 22) * 60 + minutes) * (NightLevel - DayLevel) / 120;

            return NightLevel; // should never be
        }

        private class LightCycleTimer : Timer
        {
            public LightCycleTimer() : base(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5.0))
            {
            }

            protected override void OnTick()
            {
                foreach (var ns in TcpServer.Instances)
                {
                    ns.Mobile?.CheckLightLevels(false);
                }
            }
        }
    }
}