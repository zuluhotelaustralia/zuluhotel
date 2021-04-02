using System;
using System.Threading.Tasks;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Fifth
{
    public class BladeSpiritsSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public BladeSpiritsSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public override TimeSpan GetCastDelay() => base.GetCastDelay() + TimeSpan.FromSeconds(6.0);
        
        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            
            var map = Caster.Map;
            
            if (map == null || !map.CanSpawnMobile(point.X, point.Y, point.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return;
            }

            var duration = TimeSpan.FromSeconds(Utility.Random(80, 40));

            BaseCreature.Summon(new BladeSpirit(), false, Caster, new Point3D(point), 0x212, duration);
        }
    }
}