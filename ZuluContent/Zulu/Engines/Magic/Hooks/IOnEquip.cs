using Server;

namespace ZuluContent.Zulu.Engines.Magic.Hooks
{
    public interface IOnEquip : IEnchantmentHook
    {
        public void OnEquip(Item item, Mobile mobile);
        public void OnRemoved(Item item, Mobile mobile);
    }
}