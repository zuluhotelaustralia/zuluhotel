using Server;

namespace ZuluContent.Zulu.Engines.Magic
{
    public class MagicalProperties : MagicalPropertyDictionary
    {
        public MagicalProperties(Item parent) : base(parent)
        {
        }

        public static MagicalProperties Deserialize(IGenericReader reader, Item item)
        {
            var mp = Deserialize(reader, new MagicalProperties(item));
            mp.OnMobileEquip();
            return mp;
        }
    }
}