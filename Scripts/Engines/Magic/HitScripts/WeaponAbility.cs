using Server.Network;

namespace Server.Engines.Magic.HitScripts
{
    public class WeaponAbility
    {
        public virtual void OnHit(Mobile attacker, Mobile defender, int damage)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Validate(Mobile from)
        {
            if (!from.Player)
                return true;

            NetState state = from.NetState;

            return state != null;
        }
    }
}