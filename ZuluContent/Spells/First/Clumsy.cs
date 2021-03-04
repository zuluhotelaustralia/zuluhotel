using System.Threading.Tasks;
using Server.Targeting;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class ClumsySpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ClumsySpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;
            
            SpellHelper.Turn(Caster, m);
            SpellHelper.AddStatCurse(Caster, m, StatType.Dex);

            m.Spell?.OnCasterHurt();
            m.Paralyzed = false;

            m.FixedParticles(0x3779, 10, 15, 5002, EffectLayer.Head);
            m.PlaySound(0x1DF);
        }
    }
}