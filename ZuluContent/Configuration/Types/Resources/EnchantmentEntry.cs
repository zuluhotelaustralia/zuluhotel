using System;

namespace ZuluContent.Configuration.Types.Resources
{
    public record EnchantmentEntry
    {
        public Type EnchantmentType { get; init; }
        public int EnchantmentValue { get; init; }
    }
}