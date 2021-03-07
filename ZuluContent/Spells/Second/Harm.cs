using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class HarmSpell : MagerySpell, ITargetableAsyncSpell<Mobile> 
    {
        public HarmSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;

            SpellHelper.Turn(Caster, m);

            m.FixedParticles(0x37C4, 10, 15, 5013, EffectLayer.Waist);
            m.PlaySound(0x1F1);
            
            var damage = SpellHelper.CalcSpellDamage(Caster, m, this);
            SpellHelper.Damage(damage, m, Caster, this, null, ElementalType.Water);
        }
    }
}