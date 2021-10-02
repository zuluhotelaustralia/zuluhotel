using ZuluContent.Configuration.Types.Crafting;

namespace ZuluContent.Configuration.Types
{
    public record CraftConfiguration
    {
        public AutoLoopSettings AutoLoop { get; init; }
        public CraftSettings Alchemy { get; init; }
        public CraftSettings AlchemyPlus { get; init; }
        public CraftSettings Blacksmithy { get; init; }
        public CraftSettings Carpentry { get; init; }
        public CraftSettings Cartography { get; init; }
        public CraftSettings Cooking { get; init; }
        public CraftSettings Fletching { get; init; }
        public CraftSettings Inscription { get; init; }
        public CraftSettings Tailoring { get; init; }
        public CraftSettings Tinkering { get; init; }
    }
}