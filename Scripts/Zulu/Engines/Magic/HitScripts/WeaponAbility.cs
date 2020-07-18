using Server.Network;

namespace Server.Engines.Magic.HitScripts
{
    public abstract class WeaponAbility
    {
        public abstract void OnHit(Mobile attacker, Mobile defender, double damage);
        public virtual bool Validate(Mobile from)
        {
            if (!from.Player)
                return true;

            NetState state = from.NetState;

            return state != null;
        }
    }
}