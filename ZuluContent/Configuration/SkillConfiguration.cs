using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Json;
using Server.Utilities;
using ZuluContent.Zulu.Skills;
using static ZuluContent.Zulu.Skills.BaseSkillHandler;
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global UnusedAutoPropertyAccessor.Global MemberCanBePrivate.Global CollectionNeverUpdated.Global
namespace Server.Configurations
{
    public class SkillConfiguration : BaseSingleton<SkillConfiguration>
    {
        public readonly int MaxStatCap;
        public readonly int StatCap;
        public readonly IReadOnlyDictionary<SkillName, SkillEntry> Entries;

        private static readonly Dictionary<SkillName, BaseSkillHandler> SkillHandlers = new();

        protected SkillConfiguration()
        {
            var config = ZhConfig.DeserializeJsonConfig<SkillSettings>("Data/skills.json");

            Entries = config.Entries;
            MaxStatCap = config.MaxStatCap;
            StatCap = config.StatCap;

            var path = Path.Combine(Core.BaseDirectory, "Data/skills2.json");
            JsonConfig.Serialize(path, config);
            
            RegisterSkillHandlers(Entries);
        }

        private static void RegisterSkillHandlers(IReadOnlyDictionary<SkillName, SkillEntry> entries)
        {
            foreach (var (skill, entry) in entries)
            {
                if (entry.OnUseHandler == null)
                {
                    continue;
                }

                if (!entry.OnUseHandler.IsAssignableTo(typeof(BaseSkillHandler)))
                {
                    continue;
                    throw new ArgumentOutOfRangeException(
                        $"Skill handler of type {entry.OnUseHandler} must inherit from {typeof(BaseSkillHandler)}");
                }


                var handler = entry.OnUseHandler.CreateInstance<BaseSkillHandler>();
                
                if (handler == null)
                {
                    continue;
                    throw new ArgumentNullException(
                        $"Unable to create {nameof(BaseSkillHandler)} of type {entry.OnUseHandler}");
                    
                }

                SkillHandlers.TryAdd(skill, handler);
                SkillInfo.Table[(int) skill].Callback = mobile => DispatchOnUseSkillHandler(mobile, handler);
            }
        }

        public record StatAdvancement
        {
            public double Chance { get; init; }
            public int MinGain { get; init; }
            public int MaxGain { get; init; }
        }

        public record SkillEntry
        {
            public Type OnUseHandler { get; init; }
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
    }
}