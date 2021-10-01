using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scripts.Configuration;
using Server.Json;
using Server.Utilities;
using ZuluContent.Zulu.Skills;
using static ZuluContent.Zulu.Skills.BaseSkillHandler;
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global UnusedAutoPropertyAccessor.Global MemberCanBePrivate.Global CollectionNeverUpdated.Global
namespace Server.Configurations
{
    
    
    public record StatAdvancement
    {
        public double Chance { get; init; }
        public int MinGain { get; init; }
        public int MaxGain { get; init; }
    }

    public record SkillEntry
    {
        public BaseSkillHandler OnUseHandler { get; init; }
        public TimeSpan Delay { get; init; }
        public int DefaultPoints { get; init; }
        public Dictionary<StatType, StatAdvancement> StatAdvancements { get; init; }
    }

    public record SkillSettings
    {
        public int MaxStatCap { get; init; }
        public int StatCap { get; init; }
        public Dictionary<SkillName, SkillEntry> Entries { get; init; }
    }
    
    public class SkillConfiguration : BaseSingleton<SkillConfiguration>
    {
        public int MaxStatCap => CueConfiguration.Instance.RootConfig.Skills.MaxStatCap;
        public int StatCap => CueConfiguration.Instance.RootConfig.Skills.StatCap;

        public IReadOnlyDictionary<SkillName, SkillEntry> Entries =>
            CueConfiguration.Instance.RootConfig.Skills.Entries;

        protected SkillConfiguration()
        {
        }
    }
}