namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    /// <summary>
    /// Specifies the behaviour when a hook is fired and an EnchantmentDictionary contains multiple Enchantments of the
    /// same type. 
    /// 
    /// <list type="bullet">
    /// <item>
    /// <term>None:</term>
    /// <description>Do nothing, allow multiple enchantments, e.g. multiple ArmourBonuses </description>
    /// </item>
    /// <item>
    /// <term>Descending:</term>
    /// <description>Sort the enchantments of the same type in descending order and only use the first value</description>
    /// </item>
    /// <item>
    /// <term>Ascending:</term>
    /// <description>Sort in ascending order, selecting the first value </description>
    /// </item>
    /// </list>
    /// </summary>
    public enum EnchantmentDistinctBehaviour
    {
        None,
        Descending,
        Ascending
    }
}