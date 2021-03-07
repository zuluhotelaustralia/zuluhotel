using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class StrengthSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public StrengthSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;
            
            SpellHelper.Turn(Caster, m);

            SpellHelper.AddStatBonus(Caster, m, StatType.Str);

            m.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
            m.PlaySound(0x1EE);
        }
    }
}