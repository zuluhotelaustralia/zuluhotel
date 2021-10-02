using ZuluContent.Configuration.Types.Resources;

namespace ZuluContent.Configuration.Types
{
    public record ResourceConfiguration
    {
        public ResourceSettings<OreEntry> Ores { get; init; }
        public ResourceSettings<OreEntry> Sand { get; init; }
        public ResourceSettings<OreEntry> Clay { get; init; }
        public ResourceSettings<LogEntry> Logs { get; init; }
        public ResourceSettings<HideEntry> Hides { get; init; }
        public ResourceSettings<FishEntry> Fish { get; init; }
    }
}