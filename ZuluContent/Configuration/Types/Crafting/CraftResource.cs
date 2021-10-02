using System;
using Server.Misc;

namespace ZuluContent.Configuration.Types.Crafting
{
    public record CraftResource
    {
        public Type ItemType { get; init; }
        public TextDefinition Name { get; init; }
        public int Amount { get; init; }
        public TextDefinition Message { get; init; }
    }
}