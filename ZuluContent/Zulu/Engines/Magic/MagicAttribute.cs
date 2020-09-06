using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Server;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Engines.Magic
{
    public class MagicAttribute<T> : IMagicMod<T> where T : unmanaged
    {
        public MagicProp Prop { get; }
        public EnchantNameType Place { get; }
        public T Target { get; set; }
        public string[] NormalNames { get; }
        public string[] CursedNames { get; }
        public int Color { get; }
        public int CursedColor { get; }
        public bool Cursed { get; }

        public MagicAttribute(MagicProp prop, T type)
        {
            Prop = prop;
            Place = EnchantNameType.Prefix;
            Target = type;
            NormalNames = new string[] { };
            CursedNames = new string[] { };
            Color = 0;
            CursedColor = 0;
            Cursed = false;
            // Owner = null;
        }

        public void Remove()
        {
            // Owner?.RemoveStatMod(Name);
            // Owner = null;
        }

        public void AddTo(Mobile mobile)
        {
            // Owner = mobile;
            // mobile.AddStatMod(this);
        }
        
        public static explicit operator T(MagicAttribute<T> attr) => attr.Target;
        
        public static IReadOnlyDictionary<StatType, Dictionary<string, string>> NormalToCursedMap => 
            IMagicMod<StatType>.NormalToCursedMap;

        static MagicAttribute()
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