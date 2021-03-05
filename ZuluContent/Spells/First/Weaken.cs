using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells.First
{
    public class WeakenSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public WeakenSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;

            SpellHelper.Turn(Caster, m);
            SpellHelper.AddStatCurse(Caster, m, StatType.Str);

            m.Spell?.OnCasterHurt();
            m.Paralyzed = false;

            m.FixedParticles(0x3779, 10, 15, 5009, EffectLayer.Waist);
            m.PlaySound(0x1E6);
        }
    }
}