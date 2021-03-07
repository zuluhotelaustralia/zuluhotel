using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class CunningSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public CunningSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            SpellHelper.AddStatBonus(Caster, target, StatType.Int);

            target.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
            target.PlaySound(0x1EB);
        }
    }
}