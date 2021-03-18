using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

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
        async Task IAsyncSpell.CastAsync() => await SendTargetAsync();

        public async Task OnSpellReflected(Mobile target) { }

        public async Task SendTargetAsync()
        {
            if (Info.TargetOptions == null)
            {
                Console.WriteLine($"Missing {nameof(TargetOptions)} for {GetType().FullName}");
                return;
            }
            
            var target = new AsyncSpellTarget<T>(this, Caster, Info.TargetOptions);
            Caster.Target = target;

            var response = await target;

            // If the target request is typeof IPoint3D and they target a mobile, rewrite the response Mobile.Location
            if (response is ITargetResponse<IPoint3D> {InvalidTarget: Mobile {Location: T location}})
            {
                response = new TargetResponse<T>
                {
                    Target = location,
                    Type = response.Type,
                    InvalidTarget = response.InvalidTarget,
                    CancelType = response.CancelType,
                };
            }

            // If the target request is typeof Mobile do the beneficial/harmful sequences and check magic reflection
            if (response.Target is Mobile mobile)
            {
                if (Info.TargetOptions.Flags == TargetFlags.Beneficial && !CheckBeneficialSequence(mobile))
                    return;

                if (Info.TargetOptions.Flags == TargetFlags.Harmful)
                {
                    if (!CheckHarmfulSequence(mobile))
                        return;
                    
                    if (Info.Reflectable && Caster is T c)
                    {
                        var reflected = false;
                        mobile.FireHook(h => h.OnCheckMagicReflection(mobile, this as Spell, ref reflected));

                        if (reflected)
                        {
                            response = new TargetResponse<T>
                            {
                                Target = c,
                                Type = response.Type,
                                InvalidTarget = response.InvalidTarget,
                                CancelType = response.CancelType,
                            };
                            
                            mobile.SendLocalizedMessage(1149980); // You reflect the incoming spell.
                            mobile.FixedEffect(0x374B, 10, 10);
                            mobile.PlaySound(0x1E7);

                            await OnSpellReflected(mobile);
                        }
                    }
                }
            }
            
            await OnTargetAsync(response);
        }
        
        public Task OnTargetAsync(ITargetResponse<T> response);
    }
}