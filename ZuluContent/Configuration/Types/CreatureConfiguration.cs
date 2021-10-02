using System.Collections.Generic;
using System.Text.Json.Serialization;
using Server.Json;
using ZuluContent.Configuration.Types.Creatures;

namespace ZuluContent.Configuration.Types
{
    public record CreatureConfiguration
    {
        [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<CreatureProperties>))]
        public Dictionary<string, CreatureProperties> Entries { get; init; }
    }
}