using System.Collections.Generic;
using System.Text.Json.Serialization;
using Server.Json;
using Server.Scripts.Engines.Loot;

namespace ZuluContent.Configuration.Types
{
    public record LootConfiguration
    {
        [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<LootTable>))]
        public Dictionary<string, LootTable> Tables { get; init; }
    }
}