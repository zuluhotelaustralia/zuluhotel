using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class BlessSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public BlessSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);

            var bonus = SpellHelper.GetModAmount(Caster, target, StatType.All);
            var duration = SpellHelper.GetDuration(Caster, target);

            SpellHelper.AddStatBonus(Caster, target, StatType.All, bonus, duration);

            target.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
            target.PlaySound(0x1EA);
        }
    }
}