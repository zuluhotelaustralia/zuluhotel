using Server.Network;

namespace Server.Engines.Magic.HitScripts
{
    public abstract class WeaponAbility
    {
        public abstract void OnHit(Mobile attacker, Mobile defender, ref int damage);

        public virtual bool Validate(Mobile from)
        {
            // NetState state = from.NetState;

            // return state != null;
            return true;
        }
    }
}