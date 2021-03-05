using MessagePack;
using Server;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace ZuluContent.Zulu.Engines.Magic.Enchantments
{
    [MessagePackObject]
    public class SpellReflect : Enchantment<SpellReflectInfo>
    {
        [IgnoreMember]
        private int m_Value = 0;

        [IgnoreMember]
        public override string AffixName => EnchantmentInfo.GetName(Value, Cursed);
        [Key(1)]
        public int Value
        {
            get => Cursed > CurseType.None ? -m_Value : m_Value;
            set => m_Value = value;
        }

        public override void OnAdded(IEntity entity)
        {
            base.OnAdded(entity);
            if (entity is Item {Parent: Mobile mobile})
            {
                // TODO: Figure out how to make this consume charges
                mobile.MagicDamageAbsorb += Value;
            }
        }

        public override void OnRemoved(IEntity entity,  IEntity parent)
        {
            if (entity is Item && parent is Mobile mobile)
            {
                mobile.MagicDamageAbsorb -= Value;
            }
        }
    }
    
    public class SpellReflectInfo : EnchantmentInfo
    {

        public override string Description { get; protected set; } = "Spell Reflection With Charges";
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