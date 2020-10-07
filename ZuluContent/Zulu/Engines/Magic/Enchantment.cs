using MessagePack;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace ZuluContent.Zulu.Engines.Magic
{
    public abstract class Enchantment<TEnchantmentInfo> : IEnchantmentValue where TEnchantmentInfo : EnchantmentInfo, new()
    {
        public static readonly TEnchantmentInfo EnchantmentInfo = new TEnchantmentInfo();
        [IgnoreMember]public virtual EnchantmentInfo Info => EnchantmentInfo;
        
        [IgnoreMember]public abstract string AffixName { get; }
        [Key(0)]public bool Cursed { get; set; }

        protected Enchantment(bool cursed = false)
        {
            Cursed = cursed;
        }
    }
}