using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;
#pragma warning disable 1998

namespace Server.Spells.First
{
    public class MagicArrowSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public override bool DelayedDamageStacking => true;

        public override bool DelayedDamage => true;
        
        public MagicArrowSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var mobile = response.Target;
            
            SpellHelper.Turn(Caster, mobile);
            
            Caster.MovingParticles(mobile, 0x36E4, 5, 0, false, false, 3006, 0, 0);
            Caster.PlaySound(0x1E5);

            var damage = SpellHelper.CalcSpellDamage(Caster, mobile, this);
            SpellHelper.Damage(damage, mobile, Caster, this, null, ElementalType.Earth);
        }

        public async Task OnSpellReflected(Mobile target)
        {
            Caster.MovingParticles(target, 0x36E4, 5, 0, false, false, 3006, 0, 0);

            await Timer.Pause(500);
            
            target.MovingParticles(Caster, 0x36E4, 5, 0, false, false, 3006, 0, 0);
        }
    }
}