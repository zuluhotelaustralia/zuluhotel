using System;
using System.Collections.Generic;

// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Server.Configurations
{
    public class SkillConfiguration : BaseSingleton<SkillConfiguration>
    {
        public readonly int MaxStatCap;
        public readonly int StatCap;
        public readonly IReadOnlyDictionary<SkillName, SkillEntry> Entries;

        protected SkillConfiguration()
        {
            var config = ZhConfig.DeserializeJsonConfig<SkillSettings>("Data/skills.json");

            Entries = config.Entries;
            MaxStatCap = config.MaxStatCap;
            StatCap = config.StatCap;
        }
        
        public record StatAdvancement
        {
            public int Chance { get; init; }
            public uint PointsAmount { get; init; }
            public uint PointsSides { get; init; }
            public int PointsBonus { get; init; }
        }

        public record SkillEntry
        {
            public double Delay { get; init; }

            public TimeSpan DelayTimespan => TimeSpan.FromSeconds(Delay);
            public StatAdvancement StrAdvancement { get; init; }
            public StatAdvancement DexAdvancement { get; init; }
            public StatAdvancement IntAdvancement { get; init; }
            public int DefaultPoints { get; init; }
        }

        public record SkillSettings
        {
            public int MaxStatCap { get; init; }
            public int StatCap { get; init; }
            
            public Dictionary<SkillName, SkillEntry> Entries { get; init; }
        }
        
    }
}