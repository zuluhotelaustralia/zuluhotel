using System.Threading.Tasks;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public DispelSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            //t.IsDefined(typeof(DispellableFieldAttribute), false)

            if (target is BaseCreature creature)
            {
                // TODO: summoned/animated delete()
                
                target.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!

            }
            
            if(target is IBuffable buffable) 
                buffable.BuffManager.DispelBuffs();

            // target.FixedEffect(0x3729, 10, 10);
            target.FixedParticles(0x3729, 10, 10, 5009, EffectLayer.Waist);

            target.PlaySound(0x201);
        }
    }
}