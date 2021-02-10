using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Zulu.Engines.Classes;

namespace Server.Engines.Harvest
{
    public delegate int ConsumedPerHarvestCallback(double skillValue);

    public class HarvestDefinition
    {
        private int m_BankWidth, m_BankHeight;
        private int m_MinTotal, m_MaxTotal;
        private int[] m_Tiles;
        private bool m_RangedTiles;
        private TimeSpan m_MinRespawn, m_MaxRespawn;
        private ConsumedPerHarvestCallback m_ConsumedPerHarvestCallback;
        private int m_MaxRange;
        private bool m_PlaceAtFeetIfFull;
        private SkillName m_Skill;
        private int[] m_EffectActions;
        private int[] m_EffectCounts;
        private int[] m_EffectSounds;
        private TimeSpan m_EffectSoundDelay;
        private TimeSpan m_EffectDelay;

        private object m_NoResourcesMessage,
            m_OutOfRangeMessage,
            m_TimedOutOfRangeMessage,
            m_DoubleHarvestMessage,
            m_FailMessage,
            m_PackFullMessage,
            m_ToolBrokeMessage;

        private HarvestResource[] m_Resources;
        private HarvestVein[] m_Veins;

        private HarvestVein m_DefaultVein;

        public int BankWidth
        {
            get { return m_BankWidth; }
            set { m_BankWidth = value; }
        }

        public int BankHeight
        {
            get { return m_BankHeight; }
            set { m_BankHeight = value; }
        }

        public int MinTotal
        {
            get { return m_MinTotal; }
            set { m_MinTotal = value; }
        }

        public int MaxTotal
        {
            get { return m_MaxTotal; }
            set { m_MaxTotal = value; }
        }

        public int[] Tiles
        {
            get { return m_Tiles; }
            set { m_Tiles = value; }
        }

        public bool RangedTiles
        {
            get { return m_RangedTiles; }
            set { m_RangedTiles = value; }
        }

        public TimeSpan MinRespawn
        {
            get { return m_MinRespawn; }
            set { m_MinRespawn = value; }
        }

        public TimeSpan MaxRespawn
        {
            get { return m_MaxRespawn; }
            set { m_MaxRespawn = value; }
        }

        public int MaxRange
        {
            get { return m_MaxRange; }
            set { m_MaxRange = value; }
        }

        public ConsumedPerHarvestCallback ConsumedPerHarvest
        {
            get { return m_ConsumedPerHarvestCallback; }
            set { m_ConsumedPerHarvestCallback = value; }
        }

        public bool PlaceAtFeetIfFull
        {
            get { return m_PlaceAtFeetIfFull; }
            set { m_PlaceAtFeetIfFull = value; }
        }

        public SkillName Skill
        {
            get { return m_Skill; }
            set { m_Skill = value; }
        }

        public int[] EffectActions
        {
            get { return m_EffectActions; }
            set { m_EffectActions = value; }
        }

        public int[] EffectCounts
        {
            get { return m_EffectCounts; }
            set { m_EffectCounts = value; }
        }

        public int[] EffectSounds
        {
            get { return m_EffectSounds; }
            set { m_EffectSounds = value; }
        }

        public TimeSpan EffectSoundDelay
        {
            get { return m_EffectSoundDelay; }
            set { m_EffectSoundDelay = value; }
        }

        public TimeSpan EffectDelay
        {
            get { return m_EffectDelay; }
            set { m_EffectDelay = value; }
        }

        public object NoResourcesMessage
        {
            get { return m_NoResourcesMessage; }
            set { m_NoResourcesMessage = value; }
        }

        public object OutOfRangeMessage
        {
            get { return m_OutOfRangeMessage; }
            set { m_OutOfRangeMessage = value; }
        }

        public object TimedOutOfRangeMessage
        {
            get { return m_TimedOutOfRangeMessage; }
            set { m_TimedOutOfRangeMessage = value; }
        }

        public object DoubleHarvestMessage
        {
            get { return m_DoubleHarvestMessage; }
            set { m_DoubleHarvestMessage = value; }
        }

        public object FailMessage
        {
            get { return m_FailMessage; }
            set { m_FailMessage = value; }
        }

        public object PackFullMessage
        {
            get { return m_PackFullMessage; }
            set { m_PackFullMessage = value; }
        }

        public object ToolBrokeMessage
        {
            get { return m_ToolBrokeMessage; }
            set { m_ToolBrokeMessage = value; }
        }

        public HarvestResource[] Resources
        {
            get { return m_Resources; }
            set { m_Resources = value; }
        }

        public HarvestVein[] Veins
        {
            get { return m_Veins; }
            set { m_Veins = value; }
        }

        public HarvestVein DefaultVein
        {
            get { return m_DefaultVein; }
            set { m_DefaultVein = value; }
        }

        private Dictionary<Map, Dictionary<Point2D, HarvestBank>> m_BanksByMap;

        public Dictionary<Map, Dictionary<Point2D, HarvestBank>> Banks
        {
            get { return m_BanksByMap; }
            set { m_BanksByMap = value; }
        }

        public void SendMessageTo(Mobile from, object message)
        {
            if (message is int)
                from.SendLocalizedMessage((int) message);
            else if (message is string)
                from.SendMessage((string) message);
        }

        public HarvestBank GetBank(Map map, int x, int y)
        {
            if (map == null || map == Map.Internal)
                return null;

            x /= m_BankWidth;
            y /= m_BankHeight;

            Dictionary<Point2D, HarvestBank> banks = null;
            m_BanksByMap.TryGetValue(map, out banks);

            if (banks == null)
                m_BanksByMap[map] = banks = new Dictionary<Point2D, HarvestBank>();

            Point2D key = new Point2D(x, y);
            HarvestBank bank = null;
            banks.TryGetValue(key, out bank);

            if (bank == null)
                banks[key] = bank = new HarvestBank(this);

            return bank;
        }

        public int ModifyHarvestAmount(Mobile from, int harvestAmount)
        {
            var skillValue = from.Skills[m_Skill].Value;
            var maxAmount = (int) (skillValue / 30);

            // TODO: Multiply maxAmount by harvest bonus (omeros/xarafax)

            if (harvestAmount > maxAmount)
                harvestAmount = maxAmount;

            if (harvestAmount < 1)
                harvestAmount = 1;

            return harvestAmount;
        }

        public HarvestVein GetColoredVein(Mobile from, int harvestAmount)
        {
            var skillValue = from.Skills[m_Skill].Value;
            var chance = Utility.Random(1, 155);

            if (Utility.Random(2) > 0)
            {
                var bonus = (int) (skillValue / 4);
                var toMod = 80;

                // TODO: Multiply if classed crafter

                // TODO: Multiply by harvest bonus (omeros/xarafax)

                if (chance > toMod)
                    chance -= bonus;
            }

            var possibles = m_Veins.ToList().Where(v =>
                chance <= v.VeinChance && from.ShilCheckSkill(m_Skill, (int) v.Resource.ReqSkill, 0)).ToList();

            if (possibles.Count > 0)
            {
                return possibles.First();
            }

            return null;
        }

        public HarvestDefinition()
        {
            m_BanksByMap = new Dictionary<Map, Dictionary<Point2D, HarvestBank>>();
        }

        public bool Validate(int tileID)
        {
            if (m_RangedTiles)
            {
                bool contains = false;

                for (int i = 0; !contains && i < m_Tiles.Length; i += 2)
                    contains = tileID >= m_Tiles[i] && tileID <= m_Tiles[i + 1];

                return contains;
            }
            else
            {
                int dist = -1;

                for (int i = 0; dist < 0 && i < m_Tiles.Length; ++i)
                    dist = m_Tiles[i] - tileID;

                return dist == 0;
            }
        }
    }
}