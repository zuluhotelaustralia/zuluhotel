using System;
using Server;

namespace ZuluContent.Zulu.Engines.Magic.Hooks
{
    public interface IEnchantmentHook
    {
        public void OnIdentified(Item item);

        public void OnAdded(Item item, Mobile mobile);

        public void OnRemoved(Item item, Mobile mobile);

        public void OnBeforeSwing(Mobile attacker, Mobile defender);

        public void OnSwing(Mobile attacker, Mobile defender, ref double damageBonus, ref TimeSpan result);
        
        public void OnGetDelay(ref TimeSpan delay, Mobile m);

        public void OnCheckHit(ref bool result, Mobile attacker, Mobile defender);

        public void OnGotMeleeAttack(ref double damage, Item item, Mobile mobile);

        public void OnGaveMeleeAttack(ref double damage, Item item, Mobile mobile);
    }
}