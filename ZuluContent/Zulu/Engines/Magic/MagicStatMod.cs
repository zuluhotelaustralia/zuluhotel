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
        public MagicInfo Info => MagicInfo.MagicInfoMap[Target];
        public string EnchantName => Info.GetName(Offset / 5, Cursed);

        public Mobile Owner = null;

        public bool Cursed
        {
            get => Offset < 0;
            set {}
        }
        
        public MagicStatMod(StatType type, int offset, IEntity entity = null) :
            base(type, $"{nameof(MagicStatMod)}:{type}:{entity?.Serial.ToString() ?? "generic"}", offset, TimeSpan.Zero)
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
    }
}