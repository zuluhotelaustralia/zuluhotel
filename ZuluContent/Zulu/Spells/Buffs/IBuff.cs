using System;

namespace Server.Spells
{
    public interface IBuff
    {
        public const int BlankCliloc = 1114057; // ~1_val~
        
        public BuffIcon Icon { get; }
        public int TitleCliloc { get; }
        public int SecondaryCliloc { get; }
        public TextDefinition Args { get; }
        public bool RetainThroughDeath { get; }
        public TimeSpan Duration { get; }
        public DateTime Start { get; }
        public void OnBuffAdded(Mobile parent);
        public void OnBuffRemoved(Mobile parent);
    }
}