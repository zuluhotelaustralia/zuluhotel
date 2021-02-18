using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Zulu.Engines.Classes;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Engines.Harvest
{
    public class HarvestDefinition
    {
        public int BankWidth { get; set; }

        public int BankHeight { get; set; }

        public int MinTotal { get; set; }

        public int MaxTotal { get; set; }

        public int[] Tiles { get; set; }

        public bool RangedTiles { get; set; }

        public TimeSpan MinRespawn { get; set; }

        public TimeSpan MaxRespawn { get; set; }

        public int MaxRange { get; set; }

        public Func<double, int> ConsumedPerHarvest { get; set; }

        public Action<Mobile, Item> BonusEffect { get; set; }

        public bool PlaceAtFeetIfFull { get; set; }

        public SkillName Skill { get; set; }

        public int[] EffectActions { get; set; }

        public int[] EffectCounts { get; set; }

        public int[] EffectSounds { get; set; }

        public TimeSpan EffectSoundDelay { get; set; }

        public TimeSpan EffectDelay { get; set; }

        public object NoResourcesMessage { get; set; }

        public object OutOfRangeMessage { get; set; }

        public object TimedOutOfRangeMessage { get; set; }

        public object DoubleHarvestMessage { get; set; }

        public object FailMessage { get; set; }

        public object PackFullMessage { get; set; }

        public object ToolBrokeMessage { get; set; }

        public HarvestResource[] Resources { get; set; }

        public HarvestVein[] Veins { get; set; }

        public HarvestVein DefaultVein { get; set; }

        public int MaxChance { get; set; }

        public Dictionary<Map, Dictionary<Point2D, HarvestBank>> Banks { get; set; }

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

            x /= BankWidth;
            y /= BankHeight;

            Banks.TryGetValue(map, out var banks);

            if (banks == null)
                Banks[map] = banks = new Dictionary<Point2D, HarvestBank>();

            Point2D key = new Point2D(x, y);


            banks.TryGetValue(key, out var bank);

            if (bank == null)
                banks[key] = bank = new HarvestBank(this);

            return bank;
        }

        public int ModifyHarvestAmount(Mobile from, Item tool, int harvestAmount)
        {
            var skillValue = from.Skills[Skill].Value;
            var maxAmount = (int) (skillValue / 30);

            from.FireHook(h => h.OnToolHarvestBonus(from, ref maxAmount));

            if (harvestAmount > maxAmount)
                harvestAmount = maxAmount;

            if (harvestAmount < 1)
                harvestAmount = 1;

            return harvestAmount;
        }

        public HarvestVein GetColoredVein(Mobile from, Item tool, ref int harvestAmount)
        {
            var skillValue = from.Skills[Skill].Value;
            var chance = Utility.Random(1, MaxChance);
            var amountToHarvest = harvestAmount;

            if (Utility.Random(2) > 0)
            {
                var bonus = (int) (skillValue / 4);
                var toMod = 80;

                from.FireHook(h => h.OnHarvestColoredQualityChance(from, ref bonus, ref toMod));
                from.FireHook(h => h.OnHarvestAmount(from, ref amountToHarvest));
                from.FireHook(h => h.OnToolHarvestColoredQualityChance(from, ref bonus, ref toMod));

                if (chance > toMod)
                    chance -= bonus;
            }

            harvestAmount = amountToHarvest;

            return Veins.ToList().FirstOrDefault(v =>
                chance <= v.VeinChance && from.ShilCheckSkill(Skill, (int) v.Resource.ReqSkill, 0));
        }

        public HarvestDefinition()
        {
            Banks = new Dictionary<Map, Dictionary<Point2D, HarvestBank>>();
        }

        public bool Validate(int tileID)
        {
            if (RangedTiles)
            {
                bool contains = false;

                for (int i = 0; !contains && i < Tiles.Length; i += 2)
                    contains = tileID >= Tiles[i] && tileID <= Tiles[i + 1];

                return contains;
            }

            int dist = -1;

            for (int i = 0; dist < 0 && i < Tiles.Length; ++i)
                dist = Tiles[i] - tileID;

            return dist == 0;
        }
    }
}