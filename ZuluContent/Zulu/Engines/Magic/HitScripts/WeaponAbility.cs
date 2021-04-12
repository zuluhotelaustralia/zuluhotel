using Server.Network;

namespace Server.Engines.Magic.HitScripts
{
    public abstract class WeaponAbility
    {
        public abstract void OnHit(Mobile attacker, Mobile defender, ref int damage);

        public virtual bool Validate(Mobile from)
        {
            return true;
        }
    }
}