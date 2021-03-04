using System.Threading.Tasks;
using Server.Targeting;

namespace Server.Spells
{
    public interface IAsyncSpell
    {
        public Mobile Caster { get; }
        public SpellInfo Info { get; }
        public bool CheckBeneficialSequence(Mobile target);
        public bool CheckHarmfulSequence(Mobile target);
        public Task CastAsync();
    }
    
    public interface ITargetableAsyncSpell<in T> : ISpell, IAsyncSpell
    {
        async Task IAsyncSpell.CastAsync()
        {
            var target = new AsyncTarget<T>(Caster, Info.TargetOptions);
            Caster.Target = target;

            var response = await target;

            if (response.Target is Mobile mobile)
            {
                if (Info.TargetOptions.Flags == TargetFlags.Beneficial && !CheckBeneficialSequence(mobile))
                    return;
            
                if (Info.TargetOptions.Flags == TargetFlags.Harmful && !CheckHarmfulSequence(mobile))
                    return;
            }
            
            await OnCastAsync(response);
        }
        
        public Task OnCastAsync(ITargetResponse<T> response);
    }
}