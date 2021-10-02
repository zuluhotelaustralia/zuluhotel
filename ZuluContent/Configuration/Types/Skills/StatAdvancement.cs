namespace ZuluContent.Configuration.Types.Skills
{
    public record StatAdvancement
    {
        public double Chance { get; init; }
        public int MinGain { get; init; }
        public int MaxGain { get; init; }
    }
}