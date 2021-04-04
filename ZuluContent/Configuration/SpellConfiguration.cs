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
        public readonly Dictionary<int, SpellCircle> SpellCircles;

        protected SpellConfiguration()
        {
            var circleConfig = ZhConfig.DeserializeJsonConfig<SpellCircle[]>("Data/Magic/circles.json");
            SpellCircles = circleConfig.ToDictionary(v => v.Id, v => v);
            
            var config = ZhConfig.DeserializeJsonConfig<Dictionary<SpellEntry, SpellInfo>>(
                "Data/Magic/spells.json",
                JsonConfig.GetOptions(new SpellCircleConverterFactory(SpellCircles.Values))
            );

            
            SpellInfos = config.ToDictionary(kv => kv.Value.Type, kv => kv.Value);
            SpellTypes = config.ToDictionary(kv => kv.Key, kv => kv.Value.Type);
        }
    }
}