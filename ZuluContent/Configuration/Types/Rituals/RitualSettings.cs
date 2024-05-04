using System;
using Server;

namespace ZuluContent.Configuration.Types.Rituals
{
    public record RitualDrawSettings
    {
        public string Speech { get; init; }
        public Direction Direction { get; init; }
        public int XMod { get; init; }
        public int YMod { get; init; }
        public int SoundEffect { get; init; }
        public string Text { get; init; }
        public Type Appear { get; init; }
    }
    
    public record RitualSettings
    {
        public string Speech { get; init; }
        public int SoundEffect { get; init; }
        public string Text { get; init; }
        public RitualAppear[] Appear { get; init; }
    }
    
    public record RitualAppear
    {
        public int XMod { get; init; }
        public int YMod { get; init; }
        public int ItemId { get; init; }
    }
}