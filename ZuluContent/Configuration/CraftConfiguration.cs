using System;
using System.Collections.Generic;
using Server;
using Server.Misc;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global UnusedMember.Global ClassNeverInstantiated.Global
namespace Scripts.Configuration
{
    public class CraftConfiguration : BaseSingleton<CraftConfiguration>
    {
        public AutoLoopSettings AutoLoop => CueConfiguration.Instance.RootConfig.Crafting.AutoLoop;
        public CraftSettings Alchemy => CueConfiguration.Instance.RootConfig.Crafting.Alchemy;
        public CraftSettings AlchemyPlus => CueConfiguration.Instance.RootConfig.Crafting.AlchemyPlus;
        public CraftSettings Blacksmithy => CueConfiguration.Instance.RootConfig.Crafting.Blacksmithy;
        public CraftSettings Carpentry => CueConfiguration.Instance.RootConfig.Crafting.Carpentry;
        public CraftSettings Cartography => CueConfiguration.Instance.RootConfig.Crafting.Cartography;
        public CraftSettings Cooking => CueConfiguration.Instance.RootConfig.Crafting.Cooking;
        public CraftSettings Fletching => CueConfiguration.Instance.RootConfig.Crafting.Fletching;
        public CraftSettings Inscription => CueConfiguration.Instance.RootConfig.Crafting.Inscription;
        public CraftSettings Tailoring => CueConfiguration.Instance.RootConfig.Crafting.Tailoring;
        public CraftSettings Tinkering => CueConfiguration.Instance.RootConfig.Crafting.Tinkering;
        
        protected CraftConfiguration()
        {
        }
    }
    
    public record AutoLoopSettings
    {
        public double Delay { get; init; }
    }
    
    public record CraftSettings
    {
        public SkillName MainSkill { get; init; }
        public TextDefinition GumpTitleId { get; init; }
        public List<CraftEntry> CraftEntries { get; init; }
        public int MinCraftDelays { get; init; }
        public int MaxCraftDelays { get; init; }
        public double Delay { get; init; }
        public double MinCraftChance { get; init; }
        public int CraftWorkSound { get; init; }
        public int CraftEndSound { get; init; }
    }
    
    public record CraftEntry
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public TextDefinition GroupName { get; init; }
        public double Skill { get; init; }
        public SkillName? SecondarySkill { get; init; }
        public double? Skill2 { get; init; }

        public CraftResource[] Resources { get; init; }
            
        public bool UseAllRes { get; init; }
        public bool NeedHeat { get; init; }
        public bool NeedOven { get; init; }
        public bool NeedMill { get; init; }
    }
    
    public record CraftResource
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public int Amount { get; init; }
        public TextDefinition Message { get; init; }
    }
}