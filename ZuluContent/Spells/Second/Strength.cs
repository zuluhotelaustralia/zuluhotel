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

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);

            SpellHelper.AddStatBonus(Caster, target, StatType.Str);

            target.FixedParticles(0x375A, 10, 15, 5017, EffectLayer.Waist);
            target.PlaySound(0x1EE);
        }
    }
}