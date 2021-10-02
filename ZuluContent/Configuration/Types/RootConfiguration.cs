namespace ZuluContent.Configuration.Types
{
    public record RootConfiguration
    {
        public CoreConfiguration Core { get; init; }
        public MessagingConfiguration Messaging { get; init; }
        public EmailConfiguration Email { get; init; }
        public CreatureConfiguration Creatures { get; init; }
        public CraftConfiguration Crafting { get; init; }
        public ResourceConfiguration Resources { get; init; }
        public LootConfiguration Loot { get; init; }
        public MagicConfiguration Magic { get; init; }
        public SkillConfiguration Skills { get; init; }

    }
}