using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Json;
using Server.Spells;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server.Configurations
{
    public class SpellConfiguration : BaseSingleton<SpellConfiguration>
    {
        public readonly Dictionary<Type, SpellInfo> SpellInfos;
        public readonly Dictionary<SpellEntry, Type> SpellTypes;

        protected SpellConfiguration()
        {
            var config = ZhConfig.DeserializeJsonConfig<Dictionary<SpellEntry, SpellInfo>>("Data/Magic/spells.json");

            SpellInfos = config.ToDictionary(kv => kv.Value.Type, kv => kv.Value);
            SpellTypes = config.ToDictionary(kv => kv.Key, kv => kv.Value.Type);
        }
    }
}