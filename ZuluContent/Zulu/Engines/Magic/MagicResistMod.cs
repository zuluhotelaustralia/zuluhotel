using Scripts.Engines.Magic;
using Server;
using Server.Engines.Magic;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class MagicResistMod : IMagicMod<ElementalType>
    {
        public ElementalType Target { get; }
        public int Value { get; set; } = 0;
        public MagicProp Prop => MagicProp.ElementalResist;
        public MagicInfo Info => MagicInfo.MagicInfoMap[Target];
        public bool Cursed { get; set; }
        
        public string EnchantName => Info.GetName(IElementalResistible.GetProtectionLevelForResist(Value), Cursed);


        public MagicResistMod(ElementalType type, int value)
        {
            Target = type;
            Value = value;
        }
        
        public void Remove()
        {
        }

        public void AddTo(Mobile mobile)
        {
        }

    }
}