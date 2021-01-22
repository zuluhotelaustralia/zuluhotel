using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class PermSpellReflect : Enchantment<PermSpellReflectInfo>
    {
        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);

        [Key(1)] 
        public int Value { get; set; } = 0;

        public override void OnAdded(IEntity entity)
        {
            if (entity is Item item && item.Parent is Mobile mobile)
            {
                mobile.MagicDamageAbsorb += Value;
            }
        }

        public override void OnRemoved(IEntity entity, IEntity parent)
        {
            if (entity is Item && parent is Mobile mobile)
            {
                mobile.MagicDamageAbsorb -= Value;
            }
        }
    }
    
    public class PermSpellReflectInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Permanent Magic Reflection";
        public override EnchantNameType Place { get; protected set; } = EnchantNameType.Suffix;
        public override int Hue { get; protected set; } = 0;
        public override int CursedHue { get; protected set; } = 0;

        public override string[,] Names { get; protected set; } = {
            {string.Empty, string.Empty},
            {"Raw Moonstone", "Chipped Moonstone"},
            {"Cut Moonstone", "Cracked Moonstone"},
            {"Refined Moonstone", "Flawed Moonstone"},
            {"Prepared Moonstone", "Inferior Moonstone"},
            {"Enchanted Moonstone", "Chaotic Moonstone"},
            {"Flawless Moonstone", "Corrupted Moonstone"},
        };
    }
}