using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public class MagicStatMod : StatMod, IMagicMod<StatType>
    {
        public StatType Target => Type;
        public MagicProp Prop { get; } = MagicProp.Stat;
        public EnchantNameType Place { get; } = EnchantNameType.Prefix;
        public int Color { get; } = 0;
        public int CursedColor { get; } = 0;
        public bool Cursed => Offset < 0;
        public Mobile Owner = null;

        public string[] NormalNames
        {
            get
            {
                IMagicMod<StatType>.NormalToCursedMap.TryGetValue(Target, out var dict);
                return dict?.Keys.ToArray();
            }
        }
        
        public string[] CursedNames
        {
            get
            {
                IMagicMod<StatType>.NormalToCursedMap.TryGetValue(Target, out var dict);
                return dict?.Values.ToArray();
            }
        }

        public MagicStatMod(StatType type, int offset) :
            base(type, $"{nameof(MagicStatMod)}:{type}", offset, TimeSpan.Zero)
        {
        }

        public void Remove()
        {
            Owner?.RemoveStatMod(Name);
            Owner = null;
        }

        public void AddTo(Mobile mobile)
        {
            Owner = mobile;
            mobile.AddStatMod(this);
        }


        public static IReadOnlyDictionary<StatType, Dictionary<string, string>> NormalToCursedMap =>
            IMagicMod<StatType>.NormalToCursedMap;

        static MagicStatMod()
        {
            IMagicMod<StatType>.NormalToCursedMap = new Dictionary<StatType, Dictionary<string, string>>
            {
                [StatType.Str] = new Dictionary<string, string>
                {
                    {"Warrior's", "Weakling's"},
                    {"Veteran's", "Enfeebling"},
                    {"Champion's", "Powerless"},
                    {"Hero's", "Frail"},
                    {"Warlord's", "Diseased"},
                    {"King's", "Leper's"},
                },
                [StatType.Int] = new Dictionary<string, string>
                {
                    {"Apprentice's", "Fool's"},
                    {"Adept's", "Simpleton's"},
                    {"Wizard's", "Infantile"},
                    {"Archmage's", "Senile"},
                    {"Magister's", "Demented"},
                    {"Oracle's", "Madman's"},
                },
                [StatType.Dex] = new Dictionary<string, string>
                {
                    {"Cutpurse's", "Heavy"},
                    {"Thief's", "Leaden"},
                    {"Cat Burglar's", "Encumbering"},
                    {"Tumbler's", "Binding"},
                    {"Acrobat's", "Fumbling"},
                    {"Escape Artist's", "Blundering"},
                }
            };
        }
    }
}