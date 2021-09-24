using System.Text.Json;
using Server.Network;
using Server.Spells.First;

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