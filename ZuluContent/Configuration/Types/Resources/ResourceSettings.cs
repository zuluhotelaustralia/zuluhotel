using Server;
using Server.Misc;

namespace ZuluContent.Configuration.Types.Resources
{
    public record ResourceSettings<T>
    {
        public int BankWidth { get; init; }
        public int BankHeight { get; init; }
        public int MinTotal { get; init; }
        public int MaxTotal { get; init; }
        public double MinRespawn { get; init; }
        public double MaxRespawn { get; init; }
        public SkillName Skill { get; init; }
        public int MaxRange { get; init; }
        public int MaxChance { get; init; }
        public Effect ResourceEffect { get; init; }
        public Message Messages { get; init; }
        public T[] Entries { get; init; }

        public record Effect
        {
            public int[] Actions { get; init; }
            public int[] Sounds { get; init; }
            public int[] Counts { get; init; }
            public double Delay { get; init; }
            public double SoundDelay { get; init; }
        }
        
        public record Message
        {
            public TextDefinition NoResourcesMessage { get; init; }
            public TextDefinition? DoubleHarvestMessage { get; init; }
            public TextDefinition? TimedOutOfRangeMessage { get; init; }
            public TextDefinition OutOfRangeMessage { get; init; }
            public TextDefinition FailMessage { get; init; }
            public TextDefinition PackFullMessage { get; init; }
            public TextDefinition ToolBrokeMessage { get; init; }
        }
    }
}