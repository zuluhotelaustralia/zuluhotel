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

            var m = response.Target;
            
            SpellHelper.Turn(Caster, m);
            SpellHelper.AddStatBonus(Caster, m, StatType.Int);

            m.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
            m.PlaySound(0x1EB);
        }
    }
}