using Scripts.Engines.Magic;
using Server;
using Server.Engines.Magic;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class MagicResistMod : IMagicMod<ElementalType>
    {
        public ElementalType Target { get; }
        public int Value { get; set; } = 0;
        public MagicProp Prop { get; } = MagicProp.ElementalResist;
        public EnchantNameType Place { get; } = EnchantNameType.Prefix;
        public string[] NormalNames { get; }
        public string[] CursedNames { get; }
        public int Color { get; }
        public int CursedColor { get; }
        public bool Cursed { get; }


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